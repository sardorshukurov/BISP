using Microsoft.AspNetCore.SignalR;

namespace Welisten.API.Hubs;

public class ChatHub : Hub
{
    private static List<ChatSession> ActiveSessions = new List<ChatSession>();

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var session = ActiveSessions.FirstOrDefault(s => s.ConnectionIds.Contains(Context.ConnectionId));
        if (session != null)
        {
            session.ConnectionIds.Remove(Context.ConnectionId);
            await Clients.Group(session.SessionId).SendAsync("ReceiveMessage", string.Empty, $"Other user left the chat. Waiting for someone to connect");
            if (session.ConnectionIds.Count == 0)
            {
                ActiveSessions.Remove(session);
            }
        }
        await base.OnDisconnectedAsync(exception);
    }

    public async Task Connect(string username)
    {
        var session = ActiveSessions.FirstOrDefault(s => s.ConnectionIds.Count == 1 && !s.IsFull);
        if (session == null)
        {
            session = new ChatSession { SessionId = Guid.NewGuid().ToString(), Usernames = new List<string>(), ConnectionIds = new List<string>() };
            ActiveSessions.Add(session);
        }
        session.Usernames.Add(username);
        session.ConnectionIds.Add(Context.ConnectionId);
        await Groups.AddToGroupAsync(Context.ConnectionId, session.SessionId);
        await Clients.Group(session.SessionId).SendAsync("ReceiveMessage", string.Empty, $"{username} joined the chat.");
        if (session.IsFull)
        {
            await Clients.Group(session.SessionId).SendAsync("ReceiveMessage", string.Empty, "You are now connected! Start chatting.");
        }
    }

    public async Task SendMessage(string message)
    {
        var session = ActiveSessions.FirstOrDefault(s => s.ConnectionIds.Contains(Context.ConnectionId));
        if (session != null)
        {
            await Clients.Group(session.SessionId).SendAsync("ReceiveMessage", string.Empty, message);
        }
    }

    public async Task SwitchToAnotherSession(string username)
    {
        var currentSession = ActiveSessions.FirstOrDefault(s => s.ConnectionIds.Contains(Context.ConnectionId));
        if (currentSession != null)
        {
            ActiveSessions.Remove(currentSession);
        }
    
        // Shuffle the active sessions
        var shuffledSessions = ActiveSessions.OrderBy(x => Guid.NewGuid()).ToList();
    
        // Find the first session with less than 2 connections (excluding the current user)
        var newSession = shuffledSessions.FirstOrDefault(s => s.ConnectionIds.Count < 2 && !s.Usernames.Contains(username));

        if (newSession == null)
        {
            // If no suitable session is found, create a new session
            newSession = new ChatSession { SessionId = Guid.NewGuid().ToString(), Usernames = new List<string>(), ConnectionIds = new List<string>() };
            ActiveSessions.Add(newSession);
        }

        // Add the current user to the new or existing session
        newSession.Usernames.Add(username);
        newSession.ConnectionIds.Add(Context.ConnectionId);
    
        // Notify the user about the connection
        await Groups.AddToGroupAsync(Context.ConnectionId, newSession.SessionId);
        await Clients.Group(newSession.SessionId).SendAsync("ReceiveMessage", string.Empty, $"{username} joined the chat.");
        if (newSession.IsFull)
        {
            await Clients.Group(newSession.SessionId).SendAsync("ReceiveMessage", string.Empty, "You are now connected! Start chatting.");
        }
    }

}


public class ChatSession
{
    public string SessionId { get; set; }
    public List<string> Usernames { get; set; }
    public List<string> ConnectionIds { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsFull => ConnectionIds.Count >= 2;
}

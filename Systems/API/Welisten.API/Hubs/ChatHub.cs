using Microsoft.AspNetCore.SignalR;

namespace Welisten.API.Hubs;

public class ChatHub : Hub
{
    private static Dictionary<string, ChatSession> ActiveSessions = new Dictionary<string, ChatSession>();

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var session = ActiveSessions.FirstOrDefault(s => s.Value.ConnectionIds.Contains(Context.ConnectionId));
        if (!session.Equals(default(KeyValuePair<string, ChatSession>)))
        {
            session.Value.ConnectionIds.Remove(Context.ConnectionId);
            await Clients.Group(session.Key).SendAsync("ReceiveMessage", string.Empty, $"Other user left the chat.");
            if (session.Value.ConnectionIds.Count == 0)
            {
                ActiveSessions.Remove(session.Key);
            }
        }
        await base.OnDisconnectedAsync(exception);
    }

    public async Task Connect(string username)
    {
        var activeSessions = ActiveSessions.Where(s => s.Value.ConnectionIds.Count == 1 && !s.Value.IsFull && (DateTime.UtcNow - s.Value.CreatedAt).TotalMinutes <= 5).ToList();
        var session = activeSessions.FirstOrDefault().Value;
        if (session == null)
        {
            session = new ChatSession { SessionId = Guid.NewGuid().ToString(), Usernames = new List<string>(), ConnectionIds = new List<string>(), CreatedAt = DateTime.UtcNow };
            ActiveSessions.Add(session.SessionId, session);
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
        var session = ActiveSessions.FirstOrDefault(s => s.Value.ConnectionIds.Contains(Context.ConnectionId)).Value;
        if (session != null)
        {
            await Clients.Group(session.SessionId).SendAsync("ReceiveMessage", string.Empty, message);
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

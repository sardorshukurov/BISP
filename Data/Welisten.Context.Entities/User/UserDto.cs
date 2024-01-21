namespace Welisten.Context.Entities;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }
}
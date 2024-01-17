using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Welisten.Context.Entities.User;

public class User : IdentityUser<Guid>
{
    [MaxLength(50)]
    public required string FirstName { get; set; }
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;
    public UserStatus Status { get; set; }
}
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Welisten.Context.Entities;

public class User : IdentityUser<Guid>
{
    [MaxLength(50), MinLength(10)]
    public required string FirstName { get; set; }
    [MaxLength(50), MinLength(10)]
    public string LastName { get; set; } = string.Empty;
    public UserStatus Status { get; set; }
    public virtual ICollection<Topic>? Topics { get; set; }
    public virtual ICollection<Mood>? Moods { get; set; }
}
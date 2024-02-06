using Microsoft.AspNetCore.Identity;
using Welisten.Context.Entities;

namespace Welisten.Context.Seeder.Demo;

public class DemoHelper
{
    public readonly IEnumerable<User> Users =
    [
        // new User
        // {
        //     Name = "User1",
        //     FirstName = "User1",
        //     Email = "user1@mail.com",
        //     PasswordHash = HashPassword("password")
        // },
        //
        // new User
        // {
        //     Name = "User2",
        //     FirstName = "User2",
        //     Email = "user2@mail.com",
        //     PasswordHash = HashPassword("password") 
        // }
    ];

    public readonly IEnumerable<Topic> Topics =
    [
        new Topic
        {
            Type = "Other"
        }, 
            
        new Topic
        {
            Type = "Parents"
        },
        new Topic
        {
            Type = "Relationship"
        },
        new Topic
        {
            Type = "Abuse"
        },
        new Topic
        {
            Type = "Depression"
        },
        new Topic
        {
            Type = "Bipolar disorder"
        },
    ];
    
    private static string HashPassword(string password)
    {
        var passwordHasher = new PasswordHasher<User>();
        return passwordHasher.HashPassword(null, password);
    }
}
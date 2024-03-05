using Microsoft.AspNetCore.Identity;
using Welisten.Context.Entities;

namespace Welisten.Context.Seeder.Demo;

public class DemoHelper
{
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
}
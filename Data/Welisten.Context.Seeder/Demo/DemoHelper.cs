using Welisten.Context.Entities;

namespace Welisten.Context.Seeder.Demo;

public class DemoHelper
{
    public readonly IEnumerable<Reaction> Reactions = [
        new Reaction
        {
            Type = ReactionType.Like
        }
    ];

    public readonly IEnumerable<Topic> Topics =
    [
        new Topic
        {
            Type = TopicType.Other
        }, 
            
        new Topic
        {
            Type = TopicType.Parents
        },
        new Topic
        {
            Type = TopicType.Relationship
        },
        new Topic
        {
            Type = TopicType.Abuse
        },
        new Topic
        {
            Type = TopicType.Depression
        },
        new Topic
        {
            Type = TopicType.BipolarDisorder
        },
    ];
}
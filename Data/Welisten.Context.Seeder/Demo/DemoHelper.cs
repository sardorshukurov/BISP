using Welisten.Context.Entities;

namespace Welisten.Context.Seeder.Demo;

public class DemoHelper
{
    public IEnumerable<Reaction> Reactions = new List<Reaction>
    {
        new Reaction
        {
            Type = ReactionType.Like
        }
    };
}
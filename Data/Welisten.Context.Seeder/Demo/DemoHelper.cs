using System.Text;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using Welisten.Context.Entities;
using Welisten.Context.Entities.Articles;

namespace Welisten.Context.Seeder.Demo;

public static class DemoHelper
{
    #region Topics

    public static readonly IEnumerable<Topic> Topics =
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

    #endregion
    
    #region Events

    public static readonly IEnumerable<EventType> Events = 
    [
        new EventType()
        {
            Name = "Friends"
        },
        new EventType()
        {
            Name = "Family"
        },
        new EventType()
        {
            Name = "Work"
        },
        new EventType()
        {
            Name = "Exercise"
        },
        new EventType()
        {
            Name = "Hobbies"
        },
        new EventType()
        {
            Name = "Socializing"
        },
        new EventType()
        {
            Name = "Alone Time"
        },
        new EventType()
        {
            Name = "Events"
        },
        new EventType()
        {
            Name = "Study"
        },
        new EventType()
        {
            Name = "Movies/TV Shows"
        },
        new EventType()
        {
            Name = "Reading"
        },
        new EventType()
        {
            Name = "Cooking"
        },
        new EventType()
        {
            Name = "Shopping"
        },
        new EventType()
        {
            Name = "Gaming"
        },
        new EventType()
        {
            Name = "Outdoor Activities"
        },
        new EventType()
        {
            Name = "Indoor Activities"
        },
        new EventType()
        {
            Name = "Art/Creative Work"
        },
        new EventType()
        {
            Name = "Music"
        },
        new EventType()
        {
            Name = "Spiritual"
        },
        new EventType()
        {
            Name = "Travel"
        }
    ];

    #endregion

    #region Moods

    public static readonly IEnumerable<Mood> Moods =
    [
        new Mood()
        {
            Name = "Angry",
            ImageLink = "\ud83d\ude21"
        },
        new Mood()
        {
            Name = "Happy",
            ImageLink = "\ud83d\ude03"
        },
        new Mood()
        {
            Name = "Sad",
            ImageLink = "\ud83d\ude14"
        },
        new Mood()
        {
            Name = "Excited",
            ImageLink = "\ud83e\udd29"
        },
        new Mood()
        {
            Name = "Relaxed",
            ImageLink = "\ud83d\ude1c"
        },
        new Mood()
        {
            Name = "Stressed",
            ImageLink = "\ud83d\ude1f"
        },
        new Mood()
        {
            Name = "Content",
            ImageLink = "\ud83d\ude0c"
        },
        new Mood()
        {
            Name = "Anxious",
            ImageLink = "\ud83d\ude30"
        },
        new Mood()
        {
            Name = "Confused",
            ImageLink = "\ud83d\ude15"
        },
        new Mood()
        {
            Name = "Motivated",
            ImageLink = "\ud83c\udf1f"
        },
        new Mood()
        {
            Name = "Tired",
            ImageLink = "\ud83d\ude2b"
        },
        new Mood()
        {
            Name = "Grateful",
            ImageLink = "\ud83e\udd79"
        },
        new Mood()
        {
            Name = "Hopeful",
            ImageLink = "\ud83e\udd79"
        },
        new Mood()
        {
            Name = "Lonely",
            ImageLink = "\ud83e\udee5"
        }
    ];


    #endregion
    
    public static IEnumerable<ArticleCategory> GetCategories()
    {
        // Read the CSV file
        var csvData = File.ReadAllLines("../sample_data/categories.csv");

        // Skip the header row if necessary
        var records = csvData.Skip(1);

        var categories = new List<ArticleCategory>();
        // Map CSV data to your entity model and insert into the database
        foreach (var record in records)
        {
            var fields = record.Split(',');
            var entity = new ArticleCategory()
            {
                Id = Convert.ToInt32(fields[0].Trim('\"')),
                Name = fields[1].Trim('\"'),
            };
            categories.Add(entity);
        }

        return categories;
    }
    
    public static IEnumerable<Article> GetArticles()
    {
        var articles = new List<Article>();

        using (TextFieldParser parser = new TextFieldParser("../sample_data/articles.csv"))
        {
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");

            // Skip the header row
            if (!parser.EndOfData)
            {
                parser.ReadLine(); // Skip header row
            }

            while (!parser.EndOfData)
            {
                string[] fields = parser.ReadFields();

                var entity = new Article()
                {
                    Id = Convert.ToInt32(fields[0]),
                    Title = fields[1],
                    Subtitle = fields[2],
                    Link = fields[3],
                    Category = new ArticleCategory()
                    {
                        Id = Convert.ToInt32(fields[4])
                    } 
                };

                articles.Add(entity);
            }
        }

        return articles;
        // Read the CSV file
        // var csvData = File.ReadAllLines("../../../Data/Welisten.Context.Seeder/Demo/articles.csv");
        //
        // // Skip the header row if necessary
        // var records = csvData.Skip(1);
        //
        // var articles = new List<Article>();
        // // Map CSV data to your entity model and insert into the database
        // foreach (var record in records)
        // {
        //     var fields = record.Split(',');
        //     var entity = new Article()
        //     {
        //         Id = Convert.ToInt32(fields[0].Trim('\"')),
        //         Title = fields[1].Trim('\"'),
        //         Subtitle = fields[2].Trim('\"'),
        //         Link = fields[3].Trim('\"'),
        //         Category = new ArticleCategory()
        //         {
        //             Id = Convert.ToInt32(fields[4].Trim('\"'))
        //         } 
        //     };
        //     articles.Add(entity);
    }
}
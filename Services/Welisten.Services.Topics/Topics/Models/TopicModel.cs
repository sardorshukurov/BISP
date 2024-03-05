using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Welisten.Context.Context;
using Welisten.Context.Entities;

namespace Welisten.Services.Topics;

public class TopicModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Type { get; set; }
}

public class TopicModelProfile : Profile
{
    public TopicModelProfile()
    {
        CreateMap<TopicModel, Topic>()
            .ForMember(dest => dest.Uid, opt =>
                opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Type, opt =>
                opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Id, opt =>
                opt.Ignore());

        CreateMap<Topic, TopicModel>()
            .ForMember(dest => dest.Id, opt =>
                opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.Type, opt =>
                opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Id, opt =>
                opt.Ignore());
    }
}

public class TopicModelValidator : AbstractValidator<TopicModel>
{
    public TopicModelValidator(IDbContextFactory<MainDbContext> dbContextFactory)
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Topic cannot be empty");
        RuleFor(x => x.Id).MustAsync(async (topicId, cancellationToken) =>
        {
            await using var context = await dbContextFactory.CreateDbContextAsync(cancellationToken);
            var topic = await context.Topics.FirstOrDefaultAsync(x => x.Uid == topicId);

            return topic != null;
        }).WithMessage("Topic does not exist");
    }
}
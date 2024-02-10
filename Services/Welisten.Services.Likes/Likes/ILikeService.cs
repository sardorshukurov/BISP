namespace Welisten.Services.Likes;

public interface ILikeService
{
    Task LikeUnlike(Guid userId, Guid postId);
}
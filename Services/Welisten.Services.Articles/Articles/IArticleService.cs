using Welisten.Context.Entities.Articles;

namespace Welisten.Services.Articles;

public interface IArticleService
{
    Task<IEnumerable<ArticleCategory>> GetAllCategories();
    Task<(IEnumerable<Article>, int)> GetAllArticles(int pageNumber, int pageSize);
    Task<(IEnumerable<Article>, int)> GetAllArticlesByCategory(int categoryId, int pageNumber, int pageSize);
}
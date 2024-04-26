using Welisten.Client.Pages.Articles.Models;

namespace Welisten.Client.Pages.Articles.Services;

public interface IArticleService
{
    Task<IEnumerable<ArticleCategory>> GetCategories();
    Task<ArticlePageResponse> GetArticles(int pageNumber, int pageSize);
    Task<ArticlePageResponse> GetArticles(int categoryId, int pageNumber, int pageSize);
}
using Microsoft.EntityFrameworkCore;
using Welisten.Context.Context;
using Welisten.Context.Entities.Articles;

namespace Welisten.Services.Articles;

public class ArticleService : IArticleService
{
    private readonly IDbContextFactory<MainDbContext> _dbContextFactory;

    public ArticleService(IDbContextFactory<MainDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<IEnumerable<ArticleCategory>> GetAllCategories()
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();

        var categories = await context.ArticleCategories.ToListAsync();

        return categories;
    }

    public async Task<(IEnumerable<Article>, int)> GetAllArticles(int pageNumber, int pageSize)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();

        var totalArticlesCount = await context.Articles.CountAsync();

        var articles = await context.Articles
            .Include(x => x.Category)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var totalPages = (int)Math.Ceiling((double)totalArticlesCount / pageSize);
        return (articles, totalPages);
    }

    public async Task<(IEnumerable<Article>, int)> GetAllArticlesByCategory(int categoryId, int pageNumber, int pageSize)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();

        var totalArticlesCount = await context.Articles
            .Include(x => x.Category)
            .Where(x => x.Category.Id == categoryId)
            .CountAsync();

        var articles = await context.Articles
            .Include(x => x.Category)
            .Where(x => x.Category.Id == categoryId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        var totalPages = (int)Math.Ceiling((double)totalArticlesCount / pageSize);

        return (articles, totalPages);
    }
}
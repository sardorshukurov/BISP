using System.Net.Http.Json;
using Welisten.Client.Pages.Articles.Models;

namespace Welisten.Client.Pages.Articles.Services;

public class ArticleService(HttpClient httpClient) : IArticleService
{
    public async Task<IEnumerable<ArticleCategory>> GetCategories()
    {
        var response = await httpClient.GetAsync("v1/v1.0/Article/categories");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<ArticleCategory>>() ?? [];
    }

    public async Task<ArticlePageResponse> GetArticles(int pageNumber, int pageSize = 30)
    {
        var response = await httpClient.GetAsync($"v1/v1.0/Article/articles?pageNumber={pageNumber}&pageSize={pageSize}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<ArticlePageResponse>();
    }

    public async Task<ArticlePageResponse> GetArticles(int categoryId, int pageNumber, int pageSize)
    {
        var response = await httpClient.GetAsync($"v1/v1.0/Article/articles/byCategory?categoryId={categoryId}&pageNumber={pageNumber}&pageSize={pageSize}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<ArticlePageResponse>();
    }
}
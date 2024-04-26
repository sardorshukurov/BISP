using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Welisten.Services.Articles;
using Welisten.Services.Logger.Logger;

namespace Welisten.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Article")]
[Authorize]
[Route("v{version:apiVersion}/[controller]")]
public class ArticleController : ControllerBase
{
    private readonly IArticleService _articleService;
    private readonly IAppLogger _logger;

    public ArticleController(IArticleService articleService, IAppLogger logger)
    {
        _articleService = articleService;
        _logger = logger;
    }

    [HttpGet("categories")]
    public async Task<IActionResult> GetCategories()
    {
        try
        {
            return Ok(await _articleService.GetAllCategories());
        }
        catch (Exception e)
        {
            _logger.Error(e.Message);
            return StatusCode(500);
        }
    }

    [HttpGet("articles")]
    public async Task<IActionResult> GetAllArticles([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        try
        {
            return Ok(await _articleService.GetAllArticles(pageNumber, pageSize));
        }
        catch (Exception e)
        {
            _logger.Error(e.Message);
            return StatusCode(500);
        }
    }

    [HttpGet("articles/byCategory")]
    public async Task<IActionResult> GetAllArticlesByCategory([FromQuery] int categoryId, [FromQuery] int pageNumber,
        [FromQuery] int pageSize)
    {
        try
        {
            return Ok(await _articleService.GetAllArticlesByCategory(categoryId, pageNumber, pageSize));
        }
        catch (Exception e)
        {
            _logger.Error(e.Message);
            return StatusCode(500);
        }
    }
}
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Welisten.Services.Logger.Logger;
using Welisten.Services.Topics;
using Welisten.Services.UserAccounts;

namespace Welisten.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Post")]
[Authorize]
[Route("v{version:apiVersion}/[controller]")]
public class TopicController : ControllerBase
{
    private readonly ITopicService _topicService;
    private readonly IAppLogger _logger;

    public TopicController(ITopicService topicService, IAppLogger logger)
    {
        _topicService = topicService;
        _logger = logger;
    }

    [AllowAnonymous]
    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            return Ok(await _topicService.GetAll());

        }
        catch (Exception e)
        {
            _logger.Error(e.Message);
            return StatusCode(500);
        }
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost("")]
    public async Task<IActionResult> Create([FromBody] string type)
    {
        try
        {
            await _topicService.Create(type);
            return Ok();
        }
        catch (Exception e)
        {
            _logger.Error(e.Message);
            return StatusCode(500);
        }
    }

    [Authorize(Roles = "Administrator")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] string type)
    {
        try
        {
            await _topicService.Update(id, type);
            return Ok();
        }
        catch (Exception e)
        {
            _logger.Error(e.Message);
            return StatusCode(500);
        }
    }
}
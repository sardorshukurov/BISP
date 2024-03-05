using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    private readonly IUserAccountService _userService;

    public TopicController(ITopicService topicService, IUserAccountService userService)
    {
        _topicService = topicService;
        _userService = userService;
    }

    [HttpGet("")]
    public async Task<IEnumerable<TopicModel>> GetAll()
    {
        return await _topicService.GetAll();
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
            return BadRequest(e.Message);
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
            return BadRequest(e.Message);
        }
    }
}
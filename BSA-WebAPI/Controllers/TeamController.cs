using BSA_WebAPI.DTO;
using BSA_WebAPI.Exceptions;
using BSA_WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BSA_WebAPI.Controllers;

[ApiController]
[Route("api/Teams")]
public class TeamController: ControllerBase
{
    private readonly ITeamService _data;
    private readonly ILogger<TeamController> _logger;

    public TeamController(ITeamService data, ILogger<TeamController> logger)
    {
        _logger = logger;
        _data = data;
    }
    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<TeamDTO>> GetTeamById(int id)
    {
        try
        {
            var item = await _data.GetAsync(id);
            return new OkObjectResult(item);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            if (e is InvalidOperationException or ServiceException)
            {
                return new BadRequestObjectResult(e.Message);
            }
            return new StatusCodeResult(500);
        }
    }
    [HttpGet]
    [Route("")]
    public async Task<ActionResult<List<TeamDTO>>> GetTeams()
    {
        try
        {
            var item = await _data.GetAllAsync();
            return new OkObjectResult(item);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            if (e is InvalidOperationException or ServiceException)
            {
                return new BadRequestObjectResult(e.Message);
            }
            return new StatusCodeResult(500);
        }
    }
    [HttpPut]
    [Route("")]
    public async Task<ActionResult<TeamDTO>> UpdateTeam([FromBody]TeamDTO team)
    {
        try
        {
            await _data.UpdateAsync(team);
            var item = await _data.GetAsync(team.Id);
            return new OkObjectResult(item);
        }
        catch (DbUpdateException e)
        {
            _logger.LogWarning(e.InnerException?.Message);
            return new BadRequestObjectResult(e.InnerException?.Message);
        }
        catch (Exception e)
        {
            if (e is InvalidOperationException or ServiceException)
            {
                _logger.LogWarning(e.Message);
                return new BadRequestObjectResult(e.Message);
            }
            _logger.LogError(e.Message);
            return new StatusCodeResult(500);
        }
    }
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult> DeleteTeam(int id)
    {
        try
        {
            await _data.DeleteAsync(id);
            return new OkResult();
        }
        catch (Exception e)
        {
            if (e is InvalidOperationException or ServiceException)
            {
                _logger.LogWarning(e.Message);
                return new BadRequestObjectResult(e.Message);
            }
            _logger.LogError(e.Message);
            return new StatusCodeResult(500);
        }
    }
    [HttpDelete]
    [Route("")]
    public async Task<ActionResult> DeleteTeam([FromBody]TeamDTO team)
    {
        try
        {
            await _data.DeleteAsync(team);
            return new OkResult();
        }
        catch (Exception e)
        {
            if (e is InvalidOperationException or ServiceException)
            {
                _logger.LogWarning(e.Message);
                return new BadRequestObjectResult(e.Message);
            }
            _logger.LogError(e.Message);
            return new StatusCodeResult(500);
        }
    }
    [HttpPost]
    [Route("")]
    public async Task<ActionResult<TeamDTO>> AddTeam([FromBody]TeamDTO team)
    {
        try
        {
            var item = await _data.AddAsync(team);
            return new OkObjectResult(item);
        }
        catch (Exception e)
        {
            if (e is InvalidOperationException or ServiceException)
            {
                _logger.LogWarning(e.Message);
                return new BadRequestObjectResult(e.Message);
            }
            _logger.LogError(e.Message);
            return new StatusCodeResult(500);
        }
    }
    [HttpGet]
    [Route("[action]")]
    public async Task<ActionResult<IEnumerable<IEnumerable<TeamDTO>>>> GetTeamsWithMembersOlderThenTen()
    {
        try
        {
            var result = await _data.GetTeamsWithMembersOlderThenTenAsync();
            return new OkObjectResult(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            if (e is InvalidOperationException or ServiceException)
            {
                return new BadRequestObjectResult(e.Message);
            }
            return new StatusCodeResult(500);
        }
    }
}
using BSA_WebAPI.DTO;
using BSA_WebAPI.Exceptions;
using BSA_WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BSA_WebAPI.Controllers;

[ApiController]
[Route("api/Projects")]
public class ProjectController: ControllerBase
{
    private readonly IProjectService _data;
    private readonly ILogger<ProjectController> _logger;

    public ProjectController(ILogger<ProjectController> logger, IProjectService data)
    {
        _logger = logger;
        _data = data;
    }
    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<ProjectDTO>> GetProjectById(int id)
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
    public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetAllProject()
    {
        try
        {
            var items = await _data.GetAllAsync();
            return new OkObjectResult(items);
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
    [HttpPost]
    [Route("")]
    public async Task<ActionResult<ProjectDTO>> AddProject([FromBody]ProjectDTO project)
    {
        try
        {
            var item = await _data.AddAsync(project);
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
    [HttpPut]
    [Route("")]
    public async Task<ActionResult<ProjectDTO>> UpdateProject([FromBody]ProjectDTO project)
    {
        try
        {
            await _data.UpdateAsync(project);
            var item = await _data.GetAsync(project.Id);
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
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult<bool>> DeleteProject(int id)
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
    public async Task<ActionResult> DeleteProject([FromBody]ProjectDTO project)
    {
        try
        {
            await _data.DeleteAsync(project);
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
    [HttpGet]
    [Route("[action]/{id:int}")]
    public async Task<ActionResult<List<UserWithTaskDTO>>> GetProjectDetailInfo(int id)
    {
        try
        {
            var item = await _data.GetProjectDetailInfoAsync(id);
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
}
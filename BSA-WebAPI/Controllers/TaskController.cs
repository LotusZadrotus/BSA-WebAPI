using BSA_WebAPI.DTO;
using BSA_WebAPI.Exceptions;
using BSA_WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BSA_WebAPI.Controllers;

[ApiController]
[Route("api/Tasks")]
public class TaskController: ControllerBase
{
    private readonly ITaskService _data;
    private readonly ILogger<TaskController> _logger;

    public TaskController(ITaskService data, ILogger<TaskController> logger)
    {
        _data = data;
        _logger = logger;
    }
    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<TaskDTO>> GetTaskById(int id)
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
    public async Task<ActionResult<IEnumerable<TaskDTO>>> GetAllTasks()
    {
        try
        {
            var item =await _data.GetAllAsync();
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
    [HttpPost]
    [Route("")]
    public async Task<ActionResult<TaskDTO>> AddTask([FromBody]TaskDTO task)
    {
        try
        {
            if (task.FinishedAt < task.CreatedAt)
                return new BadRequestObjectResult("Task cannot be finished before it's created");
            await _data.AddAsync(task);
            return new OkObjectResult(task);
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
    public async Task<ActionResult<TaskDTO>> UpdateTask([FromBody]TaskDTO task)
    {
        try
        {
            await _data.UpdateAsync(task);
            var item = await _data.GetAsync(task.Id);
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
    [Route("")]
    public async Task<ActionResult> DeleteTask([FromBody]TaskDTO task)
    {
        try
        {
            await _data.DeleteAsync(task);
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
    [Route("{id:int}")]
    public async Task<ActionResult> DeleteTask(int id)
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

    [HttpGet]
    [Route("[action]/{id:int}")]
    public async Task<ActionResult> GetNumberOfTaskInProjectByUserId(int id)
    {
        try
        {
            var result = await _data.GetNumberOfTaskInProjectByUserIdAsync(id);
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
    [HttpGet]
    [Route("[action]/{id:int}")]
    public async Task<ActionResult<IEnumerable<TaskDTO>>> GetNumberOfTaskByUserId(int id)
    {
        try
        {
            var result =await _data.GetNumberOfTaskByUserIdAsync(id);
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
    [HttpGet]
    [Route("[action]/{id:int}")]
    public async Task<ActionResult<IEnumerable<TaskDTO>>> GetNumberOfFinishedTaskByUserId(int id)
    {
        try
        {
            var result =await _data.GetFinishedTaskByUserIdAsync(id);
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


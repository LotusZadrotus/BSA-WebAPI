using BSA_WebAPI.DTO;
using BSA_WebAPI.Exceptions;
using BSA_WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BSA_WebAPI.Controllers;

[ApiController]
[Route("api/Users")]
public class UserController: ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _data;

    public UserController(ILogger<UserController> logger, IUserService data)
    {
        _data = data;
        _logger = logger;
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<UserDTO>> GetUserById(int id)
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
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
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

    [HttpPost]
    [Route("")]
    public async Task<ActionResult<UserDTO>> AddUser([FromBody] UserDTO user)
    {
        if (!ModelState.IsValid) return new BadRequestObjectResult("Invalid body");
        try
        {
            var item = await _data.AddAsync(user);
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
    [Route("update")]
    public async Task<ActionResult<UserDTO>> UpdateUser([FromBody]UserDTO user)
    {
        if (!ModelState.IsValid) return new BadRequestObjectResult("InvalidBody");
        try
        {
            await _data.UpdateAsync(user);
            var toReturn = await _data.GetAllAsync();
            return new OkObjectResult(toReturn.FirstOrDefault(x => x.Id == user.Id));
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
    public async Task<ActionResult> DeleteUser(int id)
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
    public async Task<ActionResult> DeleteUser([FromBody] UserDTO user)
    {
        try
        {
            await _data.DeleteAsync(user);
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
    [Route("[action]")]
    public async Task<ActionResult<List<UserWithTaskDTO>>> GetAllUsersSortedByFirstNameWithSortedTaskByNameLength()
    {
        try
        {
            var result = await _data.GetAllUsersSortedByFirstNameWithSortedTaskByNameLengthAsync();
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
    public async Task<ActionResult<List<UserDetailInfoDTO>>> GetUserDetailInfo(int id)
    {
        try
        {
            return new OkObjectResult(await _data.GetUserDetailInfoAsync(id));
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
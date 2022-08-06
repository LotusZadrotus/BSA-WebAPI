using BSA_WebAPI.DTO;
using BSA_WebAPI.Exceptions;
using BSA_WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Task = BSA_WebAPI.Models.Task;

namespace BSA_WebAPI.Services.Impl;

public class UserService: IUserService
{
    private readonly IUnitOfWork _uof;
    private readonly ILogger<UserService> _logger;

    public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger)
    {
        _uof = unitOfWork;
        _logger = logger;
    }
    public async Task<UserDTO> GetAsync(int id)
    {
        try
        {
            var item = await _uof.GetRepo<User>().GetAsync(id);
            return new UserDTO(item);
        }
        catch (Exception e)
        {

            _logger.LogError(e.Message);
            if (e is InvalidOperationException)
            {
                throw new InvalidOperationException(e.Message);
            }
            throw new ServiceException(e.Message);
        }
    }

    public async Task<UserDTO> AddAsync(UserDTO item)
    {
        try
        {
            var toReturn = await _uof.GetRepo<User>().AddAsync(Convert(item));
            await _uof.AsyncSave();
            return new UserDTO(toReturn);
        }
        catch (Exception e)
        {
            if (e is DbUpdateException)
            {
                _logger.LogError(e.InnerException?.Message);
                throw new ServiceException(e.InnerException?.Message ?? e.Message);
            }
            _logger.LogError(e.Message);
            if (e is InvalidOperationException or DbUpdateException)
            {
                throw new InvalidOperationException(e.Message);
            }
            throw new ServiceException(e.Message);
        }
    }

    public async System.Threading.Tasks.Task DeleteAsync(UserDTO item)
    {
        try
        {
            await _uof.GetRepo<User>().DeleteAsync(Convert(item));
            await _uof.AsyncSave();
        }
        catch (Exception e)
        {
            if (e is DbUpdateException)
            {
                _logger.LogError(e.InnerException?.Message);
                throw new ServiceException(e.InnerException?.Message ?? e.Message);
            }
            _logger.LogError(e.Message);
            if (e is InvalidOperationException or DbUpdateException)
            {
                throw new InvalidOperationException(e.Message);
            }
            throw new ServiceException(e.Message);
        }
    }

    public async System.Threading.Tasks.Task DeleteAsync(int id)
    {
        try
        {
            await _uof.GetRepo<User>().DeleteAsync(id);
            await _uof.AsyncSave();
        }
        catch (Exception e)
        {
            if (e is DbUpdateException)
            {
                _logger.LogError(e.InnerException?.Message);
                throw new ServiceException(e.InnerException?.Message ?? e.Message);
            }
            _logger.LogError(e.Message);
            if (e is InvalidOperationException)
            {
                throw new InvalidOperationException(e.Message);
            }
            throw new ServiceException(e.Message);
        }
    }

    public async Task<IEnumerable<UserDTO>> GetAllAsync()
    {
        try
        {
            var result = await _uof.GetRepo<User>().GetAllAsync();
            return result.Select(x=>new UserDTO(x));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            if (e is InvalidOperationException)
            {
                throw new InvalidOperationException(e.Message);
            }
            throw new ServiceException(e.Message);
        }
    }

    public async System.Threading.Tasks.Task UpdateAsync(UserDTO item)
    {
        try
        {
            await _uof.GetRepo<User>().UpdateAsync(Convert(item));
            await _uof.AsyncSave();
        }
        catch (Exception e)
        {
            if (e is DbUpdateException)
            {
                _logger.LogError(e.InnerException?.Message);
                throw new ServiceException(e.InnerException?.Message ?? e.Message);
            }
            _logger.LogError(e.Message);
            if (e is InvalidOperationException)
            {
                throw new InvalidOperationException(e.Message);
            }
            throw new ServiceException(e.Message);
        }
    }

    public async Task<IEnumerable<UserWithTaskDTO>> GetAllUsersSortedByFirstNameWithSortedTaskByNameLengthAsync()
    {
        try
        {
            var user = await _uof.GetRepo<User>().GetAllAsync();
            var tasks = await _uof.GetRepo<Task>().GetAllAsync();
            // await System.Threading.Tasks.Task.WhenAll(user, tasks);
            // var userResult = await user;
            // var tasksResult = await tasks;
            var result = from users in user
                orderby users.FirstName
                join task in tasks on users.Id equals task.PerformerId into g
                select new UserWithTaskDTO()
                {
                    Id = users.Id,
                    TeamId = users.TeamId,
                    BirthDate = users.BirthDay,
                    Email = users.Email,
                    FirstName = users.FirstName,
                    LastName = users.LastName,
                    RegisteredAt = users.RegisteredAt,
                    Tasks = g.OrderByDescending(x=>x.Name?.Length).Select(x=>new TaskDTO(x))
                };
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            if (e is InvalidOperationException)
            {
                throw new InvalidOperationException(e.Message);
            }
            throw new ServiceException(e.Message);
        }
    }

    public async Task<UserDetailInfoDTO> GetUserDetailInfoAsync(int id)
    {
        try
        {
            var user = await _uof.GetRepo<User>().GetAllAsync();
            var projects = await _uof.GetRepo<Project>().GetAllAsync() ;
            var tasks = await _uof.GetRepo<Task>().GetAllAsync();
            // await System.Threading.Tasks.Task.WhenAll(user, projects, tasks);
            var result = from users in user
                where users.Id == id
                join project in projects on users.TeamId equals project.TeamId into g
                join task in tasks on users.Id equals task.PerformerId into b
                let latest = g.MaxBy(x => x.CreateAt)
                let longest = b.Where(x => x.State == TaskState.Done).MinBy(x => x.FinishedAt - x.CreatedAt)
                select new UserDetailInfoDTO()
                {
                    User = new UserDTO(users),
                    LatestProject =latest is not null? new ProjectDTO(latest): null,
                    NumberOfTaskInLastProject = b.Count(x => x.ProjectId is not null && x.ProjectId == g.MaxBy(j => j.CreateAt).Id),
                    UnfinishedTaskCount = b.Count(x => x.State is TaskState.Canceled or TaskState.InProgress),
                    LongestTask = longest is not null? new TaskDTO(longest): null
                };
            return result.FirstOrDefault() ?? throw new InvalidOperationException("User has no task");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            if (e is InvalidOperationException)
            {
                throw new InvalidOperationException(e.Message);
            }
            throw new ServiceException(e.Message);
        }
    }

    private User Convert(UserDTO item)
    {
        return new User() with
        {
            Id = item.Id,
            TeamId = item.TeamId,
            FirstName = item.FirstName,
            LastName = item.LastName,
            Email = item.Email,
            RegisteredAt = item.RegisteredAt,
            BirthDay = item.BirthDate
        };
    }
}
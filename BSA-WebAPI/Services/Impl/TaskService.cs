using BSA_WebAPI.DTO;
using BSA_WebAPI.Exceptions;
using BSA_WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Task = BSA_WebAPI.Models.Task;

namespace BSA_WebAPI.Services.Impl;

public class TaskService: ITaskService
{
    private readonly IUnitOfWork _data;
    private readonly ILogger<TaskService> _logger;

    public TaskService(IUnitOfWork uof, ILogger<TaskService> logger)
    {
        _data = uof;
        _logger = logger;
    }
    public async Task<TaskDTO> GetAsync(int id)
    {
        try
        {
            
            var toReturn = await _data.GetRepo<Task>().GetAsync(id);
            return new TaskDTO(toReturn);
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
            throw new Exception(e.Message);
        }
    }

    public async Task<TaskDTO> AddAsync(TaskDTO item)
    {
        try
        {
            var toReturn = await _data.GetRepo<Task>().AddAsync(Convert(item));
            await _data.AsyncSave();
            return new TaskDTO(toReturn);
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
            throw new Exception(e.Message);
        }
    }

    public async System.Threading.Tasks.Task DeleteAsync(TaskDTO item)
    {
        try
        {
            await _data.GetRepo<Task>().DeleteAsync(Convert(item));
            await _data.AsyncSave();
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
            throw new Exception(e.Message);
        }
    }

    public async System.Threading.Tasks.Task DeleteAsync(int id)
    {
        try
        {
            await _data.GetRepo<Task>().DeleteAsync(id);
            await _data.AsyncSave();
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
            throw new Exception(e.Message);
        }
    }

    public async Task<IEnumerable<TaskDTO>> GetAllAsync()
    {
        try
        {
            var toReturn = await _data.GetRepo<Task>().GetAllAsync();
            return toReturn.Select(x=>new TaskDTO(x));
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
            throw new Exception(e.Message);
        }
    }

    public async System.Threading.Tasks.Task UpdateAsync(TaskDTO item)
    {
        try
        {
            await _data.GetRepo<Task>().UpdateAsync(Convert(item));
            await _data.AsyncSave();
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
            throw new Exception(e.Message);
        }
    }

    public async Task<IDictionary<ProjectDTO, int>> GetNumberOfTaskInProjectByUserIdAsync(int id)
    {
        try
        {
            var projects = await _data.GetRepo<Project>().GetAllAsync();
            var tasks = await _data.GetRepo<Task>().GetAllAsync();
            // await System.Threading.Tasks.Task.WhenAll(projects, tasks);
            var result = from project in projects
                join task in tasks on project.Id equals task.ProjectId into g
                where g.Any(x=>x.PerformerId == id)
                select new
                {
                    Key = new ProjectDTO(project),
                    Count = g.Count()
                };
            return result.ToDictionary(x=>x.Key,x=>x.Count);
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
            throw new Exception(e.Message);
        }
    }

    public async Task<IEnumerable<TaskDTO>> GetNumberOfTaskByUserIdAsync(int id)
    {
        try
        {
            var result = await _data.GetRepo<Task>().GetAllAsync();
            return result.Where(task => task.PerformerId == id && task.Name?.Length < 45).Select(x=>new TaskDTO(x));
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
            throw new Exception(e.Message);
        }
    }

    public async System.Threading.Tasks.Task FinishTaskAsync(int id)
    {
        try
        {
            var task = await _data.GetRepo<Task>().GetAsync(id);
            task.State = TaskState.Done;
            await _data.GetRepo<Task>().UpdateAsync(task);
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
            throw new Exception(e.Message);
        }
    }

    public async Task<IEnumerable<FinishedTaskDTO>> GetFinishedTaskByUserIdAsync(int id)
    {
        try
        {
            var result = await _data.GetRepo<Task>().GetAllAsync();
            return result.Where(task =>
                task.PerformerId == id && task.State == TaskState.Done &&
                task.FinishedAt!.Value.Year == DateTime.Now.Year).Select(x=>new FinishedTaskDTO(){Id = x.Id, Name = x.Name});
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
            throw new Exception(e.Message);
        }
    }

    private Task Convert(TaskDTO item)
    {
        return new Task() with
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            PerformerId = item.PerformerId,
            ProjectId = item.ProjectId,
            State = item.State,
            CreatedAt = item.CreatedAt,
            FinishedAt = item.FinishedAt
        };
    }
}
using BSA_WebAPI.DTO;
using BSA_WebAPI.Exceptions;
using BSA_WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Task = BSA_WebAPI.Models.Task;

namespace BSA_WebAPI.Services.Impl;

public class ProjectService: IProjectService
{
    private readonly IUnitOfWork _data;
    private readonly ILogger<ProjectService> _logger;

    public ProjectService(IUnitOfWork uof, ILogger<ProjectService> logger)
    {
        _data = uof;
        _logger = logger;
    }
    public async Task<ProjectDTO> GetAsync(int id)
    {
        try
        {
            var item = await _data.GetRepo<Project>().GetAsync(id);
            return new ProjectDTO(item);
        }
        catch (Exception e)
        {

            _logger.LogError(e.Message);
            if (e is InvalidOperationException)
            {
                throw new InvalidOperationException(e.Message);
            }
            throw new Exception(e.Message);
        }
    }

    public async Task<ProjectDTO> AddAsync(ProjectDTO item)
    {
        try
        {
            
            var toReturn = await _data.GetRepo<Project>().AddAsync(Convert(item));
            await _data.AsyncSave();
            return new ProjectDTO(toReturn);
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

    public async System.Threading.Tasks.Task DeleteAsync(ProjectDTO item)
    {
        try
        {
            await _data.GetRepo<Project>().DeleteAsync(Convert(item));
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
            
            await _data.GetRepo<Project>().DeleteAsync(id);
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

    public async Task<IEnumerable<ProjectDTO>> GetAllAsync()
    {
        try
        {
            
            var toReturn = await _data.GetRepo<Project>().GetAllAsync();
            await _data.AsyncSave();
            return toReturn.Select(x=>new ProjectDTO(x));
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

    public async System.Threading.Tasks.Task UpdateAsync(ProjectDTO item)
    {
        try
        {
            await _data.GetRepo<Project>().UpdateAsync(Convert(item));
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

    public async Task<IEnumerable<ProjectDetailInfoDTO>> GetProjectDetailInfoAsync(int id)
    {
        try
        {
            var projects = await _data.GetRepo<Project>().GetAllAsync();
            var tasks = await _data.GetRepo<Task>().GetAllAsync();
            var users = await _data.GetRepo<User>().GetAllAsync();
            // await System.Threading.Tasks.Task.WhenAll(projects, tasks, users);
            var result = from project in projects
                join task in tasks on project.Id equals task.ProjectId into g
                join user in users on project.TeamId equals user.TeamId into b
                let longest = g.MaxBy(x=>x.Description?.Length)
                let shortest = g.MinBy(x => x.Name?.Length)
                select new ProjectDetailInfoDTO()
                {
                    Project = new ProjectDTO(project),
                    LongestTask = longest is not null ? new TaskDTO(longest): null,
                    ShortestTask = shortest is not null ? new TaskDTO(shortest): null,
                    Members = project.Description?.Length > 20 || g.Count() < 3 ? b.Count() : null
                };
            return result;
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

    private Project Convert(ProjectDTO item)
    {
        return new Project() with
        {
            Id = item.Id,
            AuthorId = item.AuthorId,
            Name = item.Name,
            Deadline = item.Deadline,
            Description = item.Description,
            CreateAt = item.CreatedAt,
            TeamId = item.TeamId
        };
    }
}
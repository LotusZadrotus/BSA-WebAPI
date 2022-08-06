using BSA_WebAPI.DTO;
using BSA_WebAPI.Exceptions;
using BSA_WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace BSA_WebAPI.Services.Impl;

public class TeamService: ITeamService
{
    private readonly IUnitOfWork _data;
    private readonly ILogger<TeamService> _logger;

    public TeamService(IUnitOfWork data, ILogger<TeamService> logger)
    {
        _data = data;
        _logger = logger;
    }
    public async Task<TeamDTO> GetAsync(int id)
    {
        try
        {
            var item = await _data.GetRepo<Team>().GetAsync(id);
            return new TeamDTO(item);
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

    public async Task<TeamDTO> AddAsync(TeamDTO  item)
    {
        try
        {
            
            var toReturn =await _data.GetRepo<Team>().AddAsync(Convert(item));
            await _data.AsyncSave();
            return new TeamDTO(toReturn);
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

    public async Task DeleteAsync(TeamDTO item)
    {
        try
        {
            await _data.GetRepo<Team>().DeleteAsync(Convert(item));
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

    public async Task DeleteAsync(int id)
    {
        try
        {
            await _data.GetRepo<Team>().DeleteAsync(id);
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

    public async Task<IEnumerable<TeamDTO>> GetAllAsync()
    {
        try
        {
            var result = await _data.GetRepo<Team>().GetAllAsync();
            return result.Select(team => new TeamDTO(team));
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

    public async Task UpdateAsync(TeamDTO item)
    {
        try
        {
            await _data.GetRepo<Team>().UpdateAsync(Convert(item));
            await _data.AsyncSave();
        }
        catch (Exception e)
        {
            if (e is DbUpdateException)
            {
                _logger.LogError(e.InnerException?.Message);
                throw new ServiceException(e.InnerException?.Message ?? "Unknown error");
            }
            _logger.LogError(e.Message);
            if (e is InvalidOperationException)
            {
                throw new InvalidOperationException(e.Message);
            }
            throw new Exception(e.Message);
        }
    }

    public async Task<IEnumerable<TeamWithMembersDTO>> GetTeamsWithMembersOlderThenTenAsync()
    {
        try
        {
            var teams = await _data.GetRepo<Team>().GetAllAsync();
            var users = await _data.GetRepo<User>().GetAllAsync();
            // await Task.WhenAll(teams, users);
            var result = from team in teams
                join user in users on team.Id equals user.TeamId into g
                where g.All(x=>DateTime.Now.Year - x.BirthDay.Year > 10)
                select new TeamWithMembersDTO
                {
                    Id = team.Id,
                    Name = team.Name,
                    Members = g.Select(x=>new UserDTO(x)),
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

    private Team Convert(TeamDTO item)
    {
        return new Team() with
        {
            Id = item.Id,
            Name = item.Name,
            CreatedAt = item.CreatedAt
        };
    }
}
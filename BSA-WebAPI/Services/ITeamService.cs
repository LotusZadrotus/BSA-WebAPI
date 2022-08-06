using BSA_WebAPI.DTO;

namespace BSA_WebAPI.Services;

public interface ITeamService: IService<TeamDTO>
{
    public Task<IEnumerable<TeamWithMembersDTO>> GetTeamsWithMembersOlderThenTenAsync();
}
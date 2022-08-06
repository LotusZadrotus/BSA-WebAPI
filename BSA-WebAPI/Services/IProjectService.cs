using BSA_WebAPI.DTO;
namespace BSA_WebAPI.Services;

public interface IProjectService: IService<ProjectDTO>
{
    public Task<IEnumerable<ProjectDetailInfoDTO>> GetProjectDetailInfoAsync(int id);
}
using BSA_WebAPI.DTO;


namespace BSA_WebAPI.Services;

public interface ITaskService: IService<TaskDTO>
{
    public Task<IDictionary<ProjectDTO, int>> GetNumberOfTaskInProjectByUserIdAsync(int id);
    public Task<IEnumerable<TaskDTO>> GetNumberOfTaskByUserIdAsync(int id);
    public Task<IEnumerable<FinishedTaskDTO>> GetFinishedTaskByUserIdAsync(int id);
    public Task FinishTaskAsync(int id);
}
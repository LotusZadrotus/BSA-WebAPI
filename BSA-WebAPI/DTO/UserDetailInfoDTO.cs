using BSA_WebAPI.Models;
using Task = BSA_WebAPI.Models.Task;

namespace BSA_WebAPI.DTO;

public record UserDetailInfoDTO
{
    public UserDTO? User { get; init; }
    public ProjectDTO? LatestProject { get; init; }
    public int NumberOfTaskInLastProject { get; init; }
    public int UnfinishedTaskCount { get; init; }
    public TaskDTO? LongestTask { get; init; }
}
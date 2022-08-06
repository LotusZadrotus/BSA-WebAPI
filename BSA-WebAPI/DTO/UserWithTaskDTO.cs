using BSA_WebAPI.Models;
using Task = BSA_WebAPI.Models.Task;

namespace BSA_WebAPI.DTO;

public record UserWithTaskDTO: UserDTO
{
    public IEnumerable<TaskDTO>? Tasks { get; init; }
    
}
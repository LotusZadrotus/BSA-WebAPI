using BSA_WebAPI.DTO;

namespace BSA_WebAPI.Services;

public interface IUserService: IService<UserDTO>
{
    public Task<IEnumerable<UserWithTaskDTO>> GetAllUsersSortedByFirstNameWithSortedTaskByNameLengthAsync();
    public Task<UserDetailInfoDTO> GetUserDetailInfoAsync(int id);
}
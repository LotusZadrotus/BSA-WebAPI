namespace BSA_WebAPI.DTO;

public record TeamWithMembersDTO
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public IEnumerable<UserDTO>? Members { get; init; }
}
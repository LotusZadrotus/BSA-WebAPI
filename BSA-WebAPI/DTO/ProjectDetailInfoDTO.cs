namespace BSA_WebAPI.DTO;

// ReSharper disable once InconsistentNaming
public record ProjectDetailInfoDTO
{
    public ProjectDTO? Project { get; init; }
    public TaskDTO? LongestTask { get; init; }
    public TaskDTO? ShortestTask { get; init; }
    public int? Members { get; init; }
}
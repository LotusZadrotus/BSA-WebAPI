using BSA_WebAPI.Models;
using Task = BSA_WebAPI.Models.Task;

namespace BSA_WebAPI.DTO;

// ReSharper disable once InconsistentNaming
public record TaskDTO: Entity
{
    public int? ProjectId { get; init; }
    public int? PerformerId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public TaskState State { get; set; }
    public DateTime CreatedAt { get; init; }
    public DateTime? FinishedAt { get; set; }

    public TaskDTO()
    {
        
    }
    public TaskDTO(Task item)
    {
        Id = item.Id;
        ProjectId = item.ProjectId;
        PerformerId = item.PerformerId;
        Name = item.Name;
        Description = item.Description;
        State = item.State;
        CreatedAt = item.CreatedAt;
        FinishedAt = item.FinishedAt;
    }
}
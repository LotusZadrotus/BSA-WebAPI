using BSA_WebAPI.Models;

namespace BSA_WebAPI.DTO;

// ReSharper disable once InconsistentNaming
public record ProjectDTO: Entity
{
    public int AuthorId { get; init; }
    public string? Description { get; set; }
    public int TeamId { get; set; }
    public string? Name { get; set; }
    public DateTime Deadline { get; set; }
    public DateTime CreatedAt { get; set; }

    public ProjectDTO()
    {
        
    }
    public ProjectDTO(Project item)
    {
        Id = item.Id;
        AuthorId = item.AuthorId;
        Description = item.Description;
        TeamId = item.TeamId;
        Name = item.Name;
        Deadline = item.Deadline;
        CreatedAt = item.CreateAt;
    }
}
using BSA_WebAPI.Models;
using Newtonsoft.Json;

namespace BSA_WebAPI.DTO;

// ReSharper disable once InconsistentNaming
public record TeamDTO : Entity
{
    public string? Name { get; init; }
    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; init; }

    public TeamDTO()
    {
        
    }

    public TeamDTO(Team team)
    {
        Id = team.Id;
        Name = team.Name;
        CreatedAt = team.CreatedAt;
    }
}
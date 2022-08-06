using BSA_WebAPI.Models;

namespace BSA_WebAPI.DTO;

// ReSharper disable once InconsistentNaming
public record FinishedTaskDTO : Entity
{
    public string? Name { get; init; }
}
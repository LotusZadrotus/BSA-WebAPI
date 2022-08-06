
using System.ComponentModel.DataAnnotations.Schema;

namespace BSA_WebAPI.Models;

public record  Team: Entity
{
    // [Key]
    // [Column("id")]
    // public new int Id { get; init; }
    [Column("name")]
    
    public string? Name { get; set; }
    [Column("createdAt")]
    public DateTime CreatedAt { get; init; }
}
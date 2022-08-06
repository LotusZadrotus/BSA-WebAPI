using System.ComponentModel.DataAnnotations.Schema;

namespace BSA_WebAPI.Models;

public record  Task : Entity 
{
    // [Key]
    // [Column("id")]
    // public int Id { get; init; }
    [Column("projectId")]
    public int? ProjectId { get; set; }
    [ForeignKey("ProjectId")]
    public Project? Project { get; set; }
    [Column("performerId")]
    public int? PerformerId { get; set; }
    [ForeignKey("PerformerId")]
    public User? Performer { get; set; }
    [Column("name")]
    public string? Name { get; set; }
    [Column("description")]
    public string? Description { get; set; }
    [Column("state")]
    public TaskState State { get; set; }
    [Column("createdAt")]
    public DateTime CreatedAt { get; init; }
    [Column("finishedAt")]
    public DateTime? FinishedAt { get; set; }
    
}
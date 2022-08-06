using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BSA_WebAPI.Models;

public record class Project: Entity
{

    [Column("authorId")]
    public int AuthorId { get; set; }
    [ForeignKey("AuthorId")]
    public User? Author { get; set; }
    [Column("description")]
    public string? Description { get; set; }
    [Column("teamId")]
    public int TeamId { get; set; }
    [ForeignKey("TeamId")]
    public Team? Team { get; set; }
    [Column("name")]
    public string? Name { get; set; }
    [Column("deadline")]
    public DateTime Deadline { get; set; }
    [Column("createdAt")]
    public DateTime CreateAt { get; init; }
    
}
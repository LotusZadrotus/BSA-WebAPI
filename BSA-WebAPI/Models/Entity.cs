using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BSA_WebAPI.Models;

public record Entity
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
}
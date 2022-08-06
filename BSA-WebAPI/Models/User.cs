using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BSA_WebAPI.Models;

public record  User : Entity
{
    // [Key]
    // [Column("id")]
    // public new int Id { get; init; }
    [Column("teamId")]
    public int? TeamId { get; set; }
    [ForeignKey("TeamId")]
    public Team? Team { get; set; }
    [Column("firstName")]
    [Required]
    public string? FirstName { get; init; }
    [Column("lastName")]
    [Required]
    public string? LastName { get; init; }
    [EmailAddress]
    [Column("email")]
    [Required]
    public string? Email { get; set; }
    [Column("registeredAt")]
    public DateTime RegisteredAt { get; init; }
    [Column("birthDay")]
    [Required]
    public DateTime BirthDay { get; init; }
    public override string ToString()
    {
        return $"Id: {Id}\nTeamId: {TeamId}\nFirstName: {FirstName}\nLastName: {LastName}" +
               $"\nEmail: {Email}\nRegisteredAt: {RegisteredAt}\nBirthDay: {BirthDay}";
    }
}
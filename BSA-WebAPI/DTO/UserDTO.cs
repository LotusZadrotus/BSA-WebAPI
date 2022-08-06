using System.ComponentModel.DataAnnotations;
using BSA_WebAPI.Models;

namespace BSA_WebAPI.DTO;

public record UserDTO: Entity
{
    public int? TeamId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    public DateTime RegisteredAt { get; set; }
    public DateTime BirthDate { get; set; }

    public UserDTO(User user)
    {
        Id = user.Id;
        TeamId = user.TeamId;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Email = user.Email;
        RegisteredAt = user.RegisteredAt;
        BirthDate = user.BirthDay;
    }

    public UserDTO()
    {
        
    }
}
using System.ComponentModel.DataAnnotations;

namespace WildOasis.API.Controllers.Identity.Dto;

public class UserCredentials
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
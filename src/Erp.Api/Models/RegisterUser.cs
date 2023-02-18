using System.ComponentModel.DataAnnotations;

namespace Erp.Api.Models;

public class RegisterUser
{
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Password { get; set; }
    public string Role { get; set; }
}
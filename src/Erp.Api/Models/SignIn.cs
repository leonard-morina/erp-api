using System.ComponentModel.DataAnnotations;

namespace Erp.Api.Models;

public class SignIn
{
    [Required]
    public string UsernameOrEmail { get; set; }

    [Required]
    public string Password { get; set; }
}
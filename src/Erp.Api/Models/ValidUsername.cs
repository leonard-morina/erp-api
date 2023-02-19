using System.ComponentModel.DataAnnotations;

namespace Erp.Api.Models;

public class ValidUsername
{
    [Required]
    public string Username { get; set; }
}
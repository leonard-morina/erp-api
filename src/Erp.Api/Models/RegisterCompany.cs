using System.ComponentModel.DataAnnotations;

namespace Erp.Api.Models;

public class RegisterCompany
{
    [Required] public string Name { get; set; }
    [Required] public string OwnerFirstName { get; set; }
    [Required] public string OwnerLastName { get; set; }
    [Required] public string PhoneNumber { get; set; }
    [Required] [EmailAddress] public string Email { get; set; }
    public string Website { get; set; }
    [Required] public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    [Required] public string City { get; set; }

    [Required]
    //TODO: Should be selectable??
    public string Country { get; set; }

    public IFormFile Logo { get; set; }
    public string Initials { get; set; }
}
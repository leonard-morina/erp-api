using System.ComponentModel.DataAnnotations;

namespace Erp.Api.Models;

public class ReviewCompanyJoinRequest
{
    [Required]
    public bool Approved { get; set; }
    [Required]
    public string CompanyJoinRequestId { get; set; }
}
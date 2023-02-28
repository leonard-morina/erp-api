using System.ComponentModel.DataAnnotations;

namespace Erp.Api.Models;

public class RequestToJoinCompany
{
    [Required]
    public string Code { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Erp.Core.Interfaces;

namespace Erp.Core.Entities.Account;

public class UserCompany : IEntity
{
    [Key]
    public string UserCompanyId { get; set; }
    public string UserId { get; set; }
    public string CompanyId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }
    [ForeignKey("CompanyId")]
    public Company Company { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Erp.Core.Interfaces;

namespace Erp.Core.Entities.Account;

public class UserCompany : IEntity, IAuditableEntity
{
    public UserCompany()
    {
        UserCompanyId = Guid.NewGuid().ToString();
        InsertedDateTime = DateTime.Now;
    }

    [Key]
    public string UserCompanyId { get; set; }
    public string UserId { get; set; }
    public string CompanyId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }
    [ForeignKey("CompanyId")]
    public Company Company { get; set; }
    public bool IsOwner { get; set; }
    public DateTime InsertedDateTime { get; set; }
    public DateTime? ModifiedDateTime { get; set; }
    public string? InsertedByUserId { get; set; }
    public string? ModifiedByUserId { get; set; }
    [ForeignKey("InsertedByUserId")]
    public User InsertedByUser { get; set; }
    [ForeignKey("ModifiedByUserId")]
    public User? ModifiedByUser { get; set; }
}
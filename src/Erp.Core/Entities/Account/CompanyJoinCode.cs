using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Erp.Core.Interfaces;
using Erp.Core.Utils;

namespace Erp.Core.Entities.Account;

public class CompanyJoinCode : IEntity, IAuditableEntity
{
    public CompanyJoinCode()
    {
        JoinCode = RandomStringGenerator.GenerateRandomCode(6);
    }
    
    [Key]
    public string CompanyJoinCodeId { get; set; }
    public string CompanyId { get; set; }
    public string JoinCode { get; set; }
    public bool IsActive { get; set; }
    public DateTime InsertedDateTime { get; set; }
    public DateTime? ModifiedDateTime { get; set; }
    public string? InsertedByUserId { get; set; }
    public string? ModifiedByUserId { get; set; }
    [ForeignKey("CompanyId")]
    public Company Company { get; set; }
    [ForeignKey("InsertedByUserId")]
    public User? InsertedByUser { get; set; }
    [ForeignKey("ModifiedByUserId")]
    public User? ModifiedByUser { get; set; }
}
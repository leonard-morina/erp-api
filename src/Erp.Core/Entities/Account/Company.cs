using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Erp.Core.Interfaces;

namespace Erp.Core.Entities.Account;

public class Company : IEntity, IAuditableEntity
{
    public Company()
    {
        CompanyId = Guid.NewGuid().ToString();
        InsertedDateTime = DateTime.Now;
    }

    [Key]
    public string CompanyId { get; set; }
    public string CompanyName { get; set; }
    public string CompanyAddress { get; set; }
    public string CompanyOwnerFirstName { get; set; }
    public string CompanyOwnerLastName { get; set; }
    public string CompanyLogo { get; set; }
    public string CompanyEmail { get; set; }
    public string CompanyWebsite { get; set; }
    public string CompanyPhone { get; set; }
    public DateTime InsertedDateTime { get; set; }
    public DateTime? ModifiedDateTime { get; set; }
    public string InsertedByUserId { get; set; }
    public string? ModifiedByUserId { get; set; }
    [ForeignKey("InsertedByUserId")]
    public User InsertedByUser { get; set; }
    [ForeignKey("ModifiedByUserId")]
    public User? ModifiedByUser { get; set; }
}
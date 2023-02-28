using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Erp.Core.Interfaces;

namespace Erp.Core.Entities.Account;

public class Company : IEntity, IAuditableEntity
{
    public Company(string ownerId)
    {
        CompanyId = Guid.NewGuid().ToString();
        InsertedDateTime = DateTime.Now;

        CompanyJoinCode = new CompanyJoinCode
        {
            CompanyJoinCodeId = Guid.NewGuid().ToString(),
            CompanyId = CompanyId,
            IsActive = true,
            InsertedDateTime = DateTime.Now,
            InsertedByUserId = ownerId,
        };
    }
    

    [Key]
    public string CompanyId { get; set; }
    public string Name { get; set; }
    public string AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? Initials { get; set; }
    public string OwnerFirstName { get; set; }
    public string OwnerLastName { get; set; }
    public string? Logo { get; set; }
    public string Email { get; set; }
    public string? Website { get; set; }
    public string Phone { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public DateTime InsertedDateTime { get; set; }
    public DateTime? ModifiedDateTime { get; set; }
    public string? InsertedByUserId { get; set; }
    public string? ModifiedByUserId { get; set; }
    [ForeignKey("InsertedByUserId")]
    public User InsertedByUser { get; set; }
    [ForeignKey("ModifiedByUserId")]
    public User? ModifiedByUser { get; set; }
    public CompanyJoinCode CompanyJoinCode { get; set; }
}
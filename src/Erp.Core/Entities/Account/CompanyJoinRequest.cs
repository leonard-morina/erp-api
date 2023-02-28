using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Erp.Core.Interfaces;

namespace Erp.Core.Entities.Account;

public class CompanyJoinRequest : IEntity, IAuditableEntity
{
    public CompanyJoinRequest()
    {
        CompanyJoinRequestId = Guid.NewGuid().ToString();
        InsertedDateTime = DateTime.Now;
    }

    public void ApproveRequest(string approvedById)
    {
        RequestApproved = true;
        RequestApprovedDateTime = DateTime.Now;
        ModifiedDateTime = DateTime.Now;
        ModifiedByUserId = approvedById;
    }

    [Key]
    public string CompanyJoinRequestId { get; set; }
    public string CompanyId { get; set; }
    public string UserId { get; set; }
    public bool RequestApproved { get; set; }
    public bool RequestCancelled { get; set; }
    public DateTime? RequestApprovedDateTime { get; set; }
    public DateTime? RequestCancelledDateTime { get; set; }
    public string? RequestApprovedByUserId { get; set; }
    public string? RequestCancelledByUserId { get; set; }
    public bool RequestInitiatedByCompany { get; set; }
    public DateTime InsertedDateTime { get; set; }
    public DateTime? ModifiedDateTime { get; set; }
    public string? InsertedByUserId { get; set; }
    public string? ModifiedByUserId { get; set; }
    [ForeignKey("InsertedByUserId")]
    public User InsertedByUser { get; set; }
    [ForeignKey("ModifiedByUserId")]
    public User? ModifiedByUser { get; set; }
    [ForeignKey("RequestApprovedByUserId")]
    public User RequestApprovedByUser { get; set; }
    [ForeignKey("RequestCancelledByUserId")]
    public User RequestCancelledByUser { get; set; }
}
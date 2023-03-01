namespace Erp.Api.ViewModel;

public class CompanyJoinRequestDetailed
{
    public string UserId { get; set; }
    public bool? RequestApproved { get; set; }
    public bool? RequestDeclined { get; set; }
    public DateTime? ApprovedRequestDateTime { get; set; }
    public DateTime? DeclinedRequestDateTime { get; set; }
    public string CompanyId { get; set; }
    public string CompanyLogoUrl { get; set; }
    public string CompanyName { get; set; }
    public string CompanyOwnerFirstName { get; set; }
    public string CompanyOwnerLastName { get; set; }
    public string UserFirstName { get; set; }
    public string UserLastName { get; set; }
    public string UserEmail { get; set; }
    public string CompanyJoinRequestId { get; set; }
}
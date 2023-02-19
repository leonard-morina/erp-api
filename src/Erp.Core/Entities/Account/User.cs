using Erp.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Erp.Core.Entities.Account;

public class User : IdentityUser<string>, IEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime InsertedDateTime { get; set; }
    public DateTime? ModifiedDateTime { get; set; }
    public bool IsActive { get; set; }
    public DateTime? InactiveDateTime { get; set; } 
}
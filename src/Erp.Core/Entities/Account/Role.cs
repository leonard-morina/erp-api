using Erp.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Erp.Core.Entities.Account;

public class Role : IdentityRole<string>, IEntity
{
    private Role()
    {
        Id = Guid.NewGuid().ToString();
    }
    
    public Role(string roleName) : this()
    {
        Name = roleName;
    }
}
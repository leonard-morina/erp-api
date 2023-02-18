using Erp.Api.Authentication;
using Erp.Core.Entities.Account;

namespace Erp.Api.ViewModel;

public class SucceededAuthentication
{
    public User User { get; set; }
    public JwtToken Token { get; set; }
}
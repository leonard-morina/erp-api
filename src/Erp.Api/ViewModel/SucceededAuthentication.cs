using Erp.Api.Authentication;
using Erp.Core.Entities.Account;

namespace Erp.Api.ViewModel;

public class SucceededAuthentication
{
    public SucceededAuthentication(User user)
    {
        Username = user.UserName;
        Email = user.Email;
        FirstName = user.FirstName;
        LastName = user.LastName;
    }

    public string Username { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public JwtToken Token { get; set; }
}
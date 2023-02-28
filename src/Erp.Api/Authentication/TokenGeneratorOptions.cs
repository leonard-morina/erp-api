using System.Security.Claims;

namespace Erp.Api.Authentication;

public class TokenGeneratorOptions
{
    public List<Claim> Claims { get; set; }
    public string Email { get; set; }
    public string UserId { get; set; }
    public string Role { get; set; }

    public TokenGeneratorOptions(string userId, string email, string role)
    {
        Claims = new List<Claim>();
        UserId = userId;
        Email = email;
        Role = role;
    }

    public TokenGeneratorOptions(string userId, string email, string role, List<Claim> claims)
    {
        Claims = claims;
        UserId = userId;
        Email = email;
        Role = role;
    }
}

namespace Erp.Api.Authentication;

public class JwtToken
{
    public string Token { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public string RefreshToken { get; set; }
}
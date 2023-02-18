namespace Erp.Api.Authentication;

public interface ITokenGenerator
{
    JwtToken GenerateToken(string userId, string role);
}
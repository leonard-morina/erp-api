namespace Erp.Api.Authentication;

public interface ITokenGenerator
{
    JwtToken GenerateToken(TokenGeneratorOptions options);
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Erp.Api.Configuration;
using Erp.Api.Constants;
using Microsoft.IdentityModel.Tokens;

namespace Erp.Api.Authentication;


public class JwtTokenGenerator : ITokenGenerator
{
    public static Dictionary<string, string> RefreshTokenDict;
    private readonly TokenConfiguration _tokenConfig;

    public JwtTokenGenerator(TokenConfiguration tokenConfig)
    {
        RefreshTokenDict = new Dictionary<string, string>();
        _tokenConfig = tokenConfig;
    }

    public JwtToken GenerateToken(TokenGeneratorOptions options)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_tokenConfig.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypeConstants.USER_ID, options.UserId),
                new Claim(ClaimTypeConstants.ROLE, options.Role),
                new Claim(ClaimTypeConstants.EMAIL, options.Email)
            }),
            Expires = DateTime.Now.AddDays((double) _tokenConfig.ExpirationInDays),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        if (options.Claims.Any())
        {
            tokenDescriptor.Subject.AddClaims(options.Claims);
        }

        var createdToken = tokenHandler.CreateToken(tokenDescriptor);
        var token = tokenHandler.WriteToken(createdToken);

        var jwtToken = new JwtToken
        {
            Token = token,
            RefreshToken = GenerateRefreshToken(),
            ExpirationDate = tokenDescriptor.Expires,
        };

        return jwtToken;
    }

    private string GenerateRefreshToken(int size = 32)
    {
        var refreshToken = new byte[size];
        using var randomNumber = RandomNumberGenerator.Create();
        randomNumber.GetBytes(refreshToken);
        return Convert.ToBase64String(refreshToken);
    }
}
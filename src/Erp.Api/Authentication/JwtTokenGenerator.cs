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

    public JwtToken GenerateToken(string userId, string role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_tokenConfig.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
                {new Claim(ClaimTypeConstants.USER_ID, userId), new Claim(ClaimTypeConstants.ROLE_ID, role)}),
            Expires = DateTime.Now.AddDays((double) _tokenConfig.ExpirationInDays),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

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
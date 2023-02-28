using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Erp.Api.Configuration;
using Erp.Api.Constants;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Erp.Api.Middlewares;

public class JwtTokenMiddleware
{
    private readonly RequestDelegate _next;
    private readonly TokenConfiguration _tokenConfig;

    public JwtTokenMiddleware(RequestDelegate next, IOptions<TokenConfiguration> tokenConfig)
    {
        _next = next;
        _tokenConfig = tokenConfig.Value;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token != null) AttachUserToContext(context, token);

        await _next(context);
    }

    private void AttachUserToContext(HttpContext context, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenConfig.Secret);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken) validatedToken;
            var user = jwtToken.Claims.First(x => x.Type == ClaimTypeConstants.USER_ID).Value;
            context.Items["Claims"] = jwtToken.Claims;
            // attach user to context on successful jwt validation
            context.Items["User"] = user;
        }
        catch
        {
            // do nothing if jwt validation fails
            // user is not attached to context so request won't have access to secure routes
        }
    }
    
}
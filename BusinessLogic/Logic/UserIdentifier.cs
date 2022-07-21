using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;

namespace Services.Logic;
public class UserIdentifier : IUserIdentifier
{
    public int GetUserIdFromToken(string accessToken, IConfiguration configuration)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var secretKey = configuration.GetSection("AppSettings:Token").Value;
        var key = Encoding.ASCII.GetBytes(secretKey);

        tokenHandler.ValidateToken(accessToken, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        }, out SecurityToken validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;
        var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
        return userId;
    }
}

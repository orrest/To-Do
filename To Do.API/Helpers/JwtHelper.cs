using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using To_Do.API.Helpers;

namespace To_Do.Helpers;

public class JwtHelper
{
    public static string BuildToken(IEnumerable<Claim> claims)
    {
        var expireDays = Environment.GetEnvironmentVariable(Constants.JWT_EXPIRE_DAYS);
        DateTime expires = DateTime.Now.AddDays(int.Parse(expireDays!));

        var signingKey = Environment.GetEnvironmentVariable(Constants.JWT_SIGNINGKEY);
        byte[] keyBytes = Encoding.UTF8.GetBytes(signingKey!);
        var secKey = new SymmetricSecurityKey(keyBytes);
        
        var credentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new JwtSecurityToken(
            expires: expires,
            signingCredentials: credentials, 
            claims: claims);

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}
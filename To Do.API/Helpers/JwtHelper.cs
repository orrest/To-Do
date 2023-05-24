using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace To_Do.Helpers;

public class JwtHelper
{
    public static string BuildToken(IEnumerable<Claim> claims, JwtOptions options)
    {
        DateTime expires = DateTime.Now.AddSeconds(options.ExpireSeconds);
        
        byte[] keyBytes = Encoding.UTF8.GetBytes(options.SigningKey);
        var secKey = new SymmetricSecurityKey(keyBytes);
        
        var credentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new JwtSecurityToken(
            expires: expires,
            signingCredentials: credentials, 
            claims: claims);

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}
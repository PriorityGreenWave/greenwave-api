using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace priority_green_wave_api.Services
{
    public class TokenService
    {
        public static string CreateToken(int id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var encryptedKey = Encoding.ASCII.GetBytes(JWTKey.key);
            var decryptedKey = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Sid, id.ToString())
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(encryptedKey),SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(decryptedKey);
            return tokenHandler.WriteToken(token);
        }
        public static string DecodeToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = tokenHandler.ReadJwtToken(token);
            var id = jsonToken.Claims.Where(c => c.Type.Contains("identity/claims/sid")).Select(c => c.Value).SingleOrDefault();
            return id;
        }
    }
}

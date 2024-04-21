using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GestionHotelApi.Services
{
    public interface IJwtService
    {
        string GenerateToken(ClaimsIdentity claimsIdentity);
        bool ValidateToken(string token);
        ClaimsPrincipal GetPrincipalFromToken(string token);
    }

    public class JwtService : IJwtService
    {
        public readonly SymmetricSecurityKey _key;

        public JwtService()
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GenerateKey()));
        }

        public string GenerateToken(ClaimsIdentity claimsIdentity)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public string GenerateKey()
        {
            int keyLength = 32;

            byte[] keyBytes = new byte[keyLength];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(keyBytes);
            }

            string secretKey = BitConverter.ToString(keyBytes).Replace("-", "");
            return secretKey;
        }
        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = _key,
                    ValidateIssuer = true,
                    ValidIssuer = "votre_issuer",
                    ValidateAudience = true,
                    ValidAudience = "votre_audience",
                    ValidateLifetime = true
                }, out _);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = _key,
                    ValidateIssuer = true,
                    ValidIssuer = "votre_issuer",
                    ValidateAudience = true,
                    ValidAudience = "votre_audience",
                    ValidateLifetime = true
                }, out _);
                return principal;
            }
            catch
            {
                return null;
            }
        }
    }
}


using Application.Commom.Helpers;
using Application.Interfaces.Security;
using Application.Pattern.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Security.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Security.Token
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly string _KeySecurity;
        private readonly TokenManagement tokenSettings;

        public JwtGenerator(IConfiguration configuration,
            IOptions<TokenManagement> tokenSettings)
        {
            _KeySecurity = configuration["KeySecurity"];
            this.tokenSettings = tokenSettings.Value;
        }

        public async Task<AuthDto> CreateToken(AuthResponse auth)
        {
            var claims = new[] {
                new Claim(JWTClaimTypes.User, auth.User),
                new Claim(JWTClaimTypes.Name, auth.Name),
                new Claim(JWTClaimTypes.LastName, auth.LastName),
                new Claim(JWTClaimTypes.MotherLastName, auth.MotherLastName)
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Secret));
            var encryptingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.EncryptionSecret));
            var now = DateTime.UtcNow;
            var expiresAt = DateTime.UtcNow.AddMinutes(tokenSettings.AccessExpiration);

            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var encryptingCredentials = new EncryptingCredentials(encryptingKey, SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.CreateJwtSecurityToken(
                tokenSettings.Issuer,
                tokenSettings.Audience,
                new ClaimsIdentity(claims),
                now,
                expiresAt,
                now,
                signingCredentials: signingCredentials,
                encryptingCredentials: encryptingCredentials
            );

            var token = await Task.Run(() => handler.WriteToken(jwtSecurityToken));
            var expirationTimestamp = DateTimeHelper.GetUnixTimeMilliseconds(expiresAt);

            return new AuthDto(token, expirationTimestamp, auth.User, auth.Name, auth.LastName, auth.MotherLastName);
        }

        public static class JWTClaimTypes
        {
            public const string User = "https://suizasoft/user";
            public const string Name = "https://suizasoft/name";
            public const string LastName = "https://suizasoft/lastname";
            public const string MotherLastName = "https://suizasoft/mother-lastname";
        }
    }
}

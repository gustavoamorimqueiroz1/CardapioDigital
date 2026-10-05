using CompreAqui.Domain.Models.Entities;
using CompreAqui.Domain.Models.Settings;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CompreAqui.Api.Tools
{
    public static class TokenGenerator
    {
        /// <summary>
        /// Gera o token JWT com os dados passados pelo parametro
        /// </summary>
        /// <param name="jwtSettings">dados para geração do token JWT</param>
        /// <returns>string contendo o token</returns>
#pragma warning disable 1998
        public static async Task<string> Gerar(JwtSetting jwtSettings)
#pragma warning restore 1998
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //Subject = identityClaims,

                Issuer = jwtSettings.Emissor,
                Audience = jwtSettings.ValidoEm,
                Expires = DateTime.UtcNow.AddHours(jwtSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }

        public static async Task<string> GenerateTokenUser(JwtSetting jwtSettings, User user)
#pragma warning restore 1998
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                   new Claim(ClaimTypes.Name, user.UserName),
                   new Claim(ClaimTypes.Email, user.Email),
                   new Claim(ClaimTypes.AuthenticationMethod, user.Guid.ToString()),
                }),

                Issuer = jwtSettings.Emissor,
                Audience = jwtSettings.ValidoEm,
                Expires = DateTime.UtcNow.AddHours(jwtSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}

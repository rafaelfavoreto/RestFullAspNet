﻿using Microsoft.IdentityModel.Tokens;
using RestFullAspNet.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RestFullAspNet.Services.Implemetations
{
    public class TokenServices : ITokenServices
    {
        private TokenConfiguration _configuration;

        public TokenServices(TokenConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateAcessToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var options = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_configuration.Minutes),
                signingCredentials: signinCredentials
                );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(options);
            return tokenString;


        }

        public string GenerateRefreshToken()
        {
            throw new NotImplementedException();
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
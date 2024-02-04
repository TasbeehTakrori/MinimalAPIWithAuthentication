﻿using Microsoft.IdentityModel.Tokens;
using MinimalAPIWithAuthentication.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MinimalAPIWithAuthentication.Authentication
{
    public class JwtTokenGenerator : ITokenGenerator
    {
        private readonly string _issuer;
        private readonly string _audience;
        private readonly byte[] _key;
        public JwtTokenGenerator(IConfiguration configuration)
        {
            if (configuration is null)
                throw new ArgumentNullException(nameof(configuration));

            _issuer = configuration["Jwt:Issuer"];
            _audience = configuration["Jwt:Audience"];
            _key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
        }

        public string GenerateToken(User user)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(JwtRegisteredClaimNames.Name, user.Name),
            }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(_key),
                SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);

            return stringToken;
        }
    }
}
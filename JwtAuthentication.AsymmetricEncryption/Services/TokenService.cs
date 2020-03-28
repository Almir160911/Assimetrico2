﻿using JwtAuthentication.AsymmetricEncryption.Certificates;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace JwtAuthentication.AsymmetricEncryption.Services
{
    public class TokenService
    {
        private readonly SigningAudienceCertificate signingAudienceCertificate;

        public TokenService()
        {
            signingAudienceCertificate = new SigningAudienceCertificate();
        }

        public string GetToken()
        {
            SecurityTokenDescriptor tokenDescriptor = GetTokenDescriptor();
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(securityToken);

            return token;
        }

        private SecurityTokenDescriptor GetTokenDescriptor()
        {
            const int expiringDays = 7;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddDays(expiringDays),
                SigningCredentials = signingAudienceCertificate.GetAudienceSigningKey()
            };

            return tokenDescriptor;
        }
    }
}
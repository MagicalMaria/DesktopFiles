using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AuthMicroService.Models;
using AuthMicroService.Repositories;
using log4net;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthMicroService.Services
{
    public class AuthService : IAuthService
    {
        private IAuthRepo _repository;
        private readonly ILog _log4net = LogManager.GetLogger(typeof(AuthService));

        public AuthService(IAuthRepo repository)
        {
            _repository = repository;
        }

        //Check for login details
        public Credentials AuthenticateUser(Credentials credentials)
        {
            _log4net.Info("AuthenticateUser Invoked");
            try
            {
                var defaultCredentials = _repository.GetCredentials();

                if (defaultCredentials == null) return null;

                if ((credentials.UserName == defaultCredentials.UserName) &&
                    (credentials.Password == defaultCredentials.Password))
                    return credentials;

                return null;
            }
            catch (Exception e)
            {
                _log4net.Error("Exception Occured : " + e.Message + " from " + nameof(AuthenticateUser));
                return null;
            }
        }

        //Generate JWT Token
        public string GenerateJWT(Credentials credentials, IConfiguration _configuration)
        {
            _log4net.Info("GenerateJWT Invoked");

            if (credentials == null) return null;

            try
            {
                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));
                SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                JwtSecurityToken token = new JwtSecurityToken(
                    _configuration["Token:Issuer"],
                    null,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: signingCredentials
                    );

                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                var securityToken = handler.WriteToken(token);

                return securityToken;
            }
            catch (Exception e)
            {
                _log4net.Error("Exception Occured : " + e.Message + " from " + nameof(GenerateJWT));
                return null;
            }
        }
    }
}

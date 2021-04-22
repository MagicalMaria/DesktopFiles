using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuditManagementPortal.Models;
using AuditManagementPortal.Repositories;
using Microsoft.Extensions.Configuration;

namespace AuditManagementPortal.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly AuthorizationRepo _authorizationRepo;
        private readonly IConfiguration _configuration;

        public AuthorizationService(IConfiguration configuration)
        {
            _configuration = configuration;
            _authorizationRepo = new AuthorizationRepo(configuration);
        }

        

        public string GetToken(Credentials credentials)
        {
            try
            {
                var token = _authorizationRepo.GetToken(credentials);

                if (token != null)
                {
                    return token;
                }

                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}

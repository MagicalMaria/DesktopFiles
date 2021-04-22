using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthMicroService.Models;
using Microsoft.Extensions.Configuration;

namespace AuthMicroService.Services
{
    public interface IAuthService
    {
        public Credentials AuthenticateUser(Credentials credentials);
        public string GenerateJWT(Credentials credentials, IConfiguration _configuration);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthMicroService.Models;

namespace AuthMicroService.Repositories
{
    public interface IAuthRepo
    {
        public Credentials GetCredentials();
    }
}

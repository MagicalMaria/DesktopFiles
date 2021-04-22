using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuditManagementPortal.Models;

namespace AuditManagementPortal.Services
{
    public interface IAuthorizationService
    {
        public string GetToken(Credentials credentials);
    }
}

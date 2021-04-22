using AuditManagementPortal.Models;

namespace AuditManagementPortal.Repositories
{
    public interface IAuthorizationRepo
    {
        public string GetToken(Credentials credentials);
    }
}
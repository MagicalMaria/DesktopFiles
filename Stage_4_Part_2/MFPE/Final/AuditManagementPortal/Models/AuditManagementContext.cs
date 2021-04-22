using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AuditManagementPortal.Models
{
    public class AuditManagementContext : DbContext
    {
        public AuditManagementContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<AuditResponse> AuditResponses { get; set; }
    }
}

using AuditBenchmarkMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditBenchmarkMicroservice.Services
{
    public interface IAuditBenchmarkService
    {
        public List<AuditBenchmark> GetBenchmarksList();
    }
}

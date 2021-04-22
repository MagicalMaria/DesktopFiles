using AuditBenchmarkMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditBenchmarkMicroservice.Repository
{
    public interface IAuditBenchmarkRepo
    {
        public List<AuditBenchmark> GetBenchmarksList();
    }
}

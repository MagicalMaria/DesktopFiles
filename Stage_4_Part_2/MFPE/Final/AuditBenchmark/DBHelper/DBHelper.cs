using AuditBenchmarkMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditBenchmarkMicroservice.DBHelper
{
    public static class DBHelper
    {
        public static List<AuditBenchmark> AuditBenchmarkList = new List<AuditBenchmark>()
        {
       
            new AuditBenchmark
            {
                AuditType="Internal",
                BenchmarkNoAnswers=3
            },
            new AuditBenchmark
            {
                AuditType="SOX",
                BenchmarkNoAnswers=1
            }
        };
    }
}

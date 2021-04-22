using AuditBenchmarkMicroservice.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditBenchmarkMicroservice.Repository
{
    public class AuditBenchmarkRepo: IAuditBenchmarkRepo
    {
        public List<AuditBenchmark> GetBenchmarksList()
        {

            List<AuditBenchmark> listOfCriteria = new List<AuditBenchmark>();
            try
            {
                listOfCriteria =AuditBenchmarkMicroservice.DBHelper.DBHelper.AuditBenchmarkList;
                return listOfCriteria;
            }
            catch (Exception e)
            {
                Console.WriteLine(" Exception here" + e.Message);
                return null;
            }

        }
    }
}
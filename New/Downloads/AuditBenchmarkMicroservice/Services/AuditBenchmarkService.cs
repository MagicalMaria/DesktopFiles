using AuditBenchmarkMicroservice.Models;
using AuditBenchmarkMicroservice.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditBenchmarkMicroservice.Services
{
    public class AuditBenchmarkService: IAuditBenchmarkService
    {
        private readonly IAuditBenchmarkRepo objBenchmarkRepo;
       
        public AuditBenchmarkService(IAuditBenchmarkRepo _objBenchmarkRepo)
        {
            
            objBenchmarkRepo = _objBenchmarkRepo;
        }

        public List<AuditBenchmark> GetBenchmarksList()
        {
           

            List<AuditBenchmark> listOfRepository = new List<AuditBenchmark>();
            try
            {
                listOfRepository = objBenchmarkRepo.GetBenchmarksList();
                return listOfRepository;
            }
            catch (Exception e)
            {
               Console.WriteLine(" Exception here" + e.Message );
                return null;
            }

        }
    }
}

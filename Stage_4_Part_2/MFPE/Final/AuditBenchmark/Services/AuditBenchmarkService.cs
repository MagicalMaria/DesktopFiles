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
        private readonly log4net.ILog _log4net;
        public AuditBenchmarkService(IAuditBenchmarkRepo _objBenchmarkRepo)
        {
            _log4net = log4net.LogManager.GetLogger(typeof(AuditBenchmarkService));
            objBenchmarkRepo = _objBenchmarkRepo;
        }

        public List<AuditBenchmark> GetBenchmarksList()
        {

            _log4net.Info(" Http GET request " + nameof(AuditBenchmarkService));
            List<AuditBenchmark> listOfRepository = new List<AuditBenchmark>();
            try
            {
                listOfRepository = objBenchmarkRepo.GetBenchmarksList();
                return listOfRepository;
            }
            catch (Exception e)
            {
                _log4net.Error(" Exception here" + e.Message + " " + nameof(AuditBenchmarkService));
                return null;
            }

        }
    }
}

using AuditBenchmarkMicroservice.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditBenchmarkMicroservice.Repository
{
    public class AuditBenchmarkRepo: IAuditBenchmarkRepo
    {
        private readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuditBenchmarkRepo));
        public List<AuditBenchmark> GetBenchmarksList()
        {
            _log4net.Info(" Http GET request " + nameof(AuditBenchmarkRepo));
            List<AuditBenchmark> listOfCriteria = new List<AuditBenchmark>();
            try
            {
                listOfCriteria =AuditBenchmarkMicroservice.DBHelper.DBHelper.AuditBenchmarkList;
                return listOfCriteria;
            }
            catch (Exception e)
            {
                _log4net.Error(" Exception here" + e.Message + " " + nameof(AuditBenchmarkRepo));
                return null;
            }

        }
    }
}
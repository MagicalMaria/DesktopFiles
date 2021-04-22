using AuditSeverityMicroservice.Models;
using AuditSeverityMicroservice.Repositories;
using log4net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AuditSeverityMicroservice.Services
{
    public class ProjectExecutionStatusService : IProjectExecutionStatusService
    {
        readonly ILog _log4net = LogManager.GetLogger(typeof(ProjectExecutionStatusService));
        public string AuditBenchmarkMicroserviceUri { get; }

        private readonly IProjectExecutionStatusRepository _repo;

        public ProjectExecutionStatusService(IConfiguration config,IProjectExecutionStatusRepository repo)
        {
            AuditBenchmarkMicroserviceUri = config["AuditBenchmarkMicroserviceUri"];
            _repo = repo;
        }

        public AuditResponse GetProjectExecutionStatusData(AuditRequest auditRequest,string token)
        {
            try
            {
                _log4net.Info(" Http POST request from " + nameof(ProjectExecutionStatusService));

                HttpClient client = new HttpClient();
                List<AuditBenchmark> auditBenchmarks = new List<AuditBenchmark>();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = client.GetAsync(AuditBenchmarkMicroserviceUri + "api/AuditBenchmark").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    auditBenchmarks = JsonConvert.DeserializeObject<List<AuditBenchmark>>(data);
                }

                int count = 0, acceptableNoCount = 0;

                if (auditRequest.AuditDetails.Questions.Question1 == false)
                {
                    count++;
                }
                if (auditRequest.AuditDetails.Questions.Question2 == false)
                {
                    count++;
                }
                if (auditRequest.AuditDetails.Questions.Question3 == false)
                {
                    count++;
                }
                if (auditRequest.AuditDetails.Questions.Question4 == false)
                {
                    count++;
                }
                if (auditRequest.AuditDetails.Questions.Question5 == false)
                {
                    count++;
                }

                if (auditRequest.AuditDetails.Type == auditBenchmarks[0].AuditType)
                {
                    acceptableNoCount = auditBenchmarks[0].BenchmarkNoAnswers;
                }
                else if (auditRequest.AuditDetails.Type == auditBenchmarks[1].AuditType)
                {
                    acceptableNoCount = auditBenchmarks[1].BenchmarkNoAnswers;
                }

                Random randomNum = new Random();

                AuditResponse auditResponse = new AuditResponse();
                auditResponse.AuditId = randomNum.Next();

                List<AuditResponse> auditResponses = _repo.AuditResponseDetails();

                if (auditRequest.AuditDetails.Type == "Internal" && count <= acceptableNoCount)
                {
                    auditResponse.ProjectExecutionStatus = auditResponses[0].ProjectExecutionStatus;
                    auditResponse.RemedialActionDuration = auditResponses[0].RemedialActionDuration;
                }
                else if (auditRequest.AuditDetails.Type == "Internal" && count > acceptableNoCount)
                {
                    auditResponse.ProjectExecutionStatus = auditResponses[1].ProjectExecutionStatus;
                    auditResponse.RemedialActionDuration = auditResponses[1].RemedialActionDuration;
                }
                else if (auditRequest.AuditDetails.Type == "SOX" && count <= acceptableNoCount)
                {
                    auditResponse.ProjectExecutionStatus = auditResponses[0].ProjectExecutionStatus;
                    auditResponse.RemedialActionDuration = auditResponses[0].RemedialActionDuration;
                }
                else if (auditRequest.AuditDetails.Type == "SOX" && count > acceptableNoCount)
                {
                    auditResponse.ProjectExecutionStatus = auditResponses[2].ProjectExecutionStatus;
                    auditResponse.RemedialActionDuration = auditResponses[2].RemedialActionDuration;
                }

                return auditResponse;
            }
            catch(Exception e)
            {
                _log4net.Error("Exception Occured " + e.Message + " from " + nameof(ProjectExecutionStatusService));
                return null;
            }
        }
    }
}

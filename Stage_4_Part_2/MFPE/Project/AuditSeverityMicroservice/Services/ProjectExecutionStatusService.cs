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

        public ProjectExecutionStatusService(/*IConfiguration config,*/IProjectExecutionStatusRepository repo)
        {
            //AuditBenchmarkMicroserviceUri = config["AuditBenchmarkMicroserviceUri"];
            _repo = repo;
        }

        private List<AuditBenchmark> GetAuditBenchmarks(string token)
        {
            try
            {
                _log4net.Info("Entered into the method named as " + nameof(GetAuditBenchmarks) + " of " + nameof(ProjectExecutionStatusService));
                HttpClient client = new HttpClient();
                List<AuditBenchmark> auditBenchmarks = new List<AuditBenchmark>();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = client.GetAsync(AuditBenchmarkMicroserviceUri + "api/AuditBenchmark").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    auditBenchmarks = JsonConvert.DeserializeObject<List<AuditBenchmark>>(data);
                }
                return auditBenchmarks;
            }
            catch(Exception e)
            {
                _log4net.Error("Exception Occured at " + nameof(ProjectExecutionStatusService) + " - " + nameof(GetAuditBenchmarks) + " : " + e.Message);
                return null;
            }
        }

        private AuditResponse GenerateAuditResponse(string auditType,int count,int acceptableNoCount)
        {
            try
            {
                _log4net.Info("Entered into the method named as " + nameof(GenerateAuditResponse) + " of " + nameof(ProjectExecutionStatusService));
                Random randomNum = new Random();

                AuditResponse auditResponse = new AuditResponse();
                auditResponse.AuditId = randomNum.Next();

                List<AuditResponse> auditResponses = _repo.AuditResponseDetails();

                if (auditType == "Internal" && count <= acceptableNoCount)
                {
                    auditResponse.ProjectExecutionStatus = auditResponses[0].ProjectExecutionStatus;
                    auditResponse.RemedialActionDuration = auditResponses[0].RemedialActionDuration;
                }
                else if (auditType == "Internal" && count > acceptableNoCount)
                {
                    auditResponse.ProjectExecutionStatus = auditResponses[1].ProjectExecutionStatus;
                    auditResponse.RemedialActionDuration = auditResponses[1].RemedialActionDuration;
                }
                else if (auditType == "SOX" && count <= acceptableNoCount)
                {
                    auditResponse.ProjectExecutionStatus = auditResponses[0].ProjectExecutionStatus;
                    auditResponse.RemedialActionDuration = auditResponses[0].RemedialActionDuration;
                }
                else if (auditType == "SOX" && count > acceptableNoCount)
                {
                    auditResponse.ProjectExecutionStatus = auditResponses[2].ProjectExecutionStatus;
                    auditResponse.RemedialActionDuration = auditResponses[2].RemedialActionDuration;
                }
                return auditResponse;
            }
            catch(Exception e)
            {
                _log4net.Error("Exception Occured at " + nameof(ProjectExecutionStatusService) + " - " + nameof(GenerateAuditResponse) + " : " + e.Message);
                return null;
            }
        }

        public AuditResponse GetProjectExecutionStatusData(AuditRequest auditRequest/*,string token*/)
        {
            try
            {
                _log4net.Info("Entered into the method named as " + nameof(GetProjectExecutionStatusData) + " of " + nameof(ProjectExecutionStatusService));

                //List<AuditBenchmark> auditBenchmarks = GetAuditBenchmarks(token);

                List<AuditBenchmark> auditBenchmarks = new List<AuditBenchmark>()
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

                if (auditBenchmarks!=null)
                {
                    int count = 0, acceptableNoCount = 0;

                    if (!(auditRequest.AuditDetails.Questions.Question1))
                    {
                        count++;
                    }
                    if (!(auditRequest.AuditDetails.Questions.Question2))
                    {
                        count++;
                    }
                    if (!(auditRequest.AuditDetails.Questions.Question3))
                    {
                        count++;
                    }
                    if (!(auditRequest.AuditDetails.Questions.Question4))
                    {
                        count++;
                    }
                    if (!(auditRequest.AuditDetails.Questions.Question5))
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

                    AuditResponse response = GenerateAuditResponse(auditRequest.AuditDetails.Type, count, acceptableNoCount);
                    return response;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                _log4net.Error("Exception Occured at " + nameof(ProjectExecutionStatusService) + " - " + nameof(GetProjectExecutionStatusData) + " : " + e.Message);
                return null;
            }
        }
    }
}

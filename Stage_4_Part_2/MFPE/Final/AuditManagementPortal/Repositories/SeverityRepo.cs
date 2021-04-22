using AuditManagementPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AuditManagementPortal.Repositories
{
    public class SeverityRepo : ISeverityRepo
    {
        private IConfiguration _configuration;
        private string _baseAddress = "http://localhost:6443/api/";    //Severity Micro Service

        public SeverityRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AuditResponse GetInternalResponse(string token, InternalQuestions questions)
        {
            try
            {
                HttpClient client = new HttpClient();
                _baseAddress += "ProjectExecutionStatus/";

                AuditResponse auditResponse = new AuditResponse();

                AuditRequest auditRequest = new AuditRequest()
                {
                    AuditDetails = new AuditDetails()
                    {
                        Type = "Internal",
                        Questions = new Questions()
                        {
                            Question1 = questions.Question1,
                            Question2 = questions.Question2,
                            Question3 = questions.Question3,
                            Question4 = questions.Question4,
                            Question5 = questions.Question5
                        }
                    }
                };

                StringContent content = new StringContent(JsonConvert.SerializeObject(auditRequest), Encoding.UTF8,
                    "application/json");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.PostAsync(_baseAddress, content).Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var rawData = response.Content.ReadAsStringAsync().Result;
                    auditResponse = JsonConvert.DeserializeObject<AuditResponse>(rawData);
                }

                return auditResponse;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public AuditResponse GetSOXResponse(string token, SOXQuestions questions)
        {
            try
            {
                HttpClient client = new HttpClient();
                _baseAddress += "ProjectExecutionStatus/";

                AuditResponse auditResponse = new AuditResponse();

                AuditRequest auditRequest = new AuditRequest()
                {
                    AuditDetails = new AuditDetails()
                    {
                        Type = "SOX",
                        Questions = new Questions()
                        {
                            Question1 = questions.Question1,
                            Question2 = questions.Question2,
                            Question3 = questions.Question3,
                            Question4 = questions.Question4,
                            Question5 = questions.Question5
                        }
                    }
                };

                StringContent content = new StringContent(JsonConvert.SerializeObject(auditRequest), Encoding.UTF8,
                    "application/json");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.PostAsync(_baseAddress, content).Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var rawData = response.Content.ReadAsStringAsync().Result;
                    auditResponse = JsonConvert.DeserializeObject<AuditResponse>(rawData);
                }

                return auditResponse;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}

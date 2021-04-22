using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AuditManagementPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AuditManagementPortal.Repositories
{
    public class CheckListRepo : ICheckListRepo
    {
        private IConfiguration _configuration;
        private string _baseAddress = "http://localhost:25996/api/";    //GetCheckList Microservice

        public CheckListRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<CheckListQuestion> GetQuestions(string auditType, string token)
        {
            try
            {
                List<CheckListQuestion> questionsList = new List<CheckListQuestion>();
                HttpClient client = new HttpClient();
                _baseAddress += "AuditChecklist/" + auditType;

                //Adding the token in the header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.GetAsync(_baseAddress).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var rawData = response.Content.ReadAsStringAsync().Result;
                    //Convert the raw data into List
                    questionsList = JsonConvert.DeserializeObject<List<CheckListQuestion>>(rawData);    
                }

                return questionsList;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}

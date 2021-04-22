using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AuditManagementPortal.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AuditManagementPortal.Repositories
{
    public class AuthorizationRepo : IAuthorizationRepo
    {
        private readonly IConfiguration _configuration;
        private string _baseAddress = "http://localhost:32551/api/";    //Authentication Service

        public AuthorizationRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetToken(Credentials credentials)
        {
            try
            {
                HttpClient client = new HttpClient();
                _baseAddress += "auth/";

                StringContent content = new StringContent(JsonConvert.SerializeObject(credentials), Encoding.UTF8, "application/json");

                var response = client.PostAsync(_baseAddress, content).Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var token = response.Content.ReadAsStringAsync().Result;    //Extract Token from the Response
                    return token;
                }

                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}

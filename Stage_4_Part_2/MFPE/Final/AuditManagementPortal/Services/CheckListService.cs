using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuditManagementPortal.Models;
using AuditManagementPortal.Repositories;
using Microsoft.Extensions.Configuration;

namespace AuditManagementPortal.Services
{
    public class CheckListService : ICheckListService
    {
        private IConfiguration _configuration;
        private CheckListRepo _checkListRepo;

        public CheckListService(IConfiguration configuration)
        {
            _configuration = configuration;
            _checkListRepo = new CheckListRepo(_configuration);
        }

        public List<CheckListQuestion> GetQuestions(string auditType, string token)
        {
            try
            {
                var questionsList = new List<CheckListQuestion>();

                questionsList = _checkListRepo.GetQuestions(auditType, token);  //Get the Questions

                return questionsList;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}

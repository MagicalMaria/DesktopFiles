using AuditSeverityMicroservice.Controllers;
using AuditSeverityMicroservice.Models;
using AuditSeverityMicroservice.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSeverityMicroserviceTesting
{
    public class AuditSeverityControllerTest
    {
        [TestCase("Internal")]
        [TestCase("SOX")]
        public void OkResult_ProjectExecutionStatusController(string type)
        {
            Mock<IProjectExecutionStatusService> mock = new Mock<IProjectExecutionStatusService>();
            AuditResponse res = new AuditResponse();
            AuditRequest req = new AuditRequest
            {
                AuditDetails = new AuditDetail
                {
                    Type = type,
                    Questions = new Questions
                    {
                        Question1 = true,
                        Question2 = false,
                        Question3 = false,
                        Question4 = false,
                        Question5 = false
                    }
                }
            };
            mock.Setup(p => p.GetProjectExecutionStatusData(req)).Returns(res);
            ProjectExecutionStatusController controller = new ProjectExecutionStatusController(mock.Object);
            OkObjectResult result = controller.GetAuditResponse(req) as OkObjectResult;

            Assert.AreEqual(200, result.StatusCode);
        }

        [TestCase("Internal2")]
        [TestCase(null)]
        public void BadRequest_ProjectExecutionStatusController(string type)
        {
            Mock<IProjectExecutionStatusService> mock = new Mock<IProjectExecutionStatusService>();
            AuditResponse res = new AuditResponse();
            AuditRequest req = new AuditRequest
            {
                AuditDetails = new AuditDetail
                {
                    Type = type,
                    Questions = new Questions
                    {
                        Question1 = true,
                        Question2 = false,
                        Question3 = false,
                        Question4 = false,
                        Question5 = false
                    }
                }
            };
            mock.Setup(p => p.GetProjectExecutionStatusData(req)).Returns(res);
            ProjectExecutionStatusController controller = new ProjectExecutionStatusController(mock.Object);
            BadRequestObjectResult result = controller.GetAuditResponse(req) as BadRequestObjectResult;

            Assert.AreEqual(400, result.StatusCode);
        }
    }
}

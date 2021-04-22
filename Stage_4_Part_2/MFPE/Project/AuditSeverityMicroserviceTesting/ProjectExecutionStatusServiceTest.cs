using AuditSeverityMicroservice.Controllers;
using AuditSeverityMicroservice.Models;
using AuditSeverityMicroservice.Repositories;
using AuditSeverityMicroservice.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AuditSeverityMicroserviceTesting
{
    public class ProjectExecutionStatusServiceTest
    {
        List<AuditResponse> AuditResponseDetails = new List<AuditResponse>();
        [SetUp]
        public void Setup()
        {
            AuditResponseDetails = new List<AuditResponse>()
            {
                new AuditResponse
                {
                    ProjectExecutionStatus="GREEN",
                    RemedialActionDuration="No Action Needed"
                },
                new AuditResponse
                {
                    ProjectExecutionStatus="RED",
                    RemedialActionDuration="Action to be taken in 2 weeks"
                },
                new AuditResponse
                {
                    ProjectExecutionStatus="RED",
                    RemedialActionDuration="Action to be taken in 1 week"
                }
            };
        }

        [Test]
        public void Internal_ServiceResponseTest()
        {
            Mock<IProjectExecutionStatusRepository> mock = new Mock<IProjectExecutionStatusRepository>();
            mock.Setup(p => p.AuditResponseDetails()).Returns(AuditResponseDetails);
            ProjectExecutionStatusService service = new ProjectExecutionStatusService(mock.Object);
            AuditRequest req = new AuditRequest
            {
                AuditDetails = new AuditDetail
                {
                    Type = "Internal",
                    Questions = new Questions
                    {
                        Question1 = true,
                        Question2 = false,
                        Question3 = false,
                        Question4 = false,
                        Question5 = true
                    }
                }
            };

            AuditResponse result = service.GetProjectExecutionStatusData(req);

            Assert.AreEqual("GREEN", result.ProjectExecutionStatus);
        }

        [Test]
        public void SOX_ServiceResponseTest()
        {
            Mock<IProjectExecutionStatusRepository> mock = new Mock<IProjectExecutionStatusRepository>();
            mock.Setup(p => p.AuditResponseDetails()).Returns(AuditResponseDetails);
            ProjectExecutionStatusService service = new ProjectExecutionStatusService(mock.Object);
            AuditRequest req = new AuditRequest
            {
                AuditDetails = new AuditDetail
                {
                    Type = "Internal",
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

            AuditResponse result = service.GetProjectExecutionStatusData(req);

            Assert.AreEqual("RED", result.ProjectExecutionStatus);
        }

        //[TestCase("Internal")]
        //[TestCase("SOX")]
        //public void OkResult_ProjectExecutionStatusController(string type)
        //{
        //    Mock<IProjectExecutionStatusService> mock = new Mock<IProjectExecutionStatusService>();
        //    AuditResponse res = new AuditResponse();
        //    AuditRequest req = new AuditRequest
        //    {
        //        AuditDetails = new AuditDetail
        //        {
        //            Type = type,
        //            Questions = new Questions
        //            {
        //                Question1 = true,
        //                Question2 = false,
        //                Question3 = false,
        //                Question4 = false,
        //                Question5 = false
        //            }
        //        }
        //    };
        //    mock.Setup(p => p.GetProjectExecutionStatusData(req)).Returns(res);
        //    ProjectExecutionStatusController controller = new ProjectExecutionStatusController(mock.Object);
        //    OkObjectResult result = controller.GetAuditResponse(req) as OkObjectResult;

        //    Assert.AreEqual(200, result.StatusCode);
        //}

        //[TestCase("Internal2")]
        //[TestCase(null)]
        //public void BadRequest_ProjectExecutionStatusController(string type)
        //{
        //    Mock<IProjectExecutionStatusService> mock = new Mock<IProjectExecutionStatusService>();
        //    AuditResponse res = new AuditResponse();
        //    AuditRequest req = new AuditRequest
        //    {
        //        AuditDetails = new AuditDetail
        //        {
        //            Type = type,
        //            Questions = new Questions
        //            {
        //                Question1 = true,
        //                Question2 = false,
        //                Question3 = false,
        //                Question4 = false,
        //                Question5 = false
        //            }
        //        }
        //    };
        //    mock.Setup(p => p.GetProjectExecutionStatusData(req)).Returns(res);
        //    ProjectExecutionStatusController controller = new ProjectExecutionStatusController(mock.Object);
        //    BadRequestObjectResult result = controller.GetAuditResponse(req) as BadRequestObjectResult;

        //    Assert.AreEqual(400, result.StatusCode);
        //}
    }
}
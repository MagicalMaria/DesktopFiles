using AuditSeverityMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditSeverityMicroservice.DBHelpers
{
    public static class ProjectExecutionStatusDBHelper
    {
        public readonly static List<AuditResponse> AuditResponseDetails = new List<AuditResponse>()
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
}

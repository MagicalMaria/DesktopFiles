using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditManagementPortal.Models
{
    public class ResponseList
    {
        public static List<AuditResponse> responseList = new List<AuditResponse>();
        public static void Add(AuditResponse auditResponse)
        {
            responseList.Add(auditResponse);

        }
        public static List<AuditResponse> GetList()
        {
            return responseList;
        }
    }
}

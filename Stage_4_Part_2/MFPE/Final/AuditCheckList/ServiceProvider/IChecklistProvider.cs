using AuditCheckList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditCheckList.ServiceProvider
{
    public interface IChecklistProvider
    {
        public List<Questions> QuestionsProvider(string type);
    }
}

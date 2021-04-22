using AuditCheckList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditCheckList.Repository
{
    public class ChecklistRepo : IChecklistRepo
    {
        DbHelper db = new DbHelper();

        readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ChecklistRepo));
        public List<Questions> GetQuestions(string auditType)
        {
           
            try
            {
                _log4net.Info("Log from " + nameof(ChecklistRepo));
                List<Questions> listOfQuestions = new List<Questions>();

                if (auditType == "Internal")
                    listOfQuestions = db.Internallist();
                else
                    listOfQuestions = db.SOXList();

                return listOfQuestions;
            }
            catch (Exception e)
            {
                _log4net.Error("Exception " + e.Message + nameof(ChecklistRepo));
                return null;

            }

        }
    }
}

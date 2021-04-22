using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleShowAPI.Models
{
    public class ScheduleShow
    {
        [Key]
        public int ShowId { get; set; }
        public int MovieID { get; set; }
        public string ScreenName { get; set; }

        public string Time { get; set; }
    }
}

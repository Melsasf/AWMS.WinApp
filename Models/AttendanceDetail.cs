using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPCAA.WinApp.Models
{
    public class AttendanceDetail
    {
        public int AttendanceDetailId { get; set; }
        public DateTimeOffset TimeInDate { get; set; }
        public DateTimeOffset? TimeOutDate { get; set; }
    }
}

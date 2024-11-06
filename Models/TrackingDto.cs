using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPCAA.WinApp.Models
{
    public class TrackingDto
    {
        public int TrackingId { get; set; }


        public int AttendanceDetailId { get; set; }
        public AttendanceDetail AttendanceDetail { get; set; }


        public int AppUserId { get; set; }
    }
}

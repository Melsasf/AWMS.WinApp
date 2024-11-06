using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPCAA.WinApp.Models
{
    public class Tracking
    {
        public int TrackingId { get; set; }

        public int AttendanceDetailId { get; set; }

        public DateTimeOffset TimeInDate { get; set; }
        public DateTimeOffset? TimeOutDate { get; set; }

        public int AppUserId { get; set; }
    }
}

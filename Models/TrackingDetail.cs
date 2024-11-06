using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPCAA.WinApp.Models
{
    public class TrackingDetail
    {
        public int TrackingDetailId { get; set; }
        public int KeyboardClickCount { get; set; }
        public int MouseClickCount { get; set; }
        public string ProcessName { get; set; }
        public DateTimeOffset StartTrackDateTime { get; set; }
        public DateTimeOffset? EndTrackDateTime { get; set; }

        public int TrackingId { get; set; }
        public int ProcessTypeId { get; set; }
    }
}

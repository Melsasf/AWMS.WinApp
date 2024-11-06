using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPCAA.WinApp.Models
{
    public class ComputedDataForGraph
    {
        public int KeyboardClickSum { get; set; }
        public double KeyboardClickAverage { get; set; }

        public int MouseClickSum { get; set; }
        public double MouseClickAverage { get; set; }
        public int LifestyleProcessCount { get; set; }
        public int SocialMediaProcessCount { get; set; }
        public int EntertainmentProcessCount { get; set; }
        public int ProductivityProcessCount { get; set; }
        public int OtherProcessCount { get; set; }
        public DateTimeOffset DateFrom { get; set; }
        public DateTimeOffset DateTo { get; set; }

        public string ProcessName { get; set; }
    }
}

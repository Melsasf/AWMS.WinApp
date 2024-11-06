using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPCAA.WinApp.Enums;

namespace WPCAA.WinApp.Models
{
    public class GetGraphDataByUserAndDateRange
    {
        // int userId, string dateFromString, string dateToString, TimeSpan dateOffset, GraphFrequency graphFrequency, bool isForClicks
        public int UserId { get; set; }
        public DateTimeOffset DateFrom { get; set; }
        public DateTimeOffset DateTo { get; set; }
        public bool IsForClicks { get; set; }
        public GraphFrequency GraphFrequency { get; set; }
    }
}

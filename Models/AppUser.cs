using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPCAA.WinApp.Models
{
    public class AppUser
    {
        public int AppUserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string UserType { get; set; }
        public string Message { get; set; }
        public bool IsLoginSuccess { get; set; }
    }
}

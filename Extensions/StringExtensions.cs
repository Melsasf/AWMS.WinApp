using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPCAA.WinApp.Extensions
{
    public static class  StringExtensions
    {
        public static string ToCamelCase(this string str) =>
         string.IsNullOrEmpty(str) || str.Length < 2
         ? str.ToUpperInvariant()
         : char.ToUpperInvariant(str[0]) + str.Substring(1);
    }
}

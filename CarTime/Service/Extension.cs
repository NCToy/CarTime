using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTime.Service
{
    public static class Extension
    {
        public static string CutController(this string str)
        {
            return str.Replace("Controller", "");
        }
    }
}

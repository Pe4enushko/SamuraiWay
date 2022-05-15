using SamuraiDbWork.GlobalDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiWay.Services
{
    public static class CheckThings
    {
        public static bool CheckUpdates()
        {
            return Assembly.GetExecutingAssembly().GetName().Version != new Checks().CheckVersion();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiDbWork.GlobalDB
{
    public class Checks : BasicCommands
    {
        public Version CheckVersion()
        {
            var DT = GetData("SELECT * FROM Version;");
            Version ver = new Version((int)DT.Rows[0].ItemArray[0],
                (int)DT.Rows[0].ItemArray[1],
                (int)DT.Rows[0].ItemArray[2],
                (int)DT.Rows[0].ItemArray[3]);
            return ver;
        }

    }
}

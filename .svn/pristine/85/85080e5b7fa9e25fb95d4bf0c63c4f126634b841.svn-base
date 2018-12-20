using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Sunpn_BonusWeb.BonusHelper
{
    public class Log
    {
        private static object islock = new object();

        public void AppenLog(string logText)
        {
            lock (islock)
            {
                StreamWriter txt = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\DATA_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + ".log", true);
                txt.WriteLine(DateTime.Now.ToString() + " " + logText);
                txt.Close();
                txt.Dispose();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Sunpn_BonusWeb.BonusHelper
{
    public class Base64MIMA
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <returns></returns>
        public static string JIA(string name)
        {
            name = Convert.ToBase64String(Encoding.Default.GetBytes(name));
            return name;

        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string JIE(string name)
        {
            name = Encoding.Default.GetString(Convert.FromBase64String(name));
            return name;
        }
    }
}
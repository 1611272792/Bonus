using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sunpn_BonusWeb.Models
{
    public class AppConfig
    {
        //userid
        public static Dictionary<string, string> dicUserID = new Dictionary<string, string>();

        //第三方
        public static Dictionary<string, UserInfo> dicUserID2 = new Dictionary<string, UserInfo>();
        //src
        public static Dictionary<string, string> imgsrc = new Dictionary<string, string>();
    }
}
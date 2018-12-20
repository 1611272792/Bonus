using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunpn_BonusWeixin
{
    public class WechatReturn
    {
        public string errcode { get; set; }
        public string errmsg { get; set; }
        public List<WechatUser> userlist { get; set; }
    }
    public class WechatUser
    {
        public string userid { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
    }
}

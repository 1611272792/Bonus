using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sunpn_BonusWeb.BonusHelper
{
    struct weixinStrct
    {
        public string ShortCode;//临时授权码
        public string corpId;//别人公司的企业id
        public string SuiteTicket;//通过这个得到suite_access_token
    }
}
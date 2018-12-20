using Sunpn.XML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sunpn_BonusWeb.BonusHelper
{
    public class AppConfig
    {
        public static XMLHelper xml = new XMLHelper();
        public static string ConnString
        {
            get { return xml.ReadSunpnNodeValue("ConnString", "Data Source=192.168.15.139;Initial Catalog=Bonus;User ID=sa;Password=sa"); }
            set { xml.UpdateSunpnAttribute("ConnString", value); }
        }

        public static string Access_Token
        {
            get { return xml.ReadSunpnNodeValue("Access_Token", ""); }
            set { xml.UpdateSunpnAttribute("Access_Token", value); }
        }

        public static string Access_YouXRQ
        {
            get
            {
                string str = xml.ReadSunpnNodeValue("Access_YouXRQ", "");
                if (string.IsNullOrWhiteSpace(str))
                    return "1990-01-01 00:00:00";
                else
                {
                    DateTime dt;
                    DateTime.TryParse(str, out dt);
                    return dt.ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            set { xml.UpdateSunpnAttribute("Access_YouXRQ", value); }
        }

        public static string sCorpID
        {
            get { return xml.ReadSunpnNodeValue("CorpID", "wx512ad5972960e003"); }
            set { xml.UpdateSunpnAttribute("CorpID", value); }
        }

        public static string Corpsecret
        {
            get { return xml.ReadSunpnNodeValue("Corpsecret", "qGgXZPWt7RyglUrd9iaGZkl7NhahuMSqivVGWW890Jo"); }
            set { xml.UpdateSunpnAttribute("Corpsecret", value); }
        }

        public static string JSAPI_Ticket
        {
            get { return xml.ReadSunpnNodeValue("JSAPI_Ticket", ""); }
            set { xml.UpdateSunpnAttribute("JSAPI_Ticket", value); }
        }
        public static DateTime Ticket_YouXRQ
        {
            get
            {
                string str = xml.ReadSunpnNodeValue("Ticket_YouXRQ", "");
                if (string.IsNullOrWhiteSpace(str))
                    return new DateTime(1990, 1, 1);
                else
                {
                    DateTime dt;
                    DateTime.TryParse(str, out dt);
                    return dt;
                }
            }
            set { xml.UpdateSunpnAttribute("Ticket_YouXRQ", value.ToString("yyyy-MM-dd HH:mm:ss")); }
        }

        public static string ServerUrl
        {
            get { return xml.ReadSunpnNodeValue("ServerUrl", "ganwuhua009.iok.la:33735"); }
            set { xml.UpdateSunpnAttribute("ServerUrl", value); }
        }

        /// <summary>
        /// //企业接收消息token
        /// </summary>
        public static string sToken
        {
            get { return xml.ReadSunpnNodeValue("sToken", "pvUcoJyNqQiYkuhgWP2NWsEyqskS"); }
            set { xml.UpdateSunpnAttribute("sToken", value); }
        }
        /// <summary>
        /// 回调配置密码
        /// </summary>
        public static string sEncodingAESKey2
        {
            get { return xml.ReadSunpnNodeValue("sEncodingAESKey2", "BvTFlNwUs7111PQDUCalde98Az6VNip2ztGFhPO6amK"); }//接收微信消息的密码，需要与微信后台设置一致
            set { xml.UpdateSunpnAttribute("sEncodingAESKey2", value); }
        }
        ///推送suite_ticket,10分钟刷新一次
        public static string SuiteTicket
        {
            get { return xml.ReadSunpnNodeValue("SuiteTicket", ""); }
            set { xml.UpdateSunpnAttribute("SuiteTicket", value); }
        }
        /// <summary>
        /// 第三方应用id
        /// </summary>
        public static string SuiteId
        {
            get { return xml.ReadSunpnNodeValue("SuiteId", ""); }
            set { xml.UpdateSunpnAttribute("SuiteId", value); }
        }
        /// <summary>
        /// 第三方应用凭证
        /// </summary>
        public static string suite_access_token
        {
            get { return xml.ReadSunpnNodeValue("suite_access_token", ""); }
            set { xml.UpdateSunpnAttribute("suite_access_token", value); }
        }
        /// <summary>
        /// 第三方应用凭证缓存时间
        /// </summary>
        public static string suiteAccess_YouXRQ
        {
            get
            {
                string str = xml.ReadSunpnNodeValue("suiteAccess_YouXRQ", "");
                if (string.IsNullOrWhiteSpace(str))
                    return "1990-01-01 00:00:00";
                else
                {
                    DateTime dt;
                    DateTime.TryParse(str, out dt);
                    return dt.ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            set { xml.UpdateSunpnAttribute("suiteAccess_YouXRQ", value); }
        }
        /// <summary>
        /// 企业token
        /// </summary>
        public static string Access_Token_Qiye
        {
            get { return xml.ReadSunpnNodeValue("Access_Token_Qiye", ""); }
            set { xml.UpdateSunpnAttribute("Access_Token_Qiye", value); }
        }

        /// <summary>
        /// 企业token过期时间
        /// </summary>
        public static string Qiye_YouXRQ
        {
            get
            {
                string str = xml.ReadSunpnNodeValue("Qiye_YouXRQ", "");
                if (string.IsNullOrWhiteSpace(str))
                    return "1990-01-01 00:00:00";
                else
                {
                    DateTime dt;
                    DateTime.TryParse(str, out dt);
                    return dt.ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            set { xml.UpdateSunpnAttribute("Qiye_YouXRQ", value); }
        }
    }
}
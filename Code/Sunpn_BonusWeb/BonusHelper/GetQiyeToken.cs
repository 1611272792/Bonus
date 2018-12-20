using Sunpn_BonusWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;

namespace Sunpn_BonusWeb.BonusHelper
{
    public class GetQiyeToken
    {
        private static Sunpn.Data.SqlServer.SQLConnection sql = new Sunpn.Data.SqlServer.SQLConnection(BonusHelper.AppConfig.ConnString);
        /// <summary>
        /// 得到企业token
        /// </summary>
        /// <param name="compid">加密后的公司id</param>
        /// <returns></returns>
        public static string GetQiyeAttoken(string compid)
        {
            compid = Base64MIMA.JIE(compid);
            string sqlstr = string.Format($"select * from Company where CompanyID='{compid}'");
            DataTable dt = sql.GetDataTableCommand(sqlstr);
            string accecctoken = AccessTokenHelper.GetQiye(dt.Rows[0]["Longcode"].ToString(), compid, dt.Rows[0]["attoken"].ToString(), DateTime.Parse(dt.Rows[0]["expressYxq"].ToString()));
            QiYeaccess_token obj = new QiYeaccess_token();
            if (accecctoken.Contains("access_token"))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(QiYeaccess_token));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(accecctoken));
                obj = (QiYeaccess_token)ser.ReadObject(ms);
                string sqlstr2 = string.Format($"exec proc_Weixin 'UpdateAtoken','','','','','{compid}','','{obj.access_token}','{DateTime.Now.AddSeconds(int.Parse(obj.expires_in)).ToString("yyyy-MM-dd HH:mm:ss")}'");
                string info2 = sql.EditDataCommand(sqlstr2);
                accecctoken = obj.access_token;
            }
            return accecctoken;
        }
    }
}
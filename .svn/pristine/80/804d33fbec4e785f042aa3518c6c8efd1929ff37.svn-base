using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Sunpn_BonusWeb.BonusHelper
{
    public class ViewRule
    {
        private static Sunpn.Data.SqlServer.SQLConnection sql = new Sunpn.Data.SqlServer.SQLConnection(BonusHelper.AppConfig.ConnString);

        //判断登录人是否为奖金项负责人
        public static int GetRule(string userid)
        {
            string Rulesql = string.Format("SELECT BIName FROM dbo.BonusItem WHERE BIPrincipal='{0}'", userid);
            DataTable dt = sql.GetDataTableCommand(Rulesql);
            if (dt?.Rows.Count > 0)
            {
                return dt.Rows.Count;
            }
            else
            {
                return 0;
            }
        }
    }
}
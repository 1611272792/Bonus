using Sunpn_BonusWeb.BonusHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sunpn_BonusWeb.Controllers
{
    public class UserDepartBonusController : Controller
    {
        private Sunpn.Data.SqlServer.SQLConnection sql = null;

        public UserDepartBonusController()
        {
            sql = new Sunpn.Data.SqlServer.SQLConnection(BonusHelper.AppConfig.ConnString);
        }
        // GET: UserDepartBonus
        public ActionResult Index(string userID)
        {
            try
            {
                //解密
                userID = Base64MIMA.JIE(userID);
               
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }

            string sqlDepart = string.Format(@"                       
                      SELECT BonusDataID,BIName,EarMoney,Name,EarDate  
 FROM dbo.BonusData2 b INNER JOIN dbo.Depart  d ON b.EarMan=d.DepartID  INNER JOIN dbo.BonusItem b2 ON b.BonusItemID=b2.BonusItemID    INNER JOIN dbo.Employee e ON b.DisMan=e.EmpID
 WHERE BonusType=0 AND DepartPrincipal='{0}' ORDER BY EarDate  DESC
                       ", userID);
            DataTable DepartName = sql.GetDataTableCommand(sqlDepart);
            if (DepartName.Rows.Count > 0)
            {
                ViewBag.Detail = DepartName;
            }
            else
            {
                ViewBag.Detail = null;
            }
            return View();
        }
    }
}
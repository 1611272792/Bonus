using Sunpn_BonusWeb.BonusHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sunpn_BonusWeb.Controllers
{
    public class DepositAuditController : Controller
    {
        private Sunpn.Data.SqlServer.SQLConnection sql = null;
        public DepositAuditController()
        {
            sql = new Sunpn.Data.SqlServer.SQLConnection(BonusHelper.AppConfig.ConnString);
        }
        // GET: DepositAudit
        public ActionResult Index(string userID,string CompanyId)
        {
            try
            {
                //加密
                ViewBag.CompanyId = CompanyId;
                //解密 
                CompanyId = Base64MIMA.JIE(CompanyId);
                userID = Base64MIMA.JIE(userID);

                ViewBag.UserID = userID;
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
            try
            {
                string EmpSqls = string.Format(@"SELECT  AuditBonusID ,
        e.Name ,
        GetBonusID ,
        GetMoney ,
        GetTime  FROM  dbo.AuditBonus a INNER JOIN dbo.Employee e ON a.GetUserID=e.EmpID WHERE a.IsDepOrEmp=1 AND IsState=0 AND a.CompanyID='{0}'
UNION ALL 
SELECT AuditBonusID ,
        d.DepartName ,
        GetBonusID ,
        GetMoney ,
        GetTime FROM dbo.AuditBonus a2 INNER JOIN dbo.Depart d ON a2.GetUserID=DepartID WHERE a2.IsDepOrEmp=0 AND IsState=0
        AND a2.CompanyID='{0}'
        ORDER BY GetTime DESC", CompanyId,CompanyId);
               
                DataTable Empds = sql.GetDataTableCommand(EmpSqls);
                
                if (Empds.Rows.Count > 0)
                {
                    ViewBag.Emp = Empds;
                }
                else
                {
                    ViewBag.Emp = null;
                }
                //查看所有记录
                string selectAll = string.Format(@"SELECT  AuditBonusID ,
        e.Name ,
        GetBonusID ,
        GetMoney ,
        GetTime,
        CASE  IsState WHEN  0 THEN '审核中' WHEN 1 THEN '已批准' ELSE  '驳回' END isOrNO  FROM  dbo.AuditBonus a INNER JOIN dbo.Employee e ON a.GetUserID=e.EmpID  and e.CompanyID='{0}' WHERE a.IsDepOrEmp=1 AND a.CompanyID='{0}'
UNION ALL 
SELECT AuditBonusID ,
        d.DepartName ,
        GetBonusID ,
        GetMoney ,
        GetTime,
         CASE  IsState WHEN  0 THEN '审核中' WHEN 1 THEN '已批准' ELSE  '驳回' END isOrNO FROM dbo.AuditBonus a2 INNER JOIN dbo.Depart d ON a2.GetUserID=DepartID WHERE a2.IsDepOrEmp=0 
        AND a2.CompanyID='{0}' and d.CompanyID='{0}'
        ORDER BY GetTime DESC", CompanyId,CompanyId);
                DataTable selectall = sql.GetDataTableCommand(selectAll);
                if (selectall.Rows.Count>0)
                {

                    //ViewBag.Dep = selectall;
                    ViewBag.Dep = selectall;
                }
                else
                {
                    ViewBag.Dep = null;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return View();
        }

        public ActionResult DepositBonusData(string empBounsID) {
            string sqls = string.Format(@"SELECT e.Name,DisDate,EarMoney,ResonContent FROM dbo.BonusData2 b 
INNER JOIN dbo.Employee e ON b.DisMan = e.EmpID
LEFT JOIN dbo.ResonDetial r ON b.ResonID = r.ResonID
 WHERE BonusDataID IN({0})",empBounsID); 
            DataTable dt = sql.GetDataTableCommand(sqls);
            if (dt.Rows.Count>0)
            {
                ViewBag.DeBonData = dt;
            }
            else
            {
                ViewBag.DeBonData = null;
            }
            return View();
        }
        //批准
        public ActionResult Ratify(string EmpID,string userID) {
         string [] tijiaoNum = EmpID.Split(',');
            string sqls = "";
            string bd2ID = "";//bd2中的id
            string errorNum = "";

            try
            {
            for (int i = 0; i < tijiaoNum.Length; i++)
            {
                sqls = string.Format("SELECT GetBonusID FROM dbo.AuditBonus WHERE AuditBonusID='{0}'", tijiaoNum[i]);
                bd2ID = sql.GetDataTableCommand(sqls).Rows[0][0].ToString();  //查出bd2中的id

                errorNum = sql.EditDataCommand("EXEC dbo.Ratify " + tijiaoNum[i] + ",'" + bd2ID + "',"+ userID);
                if (errorNum != "0")
                {
                    return Content("失败");
                }
           
                }
                return Content("已批准");
            }
            catch (Exception)
            {
                return Content("失败");
            }

        }
        //驳回
        public ActionResult Reject(string EmpID,string userID) {
            string[] tijiaoNum = EmpID.Split(',');
            string sqls = "";
            string bd2ID = "";//bd2中的id
            string errorNum = "";

            try
            {
                for (int i = 0; i < tijiaoNum.Length; i++)
                {
                    sqls = string.Format("SELECT GetBonusID FROM dbo.AuditBonus WHERE AuditBonusID='{0}'", tijiaoNum[i]);
                    bd2ID = sql.GetDataTableCommand(sqls).Rows[0][0].ToString();  //查出bd2中的id

                    errorNum = sql.EditDataCommand("EXEC dbo.Reject " + tijiaoNum[i] + ",'" + bd2ID + "',"+ userID);
                    if (errorNum != "0")
                    {
                        return Content("失败");
                    }

                }
                return Content("已驳回");
            }
            catch (Exception)
            {
                return Content("失败");
            }
        }
    }
}
using Newtonsoft.Json;
using Sunpn_BonusWeb.BonusHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Sunpn_BonusWeb.Controllers
{
    public class EmployeeBounsController : Controller
    {
        private Sunpn.Data.SqlServer.SQLConnection sql = null;

        public EmployeeBounsController()
        {
            sql = new Sunpn.Data.SqlServer.SQLConnection(BonusHelper.AppConfig.ConnString);
        }
        // GET: EmployeeBouns
        public ActionResult Index(string userID,string CompanyID)
        {
            try
            {
                //加密
                ViewBag.empBounsID = userID;
                ViewBag.CompanyID=CompanyID;
                //解密 
                userID = Base64MIMA.JIE(userID);
               CompanyID= Base64MIMA.JIE(CompanyID);
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
            //            string sqlDepart = string.Format(@"
            //select BonusDataID,Name,DisDate,EarMoney,('收入') bty from BonusData2 b INNER join Employee e on b.DisMan=e.EmpID 
            //where   BonusType!=0  and EarMan='{0}'  UNION ALL 
            //SELECT BonusDataID,Name,DisDate,(0-EarMoney) EarMoney,('支出') btyfrom FROM BonusData2 b INNER join Employee e on b.EarMan=e.EmpID 
            //where  BonusType=2 and DisMan='{1}' 
            // UNION ALL SELECT '' BonusDataID,'',GetTime DisDate,(0-GetMoney) EarMoney,('提现') bty from dbo.AuditBonus b INNER join Employee e on b.GetUserID=e.EmpID 
            //where  GetUserID='{2}' AND IsState!=2   ORDER BY DisDate DESC", userID, userID, userID);
            string sqlDepart = string.Format(@"
select BonusDataID,Name,DisDate,EarMoney,('收入') bty from BonusData2 b INNER join Employee e on b.DisMan=e.EmpID 
where   BonusType!=0  and EarMan='{0}' and b.CompanyID='{3}' and e.CompanyID='{4}'  UNION ALL 
SELECT BonusDataID,Name,DisDate,(0-EarMoney) EarMoney,('支出') btyfrom FROM BonusData2 b INNER join Employee e on b.EarMan=e.EmpID 
where  BonusType=2 and DisMan='{1}' and b.CompanyId='{6}'  
 UNION ALL SELECT '' BonusDataID,'',GetTime DisDate,(0-GetMoney) EarMoney,('提现') bty from dbo.AuditBonus b INNER join Employee e on b.GetUserID=e.EmpID 
where  GetUserID='{2}' AND IsState!=2 and b.CompanyID='{5}' and e.CompanyID='{6}' ORDER BY DisDate DESC", userID, userID, userID,CompanyID,CompanyID,CompanyID,CompanyID);
            DataTable DepartName = sql.GetDataTableCommand(sqlDepart);
            if (DepartName.Rows.Count > 0)
            {
                ViewBag.Detail = DepartName;
            }
            else
            {
                ViewBag.Detail = null;
            }
            ////选择部门
            //string sqlDepart = "select DepartID,DepartName from Depart";
            //DataTable Depart = sql.GetDataTableCommand(sqlDepart);
            //if (Depart.Rows.Count > 0)
            //{
            //    ViewBag.dname = Depart;
            //}
            return View();
        }
        //员工时间查询分布视图
        public ActionResult SelectEmpTime(DateTime StartTime, DateTime EndTime, string EmpID,string CompanyID)
        {
            try
            {
                //加密  
                ViewBag.empBounsID = EmpID;
                //解密 
                EmpID = Base64MIMA.JIE(EmpID);
                CompanyID = Base64MIMA.JIE(CompanyID);
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }

            //            string sqls = string.Format(@"select BonusDataID,Name,DisDate,EarMoney from BonusData2 b INNER join Employee e on b.DisMan=e.EmpID 
            //where (BonusType=1 OR BonusType=2) and EarMan='{0}' AND CONVERT(DATE,EarDate) BETWEEN '{1}' AND '{2}' UNION ALL SELECT BonusDataID,Name,DisDate,(0-EarMoney) from BonusData2 b INNER join Employee e on b.EarMan=e.EmpID 
            //where (BonusType=1 OR BonusType=2) and DisMan='{3}' AND CONVERT(DATE,EarDate) BETWEEN '{4}' AND '{5}' ORDER BY DisDate DESC
            //", EmpID, StartTime, EndTime, EmpID, StartTime, EndTime);

            //            string sqls = string.Format($@"select BonusDataID,Name,DisDate,EarMoney,('收入') bty from BonusData2 b INNER join Employee e on b.DisMan=e.EmpID 
            //where   BonusType!=0  and EarMan='{EmpID}' and CONVERT(DATE,EarDate) BETWEEN '{StartTime}' AND '{EndTime}'  
            //UNION ALL 
            //SELECT BonusDataID,Name,DisDate,(0-EarMoney) EarMoney,('支出') btyfrom FROM BonusData2 b INNER join Employee e on b.EarMan=e.EmpID 
            //where  BonusType=2 and DisMan='{EmpID}' and CONVERT(DATE,EarDate) BETWEEN '{StartTime}' AND '{EndTime}'  
            // UNION ALL 
            // SELECT '' BonusDataID,'',GetTime DisDate,(0-GetMoney) EarMoney,('提现') bty from dbo.AuditBonus b INNER join Employee e on b.GetUserID=e.EmpID 
            //where  GetUserID='{EmpID}' AND IsState!=2  and CONVERT(DATE,GetTime) BETWEEN '{StartTime}' AND '{EndTime}'    ORDER BY DisDate DESC
            //");
            string sqls = string.Format($@"select BonusDataID,Name,DisDate,EarMoney,('收入') bty from BonusData2 b INNER join Employee e on b.DisMan=e.EmpID 
where b.CompanyID='{CompanyID}' AND e.CompanyID='{CompanyID}' AND BonusType!=0  and EarMan='{EmpID}' and CONVERT(DATE,EarDate) BETWEEN '{StartTime}' AND '{EndTime}'  
UNION ALL 
SELECT BonusDataID,Name,DisDate,(0-EarMoney) EarMoney,('支出') btyfrom FROM BonusData2 b INNER join Employee e on b.EarMan=e.EmpID 
where b.CompanyID='{CompanyID}' AND e.CompanyID='{CompanyID}' AND  BonusType=2 and DisMan='{EmpID}' and CONVERT(DATE,EarDate) BETWEEN '{StartTime}' AND '{EndTime}'  
 UNION ALL 
 SELECT '' BonusDataID,'',GetTime DisDate,(0-GetMoney) EarMoney,('提现') bty from dbo.AuditBonus b INNER join Employee e on b.GetUserID=e.EmpID 
where b.CompanyID='{CompanyID}' AND e.CompanyID='{CompanyID}' AND  GetUserID='{EmpID}' AND IsState!=2  and CONVERT(DATE,GetTime) BETWEEN '{StartTime}' AND '{EndTime}'    ORDER BY DisDate DESC
");
            DataTable DepartName = sql.GetDataTableCommand(sqls);
            if (DepartName.Rows.Count > 0)
            {
                ViewBag.Detail = DepartName;
            }
            else
            {
                ViewBag.Detail = null;
            }
            return PartialView("_userBonus");
        }
  
    }
}
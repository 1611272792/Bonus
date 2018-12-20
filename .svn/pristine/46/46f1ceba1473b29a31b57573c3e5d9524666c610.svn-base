using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Sunpn_BonusWeb.BonusHelper;

namespace Sunpn_BonusWeb.Controllers
{
    public class BonusNoGetController : Controller
    {
        private Sunpn.Data.SqlServer.SQLConnection sql = null;
        public BonusNoGetController()
        {
            sql = new Sunpn.Data.SqlServer.SQLConnection(BonusHelper.AppConfig.ConnString);
        }
        // GET: BonusNoGet
        public ActionResult Index(string CompanyId)
        {
            try
            {
                //解密 
                CompanyId = Base64MIMA.JIE(CompanyId);
                //加密
                ViewBag.CompanyId = Base64MIMA.JIA(CompanyId);
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }

          
            try
            {
                string EmpSqls = string.Format(@"SELECT EmpID,Name,ISNULL(noGet1,0) noGet FROM dbo.Employee INNER JOIN(SELECT  SUM(EarMoney) noGet1,EarMan  FROM dbo.BonusData2 WHERE (BonusType=1 OR BonusType=2) and IsGet=0 GROUP BY EarMan) AS A ON A.EarMan=dbo.Employee.EmpID
 WHERE CompanyID='{0}'  ORDER BY noGet DESC", CompanyId);
                string DepSqls = string.Format(@"SELECT DepartID,DepartName,ISNULL(noGet1,0) noGet FROM dbo.Depart INNER JOIN(SELECT  SUM(EarMoney) noGet1,EarMan  FROM dbo.BonusData2 WHERE BonusType=0 and IsGet=0 GROUP BY EarMan) AS A ON A.EarMan=DepartID
 WHERE CompanyID='{0}'  ORDER BY noGet DESC", CompanyId);
                DataTable Empds = sql.GetDataTableCommand(EmpSqls);
                DataTable Depds = sql.GetDataTableCommand(DepSqls);
                if (Empds.Rows.Count > 0)
                {
                    ViewBag.Emp = Empds;
                }
                else
                {
                    ViewBag.Emp = null;
                }
                if (Depds.Rows.Count > 0)
                {
                    ViewBag.Dep = Depds;
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
        //跳转分布视图
        public ActionResult SearchInfo(string EmpName,string CompanyId)
        {
            try
            {
                //解密 
                CompanyId = Base64MIMA.JIE(CompanyId);
                //加密
                ViewBag.CompanyId = Base64MIMA.JIA(CompanyId);
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
            string slqs = "";
            if (EmpName=="")
            {
                slqs = string.Format(@"SELECT EmpID,Name,ISNULL(noGet1,0) noGet FROM dbo.Employee INNER JOIN(SELECT  SUM(EarMoney) noGet1,EarMan  FROM dbo.BonusData2 WHERE (BonusType=1 OR BonusType=2) and IsGet=0 GROUP BY EarMan) AS A ON A.EarMan=dbo.Employee.EmpID
  where CompanyId='{0}' ORDER BY noGet DESC", CompanyId);
            }
            else
            {
                slqs = string.Format(@"SELECT EmpID,Name,ISNULL(noGet1,0) noGet FROM dbo.Employee INNER JOIN(SELECT  SUM(EarMoney) noGet1,EarMan  FROM dbo.BonusData2 WHERE (BonusType=1 OR BonusType=2) and IsGet=0 GROUP BY EarMan) AS A ON A.EarMan=dbo.Employee.EmpID
  WHERE Name like '%{0}%' and CompanyId='{1}'  ORDER BY noGet DESC", EmpName, CompanyId);
            }
            
            DataTable Empds = sql.GetDataTableCommand(slqs);
            if (Empds.Rows.Count > 0)
            {
                ViewBag.Emp = Empds;
            }
            else
            {
                ViewBag.Emp = null;
            }
            return PartialView("_NoGetBonus");
        }
        //员工奖金发放
        public ActionResult EmpPayBonus(string EmpID) {
            EmpID = EmpID.Replace(",","','");
            string sqls = string.Format("UPDATE  BonusData2 SET IsGet=1 WHERE EarMan IN ('{0}') AND BonusType=1 OR BonusType=2", EmpID);
            string num = sql.EditDataCommand(sqls);
            if (num=="0")
            {
                return Content("更新成功");
            }
            else
            {
                return Content("更新失败");
            }
          
        }
        //部门奖金发放
        public ActionResult DepPayBonus(string DepID)
        {
            //DepID = DepID.Replace(",", "','");
            string sqls = string.Format("UPDATE  BonusData2 SET IsGet=1 WHERE EarMan IN ({0}) AND BonusType=0", DepID);
            string num = sql.EditDataCommand(sqls);
            if (num == "0")
            {
                return Content("更新成功");
            }
            else
            {
                return Content("更新失败");
            }

        }

    }
}
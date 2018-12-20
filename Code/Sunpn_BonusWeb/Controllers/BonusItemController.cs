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
    public class BonusItemController : Controller
    {
        private Sunpn.Data.SqlServer.SQLConnection sql = null;

        public BonusItemController()
        {
            sql = new Sunpn.Data.SqlServer.SQLConnection(AppConfig.ConnString);
        }
        // GET: BonusItem        
        //public ActionResult Index()
        //{
        //    //公司级奖金
        //    string gs = string.Format("select * from BonusItem where BIType=0");
        //    DataTable gsdt = sql.GetDataTableCommand(gs);
        //    if (gsdt.Rows.Count > 0)
        //    {
        //        ViewBag.gs = gsdt;
        //    }
        //    else
        //    {
        //        ViewBag.gs = null;
        //    }
        //    //部门级奖金
        //    string gr = string.Format("select * from BonusItem where BIType=1");
        //    DataTable grdt = sql.GetDataTableCommand(gr);
        //    if (grdt.Rows.Count > 0)
        //    {
        //        ViewBag.gr = grdt;
        //    }
        //    else
        //    {
        //        ViewBag.gr = null;
        //    }
        //    //奖金项负责人（部门负责人）
        //    string prisql = string.Format(" SELECT EmpID,Name FROM dbo.Employee WHERE EmpID IN(SELECT DepartPrincipal FROM dbo.Depart)");
        //    DataTable pri = sql.GetDataTableCommand(prisql);
        //    if (pri.Rows.Count > 0)
        //    {
        //        ViewBag.pri = pri;
        //    }
        //    else
        //    {
        //        ViewBag.pri = null;
        //    }
        //    return View();
        //}

        //奖金项详细信息

        public ActionResult Info(string BonusItemID,string CompanyID)
        {
            try
            {
                //解密 
                CompanyID = Base64MIMA.JIE(CompanyID);
                //加密
                ViewBag.CompanyId = Base64MIMA.JIA(CompanyID);
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
            string bonus = string.Format("SELECT BonusItemID,BIName,EmpID,Name,BIDepID,BIState,BIType,InDate,b.CompanyID FROM BonusItem b INNER JOIN dbo.Employee e ON b.BIPrincipal = e.EmpID where BonusItemID='{0}' and b.CompanyID='{1}' AND e.CompanyID='{1}'", BonusItemID, CompanyID);
            //string bonus = string.Format("select * from dbo.BonusItem where BonusItemID='{0}' and CompanyID='{1}'", BonusItemID,CompanyID);
            DataTable bodt = sql.GetDataTableCommand(bonus);
            if (bodt.Rows.Count > 0)
            {
                ViewBag.bodt = bodt;
            }
            else
            {
                ViewBag.bodt = null;
            }
            //编辑页面的负责人下拉框数据
            string prisql = string.Format(" SELECT EmpID,Name FROM dbo.Employee WHERE EmpID IN(SELECT DepartPrincipal FROM dbo.Depart where CompanyID='{0}') AND CompanyID='{0}'",CompanyID);
            DataTable pri = sql.GetDataTableCommand(prisql);
            if (pri.Rows.Count > 0)
            {
                ViewBag.pri = pri;
            }
            else
            {
                ViewBag.pri = null;
            }
            //负责人所对应的部门
            //string pridepsql = string.Format("SELECT DepartID, DepartName FROM dbo.Depart WHERE DepartID IN (SELECT DepartID FROM dbo.Depart WHERE DepartPrincipal = (SELECT BIPrincipal FROM dbo.BonusItem WHERE BonusItemID = '{0}' and CompanyID='{1}'))", BonusItemID,CompanyID);
            string pridepsql = string.Format("select DepartID,DepartName from Depart where CompanyID='{0}'", CompanyID);
            DataTable pridep = sql.GetDataTableCommand(pridepsql);
            if (pridep.Rows.Count > 0)
            {
                ViewBag.pridep = pridep;
            }
            else
            {
                ViewBag.pridep = null;
            }
            return View();
        }

        //选择受益部门
        public string SYDepart(string Pri,string CompanyID)
        {

            try
            {
                //解密 
                CompanyID = Base64MIMA.JIE(CompanyID);
                //加密
                ViewBag.CompanyId = Base64MIMA.JIA(CompanyID);
            }
            catch (Exception)
            {
                //跳转错误页面
                return "/ErrorPage/Index";
            }
            string sydepart = string.Format("select DepartID,DepartName from dbo.Depart where CompanyID='{0}' and DepartPrincipal='{1}'",CompanyID,Pri);
            DataTable syd = sql.GetDataTableCommand(sydepart);
            string Json = "";
            if (syd.Rows.Count > 0)
            {
                Json = JsonConvert.SerializeObject(syd);
            }
            return Json;
        }

        //得到编辑的内容
        public ActionResult GetBonusItem(string BIID)
        {
            //选择员工
            string BonusItem = string.Format("select * from BonusItem where BonusItemID='{0}'", BIID);
            DataTable BIdt = sql.GetDataTableCommand(BonusItem);
            if (BIdt.Rows.Count > 0)
            {
                string BonusItemID = BIdt.Rows[0]["BonusItemID"].ToString();
                string BIName = BIdt.Rows[0]["BIName"].ToString();
                string BIPrincipal = BIdt.Rows[0]["BIPrincipal"].ToString();
                int BIState= int.Parse(BIdt.Rows[0]["BIState"].ToString().Trim());
                int BIType = int.Parse(BIdt.Rows[0]["BIType"].ToString().Trim());
                return Content(BonusItemID + "," + BIName + "," + BIPrincipal + "," + BIState + "," + BIType);
            }
            else
            {
                return Content("no");
            }
        }


        //编辑
        public ActionResult editSure(string BonusItemID,string BIName,string BIPrincipal,string BIState,string BIType,int BIDepID,int InDate,string CompanyID)
        {
            try
            {
                //解密 
                CompanyID = Base64MIMA.JIE(CompanyID);
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
            if (InDate > 12)
            {
                return Content("big");
            }
            string editsql = string.Format("update dbo.BonusItem set BonusItemID='{0}',BIName='{1}',BIPrincipal='{2}',BIState='{3}',BIType='{4}',BIDepID={6},InDate={7} where BonusItemID='{5}' and CompanyID='{8}'", BonusItemID, BIName, BIPrincipal, BIState, BIType, BonusItemID,BIDepID,InDate,CompanyID);
            string edit = sql.EditDataCommand(editsql);
            if (edit == "0")
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }

        //删除
        public ActionResult Delete(string BonusItemID,string CompanyID)
        {
            try
            {
                //解密 
                CompanyID = Base64MIMA.JIE(CompanyID);
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
            string delsql = string.Format("delete from dbo.BonusItem where BonusItemID='{0}' and CompanyID='{1}'", BonusItemID,CompanyID);
            string del = sql.EditDataCommand(delsql);
            if (del == "0")
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }

        //添加奖金项按钮
        public ActionResult addSure(string BIName, string BIPrincipal, string BIState, string BIType, int BIDepID,string CompanyID,int InDate)
        {
            try
            {
                //解密 
                CompanyID = Base64MIMA.JIE(CompanyID);
                //加密
                ViewBag.CompanyId = Base64MIMA.JIA(CompanyID);
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
            string isinsql = string.Format("select BIName from BonusItem where BIName='{0}' and CompanyID='{1}'", BIName, CompanyID);
            DataTable isin = sql.GetDataTableCommand(isinsql);
            if (isin.Rows.Count > 0)
            {
                return Content("have");
            }
            else
            {
                //string peosql = string.Format("select EmpID from Employee where BIPrincipal='{0}'", BIPrincipal);
                //DataTable ispeo = sql.GetDataTableCommand(peosql);
                //if (ispeo.Rows.Count>0)
                //{

                //}
                if (InDate>12)
                {
                    return Content("big");
                }
                string addsql = string.Format("INSERT INTO dbo.BonusItem( BonusItemID ,BIName ,BIPrincipal ,BIState ,BIType,BIDepID,InDate,CompanyID)VALUES (newid(),'{0}','{1}',{2},{3},{4},{5},'{6}')", BIName, BIPrincipal, BIState, BIType, BIDepID,InDate,CompanyID);
                string add = sql.EditDataCommand(addsql);
                if (add == "0")
                {
                    return Content("ok");
                }
                else
                {
                    return Content("no");
                }
            }
        }


        public ActionResult AddBonusItem(string CompanyID)
        {
            try
            {
                //解密 
                CompanyID = Base64MIMA.JIE(CompanyID);
                //加密
                ViewBag.CompanyId = Base64MIMA.JIA(CompanyID);
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
            //编辑页面的负责人下拉框
            //string prisql = string.Format(" SELECT EmpID,Name FROM dbo.Employee WHERE EmpID IN(SELECT DepartPrincipal FROM dbo.Depart where CompanyID='{0}')",CompanyID);
            string prisql = string.Format("select EmpID ,Name from dbo.Employee where CompanyID='{0}'", CompanyID);
            DataTable pri = sql.GetDataTableCommand(prisql);
            if (pri.Rows.Count > 0)
            {
                ViewBag.pri = pri;
            }
            else
            {
                ViewBag.pri = null;
            }
            string depsql = string.Format("select DepartID,DepartName from Depart where CompanyID='{0}'",CompanyID);
            DataTable dep = sql.GetDataTableCommand(depsql);
            if (dep.Rows.Count>0)
            {
                ViewBag.dep = dep;
            }
            else
            {
                ViewBag.dep = null;
            }
            return View();
        }

        public ActionResult EditBonusItem(string BonusItemID)
        {
            string bonus = string.Format("select * from dbo.BonusItem where BonusItemID='{0}'", BonusItemID);
            DataTable bodt = sql.GetDataTableCommand(bonus);
            if (bodt.Rows.Count > 0)
            {
                ViewBag.bodt = bodt;
            }
            else
            {
                ViewBag.bodt = null;
            }
            //编辑页面的负责人下拉框
            string prisql = string.Format(" SELECT EmpID,Name FROM dbo.Employee WHERE EmpID IN(SELECT DepartPrincipal FROM dbo.Depart)");
            DataTable pri = sql.GetDataTableCommand(prisql);
            if (pri.Rows.Count > 0)
            {
                ViewBag.pri = pri;
            }
            else
            {
                ViewBag.pri = null;
            }
            return View();
        }


        public ActionResult Detail(string CompanyID)
        {
            try
            {
                //解密 
                CompanyID = Base64MIMA.JIE(CompanyID);
                //加密
                ViewBag.companyid = Base64MIMA.JIA(CompanyID);
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
            string detialsql = string.Format("SELECT dbo.BonusItem.BonusItemID BonusItemID,dbo.BonusItemRule.BonusItemID BonusItemIDs,BIName,BIType,InDate,dbo.BonusItemRule.CompanyID FROM dbo.BonusItem Left JOIN dbo.BonusItemRule ON BonusItem.BonusItemID=dbo.BonusItemRule.BonusItemID WHERE BonusItem.CompanyID='{0}'", CompanyID);
            DataTable ds = sql.GetDataTableCommand(detialsql);
            if (ds.Rows.Count > 0)
            {
                ViewBag.ds = ds;
            }
            else
            {
                ViewBag.ds = null;
            }
            return View();
        }

        public ActionResult GetInfo(string keyword, string CompanyID)
        {
            try
            {
                //加密
                ViewBag.CompanyId = CompanyID;
                //解密 
                CompanyID = Base64MIMA.JIE(CompanyID);
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
            keyword = keyword.Trim().Replace(" ", "");
            string sqlserach = "";

            sqlserach = string.Format("select BIName from BonusItem where BIName like '%{0}%' and CompanyID='{1}'", keyword, CompanyID);

            DataSet ds = sql.GetDataSetCommand(sqlserach);
            string info = JsonConvert.SerializeObject(ds);
            info = info.Replace("Table", "data").Replace("BIName", "title");
            return Content(info);
        }
        public ActionResult SearchEmp(string Name, string CompanyID)
        {
            try
            {
                //加密
                ViewBag.CompanyId = CompanyID;
                //解密 
                CompanyID = Base64MIMA.JIE(CompanyID);
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
            string sqlEmp = "";
             //sqlEmp = string.Format("select * from dbo.BonusItem where BIName like '%{0}%' and CompanyID='{1}'",Name, CompanyID);
            sqlEmp = string.Format("SELECT dbo.BonusItem.BonusItemID BonusItemID, dbo.BonusItemRule.BonusItemID BonusItemIDs, BIName, BIType, InDate, dbo.BonusItemRule.CompanyID FROM dbo.BonusItem Left JOIN dbo.BonusItemRule ON BonusItem.BonusItemID = dbo.BonusItemRule.BonusItemID WHERE BIName like '%{0}%' AND BonusItem.CompanyID = '{1}' and BIState = 0", Name, CompanyID);
            
            DataTable DepartName = sql.GetDataTableCommand(sqlEmp);
            if (DepartName.Rows.Count > 0)
            {
                ViewBag.ds = DepartName;
            }
            else
            {
                ViewBag.ds = null;
            }
            return PartialView("_BonusItemSearch");
        }
    }
}
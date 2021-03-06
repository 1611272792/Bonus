﻿using Newtonsoft.Json;
using Sunpn_BonusWeb.BonusHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sunpn_BonusWeb.Controllers
{
    public class BonusDataQueryController : Controller
    {
        private Sunpn.Data.SqlServer.SQLConnection sql = null;
        public BonusDataQueryController()
        {
            sql = new Sunpn.Data.SqlServer.SQLConnection(BonusHelper.AppConfig.ConnString);
        }
        // 主页面
        public ActionResult Index(string CompanyId)
        {
            try
            {
                ViewBag.CompanyId = CompanyId;
                //解密
                CompanyId = Base64MIMA.JIE(CompanyId);
              
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
         
             DataTable Empds = GetEmpJjInfo("", CompanyId);
            DataTable Depds = GetDepJjInfo("", CompanyId);
            if (Depds.Rows.Count > 0)
            {
                ViewBag.Dep = Depds;
            }
            else
            {
                ViewBag.Dep = null;
            }
            if (Empds.Rows.Count > 0)
            {
                ViewBag.Emp = Empds;
            }
            else
            {
                ViewBag.Emp = null;
            }
            return View();
        }
        //主页面根据人名的分布视图
        public ActionResult SearchInfo(string EmpName,string CompanyId)
        {
            try
            { //加密
                ViewBag.CompanyId = CompanyId;
                //解密 
                CompanyId = Base64MIMA.JIE(CompanyId);
               
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
            DataTable Empds = GetEmpJjInfo(EmpName,CompanyId);
            if (Empds.Rows.Count > 0)
            {
                ViewBag.Emp = Empds;
            }
            else
            {
                ViewBag.Emp = null;
            }
            return PartialView("_bonusData");
        }
        //员工界面
        public ActionResult EmpGain(string empBounsID,string compid)
        {
          ViewBag.empBounsID = empBounsID;
            compid=Base64MIMA.JIE(compid);
            try
            {
                string sqls = string.Format(@"SELECT  CASE  WHEN BonusType=2 THEN e.Name WHEN BonusType=1 THEN  b2.BIName END Bonusform,EarMoney,EarDate,
                       CASE WHEN IsGet=0 THEN '未领取' ELSE '已领取' END isget,BonusDataID FROM BonusData2 b LEFT JOIN dbo.Employee e ON b.DisMan=e.EmpID LEFT JOIN dbo.BonusItem b2 ON b.BonusItemID=b2.BonusItemID 
                       WHERE EarMan='{0}' and b.CompanyId='{1}' and e.CompanyID='{1}' ORDER BY EarDate DESC", empBounsID, compid);
                DataTable ds = sql.GetDataTableCommand(sqls);
                if (ds.Rows.Count>0)
                {
                    ViewBag.Emp = ds;
                }
                else
                {
                    ViewBag.Emp = null;
                } 
            }
            catch (Exception)
            {

                throw;
            }
          
            return View();
        }
        //部门界面
        public ActionResult DepGain(string depBounsID, string compid)
        {
            
            ViewBag.depBounsID = depBounsID;
            compid = Base64MIMA.JIE(compid);
            try
            {
                string sqls = string.Format(@"SELECT  BIName,EarMoney,EarDate,
                       CASE WHEN IsGet=0 THEN '未领取' ELSE '已领取' END isget FROM BonusData2 b LEFT JOIN dbo.Depart d ON b.EarMan=d.DepartID LEFT JOIN dbo.BonusItem b2 ON b.BonusItemID=b2.BonusItemID 
                       WHERE b.BonusType=0 and EarMan='{0}' and b.CompanyId='{1}' and d.CompanyID='{1}'  ORDER BY EarDate DESC", depBounsID, compid);
                DataTable ds = sql.GetDataTableCommand(sqls);
      
                if (ds.Rows.Count > 0)
                {
                    ViewBag.Dep = ds;
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
        //员工时间查询分布视图
        public ActionResult SelectEmpTime(DateTime StartTime,DateTime EndTime,string EmpID) {
            string sqls = string.Format(@"SELECT  CASE  WHEN BonusType=2 THEN e.Name WHEN BonusType=1 THEN  b2.BIName END Bonusform,EarMoney,EarDate,
                       CASE WHEN IsGet=0 THEN '未领取' ELSE '已领取' END isget,BonusDataID  FROM BonusData2 b LEFT JOIN dbo.Employee e ON b.DisMan=e.EmpID LEFT JOIN dbo.BonusItem b2 ON b.BonusItemID=b2.BonusItemID 
                       WHERE EarMan='{0}' AND CONVERT(DATE,EarDate) BETWEEN '{1}' AND '{2}' ORDER BY EarDate DESC", EmpID, StartTime, EndTime);
            DataTable ds = sql.GetDataTableCommand(sqls);
            if (ds.Rows.Count > 0)
            {
                ViewBag.Emp = ds;
            }
            else
            {
                ViewBag.Emp = null;
            }
            return PartialView("_EmpDetailQuery");
        }
        //部门时间查询分布视图
        public ActionResult SelectDepTime(DateTime StartTime, DateTime EndTime, string DepID)
        {
            string sqls = string.Format(@"SELECT BIName,EarMoney,EarDate,
                       CASE WHEN IsGet=0 THEN '未领取' ELSE '已领取' END isget FROM BonusData2 b LEFT JOIN dbo.Depart d ON b.DisMan=d.DepartID LEFT JOIN dbo.BonusItem b2 ON b.BonusItemID=b2.BonusItemID 
                       WHERE EarMan='{0}' AND CONVERT(DATE,EarDate) BETWEEN '{1}' AND '{2}' ORDER BY EarDate DESC", DepID, StartTime, EndTime);
            DataTable ds = sql.GetDataTableCommand(sqls);
            if (ds.Rows.Count > 0)
            {
                ViewBag.Dep = ds;
            }
            else
            {
                ViewBag.Dep = null;
            }
            return PartialView("_DepDetailQuery");
        }
        
        /// <summary>
        /// 人名搜索补全
        /// </summary>
        /// <param name="keyword">简拼，全拼，文字</param>
        /// <returns></returns>
        public ActionResult GetInfo(string keyword,string CompanyId)
        {
            keyword = keyword.Trim().Replace(" ", "");
            try
            {
                //加密
                ViewBag.CompanyId = CompanyId;
                //解密 
                CompanyId = Base64MIMA.JIE(CompanyId);
              
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
            string sqlserach = "";
            if (keyword=="")
            {
               sqlserach = string.Format("select Name from Employee where CompanyId='{0}'",0);
            }
            else
            {
                sqlserach = string.Format("select Name from Employee where (SpellJX like '%{0}%' or SpellQP like '%{1}%' or Name like '%{2}%') and CompanyId='{3}'", keyword, keyword, keyword, CompanyId);
            }
            
            DataSet ds = sql.GetDataSetCommand(sqlserach);
            string info = JsonConvert.SerializeObject(ds);
            info = info.Replace("Table", "data").Replace("Name", "title");
            return Content(info);
        }
        
        /// <summary>
        /// 查询员工奖金
        /// </summary>
        /// <param name="Name">员工名</param>
        /// <returns>DataTable</returns>
        public DataTable GetEmpJjInfo(string empName,string CompanyId)
        {

            string EmpSqls=  string.Format($@"SELECT EmpID,Name,ISNULL(EarMoney1,0) SumMonth, ISNULL(EarMoney2,0) SumBonus FROM Employee With(NOLOCK) LEFT JOIN (SELECT EarMan, SUM(EarMoney) EarMoney1 from BonusData2 WHERE   (BonusType=1 OR  BonusType=2) AND
EarDate between Cast(dateadd(dd, -day(getdate()) + 1, getdate()) AS DATE) and Cast(dateadd(ms, -3, DATEADD(mm, DATEDIFF(m, 0, getdate()) + 1, 0)) AS DATE) AND dbo.BonusData2.CompanyId='{CompanyId}'
GROUP BY EarMan) AS A ON A.EarMan = Employee.EmpID
LEFT JOIN (SELECT EarMan, SUM(EarMoney)EarMoney2 from BonusData2 WHERE   (BonusType = 1 or BonusType = 2)  and dbo.BonusData2.CompanyID='{CompanyId}' 
GROUP BY EarMan) AS B ON B.EarMan = Employee.EmpID where 1 = 1  and dbo.Employee.CompanyID='{CompanyId}'"
);
            if (!string.IsNullOrEmpty(empName))
            {
                EmpSqls += string.Format(" And Name = '{0}'", empName);
            }
            DataTable Empds = sql.GetDataTableCommand(EmpSqls);
            return Empds;

        }

        /// <summary>
        /// 查询部门奖金
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public DataTable GetDepJjInfo(string depName,string  CompanyId)
        {

            string DepSqls = string.Format($@"SELECT DepartID,DepartName,ISNULL(EarMoney1,0) SumMonth, ISNULL(EarMoney2,0) NoGet,ISNULL(EarMoney3,0) sumBonus FROM Depart With(NOLOCK) LEFT JOIN (SELECT EarMan, SUM(EarMoney) EarMoney1 from BonusData2 where   BonusType=0 AND
DisDate between Cast(dateadd(dd, -day(getdate()) + 1, getdate()) AS DATE) and Cast(dateadd(ms, -3, DATEADD(mm, DATEDIFF(m, 0, getdate()) + 1, 0)) AS DATE) and CompanyId='{CompanyId}'
GROUP BY EarMan) AS A ON A.EarMan = dbo.Depart.DepartID
LEFT JOIN (SELECT EarMan, SUM(EarMoney)EarMoney2 from BonusData2 WHERE   BonusType = 0 AND IsGet = 0 and BonusData2.CompanyId='{CompanyId}'
GROUP BY EarMan) AS B ON B.EarMan = dbo.Depart.DepartID LEFT JOIN (SELECT EarMan, SUM(EarMoney)EarMoney3 from BonusData2 WHERE   BonusType = 0  and CompanyId='{CompanyId}'
GROUP BY EarMan) AS C ON C.EarMan = dbo.Depart.DepartID where 1=1 and CompanyId='{CompanyId}'");

            if (!string.IsNullOrEmpty(depName))
            {
               DepSqls += string.Format(" And Name='{0}'", depName);
            }
            DataTable Depds = sql.GetDataTableCommand(DepSqls);
            return Depds;

        }


    }
}
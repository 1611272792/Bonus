//using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class DepartBounsController : Controller
    {
        private Sunpn.Data.SqlServer.SQLConnection sql = null;

        public DepartBounsController()
        {
   
            sql = new Sunpn.Data.SqlServer.SQLConnection(BonusHelper.AppConfig.ConnString);
        }
        // GET: DepartBouns
        public ActionResult Index(string CompanyId)
        {
            try
            {   //加密
                ViewBag.CompanyId = CompanyId;
                //解密 
                CompanyId = Base64MIMA.JIE(CompanyId);
             
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
            string sqlDepart =string.Format("select DepartName,DepartID from Depart where CompanyId='{0}'", CompanyId);
            DataTable DepartName = sql.GetDataTableCommand(sqlDepart);
            
            if (DepartName.Rows.Count>0)
            {
                ViewBag.dname = DepartName;
            }
            else
            {
                ViewBag.dname = null;
            }
            return View();
        }
        //部门选择分布视图
        public ActionResult SearchDepart(string departName,string CompanyId)
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
            string sqlDepart = "";
            if (departName == "")
            {
                sqlDepart = string.Format("select DepartName, DepartID from Depart where   CompanyId='{0}'", CompanyId);
            }
            else
            {
                sqlDepart = string.Format("select DepartName, DepartID from Depart where DepartName like  '%{0}%' and CompanyId='{1}'", departName, CompanyId);
            }
           
            DataTable DepartName = sql.GetDataTableCommand(sqlDepart);
            
            if (DepartName.Rows.Count > 0)
            {
                ViewBag.dname = DepartName;
            }
            else
            {
                ViewBag.dname = null;
            }
            return PartialView("_departBonusShit");
        }
        //所有部门奖金页面
        public ActionResult Detail(int DepartID,string CompanyId)
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
            string sqlDepart = string.Format("select BonusDataID,b2.BIName,Name,DisDate,EarMoney from BonusData2 b inner join Employee e on b.DisMan=e.EmpID inner join  BonusItem b2 on b.BonusItemID=b2.BonusItemID where BonusType=0 and EarMan='{0}'  AND b2.CompanyID='{1}' AND e.CompanyID='{1}'", DepartID, CompanyId);
            DataTable DepartName = sql.GetDataTableCommand(sqlDepart);
            if (DepartName.Rows.Count>0)
            {
                ViewBag.Detail = DepartName;
            }
            else
            {
                ViewBag.Detail = null;
            }
            return View();
        }
      
        //查看该部门奖金明细页面
        public ActionResult Detail2(string depBounsID)
        {
            int bID = int.Parse(depBounsID);
            try
            {
                //文字原因
                string sqlDepart = string.Format("SELECT ResonContent,ResonType,BonusDataID FROM BonusData2 b INNER JOIN ResonDetial r ON b.ResonID = r.ResonID where BonusDataID={0} AND ResonType=1", bID);
                DataTable DepartName = sql.GetDataTableCommand(sqlDepart);
                if (DepartName.Rows.Count>0)
                {
                    ViewBag.Detail2 = DepartName;
                }
                else
                {
                    ViewBag.Detail2 = null;
                }
                //图片原因
                string sqlImages= string.Format("SELECT ResonContent,ResonType,BonusDataID FROM BonusData2 b INNER JOIN ResonDetial r ON b.ResonID = r.ResonID where BonusDataID={0} AND ResonType=2", bID);
                DataTable  ImagesDetail = sql.GetDataTableCommand(sqlImages);
                if (ImagesDetail.Rows.Count > 0)
                {
                    ViewBag.ImagesDetail = ImagesDetail;
                }
                else
                {
                    ViewBag.ImagesDetail = null;
                }
            }
            catch (Exception)
            {
         
                throw;
            }
            return View();
        }
       
         
        /// <summary>
        /// 部门名搜索补全 
        /// </summary>
        /// <param name="keyword">简拼，全拼，文字</param>
        /// <returns></returns>
        public ActionResult GetInfo(string keyword,string CompanyId)
         {
            keyword = keyword.Replace(" ","");
            try
            {
                //解密 
                CompanyId = Base64MIMA.JIE(CompanyId);
               
            }
            catch (Exception)
            {
                //跳转错误页面 
                return Redirect("/ErrorPage/Index");
            }
            string sqlserach = "";
                sqlserach = string.Format("select DepartName from Depart where (SpellJX like '%{0}%' or SpellQP like '%{1}%' or DepartName like '%{2}%') and CompanyId='{3}'", keyword, keyword, keyword, CompanyId);
            DataSet ds = sql.GetDataSetCommand(sqlserach);
            string info = JsonConvert.SerializeObject(ds);
            info = info.Replace("Table", "data").Replace("DepartName","title");
            return Content(info);
        } 
    }
}
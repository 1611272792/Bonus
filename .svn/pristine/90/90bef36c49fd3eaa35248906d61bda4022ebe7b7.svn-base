using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Mvc;
using Sunpn_BonusWeb.BonusHelper;

namespace Sunpn_BonusWeb.Controllers
{
    public class BonusParameterController : Controller
    {
        private Sunpn.Data.SqlServer.SQLConnection sql = null;

        public BonusParameterController()
        {
            sql = new Sunpn.Data.SqlServer.SQLConnection(BonusHelper.AppConfig.ConnString);
        }
        // GET: BonusParameter
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
                string parSql = string.Format("SELECT * FROM dbo.BonusParameter WHERE CompanyId='{0}' ORDER BY BonusNum", CompanyId);
                DataTable ds = sql.GetDataTableCommand(parSql);
                if (ds.Rows.Count > 0)
                {
                    ViewBag.Par = ds;
                }
                else
                { 
                    ViewBag.Par = null;
                }
            }
            catch (Exception) 
            {

                return Content("no");
            }
            return View();
        }
        
        public ActionResult Detail(int BonusID) {

            string sqls = string.Format("select * from BonusParameter where BonusSetID={0}", BonusID);
            DataTable ds = sql.GetDataTableCommand(sqls);
            if (ds.Rows.Count > 0)
            {
                ViewBag.BonusPar = ds;
            }
            else
            {
                ViewBag.BonusPar = null;
            }
            return View();
        }
        
        //添加页面
        public ActionResult AddBonusPar(string CompanyId)
        {
            try
            {
                //解密 
                CompanyId = Base64MIMA.JIE(CompanyId);
                 
                ViewBag.CompanyId = CompanyId;
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
            return View();
        }

        //编辑
        public ActionResult editBP(string bpname, int bpnum, int bpstate, string bpremark, int bpID) {
            string sqls = string.Format("update BonusParameter set  TypeName='{0}',BonusNum={1},IsOrNo={2},Remark='{3}'  where BonusSetID={4}", bpname, bpnum, bpstate, bpremark, bpID);
            try
            {
                string bpEdit = sql.EditDataCommand(sqls);
            }
            catch (Exception)
            {
                return Content("no");
            }
            return Content("ok");
        }

        //添加
        public ActionResult addBP(string CompanyId, string bpname, int bpnum, int bpstate, string bpremark)
        {
           
            string sqls = string.Format("insert into BonusParameter(TypeName,BonusNum,IsOrNo,Remark,CompanyId) values('{0}',{1},{2},'{3}','{4}')", bpname, bpnum, bpstate, bpremark, CompanyId);
            string bpEdit = sql.EditDataCommand(sqls);
            if (bpEdit=="0")
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
               
        
          
        }
        
        //删除 
        public ActionResult DeleteBP(int BonusID) {

            string sqls = string.Format("Delete from BonusParameter where BonusSetID={0}", BonusID);
            try
            {
                string bpEdit = sql.EditDataCommand(sqls);
            }
            catch (Exception)
            {
                return Content("no");
            }
            return Content("ok");
        }
    }
}
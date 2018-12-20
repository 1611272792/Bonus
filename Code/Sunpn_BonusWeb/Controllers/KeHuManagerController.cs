using Sunpn_BonusWeb.BonusHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Newtonsoft.Json;

namespace Sunpn_BonusWeb.Controllers
{
    public class KeHuManagerController : Controller
    {
        private Sunpn.Data.SqlServer.SQLConnection sql = null;
        Log log = new Log();
        public KeHuManagerController()
        {
            sql = new Sunpn.Data.SqlServer.SQLConnection(BonusHelper.AppConfig.ConnString);
        }
        // GET: KeHuManager
        public ActionResult Index()
        {
            //得到所有注册的公司的信息，我们决定是通过还是不通过
            string sqlstr = string.Format("exec proc_KehuManager 'SelectKehu'");
            DataSet dt = sql.GetDataSetCommand(sqlstr);
            if (dt?.Tables.Count > 0)
            {
                #region 全部公司的
                if (dt?.Tables[0].Rows.Count > 0)
                {
                    ViewBag.kehuList = dt.Tables[0];
                }
                else
                {
                    ViewBag.kehuList = null;
                }
                #endregion

                #region 已过期公司
                if (dt?.Tables[1].Rows.Count > 0)
                {
                    ViewBag.weiShenghe = dt.Tables[1];
                }
                else
                {
                    ViewBag.weiShenghe = null;
                }
                #endregion

            }
            
            return View();
        }

        //找到需要审核的数据
        public ActionResult Shenghe(string cid)
        {
            string sqlstr = string.Format($"exec proc_KehuManager 'SelectKehuBycid','{cid}'");
            DataTable dt = sql.GetDataTableCommand(sqlstr);
            if (dt?.Rows.Count > 0)
            {
                ViewBag.comp = dt;
            }
            else
            {
                ViewBag.comp = null;
            }
            return View();
        }

        //审核
        public ActionResult Sheng(string endDate,string isout,string cid,string beginDate)
        {
            if (endDate == "" || endDate == null || isout == "" || isout == null || cid == "" || cid == null)
            {
                return Content("不能为空");
            }
            //比较开始日期与结束日期
            DateTime dt1 = DateTime.Parse(beginDate);
            DateTime dt2 = DateTime.Parse(endDate);

            TimeSpan ts = dt2.Subtract(dt1);
            if (ts.Days < 0)
            {
                return Content("结束时间不能早于注册时间");
            }

            string sqlstr = string.Format($"exec proc_KehuManager 'Shenghe',{cid},'{endDate}',{isout}");
            string info = sql.EditDataCommand(sqlstr);
            if (info == "0")
            {
                return Content("ok");
            }
            else
            {
                return Content("审核失败");
            }

        }

        //公司搜索
        public ActionResult GetInfo(string keyword)
        {
            
            keyword = keyword.Trim().Replace(" ", "");

            string sqlserach = string.Format($"exec proc_KehuManager 'ShousuoCompany','','','0','{keyword}'");
            DataSet ds = sql.GetDataSetCommand(sqlserach);
            string info = JsonConvert.SerializeObject(ds);
            info = info.Replace("Table", "data").Replace("CompanyName", "title");
            return Content(info);
        }

        public ActionResult SearchInfo(string CompanyName)
        {
            string sqlserach = string.Format($"exec proc_KehuManager 'ShousuoCompany','','','0','{CompanyName}'");
            DataTable ds = sql.GetDataTableCommand(sqlserach);
            if (ds?.Rows.Count > 0)
            {
                ViewBag.kehuList = ds;
            }
            else
            {
                ViewBag.kehuList = null;
            }
            return PartialView("_PartialShenghe");
        }
    }
}
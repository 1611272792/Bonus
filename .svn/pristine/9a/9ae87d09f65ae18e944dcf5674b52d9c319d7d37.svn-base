using Newtonsoft.Json;
using Sunpn_BonusWeb.BonusHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sunpn_BonusWeb.Controllers
{
    public class TraddingController : Controller
    {
        // GET: Tradding
        private Sunpn.Data.SqlServer.SQLConnection sql = null;
        Log log = new Log();
        public TraddingController()
        {
            sql = new Sunpn.Data.SqlServer.SQLConnection(BonusHelper.AppConfig.ConnString);
        }
        public ActionResult Index(string userId,string Yue,string companyId)
        {

            ViewBag.userId = userId;
            ViewBag.yue = Yue;
            ViewBag.companyId = companyId;
            InitJsapi(companyId);
            return View();
        }
        private void InitJsapi(string companyId)
        {
            companyId = Base64MIMA.JIE(companyId);
            BonusHelper.JSAPI js = new BonusHelper.JSAPI();
            //string url = Request.RawUrl.ToString();
            //WriteLog("url:" + url);
            log.AppenLog("Request.RawUrl.ToString():" + Request.RawUrl.ToString());
            //根据公司得到公司的长期授权码
            string sqlstr = string.Format($"select * from Company where CompanyID='{companyId}'");
            DataTable dt = sql.GetDataTableCommand(sqlstr);
            Hashtable ht = js.GetJSAPI_Parameters(companyId, dt.Rows[0]["Longcode"].ToString(), dt.Rows[0]["attoken"].ToString(), DateTime.Parse(dt.Rows[0]["expressYxq"].ToString()), $"http://{BonusHelper.AppConfig.ServerUrl}" + Request.RawUrl.ToString());
            ViewBag.appid = BonusHelper.AppConfig.sCorpID;
            ViewBag.noncestr = ht["noncestr"].ToString();
            ViewBag.signature = ht["signature"].ToString();
            ViewBag.timestamp = ht["timestamp"].ToString();
            // WriteLog("signature:" + ViewBag.signature);
        }

        /// <summary>
        /// 判断是否是本公司的员工
        /// </summary>
        /// <param name="GetPerson">得到这笔钱的人</param>
        /// <param name="comUserid">进来这个系统的人</param>
        /// <returns></returns>
        public ActionResult IsPersons(string GetPerson, string comUserid,string companyId)
        {
            comUserid = Base64MIMA.JIE(comUserid);
            companyId = Base64MIMA.JIE(companyId);
            if (comUserid == GetPerson)
            {
                return Content("不能给自己交易");
            }
            string[] canshuIsPersons = new string[] { "IsPersons", comUserid, GetPerson, companyId };
            DataTable dtIsPersons = sql.GetDataTableProcedure("proc_Tradding", canshuIsPersons);
            if (dtIsPersons.Rows.Count > 0)
            {
                //是本公司的人
                return Content(dtIsPersons.Rows[0]["Name"].ToString());
            }
            else
            {
                return Content("no");
            }
        }

        /// <summary>
        /// 添加交易
        /// </summary>
        /// <returns></returns>
        public ActionResult AddTrading(string Userid, double TradingMoney, string beizhuContent,string comUserid,string companyId)
        {
            
            if (Userid == "" || TradingMoney <= 0)
            {
                return Content("输入有误");
            }
            comUserid = Base64MIMA.JIE(comUserid);//解密
            companyId = Base64MIMA.JIE(companyId);
            string guidBeizuID =null;
            string sqlstr = "";
            if (beizhuContent != "")
            {
                guidBeizuID = System.Guid.NewGuid().ToString();//原因guid
                sqlstr = string.Format("insert into ResonDetial values('{0}','{1}',2) ", guidBeizuID, beizhuContent);
                sqlstr += string.Format("insert into BonusData2 values(newid(),'{0}',GETDATE(),'{1}',{3},GETDATE(),2,0,'{2}','{4}')", comUserid, Userid, guidBeizuID, TradingMoney, companyId);

            }
            else
            {
                sqlstr += string.Format("insert into BonusData2 values(newid(),'{0}',GETDATE(),'{1}',{2},GETDATE(),2,0,null,'{3}')", comUserid, Userid, TradingMoney, companyId);

            }

            string info = sql.EditDataCommand(sqlstr);
            if (info == "0")
            {
                return Content("ok");
            }
            else
            {
                return Content("付款失败");
            }
        }

        /// <summary>
        /// 自动补全
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPersons(string EmpId,string CompanyId)
        {
            //WriteLog("GetPersons");
            //string selectname = Session["userName"].ToString();
            //WriteLog("selectname:" + selectname);
            string q = Request.QueryString["term"];
            EmpId = Base64MIMA.JIE(EmpId);
            ////得到这个人所属的公司
            //string sqlstr2 = string.Format("select CompanyID from Employee where EmpID='{0}'", EmpId);
            //DataTable dt2 = sql.GetDataTableCommand(sqlstr2);
            //int CompanyID = 0;
            //if (dt2?.Rows.Count > 0)
            //{
            //    CompanyID = int.Parse(dt2.Rows[0]["CompanyID"].ToString());
            //}
            CompanyId = Base64MIMA.JIE(CompanyId);
            string sqlstr = string.Format("select Name label,EmpID value from Employee where SpellJX like '%{0}%' and IsOut=0 and EmpID!='{1}' and CompanyID='{2}'", q, EmpId, CompanyId);
            DataTable dt = sql.GetDataTableCommand(sqlstr);
            //WriteLog("sqlstr:" + sqlstr);
            //WriteLog("序列化：" + JsonConvert.SerializeObject(dt));
            return Content(JsonConvert.SerializeObject(dt));

        }

        public ActionResult Index2()
        {
            return View();
        }
    }
}
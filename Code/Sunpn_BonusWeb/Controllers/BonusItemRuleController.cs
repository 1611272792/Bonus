using Sunpn_BonusWeb.BonusHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sunpn_BonusWeb.Controllers
{
    public class BonusItemRuleController : Controller
    {
        private Sunpn.Data.SqlServer.SQLConnection sql = null;
        Log log = new Log();
        public BonusItemRuleController()
        {
            sql = new Sunpn.Data.SqlServer.SQLConnection(AppConfig.ConnString);
        }
        // GET: BonusItemRule
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
        //    return View();
        //}

        //新增规则页面

        public ActionResult AddRule(string BonusItemID, string CompanyID)
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
            string bisql = string.Format("select * from BonusItem where BonusItemID='{0}' and CompanyID='{1}'", BonusItemID, CompanyID);
            DataTable bi = sql.GetDataTableCommand(bisql);
            if (bi.Rows.Count > 0)
            {
                ViewBag.bi = bi;
            }
            else
            {
                ViewBag.bi = null;
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
                ViewBag.CompanyId = Base64MIMA.JIA(CompanyID);
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
            string detialsql = string.Format("select * from BonusItem where CompanyID='{0}'", CompanyID);
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

        //设置手动注入
        public ActionResult AddSureGD(string BonusItemID, string AddMoney, int InDate)
        {
            log.AppenLog("111");
            //JoinType 0手动  1自动

            //注入时间
            string JoinDate = DateTime.Now.ToString("yyyy-MM-dd");
            //根据奖金项有效期推算有效时间
            DateTime EndDate = Convert.ToDateTime(DateTime.Today.AddMonths(+InDate).ToString("yyyy-MM-01"));
            string addsql = string.Format("Insert Into RuleData(BonusItemID,AddMoney,RemainMoney,JoinDate,EndDate,JoinType) values('{0}','{1}','{2}','{3}','{4}',{5})", BonusItemID, AddMoney, AddMoney, JoinDate, EndDate, 0);
            log.AppenLog("注入奖金：" + addsql);
            string dt = sql.EditDataCommand(addsql);
            log.AppenLog("dt:" + dt);
            if (dt == "0")
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }

        //新增公式规则
        public ActionResult AddSureGS(string BonusItemID, string BIType, string rname, int avgnum, string avgmoney, string CompanyID)
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
            int num;//总人数
            int GainNum;//受益人数
            string isin = string.Format("SELECT BIRuleName FROM dbo.BonusItemRule WHERE BIRuleName='{0}' and CompanyID='{1}'", rname, CompanyID);
            DataTable ist = sql.GetDataTableCommand(isin);
            if (ist?.Rows.Count > 0)
            {
                return Content("have");
            }
            else
            {
                //公司级奖金
                if (BIType == "0")
                {
                    //查询公司总人数
                    string numsql = string.Format("SELECT COUNT(*) FROM dbo.Employee where CompanyID='{0}'", CompanyID);
                    DataTable numd = sql.GetDataTableCommand(numsql);
                    if (numd.Rows.Count > 0)
                    {
                        num = int.Parse(numd.Rows[0][0].ToString());
                        if (avgnum > num)//如果刨除人数大于公司总人数则不符合要求
                        {
                            return Content("big");
                        }
                        else
                        {
                            GainNum = num - avgnum;
                            //money = (num - avgnum) * double.Parse(avgmoney);
                            //string addsql = string.Format(" INSERT dbo.BonusItemRule( BIRuleName ,SumMoney ,RemainMoney ,ActiveDate ,GainNum,AvgMoney) VALUES ('{0}','{1}','{2}',{3},{4},'{5}')", rname, money, money, activedate, avgnum, avgmoney);
                            string addsql = string.Format(" INSERT dbo.BonusItemRule( BIRuleName ,RemainNum,GainNum,AvgMoney,BonusItemID,CompanyID) VALUES ('{0}',{1},{2},'{3}','{4}','{5}')", rname, avgnum, GainNum, avgmoney, BonusItemID, CompanyID);
                            string flag = sql.EditDataCommand(addsql);
                            if (flag == "0")
                            {
                                return Content("ok");
                            }
                            else
                            {
                                return Content("no");
                            }
                        }
                    }
                    else
                    {
                        return Content("添加失败");
                    }
                }
                else if (BIType == "1")//部门级奖金
                {
                    //string numsql = string.Format("SELECT COUNT(*) FROM dbo.Employee WHERE DepartID=( SELECT BIDepID FROM dbo.BonusItem WHERE BonusItemID='{0}' and CompanyID={1})", BonusItemID, CompanyID);
                    string numsql = string.Format("with T as(select DepartID from Depart  where DepartID = (SELECT BIDepID FROM dbo.BonusItem WHERE BonusItemID = '{0}' and CompanyID = '{1}') union all select D.DepartID from Depart D, T  where D.PID = T.DepartID)SELECT COUNT(*) FROM dbo.Employee WHERE DepartID IN(SELECT DepartID from T) AND IsOut=0 AND CompanyID = '{2}'",BonusItemID,CompanyID,CompanyID);
                    DataTable numd = sql.GetDataTableCommand(numsql);
                    if (numd.Rows.Count > 0)
                    {
                        num = int.Parse(numd.Rows[0][0].ToString());
                        if (avgnum > num)//如果刨除人数大于部门总人数则不符合要求
                        {
                            return Content("bigd");
                        }
                        else
                        {
                            GainNum = num - avgnum;
                            //money = (num - avgnum) * double.Parse(avgmoney);
                            //string addsql = string.Format(" INSERT dbo.BonusItemRule( BIRuleName ,SumMoney ,RemainMoney ,ActiveDate ,GainNum,AvgMoney) VALUES ('{0}','{1}','{2}',{3},{4},'{5}')", rname, money, money, activedate, avgnum, avgmoney);
                            string addsql = string.Format(" INSERT dbo.BonusItemRule( BIRuleName ,RemainNum,GainNum,AvgMoney,BonusItemID,CompanyID) VALUES ('{0}',{1},{2},'{3}','{4}','{5}')", rname, avgnum, GainNum, avgmoney, BonusItemID, CompanyID);
                            string flag = sql.EditDataCommand(addsql);
                            if (flag == "0")
                            {
                                return Content("ok");
                            }
                            else
                            {
                                return Content("no");
                            }
                        }
                    }
                    else
                    {
                        return Content("no");
                    }
                }
                else
                {
                    return Content("no");
                }
            }
        }

        //奖金项数据
        public ActionResult DataInfo(string BonusItemID, string CompanyID)
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
            DataTable dt = sql.GetDataTableProcedure("proc_BonusData", BonusItemID, CompanyID);
            if (dt?.Rows.Count > 0 && dt.Rows[0][0].ToString() != "")
            {
                ViewBag.dt = dt;
            }
            else
            {
                ViewBag.dt = null;
            }
            return View();
        }

        //编辑规则数据
        public ActionResult EditRule(string BonusItemID)
        {
            string rulesql = string.Format("SELECT * FROM dbo.BonusItemRule INNER JOIN dbo.BonusItem ON BonusItem.BonusItemID=BonusItemRule.BonusItemID WHERE BonusItemRule.BonusItemID='{0}'", BonusItemID);
            DataTable er = sql.GetDataTableCommand(rulesql);
            if (er.Rows.Count > 0)
            {
                ViewBag.er = er;
            }
            return View();
        }

        //编辑规则
        public ActionResult EditSureGS(int BIRuleID, string BonusItemID, string BIType, string rname, int avgnum, string avgmoney)
        {
            int num;//总人数
            int GainNum;//受益人数
            //公司级奖金
            if (BIType == "0")
            {
                string numsql = string.Format("SELECT COUNT(*) FROM dbo.Employee");
                DataTable numd = sql.GetDataTableCommand(numsql);
                if (numd.Rows.Count > 0)
                {
                    num = int.Parse(numd.Rows[0][0].ToString());
                    if (avgnum > num)//如果刨除人数大于公司总人数则不符合要求
                    {
                        return Content("big");
                    }
                    else
                    {
                        GainNum = num - avgnum;
                        string addsql = string.Format(" Update dbo.BonusItemRule set BIRuleName='{0}',RemainNum={1},GainNum={2},AvgMoney='{3}' where BIRuleID={4}", rname, avgnum, GainNum, avgmoney, BIRuleID);
                        string flag = sql.EditDataCommand(addsql);
                        if (flag == "0")
                        {
                            return Content("ok");
                        }
                        else
                        {
                            return Content("no");
                        }
                    }
                }
                else
                {
                    return Content("no");
                }
            }
            else if (BIType == "1")//部门级奖金
            {
                string numsql = string.Format("SELECT COUNT(*) FROM dbo.Employee WHERE DepartID=( SELECT BIDepID FROM dbo.BonusItem WHERE BonusItemID='{0}')", BonusItemID);
                DataTable numd = sql.GetDataTableCommand(numsql);
                if (numd.Rows.Count > 0)
                {
                    num = int.Parse(numd.Rows[0][0].ToString());
                    if (avgnum > num)//如果刨除人数大于公司总人数则不符合要求
                    {
                        return Content("bigd");
                    }
                    else
                    {
                        GainNum = num - avgnum;
                        string addsql = string.Format(" Update dbo.BonusItemRule set BIRuleName='{0}',RemainNum={1},GainNum={2},AvgMoney='{3}' where BIRuleID={4}", rname, avgnum, GainNum, avgmoney, BIRuleID);
                        string flag = sql.EditDataCommand(addsql);
                        if (flag == "0")
                        {
                            return Content("ok");
                        }
                        else
                        {
                            return Content("no");
                        }
                    }
                }
                else
                {
                    return Content("no");
                }
            }
            else
            {
                return Content("no");
            }
        }


        //删除
        public ActionResult Delete(int BIRuleID, string BonusItemID)
        {
            //string info = sql.EditDataProcedure("proc_DelRule", BIRuleID.ToString(), BonusItemID);
            //if (info == "0")
            //{
            //    return Content("ok");
            //}
            string delsql = string.Format("delete from dbo.BonusItemRule where BIRuleID='{0}'", BIRuleID);
            string del = sql.EditDataCommand(delsql);
            if (del == "0")
            {
                //string upsql = string.Format("update dbo.BonusItem set BIRuleID=null where BonusItemID='{0}'", BonusItemID);
                //string up = sql.EditDataCommand(upsql);
                //if (up == "0")
                //{
                    return Content("ok");
                //}
                //else
                //{
                //    return Content("no");
                //}
            }
            else
            {
                return Content("no");
            }
        }
    }
}
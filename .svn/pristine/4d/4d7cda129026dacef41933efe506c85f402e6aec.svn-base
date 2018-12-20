using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sunpn_BonusWeb.BonusHelper;

namespace Sunpn_BonusWeb.Controllers
{
    public class BonusImpowerController : Controller
    {
        private Sunpn.Data.SqlServer.SQLConnection sql = null;

        public BonusImpowerController()
        {
            sql = new Sunpn.Data.SqlServer.SQLConnection(AppConfig.ConnString);
        }
        // GET: BonusImpower
        public ActionResult Index(string userID,string CompanyID)
        {
            try
            {
                //解密 
                CompanyID = Base64MIMA.JIE(CompanyID);
                userID = Base64MIMA.JIE(userID);
                //加密
                ViewBag.CompanyId = Base64MIMA.JIA(CompanyID);
                ViewBag.UserID = Base64MIMA.JIA(userID);
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
            //判断登录系统的用户是不是奖金项负责人
            int rule = ViewRule.GetRule(userID);
            if (rule > 0)
            {
                //主页面显示被授权人列表
                //string namesql = string.Format("SELECT DISTINCT e.EmpID,Name FROM dbo.Employee e INNER JOIN dbo.BonusImpower b ON b.EmpID = e.EmpID");
                //DataTable emp = sql.GetDataTableCommand(namesql);
                //if (emp.Rows.Count > 0)
                //{
                //    ViewBag.emp = emp;
                //}
                //else
                //{
                //    ViewBag.emp = emp;
                //}

                //主页面显示奖金项负责人被授权过的奖金项列表
                //string itemsql = string.Format("SELECT BonusImpower.BonusItemID,BIName,BIPrincipal FROM dbo.BonusImpower INNER JOIN dbo.BonusItem ON BonusItem.BonusItemID = BonusImpower.BonusItemID WHERE BIPrincipal='{0}'",userID);

                //主页面显示奖金项负责人可以授权的奖金项列表
                string itemsql = string.Format("SELECT BonusItem.BonusItemID,BIPrincipal,BIName FROM BonusItem WHERE BIState=0 AND BIPRincipal='{0}' and CompanyID='{1}'", userID,CompanyID);
                DataTable emp = sql.GetDataTableCommand(itemsql);
                if (emp.Rows.Count > 0)
                {
                    ViewBag.emp = emp;
                }
                else
                {
                    ViewBag.emp = null;
                    ViewBag.userid = userID;
                }
                return View();
            }
            else
            {
                return Content("<script> alert('您不是奖金项负责人暂无此页面的操作权限！'); history.go(-1);</script>");
            }
        }

        //奖金授权页面
        public ActionResult AddImpower(string BonusItemID,string CompanyID)
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
            string addsql = string.Format("SELECT BonusItem.BonusItemID,BIPrincipal,BIName FROM BonusItem WHERE BonusItemID='{0}' and CompanyID='{1}'", BonusItemID,CompanyID);
            DataTable dt = sql.GetDataTableCommand(addsql);
            if (dt.Rows.Count>0)
            {
                ViewBag.dt = dt;
            }
            else
            {
                ViewBag.dt = null;
            }
            return View();
        }

       //搜索自动补全员工信息
        public ActionResult GetEmp(string keyword,string CompanyID)
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
            string sqlserach = string.Format("select Name,EmpID from Employee where CompanyID='{3}' and (SpellJX like '%{0}%' or SpellQP like '%{1}%' or Name like '%{2}%') ", keyword, keyword, keyword,CompanyID);
            DataSet ds = sql.GetDataSetCommand(sqlserach);
            string info = JsonConvert.SerializeObject(ds);
            info = info.Replace("Table", "data").Replace("Name", "title");
            return Content(info);
        }

        public ActionResult getval(string name)
        {
            string valsql = string.Format("select Name from Employee where Name='{0}'",name);
            DataTable dt = sql.GetDataTableCommand(valsql);
            if (dt.Rows.Count > 0)
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }

        //添加奖金授权
        public ActionResult AddSure(string BonusItemID, string BIPrincipal, string EmpID, string Impowermoney, string ImpowerType, string Remark, string UserID)
        {
            try
            {
                //解密 
                UserID = Base64MIMA.JIE(UserID);
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
            try
            {
                if (EmpID == UserID)
                {
                    return Content("nome");
                }
                else
                {
                    string ImpowerDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string remainsql = string.Format("SELECT SUM(RemainMoney) AllMoney  FROM dbo.RuleData WHERE BonusItemID='{0}' AND Convert(varchar(30),getdate(),102)<EndDate ", BonusItemID);
                    DataTable redt = sql.GetDataTableCommand(remainsql);
                    //被授权金额的累计月份不能大于授权金额的累计月份
                    if (redt.Rows.Count > 0 && redt.Rows[0]["AllMoney"].ToString() != "")
                    {
                        if (double.Parse(redt.Rows[0]["AllMoney"].ToString()) >= int.Parse(Impowermoney))
                        {
                            //string addsql = string.Format("INSERT dbo.BonusImpower( BonusItemID ,BIPID,EmpID ,AddMoney ,RemainMoney ,ImpowerDate,EndDate, Model,IsValid,Remark) values('{0}','{1}','{2}','{3}','{4}','{5}',(select CONVERT(varchar(7), dateadd(mm,(SELECT InDate FROM dbo.BonusItem WHERE  BonusItemID='{6}'),getdate()) , 120) + '-01'),{7},{8},'{9}')", BonusItemID, BIPrincipal, EmpID, Impowermoney, Impowermoney, ImpowerDate, BonusItemID, ImpowerType, 0, Remark);
                            string addsql = string.Format("INSERT dbo.BonusImpower( BonusItemID ,BIPID,EmpID ,AddMoney ,RemainMoney ,ImpowerDate, Model,IsValid,Remark) values('{0}','{1}','{2}','{3}','{4}','{5}',{6},{7},'{8}')", BonusItemID, BIPrincipal, EmpID, Impowermoney, Impowermoney, ImpowerDate, ImpowerType, 0, Remark);
                            string dt = sql.EditDataCommand(addsql);
                            if (dt == "0")
                            {
                                return Content("ok");
                            }
                            else
                            {
                                return Content("no");
                            }
                        }
                        else
                        {
                            return Content("big");
                        }
                    }
                    else
                    {
                        return Content("not");
                    }
                }
            }
            catch (Exception ex)
            {
                return Content("错误" + ex);
            }
        }

        //被授权人详情页面
        public ActionResult Detail(string BonusItemID,string EmpID,string CompanyID)
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
            //详情
            string detailsql = string.Format("SELECT ImpowerID,BIName,Name,AddMoney,Model,ImpowerDate,Remark FROM dbo.BonusImpower INNER JOIN dbo.BonusItem ON BonusItem.BonusItemID = BonusImpower.BonusItemID INNER JOIN dbo.Employee ON Employee.EmpID = BonusImpower.EmpID WHERE ImpowerDate>=CONVERT(varchar(30),DATEADD(MM,DATEDIFF(MM,0,GETDATE()),0),102) AND dbo.BonusImpower.EmpID='{0}' AND dbo.BonusImpower.BonusItemID='{1}' and dbo.Employee.CompanyID='{2}' and IsValid=0 ORDER BY ImpowerDate DESC", EmpID,BonusItemID,CompanyID);
            DataTable detail = sql.GetDataTableCommand(detailsql);
            if (detail?.Rows.Count>0)
            {
                ViewBag.detail = detail;
            }
            else
            {
                ViewBag.detail = null;
            }

            //汇总
            string datasql = string.Format("SELECT DISTINCT BIName,Name,SUM(AddMoney) AllMoney,SUM(RemainMoney) RemainMoney FROM dbo.BonusImpower INNER JOIN dbo.BonusItem ON BonusItem.BonusItemID = BonusImpower.BonusItemID INNER JOIN dbo.Employee ON Employee.EmpID = BonusImpower.EmpID WHERE ImpowerDate>=DATEADD(MM,DATEDIFF(MM,0,GETDATE()),0) AND dbo.BonusImpower.EmpID='{0}' AND dbo.BonusImpower.BonusItemID='{1}' and dbo.Employee.CompanyID='{2}' and IsValid=0 GROUP BY BIName,Name", EmpID, BonusItemID,CompanyID);
            DataTable dt = sql.GetDataTableCommand(datasql);
            if (dt?.Rows.Count > 0)
            {
                string bmsql = string.Format(@"SELECT  SUM(RemainMoney) AllMoney
                                    FROM    dbo.RuleData
                                    WHERE   BonusItemID = '{0}'
                                            AND CONVERT(VARCHAR(30), GETDATE(), 102) < EndDate", BonusItemID);
                DataTable dt1 = sql.GetDataTableCommand(bmsql);
                if (dt1?.Rows.Count>0)
                {
                    if (double.Parse(dt.Rows[0]["RemainMoney"].ToString()) <= double.Parse(dt1.Rows[0]["AllMoney"].ToString()))
                    {
                        dt.Rows[0]["RemainMoney"] = dt.Rows[0]["RemainMoney"].ToString();
                    }
                    else
                    {
                        dt.Rows[0]["RemainMoney"] = dt1.Rows[0]["AllMoney"].ToString();
                    }
                }
                ViewBag.dt = dt;
            }
            else
            {
                ViewBag.dt = null;
            }

            string cqsql = string.Format("SELECT SUM(AddMoney) Allmoney FROM dbo.BonusImpower WHERE Model=0 AND BonusItemID='{0}' and ImpowerDate>=DATEADD(MM,DATEDIFF(MM,0,GETDATE()),0) and EmpID='{1}'", BonusItemID, EmpID);
            DataTable cq = sql.GetDataTableCommand(cqsql);
            if (cq?.Rows.Count>0)
            {
                ViewBag.cq = cq;
            }
            else
            {
                ViewBag.cq = null;
            }
            return View();
        }

        //被授权人列表页面
        public ActionResult Collect(string BonusItemID,string CompanyID)
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
            //string dtsql = string.Format("SELECT DISTINCT BonusImpower.BonusItemID,BIPID,BIName,EmpID FROM dbo.BonusImpower INNER JOIN dbo.BonusItem ON BonusItem.BonusItemID = BonusImpower.BonusItemID WHERE ImpowerDate>=CONVERT(varchar(30),DATEADD(MM,DATEDIFF(MM,0,GETDATE()),0),102) AND EmpID='{0}'", EmpID);

            //查看此奖金项本月授权给过哪些人
            string dtsql = string.Format("SELECT DISTINCT BonusImpower.EmpID,Employee.EmpPhotos,BonusItemID,Name FROM dbo.BonusImpower INNER JOIN dbo.Employee ON Employee.EmpID = BonusImpower.EmpID WHERE BonusItemID='{0}' and CompanyID='{1}' AND ImpowerDate>=CONVERT(varchar(30),DATEADD(MM,DATEDIFF(MM,0,GETDATE()),0),102) GROUP BY BonusImpower.EmpID,Employee.EmpPhotos,BonusItemID,Name having SUM(RemainMoney)>0", BonusItemID,CompanyID);
            DataTable dt = sql.GetDataTableCommand(dtsql);
            if (dt.Rows.Count>0)
            {
                ViewBag.ds = dt;
            }
            else
            {
                ViewBag.ds = null;
            }
            return View();
        }

        //备注页面
        public ActionResult Reason(int ImpowerID)
        {
            string reasonsql = string.Format("select Remark from dbo.BonusImpower where ImpowerID={0}",ImpowerID);
            DataTable reason = sql.GetDataTableCommand(reasonsql);
            if (reason.Rows.Count > 0)
            {
                ViewBag.Reason = reason;
            }
            else
            {
                ViewBag.Reason = null;
            }
            return View();
        }

        public ActionResult BackSure(int ImpowerID)
        {
            string editvalid = string.Format("UPDATE dbo.BonusImpower SET IsValid = 1 Where ImpowerID='{0}' ", ImpowerID);
            string valid = sql.EditDataCommand(editvalid);
            if (valid=="0")
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }

        //被授权人奖金汇总
        public ActionResult DataInfo(string BonusItemID, string EmpID)
        {
            string datasql = string.Format("SELECT DISTINCT BIName,Name,SUM(AddMoney) AllMoney,SUM(RemainMoney) RemainMoney FROM dbo.BonusImpower INNER JOIN dbo.BonusItem ON BonusItem.BonusItemID = BonusImpower.BonusItemID INNER JOIN dbo.Employee ON Employee.EmpID = BonusImpower.BIPID WHERE ImpowerDate>=DATEADD(MM,DATEDIFF(MM,0,GETDATE()),0) AND dbo.BonusImpower.EmpID='{0}' AND dbo.BonusImpower.BonusItemID='{1}' GROUP BY BIName,Name", EmpID,BonusItemID);
            DataTable dt = sql.GetDataTableCommand(datasql);
            if (dt.Rows.Count>0)    
            {
                ViewBag.dt = dt;
            }
            else
            {
                ViewBag.dt = null;
            }
            return View();
        }

        public ActionResult EditImpower(string BonusItemID)
        {
            string editsql = string.Format("select dbo.BonusImpower.BonusItemID,BIName,Name,AddMoney,Model,Remark from dbo.BonusImpower INNER JOIN dbo.BonusItem ON BonusItem.BonusItemID = BonusImpower.BonusItemID INNER JOIN dbo.Employee ON Employee.EmpID = BonusImpower.EmpID where dbo.BonusImpower.BonusItemID='{0}' and Model=0 and ImpowerDate>=DATEADD(MM,DATEDIFF(MM,0,GETDATE()),0)", BonusItemID);
            DataTable et = sql.GetDataTableCommand(editsql);
            if (et?.Rows.Count > 0)
            {
                ViewBag.et = et;
            }
            else
            {
                ViewBag.et = null;
            }
            return View();
        }


        //我的想法是应该没有修改，少了就加，多了就减
        public ActionResult EditImpowerSure(string BonusItemID, string Impowermoney, int Model, string Remark)
        {
            //如果修改为长期有效
            if (Model==0)
            {
                //string editsure = string.Format("update dbo.BonusImpower set AddMoney='{0}',Model={1},Remark='{2}' where BonusItemID='{3}' and Model=0 and ImpowerDate>=DATEADD(MM,DATEDIFF(MM,0,GETDATE()),0)", Impowermoney, Model, Remark, BonusItemID);
                string editsure = string.Format("update dbo.BonusImpower set Model=0,EditMoney='{0}',Remark='{1}' where BonusItemID='{2}' and Model=0 and ImpowerDate>=DATEADD(MM,DATEDIFF(MM,0,GETDATE()),0)",Impowermoney,Remark, BonusItemID);
                string edit = sql.EditDataCommand(editsure);
                if (edit == "0")
                {
                    return Content("ok");
                }
                else
                {
                    return Content("no");
                }
            }
            //如果修改为当月有效
            else
            {
                //string editsure = string.Format("update dbo.BonusImpower set AddMoney='{0}',Model={1},Remark='{2}' where BonusItemID='{3}' and Model=0 and ImpowerDate>=DATEADD(MM,DATEDIFF(MM,0,GETDATE()),0)", Impowermoney, Model, Remark, BonusItemID);
                string editsure = string.Format("update dbo.BonusImpower set Model=1,AddMoney='{0}',Remark='{1}' where BonusItemID='{2}' and Model=0 and ImpowerDate>=DATEADD(MM,DATEDIFF(MM,0,GETDATE()),0)", Impowermoney, Remark, BonusItemID);
                string edit = sql.EditDataCommand(editsure);
                if (edit == "0")
                {
                    return Content("ok");
                }
                else
                {
                    return Content("no");
                }
            }
        }

        public ActionResult myImpower(string userID)
        {
            string isin = string.Format("select * from dbo.BonusImpower where EmpID='{0}'",userID);
            DataTable isdt = sql.GetDataTableCommand(isin);
            if (isdt?.Rows.Count>0)
            {
                string dtsql = string.Format("SELECT DISTINCT dbo.BonusImpower.BonusItemID,BIName,EmpID FROM dbo.BonusImpower INNER JOIN dbo.BonusItem ON BonusItem.BonusItemID = BonusImpower.BonusItemID WHERE EmpID='{0}'",userID);
                DataTable dt = sql.GetDataTableCommand(dtsql);
                if (dt?.Rows.Count>0)
                {
                    ViewBag.dt = dt;
                }
                else
                {
                    ViewBag.dt = null;
                }
                return View();
            }
            else
            {
                return Content("<script> alert('您暂时没有被授权的奖金！'); history.go(-1);</script>");
            }                 
        }

        public ActionResult myDetail(string BonusItemID,string EmpID)
        {
            string dtsql = string.Format("SELECT ImpowerID,Name,AddMoney,RemainMoney,Model,ImpowerDate FROM dbo.BonusImpower INNER JOIN dbo.Employee ON Employee.EmpID = BonusImpower.EmpID WHERE BonusItemID='{0}' and BonusImpower.EmpID='{1}' ORDER BY ImpowerDate DESC", BonusItemID,EmpID);
            DataTable dt = sql.GetDataTableCommand(dtsql);
            if (dt?.Rows.Count>0)
            {
                ViewBag.dt = dt;
            }
            else
            {
                ViewBag.dt = null;
            }

            //汇总
            string datasql = string.Format("SELECT DISTINCT BIName,Name,SUM(AddMoney) AllMoney,SUM(RemainMoney) RemainMoney FROM dbo.BonusImpower INNER JOIN dbo.BonusItem ON BonusItem.BonusItemID = BonusImpower.BonusItemID INNER JOIN dbo.Employee ON Employee.EmpID = BonusImpower.EmpID WHERE ImpowerDate>=DATEADD(MM,DATEDIFF(MM,0,GETDATE()),0) AND dbo.BonusImpower.EmpID='{0}' AND dbo.BonusImpower.BonusItemID='{1}' GROUP BY BIName,Name", EmpID, BonusItemID);
            DataTable dts = sql.GetDataTableCommand(datasql);
            if (dts?.Rows.Count > 0)
            {
                ViewBag.dts = dts;
            }
            else
            {
                ViewBag.dts = null;
            }

            string cqsql = string.Format("SELECT SUM(AddMoney) Allmoney FROM dbo.BonusImpower WHERE Model=0 AND BonusItemID='{0}' and ImpowerDate>=DATEADD(MM,DATEDIFF(MM,0,GETDATE()),0) and EmpID='{1}'", BonusItemID,EmpID);
            DataTable cq = sql.GetDataTableCommand(cqsql);
            if (cq?.Rows.Count > 0)
            {
                ViewBag.cq = cq;
            }
            else
            {
                ViewBag.cq = null;
            }           
            return View();
        }

        public ActionResult SelectImpowerData(string BonusItemID ,string EmpID,string StartTime,string EndTime)
        {
            //详情
            string detailsql = string.Format("SELECT ImpowerID,BIName,Name,AddMoney,Model,ImpowerDate,IsValid,Remark FROM dbo.BonusImpower INNER JOIN dbo.BonusItem ON BonusItem.BonusItemID = BonusImpower.BonusItemID INNER JOIN dbo.Employee ON Employee.EmpID = BonusImpower.EmpID WHERE CONVERT(DATE,ImpowerDate) BETWEEN '{2}' AND '{3}' AND dbo.BonusImpower.EmpID='{0}' AND dbo.BonusImpower.BonusItemID='{1}' ORDER BY ImpowerDate DESC", EmpID, BonusItemID,StartTime,EndTime);
            DataTable detail = sql.GetDataTableCommand(detailsql);
            if (detail?.Rows.Count > 0)
            {
                ViewBag.detail = detail;
            }
            else
            {
                ViewBag.detail = null;
            }
            return View("_ImpowerData");
        }
    }
}
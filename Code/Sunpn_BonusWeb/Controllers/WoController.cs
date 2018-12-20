﻿using Sunpn.Pinyin;
using Sunpn_BonusWeb.BonusHelper;
using Sunpn_BonusWeb.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace Sunpn_BonusWeb.Controllers
{
    public class WoController : Controller
    {
        private Sunpn.Data.SqlServer.SQLConnection sql = null;
        Log log = new Log();
        public WoController()
        {
            sql = new Sunpn.Data.SqlServer.SQLConnection(BonusHelper.AppConfig.ConnString);
        }
        // GET: Wo
        public ActionResult Index(string userId, string counts, string companyId)

        {
            log.AppenLog("我看看22");
            try
            {
                string userID = "";
                string CompanyId = "";
                string user_ticket = "";//成员票据，通过这个得到登陆进来的人的详情信息
                #region 得到userid和公司id
                //当是从别的页面回来时
                if (!string.IsNullOrWhiteSpace(userId))
                {
                    userID = Base64MIMA.JIE(userId);
                }
                if (!string.IsNullOrWhiteSpace(companyId))
                {
                    CompanyId = Base64MIMA.JIE(companyId);
                }
                if (userID == "")
                {
                    //通过code得到userid
                    string code1 = Request["code"];
                    if (code1 != "")
                    {
                        Tuple<bool, UserInfo> tu2 = BonusHelper.AccessTokenHelper.GetWechatUserInfo2(code1);//第三方应用
                        if (tu2.Item1 || Session["aaa"] != null)
                        {
                            if (Session["aaa"] == null)
                            {
                                log.AppenLog("if");
                                userID = tu2.Item2.UserId;//得到userid
                                Session["aaa"] = userID;
                                // c.Value = tu2.Item2;
                            }
                            else
                            {
                                log.AppenLog("else");
                                userID = Session["aaa"].ToString();
                                //userid = c.Value;
                            }
                            
                        }

                        try
                        {
                            userID = userID.Replace('"', ' ');
                            userID = userID.Trim();
                            CompanyId = tu2.Item2.CorpId;
                            user_ticket = tu2.Item2.user_ticket;
                        }
                        catch (Exception ex)
                        {

                           
                        }
                    }
                    else
                    {
                        //code为空，跳到错误页面
                        return Redirect("/ErrorPage/Index");
                    }

                }

                #endregion

                userID = "lzc";
                CompanyId = "wx512ad5972960e003";
                //加密公司id和userid
                log.AppenLog("登陆进来userid:" + userId);
                ViewBag.CompanyID = Base64MIMA.JIA(CompanyId);
                ViewBag.userID = Base64MIMA.JIA(userID);
                //工号
                ViewBag.SuserId = userID;
                #region 判断公司是否在有效期
                //在：看登陆进来的人是否是某个公司的负责人，如果是的话就把他的联系方式等填到相关公司表里，不是直接进入后台
                //不在：提示已经过了有效期，需要联系我们来审核
                string sqlstr = string.Format($"exec Proc_Wo 'IsValidComoany','{CompanyId}'");
                DataTable dt_IsValidComoany = sql.GetDataTableCommand(sqlstr);
                if (dt_IsValidComoany?.Rows.Count > 0)
                {
                    //通过user_ticket得到详细信息
                    string strResult = AccessTokenHelper.GetUserDetail2(user_ticket);
                    log.AppenLog("页面成员信息：" + strResult);
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(UserDetials));
                    MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(strResult));
                    UserDetials obj = (UserDetials)ser.ReadObject(ms);
                    //判断是否是某个公司的负责人
                    string sqlstr2 = string.Format($"select * from Company where CompanyPrincipal='{userID}'");
                    DataTable dt2 = sql.GetDataTableCommand(sqlstr2);
                    if (dt2?.Rows.Count > 0)
                    {
                        //是某个公司负责人==>将他的信息放到公司列表里面去
                        string CompanyPhone = "";
                        string CompanyEmail = "";
                        string upCom = "";
                        for (int i = 0; i < dt2?.Rows.Count; i++)
                        {
                            
                            if (dt2.Rows[i]["CompanyPhone"].ToString() == ""|| dt2.Rows[i]["CompanyEmail"].ToString() == "")
                            {
                                CompanyPhone = obj.mobile;
                                CompanyEmail = obj.email;
                                upCom += string.Format($" update Company set CompanyPhone='{CompanyPhone}' where CompanyID={dt2.Rows[i]["CompanyID"].ToString()}");
                            }
                        }

                        if (upCom != "")
                        {
                            sql.EditDataCommand(upCom);
                        }
                    }
                    //更新对应人的信息
                    try
                    {
                        string py = Pinyin.GetInitials(obj.name);//简拼
                        string pinyin = Pinyin.GetPinyin(obj.name).Replace(" ", "");//全拼
                        string upEmp = string.Format($"exec Proc_Wo 'UpdateTouXiang','{obj.corpid}','{obj.userid}','{obj.avatar}','{obj.mobile}','{obj.email}','{pinyin}','{py}',{obj.gender}");
                        log.AppenLog("更新人员信息:" + upEmp);
                        string info_upEmp = sql.EditDataCommand(upEmp);
                        if (info_upEmp != "0")
                        {
                            return Content("<script>alert('获取个人信息失败');history.go(-1);</script>");

                        }
                    }
                    catch (Exception ex)
                    {

                        
                    }
                }
                else
                {
                    //return Content("<script>alert('您的使用期限已过,请联系我们!8888888');history.go(-1);</script>");
                    return Redirect("/Wo/EndIndex");
                }
                #endregion

                #region 个人信息与个人奖金
                string infosql = string.Format($"exec proc_Main 'SelectEmpDetialWo','{userID}','','{CompanyId}'");
                DataSet info = sql.GetDataSetCommand(infosql);
                if (info?.Tables.Count > 0)
                {
                    
                    //个人信息
                    if (info?.Tables[0].Rows.Count > 0)
                    {
                        //是否位超级管理员
                        if (info.Tables[0].Rows[0]["RoleID"].ToString() == "8EA2CC5C-308D-4538-A9D8-FCAD7CA6FEB0")
                        {
                            ViewBag.chaoji = 1;
                        }
                        else
                        {
                            ViewBag.chaoji = 0;
                        }

                        ViewBag.info = info.Tables[0];
                    }
                    else
                    {
                        ViewBag.info = null;
                        ViewBag.chaoji = 0;
                       
                    }
                    //个人奖金
                    if (info?.Tables[1].Rows.Count > 0)
                    {
                        ViewBag.SumMoney = info.Tables[1].Rows[0][0];
                    }
                    else
                    {
                        ViewBag.SumMoney = "0.00";
                    }
                }
                else
                {
                    ViewBag.info = null;
                    ViewBag.chaoji = 0;
                }

                #endregion

                #region 部门奖金
                string isDepartPri = string.Format($"exec proc_Main 'SelectDepJIangjin','{userID}','','{CompanyId}'");
                DataTable dss = sql.GetDataTableCommand(isDepartPri);
                if (dss?.Rows.Count > 0)
                {
                    ViewBag.depSumMoney = dss;
                }
                else
                {
                    ViewBag.depSumMoney = null;
                }
                //string isDepartPri = string.Format("SELECT DepartID FROM dbo.Depart WHERE DepartPrincipal='{0}'", userID);
                //                string isDepartPri = string.Format("SELECT DepartID FROM dbo.Depart WHERE DepartPrincipal='{0}' AND CompanyID='{1}'", userID, CompanyId);
                //                DataTable dss = sql.GetDataTableCommand(isDepartPri);
                //                if (dss.Rows.Count > 0)
                //                {
                //                    //是部门负责人
                //                    //ViewBag.isDepartPri = "youarePri";
                //                    //部门奖金
                //                    string sqlDepart = string.Format(@"SELECT DepartID,DepartName,ISNULL(EarMoney,'0.00') Earmoney,EarMan FROM  dbo.Depart d  LEFT JOIN 
                //(SELECT  SUM(EarMoney) EarMoney ,EarMan   FROM dbo.BonusData2 WHERE BonusType=0 AND IsGet=0 GROUP BY EarMan )
                // b ON d.DepartID=b.EarMan  WHERE   DepartPrincipal='{0}'  and CompanyID='{1}'", userID, CompanyId);
                //                    DataTable DepartName = sql.GetDataTableCommand(sqlDepart);
                //                    if (DepartName.Rows.Count > 0)
                //                    {

                //                        ViewBag.depSumMoney = DepartName;
                //                    }
                //                    else
                //                    {
                //                        ViewBag.depSumMoney = null;
                //                    }
                //                }
                //                else
                //                {
                //                    //ViewBag.isDepartPri = null;
                //                }
                #endregion

                #region 奖金项
                string isitp = string.Format(@"SELECT A.BonusItemID,A.BIName,RM,IM FROM (
SELECT RuleData.BonusItemID,BIName,SUM(RemainMoney)RM FROM dbo.RuleData 
INNER JOIN dbo.BonusItem ON BonusItem.BonusItemID = RuleData.BonusItemID
WHERE  RuleData.BonusItemID IN (SELECT BonusItemID FROM dbo.BonusImpower WHERE BIState=0 AND EmpID='{0}') AND CompanyID='{2}'
AND GETDATE()<EndDate
GROUP BY RuleData.BonusItemID,BIName) AS A
LEFT JOIN 
(SELECT b2.BonusItemID,BIName,SUM(b1.RemainMoney) IM FROM dbo.BonusImpower  b1
INNER JOIN dbo.BonusItem b2 ON b2.BonusItemID = b1.BonusItemID 
WHERE BIState=0 AND EmpID = '{1}' AND (ImpowerDate >= DATEADD(MM, DATEDIFF(MM, 0, GETDATE()), 0) and IsValid=0)
GROUP BY b2.BonusItemID, BIName) AS B ON A.BonusItemID=B.BonusItemID AND A.BIName=B.BIName", userID, userID, CompanyId);

                DataTable itp = sql.GetDataTableCommand(isitp);
                if (itp?.Rows.Count > 0)
                {
                    ViewBag.itp = itp;
                }
                else
                {
                    ViewBag.itp = null;
                }
                //自己负责的奖金项
                string isitem = string.Format("SELECT dbo.RuleData.BonusItemID,dbo.BonusItem.BIName,SUM(RemainMoney) AllMoney FROM dbo.RuleData INNER JOIN dbo.BonusItem ON BonusItem.BonusItemID = RuleData.BonusItemID WHERE BIState=0 AND CompanyID='{1}' AND BIPrincipal = '{0}' AND CONVERT(VARCHAR(30), GETDATE(), 102) < EndDate GROUP BY dbo.RuleData.BonusItemID, dbo.BonusItem.BIName", userID, CompanyId);
                DataTable item = sql.GetDataTableCommand(isitem);
                if (item?.Rows.Count > 0)
                {
                    for (int i = 0; i < item?.Rows.Count; i++)
                    {
                        Session["shit" + i] = sql.GetDataTableProcedure("proc_BonusData", item.Rows[i]["BonusItemID"].ToString(), "1");
                    }
                    ViewBag.item = item;
                }
                else
                {
                    ViewBag.item = null;
                }
                //查自己有没有奖金发放权限
                string sqlsq = string.Format($@"
SELECT b2.BonusItemID, BIName, SUM(b1.RemainMoney) IM FROM dbo.BonusImpower  b1
INNER JOIN dbo.BonusItem b2 ON b2.BonusItemID = b1.BonusItemID
WHERE BIState = 0 AND EmpID = '{userID}' AND(ImpowerDate >= DATEADD(MM, DATEDIFF(MM, 0, GETDATE()), 0) and IsValid = 0)
GROUP BY b2.BonusItemID, BIName");
                DataTable dtsq = sql.GetDataTableCommand(sqlsq);
                ViewBag.counts = dtsq.Rows.Count + item.Rows.Count;
                ViewBag.count = Base64MIMA.JIA((dtsq.Rows.Count + item.Rows.Count).ToString());
                #endregion

                InitJsapi(Base64MIMA.JIA(CompanyId));
            }
            catch (Exception ex)
            {
                log.AppenLog("woIndex错误:" + ex.Message);
                return Redirect("/ErrorPage/Index");

            }
            return View();
        }
        //我的部门奖金页面
        public ActionResult MyDepartBonus(string DepartID,string compId)
        {
            try
            {
                compId = Base64MIMA.JIE(compId);

                string sqlDepart = string.Format(@"                       
                      
 SELECT BonusDataID,BIName,EarMoney,Name,EarDate  
 FROM dbo.BonusData2 b INNER JOIN dbo.Depart  d 
 ON b.EarMan=d.DepartID  INNER JOIN dbo.BonusItem b2 
 ON b.BonusItemID=b2.BonusItemID    INNER JOIN dbo.Employee e 
 ON b.DisMan=e.EmpID
 WHERE BonusType=0 AND EarMan='{0}' and b.CompanyId='{1}' 
 and d.CompanyID='{1}' and b2.CompanyID='{1}'
 and e.CompanyID='{1}'
  ORDER BY EarDate  DESC
                       ", DepartID, compId);
                DataTable DepartName = sql.GetDataTableCommand(sqlDepart);
                if (DepartName.Rows.Count > 0)
                {
                    ViewBag.Detail = DepartName;
                }
                else
                {
                    ViewBag.Detail = null;
                }
            }
            catch (Exception ex)
            {

                return Redirect("/ErrorPage/Index");
            }
            return View();
        }
        //奖金项明细
        public ActionResult BonusItemsPay(string userID, string itemsID,string CompanyID)
        {
            try
            {
                //加密
                //ViewBag.userID = userID;
                //解密 
                userID = Base64MIMA.JIE(userID);
                CompanyID = Base64MIMA.JIE(CompanyID);
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
            #region  支出
            //查询该用户是否是这个奖金项的负责人
            string isItemsPic = string.Format("SELECT * FROM dbo.BonusItem WHERE BIPrincipal='{0}' AND BonusItemID='{1}' AND CompanyID='{2}'", userID, itemsID,CompanyID);
            DataTable ds = sql.GetDataTableCommand(isItemsPic);
            string Picsqls = "";
            if (ds?.Rows.Count > 0)
            {
                //为该奖金项负责人
                Picsqls = string.Format(@"SELECT b2.BonusDataID,e.Name DisName,e2.Name EarName ,EarMoney,DisDate  FROM  BonusData2 b2 
INNER JOIN Employee e ON b2.DisMan=e.EmpID
INNER JOIN Employee e2 ON b2.EarMan=e2.EmpID
 WHERE b2.CompanyID='{2}'AND e.CompanyID='{4}' AND e2.CompanyID='{5}' AND b2.BonusItemID='{0}' AND (b2.BonusType=1 OR BonusType=2)  
 UNION ALL
SELECT b2.BonusDataID, e.Name DisName,d.DepartName EarName,EarMoney,DisDate  FROM  BonusData2 b2 
INNER JOIN  Employee e ON b2.DisMan= e.EmpID
INNER JOIN Depart d ON b2.EarMan=d.DepartID
 WHERE b2.CompanyID='{3}' AND e.CompanyID='{6}' AND d.CompanyID='{7}' AND  b2.BonusItemID='{1}'  AND b2.BonusType=0  ORDER BY b2.DisDate DESC", itemsID, itemsID,CompanyID, CompanyID, CompanyID, CompanyID, CompanyID, CompanyID);
            }
            else
            {
                //为被授权人
                Picsqls = string.Format(@"SELECT b2.BonusDataID, e.Name DisName,e2.Name EarName ,EarMoney,DisDate  FROM  BonusData2 b2 
INNER JOIN Employee e ON b2.DisMan=e.EmpID
INNER JOIN Employee e2 ON b2.EarMan=e2.EmpID
 WHERE b2.CompanyID='{4}' AND e.CompanyID='{6}' AND e2.CompanyID='{7}' AND b2.BonusItemID='{0}' AND (b2.BonusType=1 OR BonusType=2)  AND b2.DisMan='{1}'
 UNION ALL
SELECT b2.BonusDataID,e.Name DisName,d.DepartName EarName,EarMoney,DisDate  FROM  BonusData2 b2 
INNER JOIN  Employee e ON b2.DisMan= e.EmpID
INNER JOIN Depart d ON b2.EarMan=d.DepartID
 WHERE b2.CompanyID='{5}'AND e.CompanyID='{8}' AND d.CompanyID='{9}' AND b2.BonusItemID='{2}'  AND b2.BonusType=0 AND b2.DisMan='{3}' ORDER BY b2.DisDate DESC", itemsID, userID, itemsID, userID,CompanyID,CompanyID, CompanyID, CompanyID, CompanyID, CompanyID);
            }
            DataTable Picdt = sql.GetDataTableCommand(Picsqls);
            if (Picdt.Rows.Count > 0)
            {
                ViewBag.PicData = Picdt;
            }
            else
            {
                ViewBag.PicData = null;
            }
            #endregion
            #region 收入
            if (ds?.Rows.Count > 0)
            {
                string dtsql = string.Format("SELECT Name,AddMoney,RuleData.JoinDate,EndDate FROM dbo.RuleData JOIN dbo.BonusItem ON BonusItem.BonusItemID = RuleData.BonusItemID INNER JOIN dbo.Employee ON Employee.EmpID=dbo.BonusItem.BIPrincipal WHERE BonusItem.CompanyID='{2}' AND Employee.CompanyID='{3}' AND BonusItem.BonusItemID='{0}' AND BIPrincipal='{1}' ORDER BY dbo.RuleData.JoinDate DESC", itemsID, userID,CompanyID,CompanyID);
                DataTable item = sql.GetDataTableCommand(dtsql);
                if (item?.Rows.Count > 0)
                {
                    ViewBag.item = item;
                    ViewBag.type = "负责人";
                }
                else
                {
                    ViewBag.item = null;
                }
            }
            else
            {
                //被授权人
                string dtsql = string.Format("SELECT ImpowerID,e.Name EarName,e1.Name DisName,AddMoney,RemainMoney,Model,ImpowerDate FROM dbo.BonusImpower INNER JOIN dbo.Employee e ON e.EmpID = BonusImpower.EmpID INNER JOIN dbo.Employee e1 ON e1.EmpID = dbo.BonusImpower.BIPID WHERE e.CompanyID='{2}' AND e1.CompanyID='{3}' AND BonusItemID = '{0}' and IsValid=0 and BonusImpower.EmpID = '{1}' ORDER BY ImpowerDate DESC", itemsID, userID,CompanyID,CompanyID);
                DataTable join = sql.GetDataTableCommand(dtsql);
                if (join?.Rows.Count > 0)
                {
                    ViewBag.join = join;
                    ViewBag.type = "被授权";
                }
                else
                {
                    ViewBag.join = null;
                }
            }

            #endregion

            return View();
        }
        public ActionResult EndIndex()
        {
            string sqlstr = string.Format($"EXEC proc_SysParams 'SelectSysParam',1");
            DataTable dt = sql.GetDataTableCommand(sqlstr);
            if (dt?.Rows.Count > 0)
            {
                ViewBag.lxdh = dt.Rows[0]["SysContent"]?.ToString();
            }
            else
            {
                ViewBag.lxdh = "无";
            }
            return View();
        }
        public ActionResult Index3()
        {
            return View();
        }
        //意见与反馈页面
        public ActionResult SuggestionCommit(string UserId, string CompanyId)
        {
            try
            {
                //CompanyId = Base64MIMA.JIA(CompanyId);
                log.AppenLog("提交意见与反馈：" + CompanyId);
                InitJsapi(CompanyId);
                ViewBag.userId = UserId;
                ViewBag.compid = CompanyId;
            }
            catch (Exception ex)
            {
                log.AppenLog("提交页面错误：" + ex.Message);
                throw;
            }
            return View();
        }

        //提交意见反馈
        public ActionResult CommSugg(string ResonContent, string ResonImg, string comType, string comId, string compId)
        {
            if (comId == "" || comId == null)
            {
                return Content("网络错误，稍后请重试");
            }
            comId = Base64MIMA.JIE(comId);
            log.AppenLog("提交反馈：" + ResonContent + ",ResonImg:" + ResonImg + ",comType:" + comType + ",comId:" + comId);
            int ImgCount = 0;
            if (ResonImg != "")
            {
                ResonImg = ResonImg.Substring(0, ResonImg.Length - 1);
                ImgCount = ResonImg.Split(',').Count();
            }
            log.AppenLog("ImgCount:" + ImgCount);
            string sqlstr = string.Format($"exec proc_Suggection 'InsertSuggestion','{ResonContent}','{ResonImg}','{comType}','{comId}','',{ImgCount},'{Base64MIMA.JIE(compId)}'");
            log.AppenLog("提交反馈sqlstr：" + sqlstr);
            string info = sql.EditDataCommand(sqlstr);
            if (info == "0")
            {
                return Content("ok");
            }
            else
            {
                return Content("提交意见失败");
            }
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="serverId"></param>
        /// <returns></returns>
        public ActionResult UploadImge(string serverId, string compid)//原图
        {
            compid = Base64MIMA.JIE(compid);
            log.AppenLog("上传图片" + compid);

            //return Content("222");
            //
            //WriteLog("serverId" + serverId);
            List<string> rsFilePathList = new List<string>();
            try
            {
                string imageFilePath = "";
                string imgServerIds = serverId; // 微信服务器图片Id
                //WriteLog("imgServerIds微信服务器图片Id" + imgServerIds);
                List<string> imgServerIdList = imgServerIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                //WriteLog("imgServerIdList集合:" + imgServerIdList);
                // WriteLog("imgServerIdList.Count():图片长度" + imgServerIdList.Count());
                foreach (string imgServerId in imgServerIdList)
                {

                    //WriteLog("imgServerId:" + imgServerId);
                    // 1)获取图片
                    ImgHelper imgH = new ImgHelper();
                    Image img = imgH.GetImage(imgServerId, compid);
                    // 2)存放本地
                    imageFilePath = imgH.SaveWeChatAttFileOfImage(img, "Sunpn");
                    rsFilePathList.Add(imageFilePath);
                }
                return Content(imageFilePath);
            }
            catch (Exception ex)
            {
                log.AppenLog("意见与反馈上传图片时出错：" + ex.Message);
                return Content(ex.Message);
            }


        }

        //反馈记录
        public ActionResult ProblemData()
        {

            //            string sqls = string.Format(@" SELECT s1.id,c.CompanyName,Name,resonId,lxType,SugContent,CommitTime,(SELECT COUNT(1)  FROM SugReson WHERE SugType=2 AND SugGuid=s1.resonId) ImgNum FROM dbo.SuggectionComm s1 
            //INNER JOIN dbo.SugReson s2 ON s1.resonId=s2.SugGuid
            //INNER JOIN dbo.Employee e ON s1.UserId=e.EmpID
            //INNER JOIN dbo.Company c ON s1.CompId=c.CompanyID
            // WHERE SugType=1 ORDER BY CommitTime DESC");
            string sqls = string.Format(@" select s.CommitTime,s.CompId,s.id,s.resonId,s.UserId,s.lxType,c.CompanyName,e.Name
from SuggectionComm s left join Employee e on s.UserId=e.EmpID and e.CompanyID=s.CompId
 left join Company c on s.CompId = c.CompanyID order by CommitTime desc");
            DataTable dt = sql.GetDataTableCommand(sqls);
            if (dt.Rows.Count > 0)
            {
                ViewBag.ProblemData = dt;
            }
            else
            {
                ViewBag.ProblemData = null;
            }
            return View();
        }

        /// <summary>
        /// 根据日期查询反馈记录
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectFankui(string startTime, string endTime)
        {
            string sqlstr = string.Format($@" select s.CommitTime,s.CompId,s.id,s.resonId,s.UserId,s.lxType,c.CompanyName,e.Name from SuggectionComm s left join Employee e on s.UserId=e.EmpID and e.CompanyID=s.CompId
 left join Company c on s.CompId=c.CompanyID
 where s.CommitTime BETWEEN '{startTime}' and '{endTime}' order by CommitTime desc");
            DataTable dt = sql.GetDataTableCommand(sqlstr);
            if (dt?.Rows.Count > 0)
            {
                ViewBag.ProblemData = dt;
            }
            else
            {
                ViewBag.ProblemData = null;
            }
            return PartialView("_PartialSelectFankui");
        }

        //ImageDetail查看原因详情
        public ActionResult ImageDetail(string ResonId)
        {
            //tupian原因
            string sqls = string.Format("SELECT SugContent FROM dbo.SugReson WHERE SugGuid='{0}' AND SugType=2", ResonId);
            DataTable dt = sql.GetDataTableCommand(sqls);
            if (dt.Rows.Count > 0)
            {
                ViewBag.imgDetail = dt;
            }
            else
            {
                ViewBag.imgDetail = null;
            }
            //文字原因
            string wenzi = string.Format("SELECT SugContent FROM dbo.SugReson WHERE SugGuid='{0}' AND SugType=1", ResonId);
            DataTable wenziDt = sql.GetDataTableCommand(wenzi);
            if (wenziDt.Rows.Count > 0)
            {
                ViewBag.wenzi = wenziDt;
            }
            else
            {
                ViewBag.wenzi = null;
            }


            return View();
        }

        //更换头像
        public ActionResult UpdateImg(string img, string userID)
        {

            try
            {
                userID = Base64MIMA.JIE(userID);
                log.AppenLog("选中的图片" + img);
                string[] a = img.Split('.');
                //BMP（位图）、JPG、JPEG、PNG、GIF
                if (a[1] == "jpg" || a[1] == "gif" || a[1] == "png" || a[1] == "JPEG" || a[1] == "BMP")
                {


                    string photosql = string.Format("update dbo.Employee set EmpPhotos='{0}' where EmpID='{1}'", img, userID);
                    log.AppenLog("photosql:" + photosql);
                    string photo = sql.EditDataCommand(photosql);
                    if (photo == "0")
                    {
                        return Content("ok");
                    }
                    else
                    {
                        return Content("更换头像失败，请稍后重试");
                    }
                }
                else
                {
                    return Content("图片格式不正确");
                }
            }
            catch (Exception ex)
            {
                return Content("更换图片时错误:" + ex.Message);
                throw;
            }

        }


        //个人提现
        public ActionResult Deposit(string userID, string companyID, decimal Summoney)
        {
            try
            {
                ////加密
                //ViewBag.userID = userID;
                //解密 
                userID = Base64MIMA.JIE(userID);
                companyID = Base64MIMA.JIE(companyID);
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
            try
            {
                string allUserID = getAllUserID(userID, "emp");//获取提取的所有BD2中ID
                //decimal SumMoney = getAllMoney(allUserID);//获取要提取的总金额
                decimal SumMoney = Summoney; ;//要提取的总金额
                string CompanyID = companyID;//公司ID
                string aa = sql.EditDataCommand("EXEC aboutTIJIAO " + userID + ",'" + allUserID + "'," + SumMoney + "," + companyID + "");
                if (aa == "0")
                {
                    return Content("提现成功，正在审核");
                }
                else
                {
                    return Content("未知错误，提现失败");
                }

            }
            catch (Exception)
            {

                return Content("提现失败");
            }

        }
        //部门提现
        public ActionResult depDeposit(string departID, string companyID)
        {
            try
            {
                ////加密
                //ViewBag.userID = userID;
                //解密 
                companyID = Base64MIMA.JIE(companyID);

            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
            try
            {
                /*  string departID = getDepartID(userID);*/ //获得部门ID
                string allUserID = getAllUserID(departID, "dep");//获取提取的所有BD2中ID
                decimal SumMoney = getAllMoney(allUserID);//获取要提取的总金额
                string CompanyID = companyID;//公司ID
                string aa = sql.EditDataCommand("EXEC departTIJIAO " + departID + ",'" + allUserID + "'," + SumMoney + "," + companyID + "");
                if (aa == "0")
                {
                    return Content("提现成功，正在审核");
                }
                else
                {
                    return Content("未知错误，提现失败");
                }

            }
            catch (Exception)
            {

                return Content("提现失败");
            }

        }
        /// <summary>
        /// 要提取的所有奖金id
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public string getAllUserID(string userID, string state)
        {
            string sqls = "";
            if (state == "emp")
            {
                sqls = string.Format("SELECT * FROM BonusData2 WHERE EarMan='{0}' AND IsGet=0 AND BonusType!=0", userID);
            }
            else if (state == "dep")
            {
                sqls = string.Format("SELECT * FROM BonusData2 WHERE EarMan='{0}' AND IsGet=0 AND BonusType=0", userID);
            }

            DataTable dt = sql.GetDataTableCommand(sqls);
            string BonusAllID = "";//要提取的所有奖金id
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BonusAllID = BonusAllID + "," + dt.Rows[i]["BonusDataID"].ToString();
                }
                BonusAllID = BonusAllID.Substring(1, BonusAllID.Length - 1);
            }
            else
            {
                BonusAllID = "";
            }

            return BonusAllID;
        }
        /// <summary>
        /// 获取提取的奖金总额
        /// </summary>
        /// <param name="bonusAllID">BD2要提取的id</param>
        /// <returns></returns>      
        public decimal getAllMoney(string bonusAllID)
        {
            decimal SumMoney = 0;
            try
            {
                string SMsqls = string.Format("SELECT ISNULL(SUM(EarMoney),0) SumMoney FROM dbo.BonusData2 WHERE BonusDataID IN ({0});", bonusAllID);
                DataTable SMdt = sql.GetDataTableCommand(SMsqls);
                if (SMdt.Rows.Count > 0)
                {
                    SumMoney = decimal.Parse(SMdt.Rows[0][0].ToString());
                }
                else
                {
                    SumMoney = 0;
                }

            }
            catch (Exception)
            {
                throw;
            }
            return SumMoney;
        }

        private void InitJsapi(string companyId)
        {
            log.AppenLog("companyId1" + companyId);
            companyId = Base64MIMA.JIE(companyId);
            log.AppenLog("companyId" + companyId);
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

        public ActionResult FAQ()
        {
            return View();
        }
        /// <summary>   
        /// 获取该用户所在的部门
        /// </summary>
        /// <returns></returns>
        public string getDepartID(string userID)
        {
            string depID = "";
            string sqls = string.Format("SELECT DepartID FROM dbo.Employee WHERE EmpID='{0}'", userID);
            DataTable ds = sql.GetDataTableCommand(sqls);
            if (ds.Rows.Count > 0)
            {
                depID = ds.Rows[0][0].ToString();
            }
            else
            {

            }

            return depID;
        }

        public ActionResult About()
        {
            return View();
        }

        #region 微信第三方应用
        /// <summary>
        /// 数据回调
        /// </summary>
        /// <param name="corpid"></param>
        /// <returns></returns>
        public ActionResult DataReturn(string corpid)
        {
            log.AppenLog("数据corpid:" + corpid);

            string userID = "";

            BonusHelper.AppConfig.sCorpID = corpid;
            try
            {
            }
            catch (Exception ex)
            {

                log.AppenLog("数据回调错误：" + ex.Message);
            }


            return Content(MathUrl());
        }

        /// <summary>
        /// 指令回调
        /// </summary>
        /// <param name="corpid">公司id</param>
        /// <returns></returns>
        public ActionResult ActionReturn(string corpid)
        {
            WXBizMsgCrypt wx = new WXBizMsgCrypt(BonusHelper.AppConfig.sToken, BonusHelper.AppConfig.sEncodingAESKey2, corpid);

            log.AppenLog("指令corpid:" + corpid);
            string sCorpID = Request.QueryString["corpid"];
            weixinStrct bs = new weixinStrct();
            if (sCorpID == "$CORPID$")
            {
                try
                {
                    //Timer ReadDBValue = new Timer(new TimerCallback(GetLongCode), null, 0, 60000);//从数据库中读数据到集合中
                    //log.AppenLog("ActionReturn:$CORPID$");
                    //log.AppenLog($"msg_signature:{Request.QueryString["msg_signature"]}");
                    //log.AppenLog($"timestamp:{Request.QueryString["timestamp"]}");
                    //log.AppenLog($"nonce:{Request.QueryString["nonce"]}");
                    //log.AppenLog("ActionReturn:1");
                    Stream requestStream = System.Web.HttpContext.Current.Request.InputStream;
                    byte[] requestByte = new byte[requestStream.Length];
                    requestStream.Read(requestByte, 0, (int)requestStream.Length);
                    string requestStr = Encoding.UTF8.GetString(requestByte);
                    //log.AppenLog("ActionReturn:2");
                    if (string.IsNullOrWhiteSpace(requestStr))
                    {
                        //log.AppenLog("ActionReturn:3");
                        log.AppenLog("ActionReturn:requestStr is null");
                    }
                    else
                    {
                        //log.AppenLog($"ActionReturn:4 {requestStr}");
                        XmlDocument requestDocXml = new XmlDocument();
                        requestDocXml.LoadXml(requestStr);
                        XmlElement rootElement = requestDocXml.DocumentElement;
                        string XML = "未知类型";
                        string sVerifyMsgSig = Request.QueryString["msg_signature"];
                        string sVerifyTimeStamp = Request.QueryString["timestamp"];
                        string sVerifyNonce = Request.QueryString["nonce"];
                        string sMsg = "";
                        int iresult = wx.DecryptMsg(sVerifyMsgSig, sVerifyTimeStamp, sVerifyNonce, requestStr, ref sMsg);

                        //log.AppenLog($"ActionReturn:5 {sMsg}");
                        requestDocXml = new XmlDocument();
                        requestDocXml.LoadXml(sMsg);
                        rootElement = requestDocXml.DocumentElement;
                        XmlNode node = rootElement.SelectSingleNode("SuiteTicket");
                        if (node != null)
                        {

                            log.AppenLog("推送suite_ticket:" + node.InnerText);
                            BonusHelper.AppConfig.SuiteTicket = node.InnerText;

                        }

                        XmlNode node2 = rootElement.SelectSingleNode("SuiteId");//第三方应用的id
                        if (node2 != null)
                        {
                            log.AppenLog("第三方应用id：" + node2.InnerText);
                            BonusHelper.AppConfig.SuiteId = node2.InnerText;
                            bs.corpId = node2.InnerText;
                        }
                        XmlNode node_AuthCode = rootElement.SelectSingleNode("AuthCode");//临时授权码
                        if (node_AuthCode != null)
                        {
                            //BonusHelper.AppConfig.node_AuthCode = node_AuthCode.InnerText;
                            //Session["node_AuthCode"] = node_AuthCode.InnerText;
                            log.AppenLog("临时授权码：" + node_AuthCode.InnerText);
                            bs.ShortCode = node_AuthCode.InnerText;
                        }

                        //string AuthCode = rootElement.SelectSingleNode("AuthCode").InnerText;//临时授权码
                        //string InfoType = rootElement.SelectSingleNode("InfoType").InnerText;
                        string TimeStamp = rootElement.SelectSingleNode("TimeStamp").InnerText;//时间戳
                        log.AppenLog("时间戳：" + TimeStamp);
                        log.AppenLog("ActionReturn:6");

                        Thread t = new Thread(new ParameterizedThreadStart(GetLongCode));
                        bs.SuiteTicket = BonusHelper.AppConfig.SuiteTicket;
                        t.Start(bs);
                    }
                }
                catch (Exception ex)
                {
                    log.AppenLog($"ActionReturn: Exception:{ex.Message}");
                }

                return Content("success");
            }
            else
            {
                BonusHelper.AppConfig.sCorpID = corpid;
                log.AppenLog("sss");
                return Content(MathUrl());
            }
        }

        /// <summary>
        /// 得到永久授权码
        /// </summary>
        /// <param name="rc"></param>
        private void GetLongCode(object rc)
        {
            weixinStrct bs = (weixinStrct)rc;
            string ShortCode = bs.ShortCode;
            string corId = bs.corpId;
            string SuiteTicket = bs.SuiteTicket;
            log.AppenLog("线程:ShortCode:" + ShortCode + "_corId:" + corId + "_SuiteTicket:" + SuiteTicket);
            try
            {
                #region

                //通过临时授权码得到永久授权码
                //得到第三方应用凭证
                string suite_access_token = AccessTokenHelper.IssuitResult(corId, BonusHelper.AppConfig.Corpsecret, SuiteTicket);
                log.AppenLog("第三方应用凭证：" + suite_access_token);
                #region 
                //ReturnJson rj = new ReturnJson();
                //if (!string.IsNullOrWhiteSpace(suitResult) && suitResult.Substring(0, 1) == "{" && suitResult.Substring(suitResult.Length - 1, 1) == "}")
                //{
                //    log.AppenLog("不为null");
                //    rj=JSON.parse<ReturnJson>(suitResult);
                //}
                //else
                //{
                //    log.AppenLog("为null");
                //    rj =new ReturnJson() { errmsg = string.IsNullOrWhiteSpace(suitResult) ? "error" : suitResult };
                //}
                #endregion
                //通过第三方应用凭证得到永久授权码
                Tuple<bool, LongShouquan> tu = GetLongCode(suite_access_token, ShortCode);
                if (tu.Item1)
                {
                    log.AppenLog("tu.Item1");
                    string sqlstr = string.Format($"exec proc_Weixin 'ZhuceCompany','{tu.Item2.auth_corp_info.corp_full_name}','{tu.Item2.auth_user_info.userid}','{tu.Item2.auth_corp_info.corp_square_logo_url}','{tu.Item2.permanent_code}','{tu.Item2.auth_corp_info.corpid}','{tu.Item2.auth_user_info.name}'");

                    log.AppenLog("Sqlstr:" + sqlstr);
                    //string str = "";
                    //Session[str] = tu.Item2;
                    string info = sql.EditDataCommand(sqlstr);
                    if (info == "0")
                    {
                        log.AppenLog("跟新成功");
                        //获取企业AccessToken  用于得到用户和部门

                        string Access_Token = AccessTokenHelper.GetQiye(tu.Item2.permanent_code.Replace("\"", ""), tu.Item2.auth_corp_info.corpid, string.Empty, DateTime.Now.AddDays(-1));
                        log.AppenLog("获取企业AccessToken：" + Access_Token);
                        if (Access_Token.Contains("access_token"))
                        {
                            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(QiYeaccess_token));
                            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(Access_Token));
                            QiYeaccess_token obj = (QiYeaccess_token)ser.ReadObject(ms);
                            string sqlstr2 = string.Format($"exec proc_Weixin 'UpdateAtoken','','','','','{tu.Item2.auth_corp_info.corpid}','','{obj.access_token}','{DateTime.Now.AddSeconds(int.Parse(obj.expires_in)).ToString("yyyy-MM-dd HH:mm:ss")}'");
                            string info2 = sql.EditDataCommand(sqlstr2);
                            BonusHelper.AppConfig.Access_Token_Qiye = obj.access_token;
                            BonusHelper.AppConfig.Qiye_YouXRQ = DateTime.Now.AddSeconds(int.Parse(obj.expires_in)).ToString("yyyy-MM-dd HH:mm:ss");

                        }


                    }
                    else
                    {
                        log.AppenLog("跟新失败");
                    }
                }
                else
                {
                    log.AppenLog("false");
                }


                #endregion
            }
            catch (Exception ex)
            {
                log.AppenLog("线程错误：" + ex.Message);

            }
            //通过永久和id得到token
        }

        /// <summary>
        /// 得到永久授权码
        /// </summary>
        /// <param name="suite_access_token">第三方凭证</param>
        /// <returns></returns>
        public Tuple<bool, LongShouquan> GetLongCode(string suite_access_token, string ShortCode)
        {
            try
            {
                string url = string.Format($"https://qyapi.weixin.qq.com/cgi-bin/service/get_permanent_code?suite_access_token=" + suite_access_token);
                string strpost = "{ \"auth_code\": \"" + ShortCode + "\"}";
                log.AppenLog("永久授权码：" + strpost);
                string strResult = PostRequest(url, strpost, Encoding.UTF8);
                log.AppenLog("返回永久授权码：" + strResult);
                if (strResult.Contains("permanent_code"))
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(LongShouquan));
                    MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(strResult));
                    LongShouquan obj = (LongShouquan)ser.ReadObject(ms);
                    log.AppenLog("access_token:" + obj.access_token);
                    log.AppenLog("permanent_code:" + obj.permanent_code);
                    log.AppenLog("corip:" + obj.auth_corp_info.corpid);
                    log.AppenLog("corp_full_name:" + obj.auth_corp_info.corp_full_name);

                    //得到这个人的联系方式  由于授权页面没有跳过去，所以没办法得到
                    //Guid2();

                    return new Tuple<bool, LongShouquan>(true, obj);
                }

                return new Tuple<bool, LongShouquan>(false, null);

            }
            catch (Exception ex)
            {

                return new Tuple<bool, LongShouquan>(false, null);

            }
        }

        /// <summary>
        /// post请求带参数
        /// </summary>
        /// <param name="url"></param>
        /// <param name="strpost"></param>
        /// <param name="dataEncode"></param>
        /// <returns></returns>
        public string PostRequest(string url, string strpost, Encoding dataEncode)
        {
            string ret = string.Empty;
            try
            {

                // byte[] byteArray = Encoding.GetBytes(strpost); //转化
                //log.AppenLog("临时1：" + strpost);
                byte[] byteArray = dataEncode.GetBytes(strpost);
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(url));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded;charset=utf-8";

                //  webReq.ContentType = "application/json";
                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();

                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
                ret = ex.Message;
            }
            return ret;
        }


        private string MathUrl()
        {
            string sCorpID = Request.QueryString["corpid"];
            WXBizMsgCrypt wxcpt = new WXBizMsgCrypt(BonusHelper.AppConfig.sToken, BonusHelper.AppConfig.sEncodingAESKey2, sCorpID);
            string sVerifyMsgSig = Request.QueryString["msg_signature"];
            string sVerifyTimeStamp = Request.QueryString["timestamp"];
            string sVerifyNonce = Request.QueryString["nonce"];
            string sVerifyEchoStr = Request.QueryString["echostr"];
            int ret = 0;
            string sEchoStr = "";
            ret = wxcpt.VerifyURL(sVerifyMsgSig, sVerifyTimeStamp, sVerifyNonce, sVerifyEchoStr, ref sEchoStr);
            if (ret != 0)
            {
                System.Console.WriteLine("ERR: VerifyURL fail, ret: " + ret);
                return "";
            }
            if (!string.IsNullOrEmpty(sEchoStr))
            {
                return sEchoStr;
            }
            log.AppenLog("sEchoStr:" + sEchoStr);
            return "";
        }
        #endregion
    }
}
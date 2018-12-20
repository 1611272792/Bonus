using Newtonsoft.Json;
using Sunpn_BonusWeb.BonusHelper;
using Sunpn_BonusWeb.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sunpn_BonusWeb.Controllers
{
    public class SendBonusController : Controller
    {
        private Sunpn.Data.SqlServer.SQLConnection sql = null;
        Log log = new Log();
        public SendBonusController()
        {
            sql = new Sunpn.Data.SqlServer.SQLConnection(BonusHelper.AppConfig.ConnString);
        }

        // GET: SendBonus
        public ActionResult Index(string spCodesTemp, string userID, string counts, string companyId)
        {
            ViewBag.companyId = companyId;

            InitJsapi(companyId);
            companyId = Base64MIMA.JIE(companyId);
            ViewBag.counts = int.Parse(Base64MIMA.JIE(counts));
            ViewBag.count = counts;//加密的
            string userid = Base64MIMA.JIE(userID);
            ViewBag.userid = userID;
            string sqlstr = string.Format("exec proc_BonusOutput 'SelectItem','{0}','{1}'", userid, companyId);

            DataSet ds = sql.GetDataSetCommand(sqlstr);
            List<BonusItems> BonusItemsList = new List<BonusItems>();
            if (ds?.Tables.Count > 0)
            {
                #region 奖金项

                if (ds?.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        BonusItems bi = new BonusItems();
                        bi.BonusItemsId = ds.Tables[0].Rows[i]["BonusItemID"].ToString();
                        bi.BonusName = ds.Tables[0].Rows[i]["BIName"].ToString();
                        bi.BonusMoney = double.Parse(ds.Tables[0].Rows[i]["sumRemainMoney"].ToString());
                        bi.BonusType = 1;//奖金项的
                        BonusItemsList.Add(bi);
                    }
                }
                if (ds?.Tables[1].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        BonusItems bi = new BonusItems();
                        bi.BonusItemsId = ds.Tables[1].Rows[i]["BonusItemID"].ToString();
                        bi.BonusName = ds.Tables[1].Rows[i]["BIName"].ToString();
                        bi.BonusMoney = double.Parse(ds.Tables[1].Rows[i]["sumRemainMoney"].ToString());
                        bi.BonusType = 2;//授权的
                        BonusItemsList.Add(bi);
                    }
                }
                //得到授权的奖金项

                if (BonusItemsList.Where(b => b.BonusType == 2).Count() > 0)
                {
                    //判断授权的可用余额是否大于这个奖金项的余额
                    foreach (var item in BonusItemsList.Where(b => b.BonusType == 2))
                    {
                        string ss = string.Format(@"select ISNULL(SUM(RemainMoney),0) re from RuleData 
where BonusItemID='{0}'
and EndDate>GETDATE()", item.BonusItemsId);
                        DataTable dt_ss = sql.GetDataTableCommand(ss);
                        if (dt_ss.Rows.Count > 0)
                        {
                            if (double.Parse(dt_ss.Rows[0]["re"].ToString()) < item.BonusMoney)
                            {
                                item.BonusMoney = double.Parse(dt_ss.Rows[0]["re"].ToString());
                            }
                        }
                    }
                }


                ViewBag.BonusItemsList = BonusItemsList;
                #endregion

                #region 奖金金额
                List<BonusParameter> BonusParamList = new List<BonusParameter>();
                if (ds?.Tables[2].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                    {
                        BonusParameter bp = new BonusParameter();
                        bp.BonusSetID = int.Parse(ds.Tables[2].Rows[i]["BonusSetID"].ToString());
                        bp.TypeName = ds.Tables[2].Rows[i]["TypeName"].ToString();
                        bp.BonusNum = int.Parse(ds.Tables[2].Rows[i]["BonusNum"].ToString());
                        BonusParamList.Add(bp);
                    }
                    ViewBag.BonusParamList = BonusParamList;
                }
                else
                {
                    ViewBag.BonusParamList = null;
                }
                #endregion

                #region 常用原因
                List<BonusReson> BonusResonList = new List<BonusReson>();
                if (ds.Tables[3].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                    {
                        BonusReson br = new BonusReson();
                        br.ID = long.Parse(ds.Tables[3].Rows[i]["ID"].ToString());
                        br.BonusResonName = ds.Tables[3].Rows[i]["BonusResonName"].ToString();
                        br.BonusResonUserId = ds.Tables[3].Rows[i]["BonusResonUserId"].ToString();
                        BonusResonList.Add(br);
                    }
                    ViewBag.BonusResonList = BonusResonList;
                }
                else
                {
                    ViewBag.BonusResonList = null;
                }
                #endregion
            }
            //通过userid得到公司id
            //string sqlstr222 = string.Format($"select CompanyID from Employee where EmpID='{userid}'");
            //DataTable dt222 = sql.GetDataTableCommand(sqlstr222);
            //if (dt222.Rows.Count > 0)
            //{
            //    ViewBag.companyId = Base64MIMA.JIA(dt222.Rows[0]["CompanyID"].ToString());
            //}
            //else
            //{
            //    //公司注册页面
            //    return Redirect("/CompanyRegist/Index?UserID=" + userid);
            //    //return Content("<script>alert('出现错误，请联系相关人员处理');history.go(-1);</script>");
            //}
            //companyId = dt222.Rows[0]["CompanyID"].ToString();
            //ViewBag.companyId = CompanyId;

            #region 得到本公司的部门和人员（不包括已离职的和本人）
            string sqlstr2 = string.Format($"exec proc_BonusOutput 'SelectUsers','{userid}',{companyId}");
            DataSet ds2 = sql.GetDataSetCommand(sqlstr2);
            if (ds2?.Tables.Count > 0)
            {
                #region 部门

                if (ds2?.Tables[0].Rows.Count > 0)
                {
                    ViewBag.depList = ds2.Tables[0];
                }
                else
                {
                    ViewBag.depList = null;
                }

                #endregion

                #region 人员
                if (ds2?.Tables[1].Rows.Count > 0)
                {
                    ViewBag.empList = ds2.Tables[1];
                }
                else
                {
                    ViewBag.empList = null;
                }
                #endregion
            }
            #endregion
            if (spCodesTemp == "" || spCodesTemp == null)
            {
                ViewBag.us = "";
            }
            else
            {
                ViewBag.us = Base64MIMA.JIE(spCodesTemp);
            }

            #region 人员
            ////判断是公司级还是部门级
            //string BonusItemID = "";
            //if (BonusItemsList.Count > 0)
            //{
            //    BonusItemID = BonusItemsList[0].BonusItemsId;
            //}
            //else
            //{
            //    BonusItemID = "00000000-0000-0000-0000-000000000000";
            //}
            ////string selectComOrDep = string.Format($"select * from BonusItem where BonusItemID='{BonusItemsList[0]}'");

            //string sqlstr22 = string.Format("exec proc_BonusOutput 'gongshiOrbumen','','{0}','','','{1}'", companyId, BonusItemID);
            //DataTable dt = sql.GetDataTableCommand(sqlstr22);
            //List<Users> userList = new List<Users>();
            //if (dt?.Rows.Count > 0)
            //{
            //    ViewBag.zimu = dt;

            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        Users us = new Users();
            //        us.Shou = dt.Rows[i]["py"].ToString();
            //        List<User> uList = new List<User>();
            //        string sqlstr23 = string.Format("select * from Employee where SpellJX like '{0}%' and CompanyID='{1}' and EmpID!='{2}'", dt.Rows[i]["py"].ToString(),companyId,userid);
            //        DataTable dt2 = sql.GetDataTableCommand(sqlstr23);
            //        if (dt2?.Rows.Count > 0)
            //        {
            //            for (int ii = 0; ii < dt2.Rows.Count; ii++)
            //            {
            //                User u = new Models.User();
            //                u.name = dt2.Rows[ii]["Name"].ToString();
            //                u.userid = dt2.Rows[ii]["EmpID"].ToString();
            //                u.photo = dt2.Rows[ii]["EmpPhotos"].ToString();
            //                u.isEmp= int.Parse(dt2.Rows[ii]["isEmp"].ToString());
            //                uList.Add(u);
            //            }
            //        }
            //        us.user = uList;
            //        userList.Add(us);
            //    }
            //}
            //else
            //{
            //    ViewBag.zimu = null;
            //}
            //ViewBag.userList = userList;
            #endregion
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

        public ActionResult Index2()
        {

            return View();
        }
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="serverId"></param>
        /// <returns></returns>
        public ActionResult UploadImge(string serverId, string companyid)//原图
        {
            log.AppenLog("上传图片");
            companyid = Base64MIMA.JIE(companyid);
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
                    Image img = imgH.GetImage(imgServerId, companyid);
                    // 2)存放本地
                    imageFilePath = imgH.SaveWeChatAttFileOfImage(img, "Sunpn");
                    rsFilePathList.Add(imageFilePath);
                }
                return Content(imageFilePath);
            }
            catch (Exception ex)
            {
                log.AppenLog(ex.Message);
                return Content(ex.Message);
            }


        }

        /// <summary>
        /// 选择人员
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectUsers(string companyId, string userid)
        {
            companyId = "1";
            ViewBag.companyId = companyId;
            userid = "zy";
            #region 得到本公司的部门和人员（不包括已离职的和本人）
            string sqlstr = string.Format($"exec proc_BonusOutput 'SelectUsers','{userid}',{companyId}");
            DataSet ds = sql.GetDataSetCommand(sqlstr);
            if (ds?.Tables.Count > 0)
            {
                #region 部门

                if (ds?.Tables[0].Rows.Count > 0)
                {
                    ViewBag.depList = ds.Tables[0];
                }
                else
                {
                    ViewBag.depList = null;
                }

                #endregion

                #region 人员
                if (ds?.Tables[1].Rows.Count > 0)
                {
                    ViewBag.empList = ds.Tables[1];
                }
                else
                {
                    ViewBag.empList = null;
                }
                #endregion
            }
            #endregion
            return View();
        }

        //选择人自动补全
        public ActionResult SelectUsersZidong(string keyword, string CompanyId)
        {
            try
            {
                ////加密
                //ViewBag.CompanyId = CompanyId;
                ////解密 
                //CompanyId = Base64MIMA.JIE(CompanyId);
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
            keyword = keyword.Trim().Replace(" ", "");
            string sqlserach = "";

            sqlserach = string.Format("select Name from dbo.Employee where (SpellJX like '%{0}%' or SpellQP like '%{1}%' or Name like '%{2}%') and CompanyId='{3}'", keyword, keyword, keyword, CompanyId);

            DataSet ds = sql.GetDataSetCommand(sqlserach);
            string info = JsonConvert.SerializeObject(ds);
            info = info.Replace("Table", "data").Replace("Name", "title");
            return Content(info);
        }

        public ActionResult SearchEmp(string Name, string CompanyID)
        {
            try
            {
                ////加密
                //ViewBag.CompanyId = CompanyID;
                ////解密 
                //CompanyID = Base64MIMA.JIE(CompanyID);
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
            string sqlEmp = "";
            if (Name == "")
            {
                sqlEmp = string.Format("select Name, EmpID from dbo.Employee where CompanyId='{0}'", CompanyID);
            }
            else
            {
                sqlEmp = string.Format("select Name, EmpID from dbo.Employee where (SpellJX like '%{0}%' or SpellQP like '%{1}%' or Name like '%{2}%') and CompanyId='{3}'", Name, Name, Name, CompanyID);
            }

            DataTable DepartName = sql.GetDataTableCommand(sqlEmp);
            if (DepartName.Rows.Count > 0)
            {
                ViewBag.dname = DepartName;
            }
            else
            {
                ViewBag.dname = null;
            }
            return PartialView("_EmpUsers2");
        }

        public ActionResult LuckPerson(string[] uid, string companyId, string BenrenID, string BonusItemID, string[] isEmp)
        {
            BenrenID = Base64MIMA.JIE(BenrenID);
            companyId = Base64MIMA.JIE(companyId);
            try
            {
                bool isok = true;
                //WriteLog("进来人" + uid);
                //var pid2 = uid.Split(',');
                var pid2 = uid;
                string info = "";
                #region
                //判断领奖人是否为本公司的人,或者不能给自己发奖金，如果不是，则不能发奖成功
                ////string sqlstr2 = string.Format($"exec proc_BonusOutput 'IsPeops','{BenrenID}','{companyId}','{uid}','','{BonusItemID}'");
                //string sqlstr2 = string.Format($"select EmpID,Name from Employee where CompanyID='{companyId}' and EmpID!='{BenrenID}'");
                //DataTable dtok = sql.GetDataTableCommand(sqlstr2);
                //string info = "";

                //List<string> jh = new List<string>();

                ////if (dt?.Rows.Count > 0)
                ////{
                ////    Isok = dt.Rows[0]["isok"].ToString();
                ////    if (Isok == "0")
                ////    {
                ////        return Content("nofind");
                ////    }

                ////    UserName = dt.Rows[0]["UserName"].ToString();
                ////    isp.Isok = Isok;
                ////    isp.UserName = UserName;
                ////    return Json(isp);
                ////}
                ////else
                ////{
                ////    return Content("nofind");
                ////}
                //if (dtok.Rows.Count > 0)
                //{

                //    for (int i = 0; i < dtok.Rows.Count; i++)
                //    {
                //        jh.Add(dtok.Rows[i]["EmpID"].ToString());

                //    }


                //}

                //for (int i = 0; i < pid2.Length; i++)
                //{
                //    //  WriteLog("第" + i + "个:" + pid2[i]);
                //    bool isContain = jh.Contains(pid2[i]);
                //    if (!isContain)
                //    {
                //        isok = false;
                //    }

                //}
                #endregion
                if (isok)
                {
                    //全部包含
                    info = "ok";
                    //通过工号得到你的姓名然后显示到表格上
                    //string sqlbyid = "select EmpID,Name from Employee where CompanyID='"+ companyId + "' and (EmpID ='" + pid2[0] + "'";
                    //string sqlbyid2 = "select DepartID EmpID,DepartName Name from Depart where CompanyID='" + companyId + "' and (DepartID ='" + pid2[0] + "'";
                    string sqlbyid = "select EmpID,Name from Employee where CompanyID='" + companyId + "' and EmpID in({0})";
                    string sqlbyid2 = "select CONVERT(nvarchar(50),DepartID) EmpID,DepartName Name from Depart where CompanyID='" + companyId + "' and DepartID in({0})";
                    List<isPer> data = new List<isPer>();
                    string sqlbyidPara = "";
                    string sqlbyidPara2 = "";
                    for (int i = 0; i < pid2.Length; i++)
                    {
                        if (int.Parse(isEmp[i]) == 1)
                        {
                            //sqlbyid += " or EmpID='" + pid2[i] + "'";
                            sqlbyidPara += "'" + pid2[i] + "',";


                        }
                        else
                        {
                            //sqlbyid2 += " or DepartID='" + pid2[i] + "'";
                            sqlbyidPara2 += "" + pid2[i] + ",";

                        }

                    }
                    if (sqlbyidPara != "")
                    {
                        sqlbyid = string.Format(sqlbyid, sqlbyidPara.Substring(0, sqlbyidPara.Length - 1));
                        data.AddRange(selectEmployee(sqlbyid, 1));
                    }
                    if (sqlbyidPara2 != "")
                    {
                        sqlbyid2 = string.Format(sqlbyid2, sqlbyidPara2.Substring(0, sqlbyidPara2.Length - 1));
                        data.AddRange(selectEmployee(sqlbyid2, 2));
                    }



                    if (data.Count > 0)
                    {
                        ViewBag.perlist = data;
                    }
                    else
                    {
                        ViewBag.perlist = null;
                    }

                }
                else
                {
                    info = "no";
                }


            }
            catch (Exception ex)
            {

                //WriteLog(ex.Message);

            }
            return PartialView("_PartialLuckPerson");
        }

        private List<isPer> selectEmployee(string sqlbyid, int isEmp)
        {
            //执行查询sql语句
            List<isPer> perlist = null;
            DataTable Luck_dt = sql.GetDataTableCommand(sqlbyid);
            if (Luck_dt.Rows.Count > 0)
            {
                if (isEmp == 1)
                {
                    //员工
                    perlist = (from s1 in Luck_dt.AsEnumerable()
                               select new isPer()
                               {
                                   Uid = s1.Field<string>("EmpID"),
                                   Name = s1.Field<string>("Name"),
                                   IsEmp = 1
                               }
                          ).ToList();
                }
                else
                {
                    //部门
                    perlist = (from s1 in Luck_dt.AsEnumerable()
                               select new isPer()
                               {
                                   Uid = s1.Field<string>("EmpID"),
                                   Name = s1.Field<string>("Name"),
                                   IsEmp = 2
                               }
                          ).ToList();
                }


            }

            return perlist;
        }



        /// <summary>
        /// 点击右边的加号多选人时更具奖金项出现相关的人
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="companyId"></param>
        /// <param name="BenrenID"></param>
        /// <param name="BonusItemID"></param>
        /// <returns></returns>
        public ActionResult LuckPersons(string companyId, string BenrenID, string BonusItemID)
        {
            companyId = Base64MIMA.JIE(companyId);
            BenrenID = Base64MIMA.JIE(BenrenID);
            
            
            //判断是公司级还是部门级
            string sqlstr22 = string.Format($"exec proc_BonusOutput 'gongshiOrbumen','{BenrenID}','{companyId}','','','{BonusItemID}'");
            DataTable dt = sql.GetDataTableCommand(sqlstr22);
            List<Users> userList = new List<Users>();
            if (dt?.Rows.Count > 0)
            {
                ViewBag.zimu = dt;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Users us = new Users();
                    us.Shou = dt.Rows[i]["py"].ToString();
                    List<User> uList = new List<User>();
                    string sqlstr23 = string.Format($"exec proc_BonusOutput 'IsShouyis','{BenrenID}','{companyId}','','{us.Shou}','{BonusItemID}'");
                    DataTable dt2 = sql.GetDataTableCommand(sqlstr23);
                    if (dt2?.Rows.Count > 0)
                    {
                        for (int ii = 0; ii < dt2.Rows.Count; ii++)
                        {
                            User u = new Models.User();
                            u.name = dt2.Rows[ii]["Name"].ToString();
                            u.userid = dt2.Rows[ii]["EmpID"].ToString();
                            u.photo = dt2.Rows[ii]["EmpPhotos"].ToString();
                            u.isEmp = int.Parse(dt2.Rows[ii]["isEmp"].ToString());
                            uList.Add(u);
                        }
                    }
                    us.user = uList;
                    userList.Add(us);
                }
            }
            else
            {
                ViewBag.zimu = null;
            }
            ViewBag.userList = userList;
            return PartialView("_PartialLuckPersons");
            //显示可以收到奖金的人
        }

        /// <summary>
        /// 添加常用原因
        /// </summary>
        /// <returns></returns>
        public ActionResult AddBonusReson(string CommReasons, string Userid, string companyId)
        {
            if (CommReasons == "")
            {
                return Content("原因不能为空");
            }
            if (Userid == "" || Userid == null)
            {
                return Content("网络错误，稍后请重试");
            }
            companyId = Base64MIMA.JIE(companyId);
            Userid = Base64MIMA.JIE(Userid);
            string sqlstr = string.Format($"insert into BonusReson values('{CommReasons}','{Userid}','{companyId}')");
            string info = sql.EditDataCommand(sqlstr);
            if (info == "0")
            {
                return Content("ok");
            }
            else
            {
                return Content("常用原因添加失败");
            }
        }

        /// <summary>
        /// 判断是否是同一家公司的人和不能给自己发
        /// </summary>
        /// <param name="DisMan">发奖金的人</param>
        /// <param name="GetMan">得到奖金的人</param>
        /// <returns></returns>
        public ActionResult IsPeopes(string DisMan, string GetMan, string compid, string BonusItemId)
        {
            DisMan = Base64MIMA.JIE(DisMan);
            compid = Base64MIMA.JIE(compid);
            IsPeop isp = new IsPeop();
            string Isok = "0";
            string UserName = "";
            try
            {
                string sqlstr = string.Format($"exec proc_BonusOutput 'IsPeops','{DisMan}','{compid}','{GetMan}','','{BonusItemId}'");
                log.AppenLog("判断人：" + sqlstr);
                DataTable dt = sql.GetDataTableCommand(sqlstr);
                if (dt?.Rows.Count > 0)
                {
                    Isok = dt.Rows[0]["isok"].ToString();
                    if (Isok == "0")
                    {
                        return Content("nofind");
                    }

                    UserName = dt.Rows[0]["UserName"].ToString();
                    isp.Isok = Isok;
                    isp.UserName = UserName;
                    isp.isEmp =int.Parse(dt.Rows[0]["isEmp"].ToString());
                    return Json(isp);
                }
                else
                {
                    return Content("nofind");
                }


            }
            catch (Exception ex)
            {
                log.AppenLog("BonusOutput的IsPeopes错误：" + ex.Message);
                return Content("nofind");
                throw;
            }
        }

        /// <summary>
        /// 加密处理
        /// </summary>
        /// <returns></returns>
        public ActionResult JiaMi(string spCodesTemp)
        {
            string jiami = Base64MIMA.JIA(spCodesTemp);
            return Content(jiami);
        }

        /// <summary>
        /// 发放奖金
        /// </summary>
        /// <param name="BonusOut"></param>
        /// <returns></returns>
        public ActionResult SendOutput(BonusOut BonusOut)
        {
            log.AppenLog("发放奖金");
            try
            {
                //发放人解密
                BonusOut.DisMan = Base64MIMA.JIE(BonusOut.DisMan);
                log.AppenLog("BonusOut.ResonContentImg：" + BonusOut.ResonContentImg);
                string BonusId = System.Guid.NewGuid().ToString();//原因guid
                BonusOut.ResonID = BonusId;
                //如果是部门id传进来，那么类型就是0，否则就是1
                //图片
                if (BonusOut.ResonContentImg != "" && BonusOut.ResonContentImg != null)
                {
                    log.AppenLog("BonusOut.ResonContentImg：" + BonusOut.ResonContentImg);
                    BonusOut.ResonContentImg = BonusOut.ResonContentImg.Substring(0, BonusOut.ResonContentImg.Length - 1);
                    //图片数量
                    BonusOut.ResonCount = BonusOut.ResonContentImg.Split(',').Count();
                }
                else
                {
                    log.AppenLog("BonusOut.ResonContentImg：" + BonusOut.ResonContentImg);
                    //图片数量
                    BonusOut.ResonCount = 0;
                }
                string sqlstr = string.Format($"exec proc_SendBonus 'SendBonus','{BonusOut.BonusItemId}','{BonusOut.DisMan}','{BonusOut.EarMan}',{BonusOut.EarMoney},{BonusOut.BonusType},{BonusOut.IsGet},'{BonusOut.ResonID}',{BonusOut.PersonCount},'{BonusOut.ResonContent}','{BonusOut.ResonContentImg}',{BonusOut.ResonCount},{0},{BonusOut.BonusTypes},{Base64MIMA.JIE(BonusOut.companyid)}");
                log.AppenLog("sqlstr发放奖金:" + sqlstr);
                string info = sql.EditDataCommand(sqlstr);
                if (info == "0")
                {
                    return Content("ok");
                }
                else
                {
                    return Content("奖金发放失败");
                }
            }
            catch (Exception ex)
            {
                return Content("发放奖金时遇到错误:" + ex.Message);
                throw;
            }
        }
        /// <summary>
        /// 罚
        /// </summary>
        /// <param name="BonusOut"></param>
        /// <returns></returns>
        public ActionResult SendFa(BonusOut BonusOut)
        {

            try
            {
                string BonusId = System.Guid.NewGuid().ToString();//原因guid
                BonusOut.ResonID = BonusId;
                //如果是部门id传进来，那么类型就是0，否则就是1
                //图片
                if (BonusOut.ResonContentImg != "" && BonusOut.ResonContentImg != null)
                {
                    log.AppenLog("BonusOut.ResonContentImg：" + BonusOut.ResonContentImg);
                    BonusOut.ResonContentImg = BonusOut.ResonContentImg.Substring(0, BonusOut.ResonContentImg.Length - 1);
                    //图片数量
                    BonusOut.ResonCount = BonusOut.ResonContentImg.Split(',').Count();
                }
                else
                {
                    log.AppenLog("BonusOut.ResonContentImg：" + BonusOut.ResonContentImg);
                    //图片数量
                    BonusOut.ResonCount = 0;
                }
                string sqlstr = string.Format($"exec proc_SendBonus 'BonusFa','{BonusOut.BonusItemId}','{Base64MIMA.JIE(BonusOut.DisMan)}','{BonusOut.EarMan}',{BonusOut.EarMoney},{BonusOut.BonusType},{BonusOut.IsGet},'{BonusOut.ResonID}',{BonusOut.PersonCount},'{BonusOut.ResonContent}','{BonusOut.ResonContentImg}',{BonusOut.ResonCount},0,0,{Base64MIMA.JIE(BonusOut.companyid)}");
                log.AppenLog("sqlstr发放奖金:" + sqlstr);
                string info = sql.EditDataCommand(sqlstr);
                if (info == "0")
                {
                    return Content("ok");
                }
                else
                {
                    return Content("罚奖金失败");
                }
            }
            catch (Exception ex)
            {
                return Content("罚奖金时遇到错误:" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 重新加载这个人的常用原因
        /// </summary>
        /// <param name="Userid">用户id</param>
        /// <returns></returns>
        public ActionResult ReloadBonusReson(string Userid, string companyId)
        {
            string str = string.Format("select * from BonusReson where BonusResonUserId='{0}' and companyId='{1}'", Base64MIMA.JIE(Userid), Base64MIMA.JIE(companyId));
            DataTable dt = sql.GetDataTableCommand(str);
            List<BonusReson> BonusResonList = new List<BonusReson>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BonusReson br = new BonusReson();
                    br.ID = long.Parse(dt.Rows[i]["ID"].ToString());
                    br.BonusResonName = dt.Rows[i]["BonusResonName"].ToString();
                    br.BonusResonUserId = dt.Rows[i]["BonusResonUserId"].ToString();
                    BonusResonList.Add(br);
                }
                ViewBag.BonusResonList = BonusResonList;

            }
            else
            {
                ViewBag.BonusResonList = null;
            }
            return PartialView("_PartialReBonus");
        }

        /// <summary>
        /// 自动补全
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPersons(string EmpId, string CompanyId)
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
            //string sqlstr = string.Format("select Name label,EmpID value from Employee where SpellJX like '%{0}%' and IsOut=0 and EmpID!='{1}' and CompanyID='{2}'", q, EmpId, CompanyId);
            string sqlstr = string.Format($"exec proc_BonusOutput 'zidong','{EmpId}','{CompanyId}','','{q}'");
            DataTable dt = sql.GetDataTableCommand(sqlstr);

            //WriteLog("sqlstr:" + sqlstr);
            //WriteLog("序列化：" + JsonConvert.SerializeObject(dt));
            return Content(JsonConvert.SerializeObject(dt));

        }

        public ActionResult IsShouyi(string BonusItemID, string companyId, string userid)
        {
            try
            {
                string sqlstr = string.Format($"exec proc_BonusOutput 'IsShouyi','{userid}','{Base64MIMA.JIE(companyId)}','','','{BonusItemID}'");
                DataTable dt = sql.GetDataTableCommand(sqlstr);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["isEmp"].ToString() == "1")
                    {
                        //员工
                        return Content("ok1");
                    }
                    else
                    {
                        //部门
                        return Content("ok2");
                    }
                    
                }
                else
                {
                    return Content("no");
                }

            }
            catch (Exception ex)
            {

                return Redirect("/ErrorPage/Index");
            }
        }


        /// <summary>
        /// //发放的时候最后判断
        /// </summary>
        /// <param name="str">人</param>
        /// <param name="str2">类型1人员，2部门</param>
        /// <param name="BItemId">奖金项id</param>
        /// <param name="CompId">公司id</param>
        /// <param name="benrenId">本人id</param>
        /// <returns></returns>
        public ActionResult panduan(string str, string str2, string BItemId, string CompId, string benrenId)
        {
            try
            {
                CompId = Base64MIMA.JIE(CompId);
                benrenId = Base64MIMA.JIE(benrenId);
                //1.判断这个奖金项是公司级还是部门级
                string sqlstr = string.Format($"select BIDepID,BIPrincipal from BonusItem where BonusItemID='{BItemId}'");
                DataTable dt = sql.GetDataTableCommand(sqlstr);
                var EStr = str.Split(',');
                var EStr2 = str2.Split(',');
                string fanhui = "";
                if (dt.Rows.Count > 0)
                {
                    //公司级奖金
                    #region 
                    if (dt.Rows[0][0].ToString() == "0")
                    {
                        //除了自己和这个奖金项的负责人不能发，不是同一公司的不能发以外，其他都能发
                        for (int i = 0; i < EStr.Length; i++)
                        {
                            //员工
                            string sqlstr2 = "";
                            if (EStr2[i] == "1")
                            {
                                sqlstr2 = string.Format($"select *from Employee where EmpID='{EStr[i]}' and CompanyID='{CompId}' and EmpID not in('{BItemId}','{dt.Rows[0][1].ToString()}')");
                            }
                            else
                            {
                                sqlstr2 = string.Format($"select * from Depart where DepartID={EStr[i]} and CompanyID='{CompId}'");
                            }
                            DataTable dt2 = sql.GetDataTableCommand(sqlstr2);
                            if (dt2.Rows.Count > 0)
                            {
                                //查的出来就代表这个人可以发
                            }
                            else
                            {
                                //查不出来代表这个人不可以发奖金
                                string sqlstr3 = string.Format($"select Name from Employee where EmpID='{EStr[i]}'");
                                DataTable dt3 = sql.GetDataTableCommand(sqlstr3);
                                if (dt3.Rows.Count > 0)
                                {
                                    fanhui += "员工：" + dt3.Rows[0][0].ToString() + "不属于该奖金项范围内；";
                                }
                                else
                                {
                                    fanhui = "工号：" + EStr[i] + "有误，仔细检查；";
                                }

                            }

                        }

                    }
                    #endregion
                    //部门级奖金
                    else
                    {
                        //不是这个奖金项部门下的人员，不是本人，不是奖金项负责人，不是这个公司的人不能发
                        for (int i = 0; i < EStr.Length; i++)
                        {
                            string sqlstr2 = string.Format($@"with T as(
    select DepartID from Depart  where DepartID = {dt.Rows[0][0].ToString()}
    union all
    select D.DepartID from Depart D, T
    where D.PID = T.DepartID AND CompanyID = '{CompId}')
    SELECT
EmpID, Name, EmpPhotos, CompanyID, 1 isEmp
    FROM dbo.Employee WHERE DepartID IN
   (SELECT DepartID from T) AND CompanyID = '{CompId}'
and EmpID not in('{dt.Rows[0][1].ToString()}','{benrenId}') and EmpID = '{EStr[i]}'");
                            DataTable dt2 = sql.GetDataTableCommand(sqlstr2);
                            if (dt2.Rows.Count > 0)
                            {
                                //查的出来代表这个人可以发
                            }
                            else
                            {
                                //代表这个人不能发
                                string sqlstr3 = string.Format($"select Name from Employee where EmpID='{EStr[i]}'");
                                DataTable dt3 = sql.GetDataTableCommand(sqlstr3);
                                if (dt3.Rows.Count > 0)
                                {
                                    fanhui += "员工：" + dt3.Rows[0][0].ToString() + "不属于该奖金项范围内；";
                                }
                                else
                                {
                                    fanhui = "工号：" + EStr[i] + "有误，仔细检查；";
                                }
                            }
                        }
                    }

                    return Content(fanhui);
                }
                else
                {
                    return Content("该奖金项失效");
                }
            }
            catch (Exception ex)
            {

                return Content("校验时发生错误:" + ex.Message);
            }
        }
    }
}
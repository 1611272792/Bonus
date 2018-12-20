using Sunpn_BonusWeb.BonusHelper;
using Sunpn_BonusWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Sunpn_BonusWeb.Controllers
{
    public class MainController : Controller
    {
        private Sunpn.Data.SqlServer.SQLConnection sql = null;
        Log log = new Log();
        public MainController()
        {
            sql = new Sunpn.Data.SqlServer.SQLConnection(BonusHelper.AppConfig.ConnString);
        }
        // GET: Main
        public ActionResult Index(string userId,string counts,string companyId)
        {
            try
            {
                ViewBag.count = counts;
                ViewBag.counts = int.Parse(Base64MIMA.JIE(counts));
                ViewBag.CompanyID=companyId;
                string userid = Base64MIMA.JIE(userId);
                companyId = Base64MIMA.JIE(companyId);
                #region 
                //string code1 = Request["code"];
                ////通过code得到userid    解决办法：把code根据时间来刷新，让他可以不停的访问哪个链接
                //log.AppenLog("code1:" + code1);
                //if (code1 != "")
                //{
                //    Tuple<bool, string> tu2 = BonusHelper.AccessTokenHelper.GetWechatUserInfo(code1);
                //    //Tuple<bool, string> tu2 = new Tuple<bool, string>(true,"");
                //    if (tu2.Item1 || Session["aaa"] != null)
                //    {
                //        if (Session["aaa"] == null)
                //        {
                //            log.AppenLog("if");
                //            userid = tu2.Item2;//得到userid
                //            Session["aaa"] = userid;
                //            // c.Value = tu2.Item2;
                //        }
                //        else
                //        {
                //            log.AppenLog("else");
                //            userid = Session["aaa"].ToString();
                //            //userid = c.Value;
                //        }

                //    }
                //}
                //else
                //{

                //}

                //userid = Session["aaa"].ToString();
                //userid = "qwezzz";

                //userid = Session["aaa"].ToString();
                //WriteLog("id:" + Session["aaa"].ToString());
                //userid = userid.Replace('"', ' ');
                //userid = userid.Trim();
                #endregion

                //查找员工所属
                ViewBag.userid = userId;
                Session["ComeUserID"] = userid;
                //看登录进来的人有没有公司，如果有公司就进入主界面
                #region
                //string[] canshu = new string[] { "SelectEmp", userid };
                //DataTable dt_canshu = sql.GetDataTableProcedure("proc_Main", canshu);
                //if (dt_canshu.Rows.Count > 0)
                //{
                //    //有这个人，判断是否有公司
                //    if (dt_canshu.Rows[0]["CompanyID"].ToString() == "")
                //    {
                //        //没有公司，需要注册公司
                //        return Redirect("/CompanyRegist/Index?UserID=" + userid);
                //    }
                //    else
                //    {
                //        //有公司 
                //        //用base64机密用户id和公司id
                //        ViewBag.userids = Convert.ToBase64String(Encoding.Default.GetBytes(userid));
                //        ViewBag.CompanyID = Convert.ToBase64String(Encoding.Default.GetBytes(dt_canshu.Rows[0]["CompanyID"].ToString()));


                //    }
                //}
                //else
                //{
                //    //没有这个人，需要注册公司
                //    return Redirect("/CompanyRegist/Index?UserID=" + userid);
                //}
                #endregion

                //通过userid得到公司id
                //string sqlstr22 = string.Format($"select CompanyID from Employee where EmpID='{userid}'");
                //DataTable dt22 = sql.GetDataTableCommand(sqlstr22);
                //if (dt22.Rows.Count > 0)
                //{
                //    ViewBag.CompanyID = Base64MIMA.JIA(dt22.Rows[0]["CompanyID"].ToString());
                //}
                //else
                //{
                //    //公司注册页面
                //    return Redirect("/CompanyRegist/Index?UserID=" + userId);
                //    //return Content("<script>alert('出现错误，请联系相关人员处理');history.go(-1);</script>");
                //}

                #region 查出所有父权限
                //查出所有父权限
                //string sqlstr = string.Format("select * from Author where PID is null");
                string sqlstr = string.Format("exec proc_Main 'SelectAuthorFi','{0}',0,'{1}'", userid, companyId);
                DataTable dt = sql.GetDataTableCommand(sqlstr);
                //根据父id得到子
                List<AuthorModellist> authorlist = new List<AuthorModellist>();
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AuthorModellist Authors = new AuthorModellist();
                        Authors.Id = dt.Rows[i]["AuthorID"].ToString();
                        Authors.Name = dt.Rows[i]["AuthorName"].ToString();
                        List<AuthorModel> amlist = new List<AuthorModel>();
                        string sqlstr2 = string.Format("exec proc_Main 'SelectAuthorzi','{0}',{1},'{2}'", userid, dt.Rows[i]["AuthorID"].ToString(),companyId);
                        DataTable dt2 = sql.GetDataTableCommand(sqlstr2);
                        if (dt2.Rows.Count > 0)
                        {

                            for (int ii = 0; ii < dt2.Rows.Count; ii++)
                            {
                                AuthorModel am = new AuthorModel();
                                am.AuthorId = dt2.Rows[ii]["AuthorID"].ToString();
                                am.AuthorName = dt2.Rows[ii]["AuthorName"].ToString();
                                am.AuthorUrl = dt2.Rows[ii]["AuthorUrl"].ToString();
                                am.AuthorIcons = dt2.Rows[ii]["AuthorIcon"].ToString();
                                amlist.Add(am);
                            }
                            Authors.listAuthor = amlist;

                        }

                        authorlist.Add(Authors);
                    }
                    ViewBag.authorlist = authorlist;

                }
                else
                {
                    ViewBag.authorlist = null;
                }
                #endregion
            }
            catch (Exception ex)
            {
                log.AppenLog("mainIndex错误:" + ex.Message);
                return Redirect("/ErrorPage/Index");
                
            }
            return View();
        }

        public ActionResult Guid()
        {
            try
            {
                //WriteLog(BouHelper.AppConfig.sCorpID);
                //num2++;
                //WriteLog("num2:" + num2);
                string str = $"https://open.weixin.qq.com/connect/oauth2/authorize?appid={BonusHelper.AppConfig.SuiteId}&redirect_uri={(Sunpn.WebMes.Helper.CommonDl.UrlEncode($"http://{BonusHelper.AppConfig.ServerUrl}/Wo/Index"))}&response_type=code&scope=snsapi_privateinfo&state=sunpn&#wechat_redirect";
                Session["userId"] = ""; Session["counts"] = ""; Session["companyId"] = "";
                //string str = $"https://open.weixin.qq.com/connect/oauth2/authorize?appid={BonusHelper.AppConfig.sCorpID}&redirect_uri={(Sunpn.WebMes.Helper.CommonDl.UrlEncode($"http://{BonusHelper.AppConfig.ServerUrl}/Wo/Index"))}&response_type=code&scope=snsapi_privateinfo&state=sunpn&#wechat_redirect";
                log.AppenLog("str:" + str);

                return Redirect(str);
            }
            catch (Exception ex)
            {
                log.AppenLog($"Index Error:{ex.Message}");
                return Content("no");
            }
        }

        public ActionResult Index2()
        {
            return View();
        }
    }
}
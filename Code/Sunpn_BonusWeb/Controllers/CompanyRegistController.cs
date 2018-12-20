using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Sunpn_BonusWeb.Models;
using Sunpn.Pinyin;
using Sunpn_BonusWeb.BonusHelper;
using System.Drawing;
using System.Collections;

namespace Sunpn_BonusWeb.Controllers
{
    public class CompanyRegistController : Controller
    {
        private Sunpn.Data.SqlServer.SQLConnection sql = null;
        Log log = new Log();
        public CompanyRegistController()
        {
            sql = new Sunpn.Data.SqlServer.SQLConnection(BonusHelper.AppConfig.ConnString);
        }
        // GET: CompanyRegist
        public ActionResult Index(string userID)
        {
            string UserJie = Base64MIMA.JIE(userID);
            log.AppenLog("公司用户id：" + UserJie);
            //先判断有没有公司id
            string isact = string.Format("SELECT CompanyID FROM dbo.Employee WHERE CompanyID IN (select CompanyID from dbo.Company) AND EmpID='{0}'", UserJie);
            DataTable dt = sql.GetDataTableCommand(isact);
            if (dt?.Rows.Count > 0)
            {
                //如果有公司则判断公司是否激活
                string iscodesql = string.Format(" SELECT IsTongguo,endDate FROM dbo.Company WHERE CompanyID='{0}'", dt.Rows[0][0].ToString());
                DataTable ct = sql.GetDataTableCommand(iscodesql);
                if (ct?.Rows[0]["IsTongguo"].ToString() == "0")
                {
                    if ((DateTime)ct?.Rows[0]["endDate"] >= DateTime.Now)
                    {
                        return Redirect("/Wo/Index?userID=" + userID);//如果已经激活则进入主页面
                    }
                    else
                    {
                        return Content("<script>alert('您的使用期限已过,请联系我们!');history.go(-1);</script>");
                    }
                }
                else
                {
                    return Content("<script>alert('您的公司已注册,但是暂未激活系统,请联系我们!');history.go(-1);</script>");
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult Activa()
        {
            return View();
        }

        #region 公司注册
        //public ActionResult Regist(string name, string pri, string tel, string email, string keycode)
        //{
        //    string addsql = "";
        //    string info = "";
        //    //先判断公司是否已经注册过
        //    string gs = string.Format("select CompanyName,KeyCode from dbo.Company where CompanyName='{0}'", name);
        //    DataTable dtgs = sql.GetDataTableCommand(gs);
        //    //公司已注册
        //    if (dtgs.Rows.Count > 0)
        //    {
        //        //如果公司已激活
        //        if (dtgs.Rows[0]["KeyCode"].ToString() != null&& dtgs.Rows[0]["KeyCode"].ToString() != "")
        //        {
        //            return Content("<script>alert('此公司已激活请勿重复注册');history.go(-1);</script>");
        //        }
        //        //没写邀请码情况
        //        if (keycode == "" || keycode == null)
        //        {
        //            return Content("<script>alert('此公司已被注册，但没有邀请码暂时无法使用此系统！');history.go(-1);</script>");
        //        }
        //        //填写了邀请码的情况
        //        else
        //        {
        //            try
        //            {
        //                Guid myguid = new Guid(keycode);
        //                string isin = string.Format("select keycode,state from dbo.KeyCode where keycode='{0}'", myguid);
        //                DataTable dt = sql.GetDataTableCommand(isin);
        //                //判断邀请码是否存在
        //                if (dt.Rows.Count > 0)
        //                {
        //                    //如果存在则判断邀请码是否被使用（0为未使用，1为已使用）
        //                    if (dt.Rows[0]["State"].ToString() == "1")
        //                    {
        //                        addsql = string.Format("update dbo.Company set CompanyPrincipal='{0}',CompanyPhone='{1}',CompanyEmail='{2}' where CompanyName='{3}'", pri, tel, email, name);
        //                        info = sql.EditDataCommand(addsql);
        //                        if (info == "0")
        //                        {
        //                            return Content("<script>alert('公司已被注册但是此邀请码已被使用,请填写有效邀请码');history.go(-1);</script>");
        //                        }
        //                        else
        //                        {
        //                            return Content("<script>alert('注册失败！');history.go(-1);</script>");
        //                        }
        //                    }
        //                    //邀请码未使用
        //                    else
        //                    {
        //                        addsql = string.Format("update dbo.Company set CompanyPrincipal='{0}',CompanyPhone='{1}',CompanyEmail='{2}',KeyCode='{4}' where CompanyName='{3}'", pri, tel, email, name,myguid);
        //                        info = sql.EditDataCommand(addsql);
        //                        //注册成功后改变此邀请码的状态
        //                        if (info == "0")
        //                        {
        //                            //如果是0则改变它的状态为1
        //                            string changestate = string.Format("update dbo.KeyCode set state=1 where keycode='{0}'", myguid);
        //                            string cs = sql.EditDataCommand(changestate);
        //                            if (cs == "0")
        //                            {
        //                                return Content("<script>alert('注册成功！');history.go(-1);</script>");
        //                            }
        //                            else
        //                            {
        //                                return Content("<script>alert('注册失败！');history.go(-1);</script>");
        //                            }
        //                        }
        //                        else
        //                        {
        //                            return Content("<script>alert('注册失败！');history.go(-1);</script>");
        //                        }
        //                    }
        //                }
        //                //邀请码不存在
        //                else
        //                {
        //                    return Content("<script>alert('此邀请码无效！');history.go(-1);</script>");
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                return Content("<script>alert(\"" + e.Message + "\");history.go(-1);</script>");
        //            }
        //        }
        //    }
        //    //公司未注册
        //    else
        //    {
        //        //无邀请码情况
        //        if (keycode == "" || keycode == null)
        //        {
        //            addsql = string.Format("INSERT INTO dbo.Company(CompanyName,CompanyPrincipal,CompanyPhone,CompanyEmail) VALUES ('{0}','{1}','{2}','{3}')", name, pri, tel, email);
        //            info = sql.EditDataCommand(addsql);
        //            if (info == "0")
        //            {

        //                string py = Pinyin.GetInitials(pri);
        //                string pinyin = Pinyin.GetPinyin(pri).Replace(" ", "");
        //                string addemp = string.Format("INSERT INTO dbo.Employee(EmpID, Name ,Sex ,Birth ,JoinDate ,EmpTel ,EmpEmail ,IsOut ,SpellJX,SpellQP,EmpPassword ,DepartID,PositionID)VALUES ('{0}','{1}',null,'{2}','{3}','{4}','{5}',{6},'{7}','{8}','{9}',null,null)", py, pri, null, null, tel, email, 0, py, pinyin, py);
        //                string add = sql.EditDataCommand(addemp);
        //                if (add == "0")
        //                {
        //                    return Content("<script>alert('注册成功，但没有邀请码暂时无法使用此系统！');history.go(-1);</script>");
        //                }
        //                else
        //                {
        //                    return Content("失败");
        //                }

        //                //return Content("<script>alert('注册成功，但没有邀请码暂时无法使用此系统！');history.go(-1);</script>");
        //            }
        //            else
        //            {
        //                return Content("<script>alert('注册失败！');history.go(-1);</script>");
        //            }
        //        }
        //        //有邀请码情况
        //        else
        //        {
        //            try
        //            {
        //                Guid myguid = new Guid(keycode);
        //                string isin = string.Format("select keycode,state from dbo.KeyCode where keycode='{0}'", myguid);
        //                DataTable dt = sql.GetDataTableCommand(isin);
        //                //判断邀请码是否存在
        //                if (dt.Rows.Count > 0)
        //                {
        //                    //如果存在则判断邀请码是否被使用（0为未使用，1为已使用）
        //                    if (dt.Rows[0]["State"].ToString() == "1")
        //                    {
        //                        addsql = string.Format("INSERT INTO dbo.Company(CompanyName,CompanyPrincipal,CompanyPhone,CompanyEmail)VALUES ('{0}','{1}','{2}','{3}')", name, pri, tel, email);
        //                        info = sql.EditDataCommand(addsql);
        //                        if (info == "0")
        //                        {

        //                            string py = Pinyin.GetInitials(pri);
        //                            string pinyin = Pinyin.GetPinyin(pri).Replace(" ", "");
        //                            string addemp = string.Format("INSERT INTO dbo.Employee(EmpID, Name ,Sex ,Birth ,JoinDate ,EmpTel ,EmpEmail ,IsOut ,SpellJX,SpellQP,EmpPassword ,DepartID,PositionID)VALUES ('{0}','{1}',null,'{2}','{3}','{4}','{5}',{6},'{7}','{8}','{9}',null,null)", py, pri, null, null, tel, email, 0, py, pinyin, py);
        //                            string add = sql.EditDataCommand(addemp);
        //                            if (add == "0")
        //                            {
        //                                return Content("<script>alert('注册成功！但是此邀请码已被使用,请重新填写邀请码');history.go(-1);</script>");
        //                            }
        //                            else
        //                            {
        //                                return Content("失败");
        //                            }

        //                            //return Content("<script>alert('注册成功！但是此邀请码已被使用,请重新填写邀请码');history.go(-1);</script>");
        //                        }
        //                        else
        //                        {
        //                            return Content("<script>alert('注册失败！');history.go(-1);</script>");
        //                        }
        //                    }
        //                    //邀请码未使用
        //                    else
        //                    {
        //                        addsql = string.Format("INSERT INTO dbo.Company(CompanyName,CompanyPrincipal,CompanyPhone,CompanyEmail,KeyCode)VALUES ('{0}','{1}','{2}','{3}','{4}')", name, pri, tel, email, myguid);
        //                        info = sql.EditDataCommand(addsql);
        //                        //注册成功后改变此邀请码的状态
        //                        if (info == "0")
        //                        {
        //                            //如果是0则改变它的状态为1
        //                            string changestate = string.Format("update dbo.KeyCode set state=1 where keycode='{0}'", myguid);
        //                            string cs = sql.EditDataCommand(changestate);
        //                            if (cs == "0")
        //                            {

        //                                string py = Pinyin.GetInitials(pri);
        //                                string pinyin = Pinyin.GetPinyin(pri).Replace(" ", "");
        //                                string addemp = string.Format("INSERT INTO dbo.Employee(EmpID, Name ,Sex ,Birth ,JoinDate ,EmpTel ,EmpEmail ,IsOut ,SpellJX,SpellQP,EmpPassword ,DepartID,PositionID)VALUES ('{10}','{0}',{1},'{2}','{3}','{4}','{5}',{6},'{11}','{12}','{7}',{8},{9})", pri, null, null, null, tel, email, 0, py, null, null, py, py, pinyin);
        //                                string add = sql.EditDataCommand(addemp);
        //                                if (add == "0")
        //                                {
        //                                    return Content("<script>alert('注册成功！');history.go(-1);</script>");
        //                                }
        //                                else
        //                                {
        //                                    return Content("失败");
        //                                }

        //                                //return Content("<script>alert('注册成功！');history.go(-1);</script>");
        //                            }
        //                            else
        //                            {
        //                                return Content("<script>alert('注册失败！');history.go(-1);</script>");
        //                            }
        //                        }
        //                        else
        //                        {
        //                            return Content("<script>alert('注册失败！');history.go(-1);</script>");
        //                        }
        //                    }
        //                }
        //                //邀请码不存在
        //                else
        //                {
        //                    return Content("<script>alert('此邀请码无效！');history.go(-1);</script>");
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                return Content("<script>alert(\"" + e.Message + "\");history.go(-1);</script>");
        //            }
        //        }
        //    }
        //}

        public ActionResult Regist(string name, string pri, string tel, string email)
        {
            DateTime validtime = DateTime.Now.AddDays(7);
            string addsql = string.Format("INSERT INTO dbo.Company(CompanyName,CompanyPrincipal,CompanyPhone,CompanyEmail,IsTongguo,endDate,beginDate) VALUES ('{0}','{1}','{2}','{3}',{4},'{5}','{6}')", name, pri, tel, email, 1, validtime, DateTime.Now);
            string add = sql.EditDataCommand(addsql);
            if (add == "0")
            {
                return Content("<script>alert('注册成功！请联系我们激活系统!');history.go(-1);</script>");
            }
            else
            {
                return Content("失败");
            }
        }
        #endregion

        #region 公司激活
        public ActionResult Activation(string name2, string keycode2)
        {
            string addsql = "";
            string info = "";
            //先判断公司是否已经注册过
            string gs = string.Format("select CompanyName,KeyCode from dbo.Company where CompanyName='{0}'", name2);
            DataTable dtgs = sql.GetDataTableCommand(gs);
            //公司已注册
            if (dtgs.Rows.Count > 0)
            {
                if (dtgs.Rows[0]["KeyCode"].ToString() != null && dtgs.Rows[0]["KeyCode"].ToString() != "")
                {
                    return Content("<script>alert('此公司已激活请勿重复注册');history.go(-1);</script>");
                }
                //没填写邀请码情况
                if (keycode2 == "" || keycode2 == null)
                {
                    return Content("<script>alert('请填写邀请码！');history.go(-1);</script>");
                }
                //填写了邀请码的情况
                else
                {
                    try
                    {
                        Guid myguid = new Guid(keycode2);
                        string isin = string.Format("select keycode,state from dbo.KeyCode where keycode='{0}'", myguid);
                        DataTable dt = sql.GetDataTableCommand(isin);
                        //判断邀请码是否存在
                        if (dt.Rows.Count > 0)
                        {
                            //如果存在则判断邀请码是否被使用（0为未使用，1为已使用）
                            if (dt.Rows[0]["State"].ToString() == "1")
                            {
                                return Content("<script>alert('此邀请码已被使用,请填写有效邀请码');history.go(-1);</script>");
                            }
                            //邀请码未使用
                            else
                            {
                                addsql = string.Format("update dbo.Company set KeyCode='{1}' where CompanyName='{0}'", name2, myguid);
                                info = sql.EditDataCommand(addsql);
                                //注册成功后改变此邀请码的状态
                                if (info == "0")
                                {
                                    //如果是0则改变它的状态为1
                                    string changestate = string.Format("update dbo.KeyCode set state=1 where keycode='{0}'", myguid);
                                    string cs = sql.EditDataCommand(changestate);
                                    if (cs == "0")
                                    {
                                        return Content("<script>alert('激活成功！');history.go(-1);</script>");
                                    }
                                    else
                                    {
                                        return Content("<script>alert('激活失败！');history.go(-1);</script>");
                                    }
                                }
                                else
                                {
                                    return Content("<script>alert('激活失败！');history.go(-1);</script>");
                                }
                            }
                        }
                        //邀请码不存在
                        else
                        {
                            return Content("<script>alert('此邀请码无效！');history.go(-1);</script>");
                        }
                    }
                    catch (Exception e)
                    {
                        return Content("<script>alert(\"" + e.Message + "\");history.go(-1);</script>");
                    }
                }
            }
            //公司未注册
            else
            {
                return Content("<script>alert('此公司暂未注册请先注册！');history.go(-1);</script>");
            }
        }
        #endregion

        public ActionResult CompanyManager(string CompanyID)
        {
            try
            {
                ////加密
                //ViewBag.userID = userID;
                //解密 
                CompanyID = Base64MIMA.JIE(CompanyID);

            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
            string infosql = string.Format("select Company.CompanyID,CompanyName,EmpID,Name,CompanyPhone,CompanyEmail,CompanyLogo,endDate,beginDate from Company LEFT JOIN dbo.Employee ON CompanyPrincipal=EmpID where Company.CompanyID='{0}'", CompanyID);
            DataTable info = sql.GetDataTableCommand(infosql);
            if (info?.Rows.Count > 0)
            {
                ViewBag.info = info;
            }
            else
            {
                ViewBag.info = null;
            }
            InitJsapi(Base64MIMA.JIA(CompanyID));
            return View();
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

        public ActionResult editSure(string CompanyID, string CompanyName, string CompanyPrincipal, string CompanyPhone, string CompanyEmail)
        {
            try
            {
                ////加密
                //ViewBag.userID = userID;
                //解密 
                CompanyID = Base64MIMA.JIE(CompanyID);

            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
            string upsql = string.Format("UPDATE dbo.Company SET CompanyName='{0}',CompanyPrincipal='{1}',CompanyPhone='{2}',CompanyEmail='{3}' where CompanyID='{4}'", CompanyName, CompanyPrincipal, CompanyPhone, CompanyEmail, CompanyID);
            string up = sql.EditDataCommand(upsql);
            if (up == "0")
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }


        //更换头像
        public ActionResult UpdateImg(string img, string CompanyID)
        {

            try
            {
                CompanyID = Base64MIMA.JIE(CompanyID);
                string[] a = img.Split('.');
                //BMP（位图）、JPG、JPEG、PNG、GIF
                if (a[1] == "jpg" || a[1] == "gif" || a[1] == "png" || a[1] == "JPEG" || a[1] == "BMP")
                {


                    string photosql = string.Format("update dbo.Company set CompanyLogo='{0}' where CompanyID='{1}'", img, CompanyID);
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

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="serverId"></param>
        /// <returns></returns>
        public ActionResult UploadImge(string serverId, string compid)//原图
        {
            log.AppenLog("上传图片");
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
                    Image img = imgH.GetImage(imgServerId,Base64MIMA.JIE(compid));
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

    }
}
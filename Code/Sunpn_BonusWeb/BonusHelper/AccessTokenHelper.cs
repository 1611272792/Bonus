using Sunpn.Http;
using Sunpn.WebMes.Helper;
using Sunpn_BonusWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;

namespace Sunpn_BonusWeb.BonusHelper
{
    public class AccessTokenHelper
    {
        private static Log log = new Log();
        private Sunpn.Data.SqlServer.SQLConnection sql = new Sunpn.Data.SqlServer.SQLConnection(BonusHelper.AppConfig.ConnString);

        //得到用户信息
        public static Tuple<bool, string> GetWechatUserInfo(string code)
        {
            log.AppenLog("得到用户信息：GetWechatUserInfo");
            try
            {
                if (Models.AppConfig.dicUserID.ContainsKey(code))
                {
                    return new Tuple<bool, string>(true, Models.AppConfig.dicUserID[code]);
                }

                string result = AccessTokenHelper.GetUserIDForCode(code);

                log.AppenLog("result:" + result);
                if (result.Contains("UserId"))
                {
                    result = result.Replace("{", "").Replace("}", "").Replace("{", "\"");

                    string[] strs = result.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    if (strs != null && strs.Length > 0)
                    {
                        strs = strs[0].Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                        if (strs != null && strs.Length > 1)
                        {
                            var v = (from s in Models.AppConfig.dicUserID
                                     where s.Value == strs[1]
                                     select s).ToList();
                            for (int a = v.Count - 1; a >= 0; a--)
                            {
                                Models.AppConfig.dicUserID.Remove(v[a].Key);
                            }
                            Models.AppConfig.dicUserID.Add(code, strs[1]);
                            return new Tuple<bool, string>(true, strs[1]);
                        }
                    }
                }
                return new Tuple<bool, string>(false, result);
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, ex.Message);
            }
        }
        //得到
        public static string GetUserIDForCode(string code)
        {

            //&agenid ={ 2}, "1"
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token={0}&code={1}", BonusHelper.AccessTokenHelper.IsExistAccess_Token(), code);
            // string url = $"https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token={AccessTokenHelper.IsExistAccess_Token()}&code={code}&agentid=1";
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = "GET";
            try
            {
                WebResponse wr = req.GetResponse();
                HttpWebResponse myResponse = (HttpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                string content = reader.ReadToEnd();
                return content;
            }
            catch (Exception ex)
            {
                //if(ex.Message== "{'errcode':40029,'errmsg':'invalid code, hint: [1511148065_6_ca556b8c143abe0c3306313e4bf98659]'}")
                //{
                //    return "";
                //}
                return ex.Message;
            }
        }

        ///第三方应用得到用户信息
        public static Tuple<bool, UserInfo> GetWechatUserInfo2(string code)
        {
            log.AppenLog("第三方得到用户信息：GetWechatUserInfo");
            try
            {
                if (Models.AppConfig.dicUserID2.ContainsKey(code))
                {
                    return new Tuple<bool, UserInfo>(true, Models.AppConfig.dicUserID2[code]);
                }

                string result = AccessTokenHelper.GetUserIDForCode2(code);

                log.AppenLog("result:" + result);
                UserInfo obj = null;
                if (result.Contains("UserId"))
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(UserInfo));
                    MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
                    obj = (UserInfo)ser.ReadObject(ms);
                    var v = (from s in Models.AppConfig.dicUserID2
                             where s.Value==obj
                             select s).ToList();
                    for (int a = v.Count - 1; a >= 0; a--)
                    {
                        Models.AppConfig.dicUserID2.Remove(v[a].Key);
                    }
                    Models.AppConfig.dicUserID2.Add(code, obj);
                    log.AppenLog("得到用户信息：" + obj.UserId + "公司id：" + obj.CorpId + "user_ticket：" + obj.user_ticket);

                }
                return new Tuple<bool, UserInfo>(false, obj);
            }
            catch (Exception ex)
            {
                return new Tuple<bool, UserInfo>(false, null);
            }
        }

        /// <summary>
        /// 第三方应用得到用户
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetUserIDForCode2(string code)
        {
            log.AppenLog("第三方");
            //&agenid ={ 2}, "1"
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/service/getuserinfo3rd?access_token={0}&code={1}", IssuitResult(AppConfig.SuiteId, AppConfig.Corpsecret, AppConfig.SuiteTicket), code);
            //var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token={0}&code={1}", Helpers.AccessTokenHelper.IsExistAccess_Token(), code);
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = "GET";
            try
            {
                WebResponse wr = req.GetResponse();
                HttpWebResponse myResponse = (HttpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                string content = reader.ReadToEnd();
                return content;
            }
            catch (Exception ex)
            {
                //if(ex.Message== "{'errcode':40029,'errmsg':'invalid code, hint: [1511148065_6_ca556b8c143abe0c3306313e4bf98659]'}")
                //{
                //    return "";
                //}
                return ex.Message;
            }
        }

        /// <summary>
        /// 第三方应用得到用户详情信息
        /// </summary>
        /// ticketc成员票据
        /// <returns></returns>
        public static string GetUserDetail2(string ticket)
        {
            #region 
            //            第三方使用user_ticket获取成员详情
            //            请求方式：POST（HTTPS）
            //请求地址：https://qyapi.weixin.qq.com/cgi-bin/service/getuserdetail3rd?access_token=SUITE_ACCESS_TOKEN

            //            请求包体：

            //{
            //                "user_ticket": "USER_TICKET"
            //}
            #endregion
            string url = $"https://qyapi.weixin.qq.com/cgi-bin/service/getuserdetail3rd?access_token={AppConfig.suite_access_token}";
            string canshu="{\"user_ticket\": \""+ticket+"\"}";
            string strResult= HttpPost.PostRequest(url, canshu, Encoding.UTF8);
            log.AppenLog("成员全部详情信息包括头像：" + strResult);
            return strResult;
        }

        //得到用户详情信息
        public static string GetUserDetail()
        {
            string ret = string.Empty;
            try
            {
                Encoding dataEncode = Encoding.UTF8;
                var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/getuserdetail?access_token={0}", BonusHelper.AccessTokenHelper.IsExistAccess_Token());
                string paramData = "{\"user_ticket\": \"" + AppConfig.JSAPI_Ticket + "\"}";
                log.AppenLog("请求参数:" + paramData);
                byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(url));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";

                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
                ret = sr.ReadToEnd();

                sr.Close();
                response.Close();
                newStream.Close();
                return ret;
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
            //var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/getuserdetail?access_token={0}", BonusHelper.AccessTokenHelper.IsExistAccess_Token());
            //HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            //req.Method = "POST";
            //req.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            //req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36";
            //string user_ticket = "USER_TICKET";
            //req.Headers.Add(user_ticket);
            //try
            //{

            //    WebResponse wr = req.GetResponse();
            //    HttpWebResponse myResponse = (HttpWebResponse)req.GetResponse();
            //    StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            //    string content = reader.ReadToEnd();
            //    return content;
            //}
            //catch (Exception ex)
            //{
            //    //if(ex.Message== "{'errcode':40029,'errmsg':'invalid code, hint: [1511148065_6_ca556b8c143abe0c3306313e4bf98659]'}")
            //    //{
            //    //    return "";
            //    //}
            //    return ex.Message;
            //}
        }

        /// <summary>
        /// 根据当前日期判断是否在有效期内
        /// </summary>
        /// <returns></returns>
        public static string IsExistAccess_Token()
        {
            string Token = string.Empty;
            DateTime YouXRQ;
            // 读取XML文件中的数据，并显示出来 ，注意文件路径  
            Token = AppConfig.Access_Token;
            log.AppenLog("Token:" + Token);
            log.AppenLog("Access_YouXRQ:" + AppConfig.Access_YouXRQ);
            YouXRQ = Convert.ToDateTime(AppConfig.Access_YouXRQ);
            log.AppenLog("YouXRQ:" + YouXRQ);
            if (DateTime.Now > YouXRQ)
            {
                try
                {
                    DateTime _youxrq = DateTime.Now;
                    Access_token mode = GetAccess_token();
                    _youxrq = _youxrq.AddSeconds(int.Parse(mode.expires_in));
                    Token = mode.access_token;
                }
                catch (Exception ex)
                {
                    WriteLog(ex.Message);
                }
            }
            return Token;
        }

        public static Access_token GetAccess_token()
        {
            log.AppenLog("获取普通token");
            string strUrl = "https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid=" + AppConfig.sCorpID + "&corpsecret=" + AppConfig.Corpsecret;
            
            log.AppenLog("获取普通token_URl:"+strUrl);
            Access_token mode = new Access_token();
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(strUrl);
            req.Method = "GET";
            try
            {
                WebResponse wr = req.GetResponse();
                HttpWebResponse myResponse = (HttpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                string content = reader.ReadToEnd();
                //TextHelper.Fun_WriteTxt("GetAccess_token：" + content);
                //Response.Write(content);  
                //在这里对Access_token 赋值  

                WriteLog("content" + content);
                Access_token token = new Access_token();
                // token =Sunpn.Json.JsonHelper.DeserializeJsonToObject<Access_token>(content);
                token = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType<Access_token>(content, token);
                WriteLog("json");
                mode.access_token = token.access_token;
                mode.expires_in = token.expires_in;

                AppConfig.Access_Token = mode.access_token;
                AppConfig.Access_YouXRQ = DateTime.Now.AddSeconds(int.Parse(mode.expires_in)).ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch (Exception ex)
            {
                WriteLog("GetAccess_token is error:" + ex.Message);
            }
            //TextHelper.Fun_WriteTxt(string.Format("GetAccess_token is Return, Access_Token:{0}  Expires_In:{1}", mode.access_token, mode.expires_in));
            return mode;
        }

        private static void WriteLog(string strmsg)
        {
            strmsg = $"{strmsg}\r\n";
            using (FileStream fs = System.IO.File.Open("D:\\2.txt", FileMode.Append, FileAccess.Write, FileShare.Write))
            {
                byte[] bys = Encoding.Default.GetBytes(strmsg);
                fs.Write(bys, 0, bys.Length);
                fs.Close();
            }
        }

        /// <summary>
        /// 得到企业AccessToken
        /// </summary>
        /// <param name="permanent_code">永久授权码</param>
        /// <returns></returns>
        public static string GetQiye(string permanent_code, string corip,string token_qiye,DateTime YouXRQ_qiye)
        {
            //判断企业token是否失效
            //string sqlstr = string.Format($"select * from Company where CompanyID='{corip}'");
            //DataTable dt = sql.GetDataTableCommand(sqlstr);
            //string token_qiye = string.Empty;
            //DateTime YouXRQ_qiye;
            //if (dt?.Rows.Count > 0)
            //{

          int i=  DateTime.Now.CompareTo(YouXRQ_qiye);
            log.AppenLog("i:" + i);
                if (i>0 || token_qiye == "")
                {
                    return Gerqi(corip, permanent_code);
                
                }
                else
                {
                    return token_qiye;
                }
            //}
            //else
            //{
            //    return "no";
            //}
        }

        public static string Gerqi(string corip, string permanent_code)
        {
            string suite_access_token= IssuitResult(AppConfig.SuiteId, AppConfig.Corpsecret, AppConfig.SuiteTicket);
            string url = $"https://qyapi.weixin.qq.com/cgi-bin/service/get_corp_token?suite_access_token={suite_access_token}";
            log.AppenLog("得到企业AccessToken：" + url);
            string cansu = "{\"auth_corpid\": \"" + corip + "\",\"permanent_code\": \"" + permanent_code + "\"}";
            log.AppenLog("canshu:" + cansu);
           
            return HttpPost.PostRequest(url, cansu, Encoding.UTF8);
        }

        private static HttpHelper httpHelp = new HttpHelper();
        /// <summary>
        /// 根据当前日期获取第三方应用凭证
        /// </summary>
        /// <returns></returns>
        public static string IssuitResult(string corId, string Corpsecret, string SuiteTicket)
        {
            log.AppenLog("根据日期获取第三方应用凭证");
            string token = string.Empty;
            try
            {
                DateTime YouXRQ;
                token = BonusHelper.AppConfig.suite_access_token;
                YouXRQ = Convert.ToDateTime(BonusHelper.AppConfig.suiteAccess_YouXRQ);
                if (DateTime.Now > YouXRQ)
                {
                    DateTime _youxrq = DateTime.Now;
                    Access_token mode = GetsuiteAccess_token(corId, Corpsecret, SuiteTicket);
                    _youxrq = _youxrq.AddSeconds(int.Parse(mode.expires_in));
                    token = mode.access_token;
                }
            }
            catch (Exception ex)
            {

                log.AppenLog("第三方授权失败");
            }
            log.AppenLog("页面：" + token);
            return token;
        }

        private static Access_token GetsuiteAccess_token(string corId, string Corpsecret, string SuiteTicket)
        {
            Access_token mode = new Access_token();
            try
            {

                //1.获取第三方应用凭证（suite_access_token）
                string suitUrl = $"https://qyapi.weixin.qq.com/cgi-bin/service/get_suite_token";
                string suitpost = "{ \"suite_id\": \"" + corId + "\",\"suite_secret\":\"" + Corpsecret + "\",\"suite_ticket\":\"" + SuiteTicket + "\"}";
                log.AppenLog("获取第三方应用凭证:" + suitpost);
                string suitResult = httpHelp.PostWebRequest(suitUrl, suitpost, Encoding.UTF8);
                log.AppenLog("获取第三方应用凭证返回：" + suitResult);
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(JsonDes));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(suitResult));
                JsonDes obj = (JsonDes)ser.ReadObject(ms);
                string suite_access_token = obj.suite_access_token;
                mode.access_token = suite_access_token;
                mode.expires_in = obj.expires_in;
                AppConfig.suite_access_token = suite_access_token;
                AppConfig.suiteAccess_YouXRQ = DateTime.Now.AddSeconds(int.Parse(mode.expires_in)).ToString("yyyy-MM-dd HH:mm:ss");

            }
            catch (Exception ex)
            {
                log.AppenLog("获取第三方应用凭证失败");

            }
            return mode;
        }

    }
}
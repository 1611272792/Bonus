using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Sunpn_BonusWeb.BonusHelper
{
    public class HttpPost
    {
        /// <summary>
        /// post请求带参数
        /// </summary>
        /// <param name="url"></param>
        /// <param name="strpost"></param>
        /// <param name="dataEncode"></param>
        /// <returns></returns>
        public static string PostRequest(string url, string strpost, Encoding dataEncode)
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
    }
}
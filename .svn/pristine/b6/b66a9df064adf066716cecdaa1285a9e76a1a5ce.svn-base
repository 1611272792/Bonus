using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace Sunpn_BonusWeb.BonusHelper
{
    public class ImgHelper
    {
        Log log = new Log();
        private Sunpn.Data.SqlServer.SQLConnection sql = new Sunpn.Data.SqlServer.SQLConnection(BonusHelper.AppConfig.ConnString);
        /// <summary>
        /// 保存微信图片附件
        /// <para>保存目录：UpFiles/WXAttFiles</para>
        /// </summary>
        /// <param name="img">图片对象</param>
        /// <param name="fileName">图片名称，eg:头像；注意不要加后缀</param>
        /// <returns>如果保存成功则返回文件的位置+保存的文件名;如果保存失败则返回空</returns>
        public string SaveWeChatAttFileOfImage(Image img, string fileName)
        {
            log.AppenLog("保存微信图片附件");
            try
            {
                fileName = fileName + "-" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";
                log.AppenLog("fileName:" + fileName);

                var filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/UploadImage/"), fileName);
                //WriteLog(filePath);
                // 检测是否存在文件夹，若不存在就建立。
                string directoryName = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }
                ImageCodecInfo icInfo = ImageCodecInfo.GetImageEncoders().Where(o => o.MimeType == "image/jpeg").FirstOrDefault();
                EncoderParameters epts = new EncoderParameters(1); // 压缩图片质量
                EncoderParameter ept = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 50L);
                epts.Param[0] = ept;
                img.Save(filePath, icInfo, epts);

                log.AppenLog("保存成功");

                //string[] arr = new string[num];
                //for (int i = 0; i < arr.Count(); i++)
                //{
                //    arr[i] = fileName;
                //}
                //Session["fileName"] = arr;
                return "/UploadImage/" + fileName;
            }
            catch (Exception ex)
            {
                log.AppenLog(ex.Message);
                return string.Empty;
            }
        }

        public Image GetImage(string imgServerId,string compid)
        {

            log.AppenLog("获取图片");
            //根据公司得到公司的长期授权码
            string sqlstr = string.Format($"select * from Company where CompanyID='{compid}'");
            DataTable dt = sql.GetDataTableCommand(sqlstr);

           
            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}",AccessTokenHelper.GetQiye(dt.Rows[0]["Longcode"].ToString(),compid, dt.Rows[0]["attoken"].ToString(), DateTime.Parse(dt.Rows[0]["expressYxq"].ToString())), imgServerId);
            try
            {


                // 1.创建httpWebRequest对象
                WebRequest webRequest = WebRequest.Create(url);
                //WriteLog("tryurl:" + url);
                HttpWebRequest httpRequest = webRequest as HttpWebRequest;
                //WriteLog("try2");
                // 2.填充httpWebRequest的基本信息
                httpRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                httpRequest.ContentType = "application/x-www-form-urlencoded";
                httpRequest.Method = "get";
                //WriteLog("try3");
                Stream responseStream = httpRequest.GetResponse().GetResponseStream();
                //WriteLog("try5");
                //WriteLog("responseStream" + responseStream);
                Image img = Image.FromStream(responseStream);

                //WriteLog("try4");
                responseStream.Close();
                return img;
            }
            catch (Exception ex)
            {
                log.AppenLog("获取图片时出错:" + ex.Message);
                throw ex;
            }
        }
    }
}
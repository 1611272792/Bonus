using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Sunpn.Pinyin;
using System.Text;
using Sunpn_BonusWeb.BonusHelper;
using Sunpn_BonusWeixin;
using Sunpn.Http;
using System.Runtime.Serialization.Json;
using Sunpn_BonusWeb.Models;
using System.IO;

namespace Sunpn_BonusWeb.Controllers
{
    public class DepartMangerController : Controller
    {
        private Sunpn.Data.SqlServer.SQLConnection sql = null;
        //WeixinHelper weixin;
        Log log = new Log();
        HttpHelper httpHelp = new HttpHelper();

        
        public DepartMangerController()
        {
            sql = new Sunpn.Data.SqlServer.SQLConnection(BonusHelper.AppConfig.ConnString);
            //InitWx();
        }
        //private void InitWx()
        //{
        //    string strurl = BonusHelper.AppConfig.ServerUrl;
        //    weixin = new WeixinHelper(strurl);
        //}
        // GET: DepartManger
        public ActionResult Index(string CompanyId)
        {
          
            try
            { 
                //加密
                ViewBag.CompanyId = CompanyId;
              //解密 
              CompanyId = Base64MIMA.JIE(CompanyId);
               
              //ViewBag.CompanyId = Base64MIMA.JIA(CompanyId);
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }

            string sqlDepart = string.Format($@" select d.DepartID,d.DepartName
 from Depart d where d.PID=0 and d.CompanyID='{CompanyId}'");

 //           string sqlDepart = string.Format(@"
 //SELECT dd.DepartID,dd.DepartName,dd.CompanyID,ISNULL(num,'0')SonNum  FROM dbo.Depart dd
 //INNER JOIN 
 //(SELECT d1.CompanyID, COUNT(*) num  FROM dbo.Depart d1 INNER JOIN dbo.Depart d2 ON d1.DepartID=d2.PID  WHERE d1.PID!=0 AND d1.CompanyID='{0}' GROUP BY d1.CompanyID ) dd2
 //ON dd.CompanyID=dd2.CompanyID
 //WHERE PID=0 AND  dd.CompanyID='{1}'", CompanyId,CompanyId);
            DataTable DepartName = sql.GetDataTableCommand(sqlDepart);

            List<Dep> depList = new List<Dep>();
            if (DepartName.Rows.Count > 0)
            {
                for (int i = 0; i < DepartName.Rows.Count; i++)
                {
                    Dep d = new Dep();
                    d.depId =int.Parse(DepartName.Rows[i]["DepartID"].ToString());
                    d.depName = DepartName.Rows[i]["DepartName"].ToString();
                    d.compid = CompanyId;
                    string sqlstr = string.Format($"select COUNT(*) from Depart d1 where d1.PID={d.depId} and d1.CompanyID='{CompanyId}'");
                    DataTable dt = sql.GetDataTableCommand(sqlstr);
                    d.sonCount =int.Parse(dt.Rows[0][0].ToString());
                    depList.Add(d);
                }
                ViewBag.dname = depList;
            }
            else
            {
                ViewBag.dname = null;
            }
            return View();
        }
        //子部门页面
        public ActionResult sonView(int depID,string CompanyId)
        {
            try
            {
                //解密 
                CompanyId = Encoding.Default.GetString(Convert.FromBase64String(CompanyId));
                //加密
                ViewBag.CompanyID = Convert.ToBase64String(Encoding.Default.GetBytes(CompanyId));
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
            //            string sqls = string.Format(@"
            //select dd.DepartName,dd.DepartID,ISNULL(num,'0')SonNum FROM Depart dd 
            //LEFT JOIN (SELECT  d.DepartName,d.DepartID,COUNT(*) num FROM dbo.Depart d LEFT JOIN dbo.Depart d1 ON d1.PID=d.DepartID WHERE d1.pid!=0  and d.CompanyID='{1}' GROUP BY d.DepartID,d.DepartName) d3 
            //ON dd.DepartID=d3.DepartID WHERE  PID='{0}' AND dd.CompanyID='{1}'", depID, CompanyId);
            string sqls = string.Format($@"select * from Depart where PID={depID} and CompanyID='{CompanyId}'");
            DataTable DepartName = sql.GetDataTableCommand(sqls);
            List<Dep> depList = new List<Dep>();
            if (DepartName.Rows.Count > 0)
            {
                for (int i = 0; i < DepartName.Rows.Count; i++)
                {
                    Dep d = new Dep();
                    d.depId = int.Parse(DepartName.Rows[i]["DepartID"].ToString());
                    d.depName = DepartName.Rows[i]["DepartName"].ToString();
                    d.compid = CompanyId;
                    string sqlstr = string.Format($"select COUNT(*) from Depart d1 where d1.PID={d.depId} and d1.CompanyID='{CompanyId}'");
                    DataTable dt = sql.GetDataTableCommand(sqlstr);
                    d.sonCount = int.Parse(dt.Rows[0][0].ToString());
                    depList.Add(d);
                }
                ViewBag.dname = depList;
            }
            else
            {
                ViewBag.dname = null;
            }
            return View();
        }

        //编辑页面
        public ActionResult EditView(int depID,string CompanyId)
        {  
            try
            {  
                //加密
                ViewBag.CompanyId = CompanyId;
                //解密 
                CompanyId = Base64MIMA.JIE(CompanyId);
              
               
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
          
            try
            {
              
                string sqls = string.Format("SELECT  DepartName,PID,Name,Remark,d.DepartID  FROM Depart d   LEFT JOIN Employee e ON e.EmpID = d.DepartPrincipal where d.DepartID={0} and d.CompanyID='{1}'", depID,CompanyId);
                DataTable showDep = sql.GetDataTableCommand(sqls);
                //showDep.Rows[0]["PID"] = shiftDepName(int.Parse(showDep.Rows[0]["PID"].ToString()));
                if (showDep.Rows.Count > 0)
                {
                    ViewBag.showDeps = showDep;
                }
                else
                {
                    ViewBag.showDeps = null;
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                string namesqls = string.Format("select DepartID,DepartName from Depart where CompanyId='{0}'",CompanyId);
                DataTable dt = sql.GetDataTableCommand(namesqls);
                if (dt.Rows.Count > 0)
                {
                    ViewBag.depName = dt;
                }
                else
                {
                    ViewBag.depName = null;
                }

            }




            //string DepName = showDep.Rows[0]["DepartName"].ToString();
            //string DepPID = shiftDepName(int.Parse(showDep.Rows[0]["PID"].ToString()));//id转换为名称
            //string DepPrincipal = showDep.Rows[0]["Name"].ToString();
            //string DepRemark = showDep.Rows[0]["Remark"].ToString();
            //string depID = showDep.Rows[0]["DepartID"].ToString();
            //return Content(DepName + "." + DepPID + "." + DepPrincipal + "." + DepRemark + "." + depID);
            return View();
        }
        //添加页面
        public ActionResult addDepart(string CompanyId)
        {
            try
            {
                //加密
                ViewBag.CompanyId = CompanyId;
                //解密 
                CompanyId = Base64MIMA.JIE(CompanyId);
                
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }

            try
            {
               
                string namesqls = string.Format("select DepartID,DepartName from Depart where CompanyId='{0}'",CompanyId);
                DataTable dt = sql.GetDataTableCommand(namesqls);
                if (dt.Rows.Count > 0)
                {
                    ViewBag.depName = dt;
                }
                else
                {
                    ViewBag.depName = null;
                }
               
            }
            catch (Exception)
            {

                throw;
            }

            return View();
        }

        
        /// <summary>
        /// 添加确认方法
        /// </summary>
        /// <param name="Depart">部门名称</param>
        /// <param name="UPdepart">上级部门</param>
        /// <param name="Resman">负责人</param>
        /// <param name="Reamrk">备注</param>
        /// <returns></returns>
        public ActionResult AddSure(string Depart, string UPdepart, string Resman, string Reamrk,string CompanyId) {
            try
            {
                //加密
                ViewBag.CompanyId = CompanyId;
                //解密 
                CompanyId = Base64MIMA.JIE(CompanyId);
              
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
            string JX = Pinyin.GetInitials(Depart);//简写
            string QP = Pinyin.GetPinyin(Depart).Replace(" ", "");//全拼
            try
            {
                if (string.IsNullOrEmpty(Resman))
                {
                    Resman = "";
                }
                else
                {
                   
                  
                    
                        Resman = shiftManID(Resman,CompanyId);//负责人转id
                        if (Resman == "404")
                        {
                            return Content("添加失败！负责人不属于公司员工");
                    }
                    //else
                    //{
                    //    string sqss = string.Format("SELECT DepartName FROM dbo.Depart WHERE DepartPrincipal='{0}'", Resman);
                    //    DataTable dss = sql.GetDataTableCommand(sqss);
                    //    if (dss.Rows.Count > 0)
                    //    {
                    //        return Content("添加失败！此人已是" + dss.Rows[0][0].ToString() + "负责人");
                    //    }
                    //}
                   
                 
                 
                }
                
                string insertAdd = string.Format("insert into Depart(DepartName, PID, DepartPrincipal, Remark,SpellJX,SpellQP,CompanyId) values('{0}', '{1}', '{2}', '{3}','{4}','{5}','{6}')", Depart, UPdepart, Resman, Reamrk,JX,QP,CompanyId);
                string num = sql.EditDataCommand(insertAdd);
                if (num == "0")
                {
                    //string sqlstr = string.Format($"select DepartID from Depart where CompanyID='{CompanyId}' and DepartName='{Depart}'");
                    //DataTable dt = sql.GetDataTableCommand(sqlstr);
                    //if (dt?.Rows.Count > 0)
                    //{
                    //    //先添加到微信后台，在添加到数据库
                    //    //ReturnJson rs = weixin.AddDep(Depart,int.Parse(UPdepart), 0, int.Parse(dt.Rows[0]["DepartID"].ToString()));
                    //    ReturnJson rs = CreateDep(Depart, int.Parse(UPdepart), 0, int.Parse(dt.Rows[0]["DepartID"].ToString()),CompanyId);

                    //    if (rs.errcode != "0")
                    //    {
                    //        //没有添加成功
                    //        return Content("未同步到微信后台");
                    //    }
                        

                    //}

                }
                
            }
            catch (Exception)
            {
                return Content("添加失败");
            }

            return Content("ok");
        }
         
        /// <summary>
        /// 确认编辑
        /// </summary>
        /// <param name="DepartName">部门名称</param>
        /// <param name="UpDep">上级部门</param>
        /// <param name="Resman">负责人</param>
        /// <param name="Remark">备注</param>
        /// <param name="DepID">部门id</param>
        /// <returns></returns>
        public ActionResult EidtDep(string DepartName,string UpDep,string Resman,string Remark,string DepID, string CompanyId)
        {
            try
            {
                //加密
                ViewBag.CompanyId = CompanyId;
                //解密 
                CompanyId = Base64MIMA.JIE(CompanyId);

            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
            try
            {
                if (string.IsNullOrEmpty(Resman))
                {
                    Resman = "";
                }
                else
                {
                    Resman = shiftManID(Resman, CompanyId);//负责人转换ID
                    if (Resman == "404")
                    {
                        return Content("编辑失败！负责人不属于公司员工");
                    }
                    //else
                    //{
                    //    string sqss = string.Format("SELECT DepartName FROM dbo.Depart WHERE DepartPrincipal='{0}'", Resman);
                    //    DataTable dss = sql.GetDataTableCommand(sqss);
                    //    if (dss.Rows.Count > 0)
                    //    {
                    //        return Content("添加失败！此人已是" + dss.Rows[0][0].ToString() + "负责人");
                    //    }
                    //}
                }
              
                string sqls = string.Format("update Depart set DepartName='{0}',PID={1},DepartPrincipal='{2}',Remark='{3}' where  DepartID={4}", DepartName, UpDep, Resman, Remark, DepID);
                string da = sql.EditDataCommand(sqls);
            }
            catch (Exception)
            {

                return Content("编辑失败");
            }
         

            return Content("编辑成功");
        }
       
        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="DepID">部门id</param>
        /// <returns></returns>
        public ActionResult DelDep(string DepID) {
            int Did = int.Parse(DepID);
            try
            {
                string selectUser = string.Format("SELECT COUNT(*) FROM dbo.Employee  WHERE DepartID={0} UNION ALL SELECT COUNT(*) FROM dbo.Depart WHERE PID={1}", DepID,DepID);
                DataTable ds = sql.GetDataTableCommand(selectUser);
                if (int.Parse(ds.Rows[0][0].ToString()) +int.Parse(ds.Rows[1][0].ToString()) >0)
                {
                    return Content("删除失败，该部门存在员工或者子部门");
                }
                else
                {
                    string sqls = string.Format("delete from Depart where DepartID={0}", Did);
                    string nums = sql.EditDataCommand(sqls);
                    return Content("删除成功");
                }
            }
            catch (Exception)
            {

                return Content("删除失败");
            }
        }
        
        /// <summary>
        /// 根据负责人名称转换ID
        /// </summary>
        /// <param name="manName">人名</param>
        /// <returns></returns>
        public string shiftManID(string manName,string comID)
        {

            string parDepname = "";
             
                string nsqls = string.Format("select EmpID from Employee where Name='{0}'and CompanyId='{1}'", manName,comID);
                try
                {
                    DataTable depID = sql.GetDataTableCommand(nsqls);
                    parDepname = depID.Rows[0][0].ToString();
                }
                catch (Exception)
                {

                    return "404";
                }
 

            return parDepname;
        }


        #region 微信
        /// <summary>
        /// 创建部门到微信中
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parentid"></param>
        /// <param name="order"></param>
        /// <param name="id"></param>
        /// <param name="compid"></param>
        /// <returns></returns>
        public ReturnJson CreateDep(string name, int parentid, int order, int id,string compid)
        {
            string sqlstr = string.Format($"select * from Company where CompanyID='{compid}'");
            DataTable dt = sql.GetDataTableCommand(sqlstr);
            string accecctoken2 = AccessTokenHelper.GetQiye(dt.Rows[0]["Longcode"].ToString(), compid, dt.Rows[0]["attoken"].ToString(), DateTime.Parse(dt.Rows[0]["expressYxq"].ToString()));
            string accecctoken = "q5hqUNPXpenqP8CYoBKIXk_O1BQeEgGPGbAFAH4RZ0tlwbHg7RfUnehzzTflsBrzgTZ3SAfQoTyw-3JNenL8ZwqbJ032lpIdlzg9mHfi0JFq3i5hngTrQ39cgaSlXBCdbuFjox6je9mOIPwIJsDmJHNbznpjvJENrq5cIWzehBHSiruAKatdqY0leae1kFWJqW_izuoDRo3VpUB1r1GYfA";
            if (accecctoken.Contains("access_token"))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(QiYeaccess_token));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(accecctoken));
                QiYeaccess_token obj = (QiYeaccess_token)ser.ReadObject(ms);
                string sqlstr2 = string.Format($"exec proc_Weixin 'UpdateAtoken','','','','','{compid}','','{obj.access_token}','{DateTime.Now.AddSeconds(int.Parse(obj.expires_in)).ToString("yyyy-MM-dd HH:mm:ss")}'");
                string info2 = sql.EditDataCommand(sqlstr2);
                BonusHelper.AppConfig.Access_Token_Qiye = obj.access_token;
                BonusHelper.AppConfig.Qiye_YouXRQ = DateTime.Now.AddSeconds(int.Parse(obj.expires_in)).ToString("yyyy-MM-dd HH:mm:ss");

            }
            //string accecctoken = "q5hqUNPXpenqP8CYoBKIXk_O1BQeEgGPGbAFAH4RZ0tlwbHg7RfUnehzzTflsBrzgTZ3SAfQoTyw-3JNenL8ZwqbJ032lpIdlzg9mHfi0JFq3i5hngTrQ39cgaSlXBCdbuFjox6je9mOIPwIJsDmJHNbznpjvJENrq5cIWzehBHSiruAKatdqY0leae1kFWJqW_izuoDRo3VpUB1r1GYfA";
            string url = $"https://qyapi.weixin.qq.com/cgi-bin/department/create?access_token={accecctoken}";
            string strpost = "{\"name\": \"" + name + "\",   \"parentid\": " + parentid + ",   \"order\": " + order + "}";
            string strResult = httpHelp.PostWebRequest(url, strpost, Encoding.UTF8);
            try
            {
                if (!string.IsNullOrWhiteSpace(strResult) && strResult.Substring(0, 1) == "{" && strResult.Substring(strResult.Length - 1, 1) == "}")
                {
                    return JSON.parse<ReturnJson>(strResult);
                }
                else
                {
                    return new ReturnJson() { errmsg = string.IsNullOrWhiteSpace(strResult) ? "error" : strResult };
                }
            }
            catch (Exception ex)
            {

                log.AppenLog("创建部门到微信中失败:" + ex.Message);
                return new ReturnJson() { errmsg = string.IsNullOrWhiteSpace(strResult) ? "error" : strResult };
            }

        }

        /// <summary>
        /// 获取指定公司部门可见信息
        /// </summary>
        /// <param name="compid">公司id</param>
        /// <returns></returns>
        public ActionResult SelectDep(string compid)
        {
            compid= Base64MIMA.JIE(compid);
            string sqlstr = string.Format($"select * from Company where CompanyID='{compid}'");
            DataTable dt = sql.GetDataTableCommand(sqlstr);
            string accecctoken = AccessTokenHelper.GetQiye(dt.Rows[0]["Longcode"].ToString(), compid, dt.Rows[0]["attoken"].ToString(), DateTime.Parse(dt.Rows[0]["expressYxq"].ToString()));
            QiYeaccess_token obj=new QiYeaccess_token();
            if (accecctoken.Contains("access_token"))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(QiYeaccess_token));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(accecctoken));
                obj = (QiYeaccess_token)ser.ReadObject(ms);
                string sqlstr2 = string.Format($"exec proc_Weixin 'UpdateAtoken','','','','','{compid}','','{obj.access_token}','{DateTime.Now.AddSeconds(int.Parse(obj.expires_in)).ToString("yyyy-MM-dd HH:mm:ss")}'");
                string info2 = sql.EditDataCommand(sqlstr2);
                accecctoken = obj.access_token;
            }
            //数据库里的部门
            string sqldep = string.Format($"select * from Depart where CompanyID='wx512ad5972960e003'");
            DataTable dtdep = sql.GetDataTableCommand(sqldep);

            string url = $"https://qyapi.weixin.qq.com/cgi-bin/department/list?access_token={accecctoken}&id=ID";
            string strResult = httpHelp.GetWebRequest(url);
            log.AppenLog("部门：" + strResult);
            try
            {
                if (!string.IsNullOrWhiteSpace(strResult) && strResult.Substring(0, 1) == "{" && strResult.Substring(strResult.Length - 1, 1) == "}")
                {
                    WechatReturnDep wrd= JSON.parse<WechatReturnDep>(strResult);
                    if (wrd.errcode == "0")
                    {
                        
                        List<WechatDep> listDep = wrd.department;
                        if (listDep.Count > 0)
                        {
                            string sqlstr2 = "";
                            #region 将数据库里的数据给修改过来，以微信为准
                            var vupdate = (from s in listDep
                                           where dtdep.AsEnumerable().Any(x => s.id == x.Field<int>("DepartID")&&compid==x.Field<string>("CompanyID") || s.name!=x.Field<string>("DepartName")||s.parentid!=x.Field<int>("PID"))
                                           select s).ToList();
                            log.AppenLog("vupdate:" + vupdate.Count);
                            if (vupdate.Count > 0)
                            {
                                foreach (var item in vupdate)
                                {
                                    string JX = Pinyin.GetInitials(item.name);//简写
                                    string QP = Pinyin.GetPinyin(item.name).Replace(" ", "");//全拼
                                    sqlstr2 += string.Format($"update Depart set SpellJX='{JX}',SpellQP='{QP}',DepartName='{item.name}',PID='{item.parentid}' where DepartID={item.id} and  CompanyID='{compid}' ");
                                }
                                
                            }
                            #endregion

                            #region 将数据库里有微信没有的删掉
                            var vupdel = (from s in dtdep.AsEnumerable()
                                          where !listDep.Any(x => x.id == s.Field<int>("DepartID")&&compid==s.Field<string>("CompanyID"))
                                           select s).ToList();
                            if (vupdel.Count > 0)
                            {
                                foreach (var item in vupdel)
                                {
                                    sqlstr2 += string.Format($"delete Depart where DepartID={item.Field<int>("DepartID")} and CompanyID='{compid}' ");
                                }

                            }
                            #endregion

                            #region 将数据库中没有，微信中有的增加
                            var vupadd = (from s in listDep
                                          where !dtdep.AsEnumerable().Any(x => s.id == x.Field<int>("DepartID")&&compid==x.Field<string>("CompanyID"))
                                          select s).ToList();
                            foreach (var item in vupadd)
                            {
                                //看数据库里是否又这个部门，如果有就不用添加到数据库


                                //else
                                //{
                                string JX = Pinyin.GetInitials(item.name);//简写
                                string QP = Pinyin.GetPinyin(item.name).Replace(" ", "");//全拼
                                sqlstr2 += string.Format($"insert into Depart(DepartID,DepartName, PID, DepartPrincipal, Remark,SpellJX,SpellQP,CompanyId) values({item.id},'{item.name}', '{item.parentid}', null, '','{JX}','{QP}','{compid}') ");

                                //}
                                //循环添加到数据库中

                            }
                            #endregion
                            if (sqlstr2 == "")
                            {
                                return Content("数据一致，不需要同步");
                            }
                            string num = sql.EditDataCommand(sqlstr2);
                            if (num == "0")
                            {
                                return Content("ok");
                            }
                            else
                            {
                                return Content("同步失败");
                            }
                        }
                        else
                        {
                            return Content("暂无信息需要同步");
                        }
                        
                        
                    }
                    else
                    {
                        return Content("error：" + strResult);
                    }
                }
                else
                {
                    return Content("error："+strResult);
                    //return new ReturnJson() { errmsg = string.IsNullOrWhiteSpace(strResult) ? "error" : strResult };
                }
            }
            catch (Exception ex)
            {

                log.AppenLog("从微信中读取数据失败:" + ex.Message);
                return Content("从微信中读取数据失败：" + strResult);
            }
        }

        #endregion

    }
}
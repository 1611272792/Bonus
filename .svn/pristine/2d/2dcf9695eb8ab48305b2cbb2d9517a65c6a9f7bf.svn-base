using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Sunpn_BonusWeb.BonusHelper;
using Sunpn_BonusWeb.Models;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using Sunpn.Http;
using Sunpn_BonusWeixin;
using Sunpn.Pinyin;

namespace Sunpn_BonusWeb.Controllers
{
    public class PositionManageController : Controller
    {
        private Sunpn.Data.SqlServer.SQLConnection sql = null;

        public PositionManageController()
        {
            sql = new Sunpn.Data.SqlServer.SQLConnection(BonusHelper.AppConfig.ConnString);
        }
        // GET: PositionManage
        public ActionResult Index(string CompanyId)
        {
            try
            {
                //解密 
                CompanyId = Base64MIMA.JIE(CompanyId);
                //加密
                ViewBag.CompanyId = Base64MIMA.JIA(CompanyId);
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }

            DataTable ds = getMainDep(CompanyId);//获取主部门
         
            if (ds.Rows.Count>0)
            {
                string sq = "";
                for (int i = 0; i < ds.Rows.Count; i++)
                {
                    sq = string.Format(@"with T as(
                        select DepartID from Depart  where DepartID={0} and CompanyId='{1}'
                        union all
                        select D.DepartID from Depart D,T  
                        where D.PID=T.DepartID  AND CompanyId='{3}')
                    SELECT  COUNT(*)  Sums  FROM dbo.Position  WHERE DepartID IN
                    (SELECT DepartID from T) AND CompanyId='{3}'", ds.Rows[i]["DepartID"], CompanyId, CompanyId, CompanyId);
                    //sq = string.Format($"select COUNT(*) from Position where DepartID={ds.Rows[i]["DepartID"].ToString()} and CompanyId='{CompanyId}'");
                    Session["shit" + i] = sql.GetDataTableCommand(sq).Rows[0][0];
                }
                ViewBag.DepName = ds;
            }
            else
            {
                ViewBag.DepName = null;
            }
            return View();
        }
        //子部门页面
        public ActionResult PosSonView(int depID,string CompanyId)
        {
            try
            {
                //解密 
                CompanyId = Base64MIMA.JIE(CompanyId);
                //加密
                ViewBag.CompanyId = Base64MIMA.JIA(CompanyId);
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
          
            string sqls = string.Format(" select DepartName,DepartID from Depart WHERE  PID='{0}' and CompanyId='{1}' ", depID, CompanyId);//查询子部门
            DataTable DepartName = sql.GetDataTableCommand(sqls);
            if (DepartName.Rows.Count > 0)
            {
                string ss = "";
                for (int i = 0; i < DepartName.Rows.Count; i++)
                {
                    //ss = string.Format($"select COUNT(*) from Position where DepartID={DepartName.Rows[i]["DepartID"].ToString()} and CompanyId='{CompanyId}'");
                    ss = string.Format(@"with T as(
    select DepartID from Depart  where DepartID={0} and CompanyId='{1}'
    union all
    select D.DepartID from Depart D,T  
    where D.PID=T.DepartID AND CompanyId='{2}')
SELECT  COUNT(*)  Sums  FROM dbo.Position  WHERE DepartID IN
(SELECT DepartID from T) AND CompanyId='{3}'", DepartName.Rows[i]["DepartID"], CompanyId, CompanyId, CompanyId);
                    Session["fuc" + i] = sql.GetDataTableCommand(ss).Rows[0][0];
                }
                ViewBag.dname = DepartName;
            }
            else
            {
                ViewBag.dname = null;
            }
            //string posSqls = string.Format(@"
            //SELECT  PositionName, PositionID, DepartName, Depart.DepartID  FROM dbo.Position INNER JOIN dbo.Depart on Depart.DepartID = Position.DepartID WHERE Position.DepartID = {0}", depID);
            //查询部门下所有职位
            //            string posSqls = string.Format(@"
            //           with T as(
            //    select DepartID from Depart  where DepartID={0} and CompanyId='{1}'
            //    union all
            //    select D.DepartID from Depart D,T  
            //    where D.PID=T.DepartID)
            //SELECT  PositionName, PositionID, DepartName, Depart.DepartID  FROM dbo.Position INNER JOIN dbo.Depart on Depart.DepartID = Position.DepartID WHERE Position.DepartID IN
            //(SELECT DepartID from T)", depID, CompanyId);
            string posSqls = string.Format($@"with T as(
    select DepartID from Depart  where DepartID={depID} AND CompanyID='{CompanyId}'
    union all
    select D.DepartID from Depart D,T  
    where D.PID=T.DepartID AND CompanyID='{CompanyId}'
)
SELECT * FROM dbo.Position WHERE DepartID IN
(SELECT DepartID from T) AND CompanyID='{CompanyId}';");
            DataTable pos = sql.GetDataTableCommand(posSqls);
       
            if (pos.Rows.Count>0)
            {
                ViewBag.posName = pos;
            }
            else
            {
                ViewBag.posName = null;
            }
            return View();
        }
        //编辑页面
        public ActionResult EditView(int posID,string CompanyId)
        {
            try
            {
                //解密 
                CompanyId = Base64MIMA.JIE(CompanyId);
                //加密
                ViewBag.CompanyId = Base64MIMA.JIA(CompanyId);
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
            try
            {
                string sqls = string.Format("SELECT  PositionName,PositionID,DepartName,Depart.DepartID,Position.Remark FROM dbo.Position INNER JOIN dbo.Depart on Depart.DepartID=Position.DepartID WHERE PositionID={0} and dbo.Depart.CompanyId='{1}'", posID, CompanyId);
                DataTable showPos = sql.GetDataTableCommand(sqls);
                //showDep.Rows[0]["PID"] = shiftDepName(int.Parse(showDep.Rows[0]["PID"].ToString()));
                if (showPos.Rows.Count > 0)
                {
                    ViewBag.showPos = showPos;
                }
                else
                {
                    ViewBag.showPos = null;
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                string namesqls = string.Format("select DepartID,DepartName from Depart where CompanyId='{0}'", CompanyId);
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

            return View();
        }
        //添加页面
        public ActionResult addPosition(string CompanyId)
        {
            try
            {
                //解密 
                CompanyId = Base64MIMA.JIE(CompanyId);
                //加密
                ViewBag.CompanyId = Base64MIMA.JIA(CompanyId);
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
            try
            {
                string namesqls = string.Format("select DepartID,DepartName from Depart  where CompanyId='{0}'",CompanyId);
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
        /// 添加确认
        /// </summary>
        /// <param name="PosName">职位名</param>
        /// <param name="DepartID">部门id</param>
        /// <param name="Reamrk">备注</param>
        /// <returns></returns>
        public ActionResult AddSure(string PosName, int DepartID, string Reamrk,string CompanyId)
        {
            try
            {
                //解密 
                CompanyId = Base64MIMA.JIE(CompanyId);
                //int depid = int.Parse(shiftDepID(departName));
                string isOr = shiftPosName(DepartID, PosName);
                if (isOr == "已存在")
                {
                    return Content("添加失败，该职位在该部门已存在");
                }
                string addSql = string.Format("insert into Position(PositionName,Remark,DepartID,CompanyId) values('{0}','{1}',{2},'{3}')", PosName, Reamrk, DepartID, CompanyId);
                sql.EditDataCommand(addSql);
            }
            catch (Exception)
            {
                return Content("添加失败");
            }

            return Content("添加成功");
        }


        /// <summary>
        /// 编辑确认
        /// </summary>
        /// <param name="PosName">职位名称</param>
        /// <param name="DepID">部门id</param>
        /// <param name="Remark">备注</param>
        /// <param name="PosID">职位id</param>
        /// <returns></returns>
        public ActionResult eidtPos(string PosName, string DepID, string Remark, string PosID) {
            try
            {
                //int depID = int.Parse(shiftDepID(DepartName));
                string eidtpos = string.Format("Update Position set PositionName='{0}',Remark='{1}',DepartID={2} where PositionID={3}", PosName, Remark, DepID, int.Parse(PosID));
                sql.EditDataCommand(eidtpos);
            }
            catch (Exception)
            {

                return Content("no");
            }
            return Content("编辑成功");
        }
      
        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="PosID">职位id</param>
        /// <returns></returns>
        public ActionResult delPosID(string PosID) {
            string del="";
            try
            {
                string delSql = string.Format("delete from Position where PositionID={0}", int.Parse(PosID));
                  del = sql.EditDataCommand(delSql);
            }
            catch (Exception)
            {

                return Content(del);
            }
           
            return Content("删除成功");
        }


        /// <summary>
        /// 查询所有主部门
        /// </summary>
        /// <returns></returns>
        public DataTable getMainDep(string CompanyId) {
            string depSql = string.Format("select DepartName,DepartID from Depart where PID=0 and CompanyId='{0}'",CompanyId);
            DataTable dedt = sql.GetDataTableCommand(depSql);
            return dedt;
        }
    
        
        /// <summary>
        /// 查看该部门的职位是否存在
        /// </summary>
        /// <param name="dID"></param>
        /// <param name="posName"></param>
        /// <returns></returns>
        public string shiftPosName(int dID,string posName)
        {
            string parDepname = "";
            string nsqls = string.Format("select PositionName from Position where PositionName='{0}' and DepartID={1}", posName, dID);
            try
            {
                DataTable depID = sql.GetDataTableCommand(nsqls);
                if (depID.Rows.Count>0)
                {
                    parDepname = "已存在";
                }
                 
            }
              
            catch (Exception)
            {
                return "异常";
            }
            return parDepname;
        }

        #region 微信
        HttpHelper httpHelp = new HttpHelper();
        public ActionResult SelectZw(string compid)
        {
            
            string accecctoken= GetQiyeToken.GetQiyeAttoken(compid);

            string url = $"https://qyapi.weixin.qq.com/cgi-bin/tag/list?access_token={accecctoken}";
            string strResult = httpHelp.GetWebRequest(url);
            try
            {
                if (!string.IsNullOrWhiteSpace(strResult) && strResult.Substring(0, 1) == "{" && strResult.Substring(strResult.Length - 1, 1) == "}")
                {
                    WechatReturnDep wrd = JSON.parse<WechatReturnDep>(strResult);
                    if (wrd.errcode == "0")
                    {
                        List<WechatDep> listDep = wrd.department;
                        if (listDep.Count > 0)
                        {
                            //string sqlstr2 = "";
                            //foreach (var item in listDep)
                            //{
                            //    //循环添加到数据库中
                            //    string JX = Pinyin.GetInitials(item.name);//简写
                            //    string QP = Pinyin.GetPinyin(item.name).Replace(" ", "");//全拼
                            //    sqlstr2 += string.Format($"insert into Depart(DepartID,DepartName, PID, DepartPrincipal, Remark,SpellJX,SpellQP,CompanyId) values({item.id},'{item.name}', '{item.parentid}', null, '','{JX}','{QP}','{compid}') ");

                            //}
                            //string num = sql.EditDataCommand(sqlstr2);
                            //if (num == "0")
                            //{
                            //    return Content("同步成功");
                            //}
                            //else
                            //{
                            //    return Content("同步失败");
                            //}
                            return Content("ok");
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
                    return Content("error：" + strResult);
                    //return new ReturnJson() { errmsg = string.IsNullOrWhiteSpace(strResult) ? "error" : strResult };
                }
            }
            catch (Exception ex)
            {

                //log.AppenLog("从微信中读取数据失败:" + ex.Message);
                return Content("从微信中读取数据失败：" + strResult);
            }
        }

        
        #endregion

    }
}
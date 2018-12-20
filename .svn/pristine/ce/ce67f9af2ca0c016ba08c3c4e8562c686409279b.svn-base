using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sunpn.Pinyin;
using System.Text;
using System.Globalization;
using Sunpn_BonusWeb.BonusHelper;
using Sunpn_BonusWeb.Models;
using Sunpn.Http;
using Sunpn_BonusWeixin;

namespace Sunpn_BonusWeb.Controllers
{
    public class UserManagerController : Controller
    {
        private Sunpn.Data.SqlServer.SQLConnection sql = null;

        public UserManagerController()
        {
            sql = new Sunpn.Data.SqlServer.SQLConnection(BonusHelper.AppConfig.ConnString);
        }

        // GET: UserManager
        public ActionResult Index(string CompanyID)
        {
            try
            {
                ViewBag.Companyid = CompanyID;
                //解密 
                CompanyID = Base64MIMA.JIE(CompanyID);
                //加密
                //ViewBag.Companyid = Base64MIMA.JIA(CompanyID);
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
            //选择部门
            string sqlDepart = string.Format("select DepartID,DepartName from Depart WHERE  PID=0 and CompanyID='{0}'", CompanyID);
            //string sqlDepart = string.Format("SELECT COUNT(EmpID) Num,DepartName,Depart.DepartID FROM dbo.Employee RIGHT JOIN dbo.Depart ON Depart.DepartID = Employee.DepartID WHERE PID=0 AND Depart.CompanyID={0} GROUP BY DepartName,Depart.DepartID",CompanyID);
            DataTable Depart = sql.GetDataTableCommand(sqlDepart);
            if (Depart.Rows.Count > 0)
            {
                for (int i = 0; i < Depart.Rows.Count; i++)
                {
                    string numsql = string.Format(@"with T as(
    select DepartID from Depart WHERE DepartID='{0}' AND CompanyID='{1}'
    union all
    select D.DepartID from Depart D,T  
    where D.PID=T.DepartID AND CompanyID='{2}'
)
SELECT COUNT(*) Num FROM dbo.Employee WHERE DepartID IN
(SELECT DepartID from T) AND CompanyID='{2}';
", Depart.Rows[i]["DepartID"],CompanyID, CompanyID);
                    Session["bottshit" + i] = sql.GetDataTableCommand(numsql).Rows[0][0];
                }

                ViewBag.dname = Depart;
            }
            else
            {
                ViewBag.dname = null;
            }
            return View();
        }

        //public ActionResult SelectPosition(int dep)
        //{ 
        //    //选择职位
        //    string sqlPosition = string.Format("select PositionID,PositionName from Position where DepartID={0}", dep);
        //    DataTable Position = sql.GetDataTableCommand(sqlPosition);
        //    if (Position.Rows.Count > 0)
        //    {
        //        ViewBag.pname = Position;
        //    }
        //    return Content("111");
        //}


        public ActionResult Detail(int DepartID,string CompanyID)
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
            //查询它下面的子部门
            string sqls = string.Format(" select DepartName,DepartID from Depart WHERE  PID='{0}' and CompanyID='{1}'", DepartID, CompanyID);
            //string sqls = string.Format("SELECT COUNT(EmpID) Num,DepartName,Depart.DepartID FROM dbo.Employee RIGHT JOIN dbo.Depart ON Depart.DepartID = Employee.DepartID WHERE PID={0} AND Depart.CompanyID={1} GROUP BY DepartName,Depart.DepartID", DepartID, CompanyID);           
            DataTable DepartName = sql.GetDataTableCommand(sqls);
            if (DepartName.Rows.Count > 0)
            {
                for (int i = 0; i < DepartName.Rows.Count; i++)
                {
                    string numsql = string.Format(@"with T as(
    select DepartID from Depart WHERE DepartID={0} AND CompanyID='{1}'
    union all
    select D.DepartID from Depart D,T  
    where D.PID=T.DepartID  AND CompanyID='{2}'  
)
SELECT COUNT(*) Num FROM dbo.Employee WHERE DepartID IN
(SELECT DepartID from T) AND CompanyID='{2}';
", DepartName.Rows[i]["DepartID"], CompanyID, CompanyID);
                    Session["Num" + i] = sql.GetDataTableCommand(numsql).Rows[0][0];
                }
                ViewBag.dname = DepartName;
            }
            else
            {
                ViewBag.dname = null;
            }
            //选择员工
            string sqlEmp = string.Format("with T as(select DepartID from Depart  where DepartID ='{0}' AND CompanyID = '{1}' union all select D.DepartID from Depart D, T where D.PID = T.DepartID)SELECT EmpID,DepartID,EmpPhotos,Name FROM dbo.Employee WHERE DepartID IN(SELECT DepartID from T) AND CompanyID = '{2}'", DepartID, CompanyID, CompanyID);
            DataTable emp = sql.GetDataTableCommand(sqlEmp);
            if (emp.Rows.Count>0)
            {
                ViewBag.einfo = emp;
            }
            return View();
        }

        public ActionResult Info(string EmpID,string CompanyID)
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
            //选择部门            
            string sqlDepart = string.Format("select DepartID,DepartName from Depart where CompanyID='{0}'",CompanyID);
            DataTable DepartName = sql.GetDataTableCommand(sqlDepart);
            if (DepartName.Rows.Count > 0)
            {
                ViewBag.dname = DepartName;
            }
            //选择权限
            string sqlrole = string.Format("select RoleID, RoleName from dbo.Role where CompanyID='{0}'", CompanyID);
            DataTable Role = sql.GetDataTableCommand(sqlrole);
            if (Role.Rows.Count > 0)
            {
                ViewBag.role = Role;
            }
            else
            {
                ViewBag.role = null;
            }
            //职位
            string sqlPosi = string.Format("SELECT PositionID,PositionName FROM dbo.Position WHERE DepartID=(SELECT DepartID FROM dbo.Employee WHERE EmpID='{0}' AND CompanyID='{1}') AND CompanyId='{1}'", EmpID, CompanyID);
            DataTable PosiName = sql.GetDataTableCommand(sqlPosi);
            if (PosiName.Rows.Count > 0)
            {
                ViewBag.pname = PosiName;
            }
            //员工信息
            //string sqlEmp = string.Format("SELECT EmpID,Name, CASE WHEN sex = '1' THEN '男' WHEN sex = '0' THEN '女' ELSE '其他' END AS Sex,Birth,JoinDate,EmpTel,EmpEmail,CASE WHEN IsOut=0 THEN '未离职' WHEN IsOut=1 THEN '已离职' ELSE '其他' END AS IsOut,SpellJX,SpellQP,EmpPassword,DepartName,PositionName FROM dbo.Employee e LEFT JOIN dbo.Depart d ON d.DepartID = e.DepartID LEFT JOIN dbo.Position p ON e.PositionID=p.PositionID where EmpID='{0}'", EmpID);
            string sqlEmp = string.Format("SELECT *  from dbo.Employee where EmpID='{0}' and CompanyID='{1}'", EmpID,CompanyID);
            DataTable emp = sql.GetDataTableCommand(sqlEmp);
            if (emp.Rows.Count > 0)
            {
                ViewBag.empinfo = emp;
            }
            return View();
        }

         
        public string SelectPosition(int DepartID)
        {
            //选择职位
            string sqlPosition = string.Format("select PositionID,PositionName from Position where DepartID={0}",DepartID);
            DataTable Position = sql.GetDataTableCommand(sqlPosition);
            string Json = "";
            if (Position.Rows.Count > 0)
            {
              Json = JsonConvert.SerializeObject(Position);
            }
            return Json;
        }

        //增加
        public ActionResult AddSure(string id,string name,int sex,DateTime bir, DateTime joindate,string tel, string email,int isout,string password,string depart,string posi,string CompanyID,string Role)
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
            string isinsql = string.Format("select EmpID from dbo.Employee where EmpID='{0}' and CompanyID='{1}'",id,CompanyID);
            DataTable isin = sql.GetDataTableCommand(isinsql);
            if (isin?.Rows.Count>0)
            {
                return Content("have");
            }
            else
            {
                string py = Pinyin.GetInitials(name);
                string pinyin = Pinyin.GetPinyin(name).Replace(" ", "");
                

                string addsql = string.Format("INSERT INTO dbo.Employee(EmpID, Name ,Sex ,Birth ,JoinDate ,EmpTel ,EmpEmail ,IsOut ,SpellJX,SpellQP,EmpPassword ,DepartID,PositionID,CompanyID,RoleID)VALUES ('{10}','{0}',{1},'{2}','{3}','{4}','{5}',{6},'{11}','{12}','{7}',{8},{9},'{13}','{14}')", name, sex, bir, joindate, tel, email, isout, password, depart, posi, id, py, pinyin,CompanyID,Role);
                string add = sql.EditDataCommand(addsql);
                if (add == "0")
                {
                    return Content("ok");
                }
                else
                {
                    return Content("no");
                }
            }
        }

        //删除
        public ActionResult Delete(string EmpID,string CompanyID)
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
            string delsql = string.Format("delete from dbo.Employee where EmpID='{0}' and CompanyID='{1}'",EmpID,CompanyID);
            string del = sql.EditDataCommand(delsql);
            if (del == "0")
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }

        //得到编辑的内容
        public ActionResult GetEmp(string EmpID,string CompanyID)
        {
            try
            {
                //解密 
                CompanyID = Base64MIMA.JIE(CompanyID);
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
            //选择员工
            string sqlEmp = string.Format("select * from Employee where EmpID='{0}' and CompanyID='{1}'", EmpID,CompanyID);
            DataTable emp = sql.GetDataTableCommand(sqlEmp);
            if (emp.Rows.Count > 0)
            {
                string name = emp.Rows[0]["Name"].ToString();
                int sex = int.Parse(emp.Rows[0]["Sex"].ToString());
                string bir = DateTime.Parse(emp.Rows[0]["Birth"].ToString().Trim()).ToString("yyyy-MM-dd");
                string joindate = DateTime.Parse(emp.Rows[0]["JoinDate"].ToString().Trim()).ToString("yyyy-MM-dd");
                string tel = emp.Rows[0]["EmpTel"].ToString();
                string email = emp.Rows[0]["EmpEmail"].ToString();
                int isout = int.Parse(emp.Rows[0]["IsOut"].ToString());
                string password = emp.Rows[0]["EmpPassword"].ToString();
                int depart = int.Parse(emp.Rows[0]["DepartID"].ToString());
                int posi = int.Parse(emp.Rows[0]["PositionID"].ToString());
                //string posiname = shiftPosi(EmpID);
                return Content(name + "," + sex + "," + bir + "," + joindate + "," + tel + "," + email + "," + isout + "," + password + "," + depart + "," + posi);
            }
            else
            {
                return Content("no");
            }
        }

        //根据上级部门名称转换ID
        public string shiftPosi(string EmpID)
        {

            string PositionName = "";
            if (EmpID =="0")
            {
                PositionName = "0";
            }
            else
            {
                string nsqls = string.Format("SELECT PositionName FROM dbo.Employee e JOIN dbo.Position p ON p.PositionID = e.PositionID WHERE EmpID='{0}'", EmpID);
                try
                {
                    DataTable depID = sql.GetDataTableCommand(nsqls);
                    PositionName = depID.Rows[0][0].ToString();
                }
                catch (Exception)
                {

                    return "暂无职位";
                }

            }
            return PositionName;
        }

        public ActionResult editSure(string name, string sex, string bir, string joindate, string posi, string tel, string email, int isout, string password, string depart,string EmpID,string Role,string CompanyID)
        {
            try
            {
                //解密 
                CompanyID = Base64MIMA.JIE(CompanyID);
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
            string py = Pinyin.GetInitials(name);
            string pinyin = Pinyin.GetPinyin(name).Replace(" ","");
            string editsql = string.Format("update dbo.Employee set Name='{0}',Sex={1},Birth='{2}',JoinDate='{3}',EmpTel='{4}',EmpEmail='{5}',IsOut={6},EmpPassword='{7}',DepartID='{8}',PositionID='{9}',SpellJX='{11}',SpellQP='{12}',RoleID='{13}' where EmpID='{10}' and CompanyID='{14}'", name,sex,bir,joindate,tel,email,isout,password,depart,posi,EmpID,py,pinyin,Role,CompanyID);
            string edit = sql.EditDataCommand(editsql);
            if (edit == "0")
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }

        public ActionResult AddUser(string CompanyID)
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
            //选择部门
            string sqlDepart = string.Format("select DepartID,DepartName from Depart where CompanyID='{0}'",CompanyID);
            DataTable Depart = sql.GetDataTableCommand(sqlDepart);
            if (Depart.Rows.Count > 0)
            {
                ViewBag.dname = Depart;
            }
            //选择权限
            string sqlrole = string.Format("select RoleID, RoleName from dbo.Role where CompanyID='{0}'", CompanyID);
            DataTable Role = sql.GetDataTableCommand(sqlrole);
            if (Role.Rows.Count > 0)
            {
                ViewBag.role = Role;
            }
            else
            {
                ViewBag.role = null;
            }
            return View();
        }

        public ActionResult EditUser(string EmpID)
        {
            //选择部门
            string sqlDepart = "select DepartID,DepartName from Depart";
            DataTable DepartName = sql.GetDataTableCommand(sqlDepart);
            if (DepartName.Rows.Count > 0)
            {
                ViewBag.dname = DepartName;
            }

            string sqlPosi = string.Format("SELECT PositionID,PositionName FROM dbo.Position WHERE DepartID=(SELECT DepartID FROM dbo.Employee WHERE EmpID='{0}')", EmpID);
            DataTable PosiName = sql.GetDataTableCommand(sqlPosi);
            if (PosiName.Rows.Count > 0)
            {
                ViewBag.pname = PosiName;
            }
            //员工信息
            string sqlEmp = string.Format("SELECT *  from dbo.Employee where EmpID='{0}'", EmpID);
            DataTable emp = sql.GetDataTableCommand(sqlEmp);
            if (emp.Rows.Count > 0)
            {
                ViewBag.empinfo = emp;
            }
            return View();
        }

        /// <summary>
        /// 员工搜索补全
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public ActionResult GetInfo(string keyword, string CompanyID)
        {
            try
            {
                //加密
                ViewBag.CompanyId = CompanyID;
                //解密 
                CompanyID = Base64MIMA.JIE(CompanyID);
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
            keyword = keyword.Trim().Replace(" ","");
            string sqlserach = "";

            sqlserach = string.Format("select Name from dbo.Employee where (SpellJX like '%{0}%' or SpellQP like '%{1}%' or Name like '%{2}%') and CompanyID='{3}'", keyword, keyword, keyword, CompanyID);

            DataSet ds = sql.GetDataSetCommand(sqlserach);
            string info = JsonConvert.SerializeObject(ds);
            info = info.Replace("Table", "data").Replace("Name", "title");
            return Content(info);
        }

        //员工选择分布视图
        public ActionResult SearchEmp(string Name, string CompanyID)
        {
            try
            {
                //加密
                ViewBag.CompanyId = CompanyID;
                //解密 
                CompanyID = Base64MIMA.JIE(CompanyID);
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
            string sqlEmp = "";
            if (Name == "")
            {
                sqlEmp = string.Format("select Name, EmpID, EmpPhotos from dbo.Employee where CompanyID='{0}'", CompanyID);
            }
            else
            {
                sqlEmp = string.Format("select Name, EmpID, EmpPhotos from dbo.Employee where (SpellJX like '%{0}%' or SpellQP like '%{1}%' or Name like '%{2}%') and CompanyID='{3}'", Name,Name,Name, CompanyID);
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
            return PartialView("_EmpSearch");
        }

        #region 微信
        HttpHelper httpHelp = new HttpHelper();
        /// <summary>
        /// 同步员工
        /// </summary>
        /// <param name="compid">加密后的公司id</param>
        /// <returns></returns>
        public ActionResult SelectEmp(string compid)
        {

            string accecctoken = GetQiyeToken.GetQiyeAttoken(compid);

            //应用须拥有指定部门的查看权限。
            //得到部门信息
            string sqlstr = string.Format("select * from Depart where PID=0 and CompanyID='{0}'",Base64MIMA.JIE(compid));
            DataTable dt = sql.GetDataTableCommand(sqlstr);
            string sqlUser2 = string.Format("select * from Employee where CompanyID='{0}'", Base64MIMA.JIE(compid));
            DataTable dtUser2 = sql.GetDataTableCommand(sqlUser2);
            if (dt?.Rows.Count > 0)
            {
                int a = 0;
                string sqlstr2 = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    a++;
                    int depId =int.Parse(dt.Rows[i]["DepartID"].ToString());
                    string url = $"https://qyapi.weixin.qq.com/cgi-bin/user/simplelist?access_token={accecctoken}&department_id={depId}&fetch_child=1";
                    string strResult = httpHelp.GetWebRequest(url);
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(strResult) && strResult.Substring(0, 1) == "{" && strResult.Substring(strResult.Length - 1, 1) == "}")
                        {
                            WechatReturnUser wrd = JSON.parse<WechatReturnUser>(strResult);
                            if (wrd.errcode == "0")
                            {
                                List<WechatUserList> listUser = wrd.userlist;
                                

                                if (listUser.Count > 0)
                                {
                                    #region 如果微信那边没有，数据库中有，就把数据库中的删掉
                                    if (dtUser2.Rows.Count > 0)
                                    {
                                        var vupdel = (from s in dtUser2.AsEnumerable()
                                                      where !listUser.Any(x => x.userid == s.Field<string>("EmpID")&&Base64MIMA.JIE(compid)==s.Field<string>("CompanyID"))
                                                      select s).ToList();
                                        if (vupdel.Count > 0)
                                        {
                                            foreach (var item in vupdel)
                                            {
                                            
                                                sqlstr2 += string.Format($"delete Employee where EmpID='{item.Field<string>("EmpID")}' and CompanyID='{Base64MIMA.JIE(compid)}' ");
                                            }
                                        }
                                       
                                    }


                                    #endregion
                                    foreach (var item in listUser)
                                    {
                                        //比较数据库中是否又这个人
                                        string sqlUser = string.Format($"select * from Employee where EmpID ='{item.userid}' and CompanyID='{Base64MIMA.JIE(compid)}'");
                                        DataTable dtUser = sql.GetDataTableCommand(sqlUser);
                                        string url2 = $"https://qyapi.weixin.qq.com/cgi-bin/user/get?access_token={accecctoken}&userid={item.userid}";
                                        string strResult2 = httpHelp.GetWebRequest(url2);
                                        UserDetials ud = JSON.parse<UserDetials>(strResult2);
                                        if (dtUser?.Rows.Count > 0)
                                        {
                                            //数据库又这个人不用同步
                                            if (ud.errcode == 0)
                                            {
                                                #region 看微信那边的信息有没有更改,以微信为准
                                                int b = 0;
                                                
                                                bool Isok= int.TryParse(dtUser.Rows[0]["DepartID"].ToString(),out b);//部门id
                                                if (Isok)
                                                {
                                                    b = int.Parse(dtUser.Rows[0]["DepartID"].ToString());
                                                }
                                                if (ud.name != dtUser.Rows[0]["Name"].ToString() ||item.department[0]!=b)
                                                {
                                                    string JX = Pinyin.GetInitials(ud.name);//简写
                                                    string QP = Pinyin.GetPinyin(ud.name).Replace(" ", "");//全拼
                                                    sqlstr2 += string.Format($"update Employee set SpellQP='{QP}',SpellJX='{JX}',Name='{ud.name}',DepartID={item.department[0]} where EmpID='{ud.userid}' and CompanyID='{Base64MIMA.JIE(compid)}' ");
                                                }
                                                #endregion
                                            }
                                        }
                                        else
                                        {
                                            //获取这个人的详情信息
                                            
                                            if (ud.errcode == 0)
                                            {
                                               
                                                //循环添加到数据库中
                                                string JX = Pinyin.GetInitials(item.name);//简写
                                                string QP = Pinyin.GetPinyin(item.name).Replace(" ", "");//全拼
                                                sqlstr2 += $@"INSERT INTO dbo.Employee
                                            (EmpID, Name ,Sex,EmpTel ,EmpEmail 
                                            ,IsOut ,SpellJX,SpellQP ,DepartID,CompanyID,EmpPhotos)VALUES 
                                            ('{ud.userid}','{ud.name}',{ud.gender},'{ud.mobile}','{ud.email}'
                                             ,{0},'{JX}','{QP}','{item.department[0]}','{Base64MIMA.JIE(compid)}','{ud.avatar}') ";

                                            }

                                        }

                                    }
                                    
                                    
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
                        return Content("从微信中读取数据失败：" + ex.Message);
                    }
                }
                if (a == 0)
                {
                    return Content("暂无需要同步的信息");
                }
                else
                {
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
            }
            else
            {
                return Content("请先同步部门");
            }
           
        }
        #endregion

        //        public string Num(int DepartID)
        //        {
        //            string Numsql = string.Format(@"with T as(
        //    select DepartID from Depart WHERE DepartID={0}
        //    union all
        //    select D.DepartID from Depart D,T  
        //    where D.PID=T.DepartID    
        //)
        //SELECT COUNT(*) Num FROM dbo.Employee WHERE DepartID IN
        //(SELECT DepartID from T) AND CompanyID=1;", DepartID);
        //            DataTable Num = sql.GetDataTableCommand(Numsql);
        //            string Json = "";
        //            if (Num.Rows.Count > 0)
        //            {
        //                Json = JsonConvert.SerializeObject(Num);
        //            }
        //            return Json;
        //        }

    }
}
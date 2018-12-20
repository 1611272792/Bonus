﻿using Sunpn_BonusWeb.BonusHelper;
using Sunpn_BonusWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sunpn_BonusWeb.Controllers
{
    public class RoleController : Controller
    {
        private Sunpn.Data.SqlServer.SQLConnection sql = null;
        public RoleController()
        {
            sql = new Sunpn.Data.SqlServer.SQLConnection(BonusHelper.AppConfig.ConnString);
        }
        // GET: Role
        public ActionResult Index(string CompanyId)
        {
            ViewBag.Companyid = CompanyId;
            CompanyId = Base64MIMA.JIE(CompanyId);
            #region 添加显示的权限
            //查出所有父权限
            string sqlstr = string.Format("select * from Author where PID is null and IsValid=0 and Fanwei=0");
            DataTable dt = sql.GetDataTableCommand(sqlstr);
            //根据父id得到子id
            List<AuthorModellist> authorlist = new List<AuthorModellist>();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    AuthorModellist Authors = new AuthorModellist();
                    Authors.Id = dt.Rows[i]["AuthorID"].ToString();
                    Authors.Name = dt.Rows[i]["AuthorName"].ToString();
                    List<AuthorModel> amlist = new List<AuthorModel>();
                    string sqlstr2 = string.Format("select * from Author where PID={0} and IsValid=0 and Fanwei=0", dt.Rows[i]["AuthorID"].ToString());
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
                ViewBag.authorlist2 = authorlist;

            }
            else
            {
                ViewBag.authorlist2 = null;
            }
            #endregion

            #region 查询
            //根据公司查对应的角色
            string sqlstrRole = string.Format("select * from Role where CompanyID='{0}'", CompanyId);
            DataTable dtRole = sql.GetDataTableCommand(sqlstrRole);
            if (dtRole.Rows.Count > 0)
            {
                ViewBag.Rolelist = dtRole;
            }
            else
            {
                ViewBag.Rolelist = null;
            }
            #endregion
            return View();
        }

        //找到编辑的数据
        public ActionResult Detial(string RoleId)
        {
            string sqlstr3 = string.Format("select r.RoleName,r.RoleID,a.ModuleCode,r.IsActive from Role r,Authorities a where r.RoleID=a.RoleID and a.RoleID='{0}'", RoleId);
            DataTable dt3 = sql.GetDataTableCommand(sqlstr3);
            if (dt3.Rows?.Count > 0)
            {
                ViewBag.detialRole = dt3;
                #region 添加显示的权限
                //查出所有父权限
                string sqlstr = "";
                if (dt3.Rows[0]["RoleName"].ToString() == "超级管理员")
                {
                    sqlstr = string.Format("select * from Author where PID is null and IsValid=0");
                }
                else
                {
                    sqlstr = string.Format("select * from Author where PID is null and IsValid=0 and Fanwei=0");
                }
               
                DataTable dt = sql.GetDataTableCommand(sqlstr);
                //根据父id得到子id
                List<AuthorModellist> authorlist = new List<AuthorModellist>();
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AuthorModellist Authors = new AuthorModellist();
                        Authors.Id = dt.Rows[i]["AuthorID"].ToString();
                        Authors.Name = dt.Rows[i]["AuthorName"].ToString();
                        Authors.AuthName = dt3.Rows[0]["RoleName"].ToString();
                        Authors.AuthId = dt3.Rows[0]["RoleID"].ToString();
                        Authors.IsActive = dt3.Rows[0]["IsActive"].ToString();//是否有效
                        List<AuthorModel> amlist = new List<AuthorModel>();
                        string sqlstr2 = "";
                        if (dt3.Rows[0]["RoleName"].ToString() == "超级管理员")
                        {
                            sqlstr2 = string.Format("select * from Author where PID={0} and IsValid=0", dt.Rows[i]["AuthorID"].ToString());

                        }
                        else
                        {
                            sqlstr2 = string.Format("select * from Author where PID={0} and IsValid=0 and Fanwei=0", dt.Rows[i]["AuthorID"].ToString());

                        }
                        DataTable dt2 = sql.GetDataTableCommand(sqlstr2);
                        if (dt2.Rows.Count > 0)
                        {

                            for (int ii = 0; ii < dt2.Rows.Count; ii++)
                            {
                                AuthorModel am = new AuthorModel();
                                for (int a = 0; a < dt3.Rows.Count; a++)
                                {
                                    if (dt3.Rows[a]["ModuleCode"].ToString() == dt2.Rows[ii]["AuthorID"].ToString())
                                    {
                                        am.AuthorContian = "0";//为0就代表有这个
                                        a = dt3.Rows.Count;
                                    }
                                    else
                                    {
                                        am.AuthorContian = "1";
                                    }
                                }

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
                    ViewBag.authorlist2 = authorlist;

                }
                else
                {
                    ViewBag.authorlist2 = null;
                }
                #endregion
            }
            else
            {
                ViewBag.detialRole = null;
            }
            return View();
        }

        //添加页面
        public ActionResult AddRole(string CompanyID)
        {
            #region 添加显示的权限
            try
            {
                //查出所有父权限
                string sqlstr = string.Format("select * from Author where PID is null and IsValid=0 and Fanwei=0");
                DataTable dt = sql.GetDataTableCommand(sqlstr);
                //根据父id得到子id
                List<AuthorModellist> authorlist = new List<AuthorModellist>();
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AuthorModellist Authors = new AuthorModellist();
                        Authors.Id = dt.Rows[i]["AuthorID"].ToString();
                        Authors.Name = dt.Rows[i]["AuthorName"].ToString();
                        List<AuthorModel> amlist = new List<AuthorModel>();
                        string sqlstr2 = string.Format("select * from Author where PID={0} and IsValid=0 and Fanwei=0", dt.Rows[i]["AuthorID"].ToString());
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
                    ViewBag.authorlist2 = authorlist;

                }
                else
                {
                    ViewBag.authorlist2 = null;
                }
               

                
            }
            catch (Exception ex)
            {

                return Redirect("/ErrorPage/Index");
            }
            #endregion

            ViewBag.CompanyID = CompanyID;
            return View();
        }
        //添加
        public ActionResult AddRole2(string AuthorName, string cheValue,string companyId,string radValue)
        {
            try
            {
                if (AuthorName == "" || cheValue == "" || companyId == "")
                {
                    return Content("不能为空");
                }
                //名字不能输入一样


                cheValue = cheValue.Substring(0, cheValue.Length - 1);
                var changdu = cheValue.Split(',');
                string sqlstr = string.Format($"exec proc_Role 'AddRole',0,'{AuthorName}','{Base64MIMA.JIE(companyId)}',{radValue},'{cheValue}',1,{changdu.Length}");
                string info = sql.EditDataCommand(sqlstr);
                if (info == "0")
                {
                    return Content("ok");
                }
                else
                {
                    return Content("添加失败");
                }
            }
            catch (Exception ex)
            {

                return Content("添加时出现错误"+ex.Message);
            }
            #region
            ////1.添加角色
            //string sqlstr = string.Format("insert into Role values('{0}',1,0)", AuthorName);
            //string info = sql.EditDataCommand(sqlstr);
            //if (info == "0")
            //{
            //    //2.得到刚才插入的角色id
            //    string sqlstr2 = string.Format("select MAX(RoleID) RoleID from Role where RoleName='{0}'", AuthorName);
            //    DataTable dt2 = sql.GetDataTableCommand(sqlstr2);
            //    string RoleID = "";
            //    if (dt2.Rows.Count > 0)
            //    {
            //        RoleID = dt2.Rows[0]["RoleID"].ToString();
            //    }
            //    //3.插入权限
            //    var AuthArry = cheValue.Split(',');//分割权限
            //    string sqlstr3 = "";
            //    for (int i = 0; i < AuthArry.Length; i++)
            //    {
            //        sqlstr3 += string.Format("insert into Authorities values({0},{1},1)", RoleID, AuthArry[i]);
            //    }
            //    string info3 = sql.EditDataCommand(sqlstr3);
            //    if (info3 == "0")
            //    {
            //        return Content("ok");
            //    }
            //    else
            //    {
            //        return Content("添加失败");
            //    }

            //}
            //else
            //{
            //    return Content("添加失败");
            //}
            #endregion
        }

        //编辑
        public ActionResult EditRole(string AuthorName_Edit, string cheValue, string radValue, string RoleId)
        {
            if (AuthorName_Edit == "" || cheValue == "" || radValue == "")
            {
                return Content("不能为空");
            }
            //1.修改角色表的相关数据
            string CompanyId = "";//所属公司(占位)
            cheValue = cheValue.Substring(0, cheValue.Length - 1);
            string RoleType = "1";//类型
            var cheArry = cheValue.Split(',');//权限的个数
            string[] canshu = new string[] { RoleId, AuthorName_Edit, CompanyId, radValue, cheValue, RoleType, cheArry.Length.ToString() };
            string info = sql.EditDataProcedure("proc_Role", "SelectRole", canshu);
            if (info == "0")
            {
                return Content("更新成功");
            }
            else
            {
                return Content("更新失败");
            }

        }

        //删除
        public ActionResult DelRole(string RoleId)
        {
            string[] canshu = new string[] { RoleId };
            string info = sql.EditDataProcedure("proc_Role", "DelRole", canshu);
            if (info == "0")
            {
                return Content("删除成功");
            }
            else
            {
                return Content("删除失败");
            }
        }

        public ActionResult SelectRole(string RoleName)
        {
            string sqlstr = string.Format("select * from Role where RoleName like '%{0}%'", RoleName);
            DataTable dt = sql.GetDataTableCommand(sqlstr);
            if (dt?.Rows.Count > 0)
            {
                ViewBag.Rolelist = dt;
            }
            else
            {
                ViewBag.Rolelist = null;
            }
            return PartialView("_PartialSelectRole");
        }
    }
}
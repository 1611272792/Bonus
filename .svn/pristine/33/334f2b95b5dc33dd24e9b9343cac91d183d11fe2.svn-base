using Sunpn_BonusWeb.BonusHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Sunpn_BonusWeb.Controllers
{
    public class BonusPayController : Controller
    {
        private Sunpn.Data.SqlServer.SQLConnection sql = null;
            
        public BonusPayController()
        {
            sql = new Sunpn.Data.SqlServer.SQLConnection(BonusHelper.AppConfig.ConnString);
        }
        // GET: BonusPay
        public ActionResult Index(string userID)
        {
            try
            {
                //解密userid
                userID = Base64MIMA.JIE(userID);
            }
            catch (Exception)
            {
                //跳转错误页面
                return Redirect("/ErrorPage/Index");
            }
          

            ViewBag.disManID = userID;
            string isOrNO = string.Format(@"SELECT Employee.EmpID FROM dbo.Employee INNER  JOIN dbo.BonusItem ON BIPrincipal=EmpID  WHERE Employee.EmpID='{0}'
 UNION (SELECT Employee.EmpID FROM dbo.Employee INNER  JOIN dbo.BonusImpower ON BonusImpower.EmpID=Employee.EmpID  WHERE Employee.EmpID='{1}')
                      ", userID, userID);//查询是否是奖金项负责人或者被授权人
            DataTable dsIsItem = sql.GetDataTableCommand(isOrNO);
            if (dsIsItem.Rows.Count > 0)
            {
                string items = empHaveBonusItems(userID);//获取该用户拥有的奖金项
                string powers = empHavePower(userID);  //获取该用户拥有的被授权奖金项
                string sqlItems = "";
                if (!string.IsNullOrEmpty(items) && !string.IsNullOrEmpty(powers))
                {
                    sqlItems = string.Format(@"SELECT BonusDataID,b2.EarMoney,b.BIName,Name,DisDate,DepartName AS EarMan,BonusType FROM BonusData2 b2
                     INNER JOIN BonusItem b ON b2.BonusItemID=b.BonusItemID
                     INNER JOIN Employee e ON b2.DisMan=e.EmpID INNER JOIN dbo.Depart ON Depart.DepartID=b2.EarMan WHERE  BonusType=0 
                 AND b2.BonusItemID IN ('{0}')   
                 UNION   ALL         SELECT BonusDataID,b2.EarMoney,b.BIName,e.Name,DisDate,e2.Name AS EarMan,BonusType FROM BonusData2 b2
                     INNER JOIN BonusItem b ON b2.BonusItemID=b.BonusItemID
                     INNER JOIN Employee e ON b2.DisMan=e.EmpID INNER JOIN dbo.Employee e2 on b2.EarMan=e2.EmpID   WHERE  BonusType=1
                 AND b2.BonusItemID IN ('{1}')  UNION    ALL    SELECT BonusDataID,b2.EarMoney,b.BIName,Name,DisDate,DepartName AS EarMan,BonusType FROM BonusData2 b2
                     INNER JOIN BonusItem b ON b2.BonusItemID=b.BonusItemID
                     INNER JOIN Employee e ON b2.DisMan=e.EmpID INNER JOIN dbo.Depart ON Depart.DepartID=b2.EarMan WHERE  BonusType=0 
                 AND b2.BonusItemID IN ('{2}') AND DisMan='{3}'     
                 UNION    SELECT BonusDataID,b2.EarMoney,b.BIName,e.Name,DisDate,e2.Name AS EarMan,BonusType FROM BonusData2 b2
                     INNER JOIN BonusItem b ON b2.BonusItemID=b.BonusItemID
                     INNER JOIN Employee e ON b2.DisMan=e.EmpID INNER JOIN dbo.Employee e2 on b2.EarMan=e2.EmpID   WHERE  BonusType=1
                 AND b2.BonusItemID IN ('{4}') AND DisMan='{5}'  ORDER BY DisDate DESC ", items, items, powers, userID, powers, userID);//既是奖金项负责人又是被授权人
                }
               else if (!string.IsNullOrEmpty(items))
                {
                    sqlItems = string.Format(@"SELECT BonusDataID,b2.EarMoney,b.BIName,Name,DisDate,DepartName AS EarMan,BonusType FROM BonusData2 b2
                     INNER JOIN BonusItem b ON b2.BonusItemID=b.BonusItemID
                     INNER JOIN Employee e ON b2.DisMan=e.EmpID INNER JOIN dbo.Depart ON Depart.DepartID=b2.EarMan WHERE  BonusType=0 
                 AND b2.BonusItemID IN ('{0}')   
                 UNION     SELECT BonusDataID,b2.EarMoney,b.BIName,e.Name,DisDate,e2.Name AS EarMan,BonusType FROM BonusData2 b2
                     INNER JOIN BonusItem b ON b2.BonusItemID=b.BonusItemID
                     INNER JOIN Employee e ON b2.DisMan=e.EmpID INNER JOIN dbo.Employee e2 on b2.EarMan=e2.EmpID   WHERE  BonusType=1
                 AND b2.BonusItemID IN ('{1}')  ORDER BY DisDate DESC", items, items);//奖金项负责人查看奖金项明细
                }
                else if (!string.IsNullOrEmpty(powers))
                {
                    sqlItems = string.Format(@"SELECT BonusDataID,b2.EarMoney,b.BIName,Name,DisDate,DepartName AS EarMan,BonusType FROM BonusData2 b2
                     INNER JOIN BonusItem b ON b2.BonusItemID=b.BonusItemID
                     INNER JOIN Employee e ON b2.DisMan=e.EmpID INNER JOIN dbo.Depart ON Depart.DepartID=b2.EarMan WHERE  BonusType=0 
                 AND b2.BonusItemID IN ('{0}') AND DisMan='{1}'     
                 UNION            SELECT BonusDataID,b2.EarMoney,b.BIName,e.Name,DisDate,e2.Name AS EarMan,BonusType FROM BonusData2 b2
                     INNER JOIN BonusItem b ON b2.BonusItemID=b.BonusItemID
                     INNER JOIN Employee e ON b2.DisMan=e.EmpID INNER JOIN dbo.Employee e2 on b2.EarMan=e2.EmpID   WHERE  BonusType=1
                 AND b2.BonusItemID IN ('{2}') AND DisMan='{3}'  ORDER BY DisDate DESC", powers, userID, powers, userID);//被授权查看奖金项明细
                }
                DataTable dsDep = sql.GetDataTableCommand(sqlItems);

                ViewBag.Dep = dsDep;
                ViewBag.isYeOrNO = "yes";
            }
            else
            {
                ViewBag.Dep = null;
                ViewBag.isYeOrNO = "no";
            }
          
            //string sqlEmp = string.Format("select * from PayEmpBonus where DisMan='{0}'", disManID);
            string sqlEmp = string.Format(@" SELECT BonusDataID, Name, DisDate, EarMoney FROM BonusData2 b INNER JOIN Employee e ON b.EarMan = e.EmpID 
WHERE DisMan = '{0}' AND BonusType = 2  ORDER BY DisDate DESC", userID);
            DataTable dsEmp = sql.GetDataTableCommand(sqlEmp);
            if (dsEmp.Rows.Count > 0)
            {
                ViewBag.Emp = dsEmp;
            }
            else  
            {
                ViewBag.Emp = null;
            }
         
            return View();
          
        }
            
        //时间筛选分布视图
        public ActionResult SelectDismanPay(DateTime StartTime, DateTime EndTime, string disManID)
        {
            disManID = "10086";
            ViewBag.disManID = disManID;
            string isOrNO = string.Format(@"SELECT Employee.EmpID FROM dbo.Employee INNER  JOIN dbo.BonusItem ON BIPrincipal=EmpID  WHERE Employee.EmpID='{0}'
 UNION (SELECT Employee.EmpID FROM dbo.Employee INNER  JOIN dbo.BonusImpower ON BonusImpower.EmpID=Employee.EmpID  WHERE Employee.EmpID='{1}')
                      ", disManID, disManID);//查询是否是奖金项负责人或者被授权人
            DataTable dsIsItem = sql.GetDataTableCommand(isOrNO);
            if (dsIsItem.Rows.Count > 0)
            {
                string items = empHaveBonusItems(disManID);//获取该用户拥有的奖金项
                string powers = empHavePower(disManID);  //获取该用户拥有的被授权奖金项
                string sqlItems = "";
                if (!string.IsNullOrEmpty(items) && !string.IsNullOrEmpty(powers))
                {
                    sqlItems = string.Format(@"  SELECT BonusDataID,b2.EarMoney,b.BIName,Name,DisDate,DepartName AS EarMan,BonusType FROM BonusData2 b2
                     INNER JOIN BonusItem b ON b2.BonusItemID=b.BonusItemID
                     INNER JOIN Employee e ON b2.DisMan=e.EmpID INNER JOIN dbo.Depart ON Depart.DepartID=b2.EarMan WHERE  BonusType=0 
                 AND b2.BonusItemID IN ('{0}')   AND  CONVERT(DATE,EarDate) BETWEEN '{1}' AND '{2}'
                 UNION   ALL         SELECT BonusDataID,b2.EarMoney,b.BIName,e.Name,DisDate,e2.Name AS EarMan,BonusType FROM BonusData2 b2
                     INNER JOIN BonusItem b ON b2.BonusItemID=b.BonusItemID
                     INNER JOIN Employee e ON b2.DisMan=e.EmpID INNER JOIN dbo.Employee e2 on b2.EarMan=e2.EmpID   WHERE  BonusType=1
                 AND b2.BonusItemID IN ('{3}') AND  CONVERT(DATE,EarDate) BETWEEN '{4}' AND '{5}'  UNION    ALL    SELECT BonusDataID,b2.EarMoney,b.BIName,Name,DisDate,DepartName AS EarMan,BonusType FROM BonusData2 b2
                     INNER JOIN BonusItem b ON b2.BonusItemID=b.BonusItemID
                     INNER JOIN Employee e ON b2.DisMan=e.EmpID INNER JOIN dbo.Depart ON Depart.DepartID=b2.EarMan WHERE  BonusType=0 
                 AND b2.BonusItemID IN ('{6}') AND DisMan='{7}'     AND  CONVERT(DATE,EarDate) BETWEEN '{8}' AND '{9}'
                 UNION            SELECT BonusDataID,b2.EarMoney,b.BIName,e.Name,DisDate,e2.Name AS EarMan,BonusType FROM BonusData2 b2
                     INNER JOIN BonusItem b ON b2.BonusItemID=b.BonusItemID
                     INNER JOIN Employee e ON b2.DisMan=e.EmpID INNER JOIN dbo.Employee e2 on b2.EarMan=e2.EmpID   WHERE  BonusType=1
                 AND b2.BonusItemID IN ('{10}') AND DisMan='{11}' AND  CONVERT(DATE,EarDate) BETWEEN '{12}' AND '{13}'  ORDER BY DisDate DESC  
 ", items, StartTime, EndTime, items, StartTime, EndTime, powers, disManID, StartTime, EndTime, powers, disManID,StartTime,EndTime);//既是奖金项负责人又是被授权人
                }
                else if (!string.IsNullOrEmpty(items))
                {
                    sqlItems = string.Format(@"SELECT BonusDataID,b2.EarMoney,b.BIName,Name,DisDate,DepartName AS EarMan,BonusType FROM BonusData2 b2
                     INNER JOIN BonusItem b ON b2.BonusItemID=b.BonusItemID
                     INNER JOIN Employee e ON b2.DisMan=e.EmpID INNER JOIN dbo.Depart ON Depart.DepartID=b2.EarMan WHERE  BonusType=0 
                 AND b2.BonusItemID IN ('{0}')  AND  CONVERT(DATE,EarDate) BETWEEN '{1}' AND '{2}' 
                 UNION            SELECT BonusDataID,b2.EarMoney,b.BIName,e.Name,DisDate,e2.Name AS EarMan,BonusType FROM BonusData2 b2
                     INNER JOIN BonusItem b ON b2.BonusItemID=b.BonusItemID
                     INNER JOIN Employee e ON b2.DisMan=e.EmpID INNER JOIN dbo.Employee e2 on b2.EarMan=e2.EmpID   WHERE  BonusType=1
                 AND b2.BonusItemID IN ('{3}')  AND  CONVERT(DATE,EarDate) BETWEEN '{4}' AND '{5}'  ORDER BY DisDate DESC", items, StartTime, EndTime,items, StartTime, EndTime);//奖金项负责人查看奖金项明细
                }
                else if (!string.IsNullOrEmpty(powers))
                {
                    sqlItems = string.Format(@"SELECT BonusDataID,b2.EarMoney,b.BIName,Name,DisDate,DepartName AS EarMan,BonusType FROM BonusData2 b2
                     INNER JOIN BonusItem b ON b2.BonusItemID=b.BonusItemID
                     INNER JOIN Employee e ON b2.DisMan=e.EmpID INNER JOIN dbo.Depart ON Depart.DepartID=b2.EarMan WHERE  BonusType=0 
                 AND b2.BonusItemID IN ('{0}') AND DisMan='{1}'   AND  CONVERT(DATE,EarDate) BETWEEN '{2}' AND '{3}' 
                 UNION   ALL    SELECT BonusDataID,b2.EarMoney,b.BIName,e.Name,DisDate,e2.Name AS EarMan,BonusType FROM BonusData2 b2
                     INNER JOIN BonusItem b ON b2.BonusItemID=b.BonusItemID
                     INNER JOIN Employee e ON b2.DisMan=e.EmpID INNER JOIN dbo.Employee e2 on b2.EarMan=e2.EmpID   WHERE  BonusType=1
                 AND b2.BonusItemID IN ('{4}') AND DisMan='{5}'  AND  CONVERT(DATE,EarDate) BETWEEN '{6}' AND '{7}'  ORDER BY DisDate DESC", powers, disManID, StartTime, EndTime,powers, disManID, StartTime, EndTime);//被授权查看奖金项明细
                }
                DataTable dsDep = sql.GetDataTableCommand(sqlItems);

                ViewBag.Dep = dsDep;
                ViewBag.isYeOrNO = "yes";
            }
            else
            {
                ViewBag.Dep = null;
                ViewBag.isYeOrNO = "no";
            }

            //string sqlEmp = string.Format("select * from PayEmpBonus where DisMan='{0}'", disManID);
            string sqlEmp = string.Format(@" SELECT BonusDataID, Name, DisDate, EarMoney FROM BonusData2 b INNER JOIN Employee e ON b.EarMan = e.EmpID 
WHERE DisMan = '{0}' AND BonusType = 2   AND  CONVERT(DATE,EarDate) BETWEEN '{1}' AND '{2}'  ORDER BY DisDate DESC", disManID, StartTime, EndTime);
            DataTable dsEmp = sql.GetDataTableCommand(sqlEmp);
            if (dsEmp.Rows.Count > 0)
            {
                ViewBag.Emp = dsEmp;
            }
            else
            {
                ViewBag.Emp = null;
            }

            return View("_SelectDismanPay");

        }
        /// <summary>
        /// 查询该用户拥有的奖金项
        /// </summary>
        /// <param name="disManID"></param>
        /// <returns>返回所拥有的奖金项字符串</returns>
        public string empHaveBonusItems(string disManID) {
            string sqls = string.Format("SELECT BonusItemID FROM dbo.BonusItem WHERE BIPrincipal = '{0}'",disManID);
            DataTable ds = sql.GetDataTableCommand(sqls);
            string haveItems = "";
            if (ds.Rows.Count>0)
            {
                for (int i = 0; i < ds.Rows.Count; i++)
                {
                    haveItems =haveItems +"','"+ ds.Rows[i][0].ToString();
                }
                haveItems = haveItems.Substring(3);
                return haveItems;
            }
            else
            {
                return "";
            }
          
        }

        /// <summary>
        /// 查询该用户是否是被授权人
        /// </summary>
        /// <param name="disManID"></param>
        /// <returns></returns>
        public string empHavePower(string disManID)
        {
            string sqls = string.Format(" SELECT DISTINCT BonusItemID FROM dbo.BonusImpower WHERE EmpID='{0}'", disManID);
            DataTable ds = sql.GetDataTableCommand(sqls);
            string haveItems = "";
            if (ds.Rows.Count > 0)
            {
                for (int i = 0; i < ds.Rows.Count; i++)
                {
                    haveItems = haveItems + "','" + ds.Rows[i][0].ToString();
                }
                haveItems = haveItems.Substring(3);
                return haveItems;
            }
            else
            {
                return "";
            }

        }

 
    }
}
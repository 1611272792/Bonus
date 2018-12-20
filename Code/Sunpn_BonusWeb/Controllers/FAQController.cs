using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sunpn_BonusWeb.Controllers
{
    public class FAQController : Controller
    {
        // GET: FAQ
        public ActionResult Index()
        {
            return View();
        }

        //如何添加员工页面
        public ActionResult HAddUser()
        {
            return View();
        }

        //如何设置奖金项规则
        public ActionResult HSetBonusItem()
        {
            return View();
        }

        public ActionResult HSetImpower()
        {
            return View();
        }
    }
}
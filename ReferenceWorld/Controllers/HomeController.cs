using ReferenceWorld.Common;
using ReferenceWorld.Models;
using ReferenceWorld.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReferenceWorld.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {   
            ViewBag.IsLogin = CommonMethod.IsLogin();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.IsLogin = CommonMethod.IsLogin();
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.IsLogin = CommonMethod.IsLogin();
            return View();
        }
    }
}
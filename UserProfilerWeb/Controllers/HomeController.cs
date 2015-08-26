using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UserProfilerWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RequestForm()
        {
            ViewBag.Message = "Your application Request page.";

            return View("Request");
        }

        public ActionResult Details(int id)
        {
            ViewBag.Message = "Your application Details page.";
            ViewBag.RequestId = id;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
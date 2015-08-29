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

        public ActionResult RequestForm(int id)
        {
            ViewBag.Message = "Your application Request page.";
            ViewBag.RequestId = id;
            return View("ClaimRequest");
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
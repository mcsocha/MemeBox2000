using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MemeBox2000.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Title = "About";

            return View();
        }

        public ActionResult Screenshots()
        {
            ViewBag.Title = "Screenshots";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Title = "Contact";

            return View();
        }

        [HttpPost]
        public ActionResult ContactMessage(string name, string email, string subject, string message)
        {
            ViewBag.ContactMessage = string.Format("{0}-{1}-{2}-{3}",
                name, email, subject, message);

            return View("Index");
        }
    }
}

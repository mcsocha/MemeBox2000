using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MemeBox2000.Models;

namespace MemeBox2000.Controllers
{
    public class ContactController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubmitMessage(ContactMessage model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = "Message sent!";

                //Do some email processing...
            }

            return View("Index", model);
        }
    }
}

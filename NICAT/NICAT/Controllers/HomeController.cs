using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NICAT.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new Random().Next(1, 5));
        }
    }
}
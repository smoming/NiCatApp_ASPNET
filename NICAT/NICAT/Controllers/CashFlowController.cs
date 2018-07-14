using NICAT.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NICAT.Controllers
{
    public class CashFlowController : Controller
    {
        private GBService _SVC = new GBService();

        public CashFlowController()
            : base()
        { }

        public ActionResult Index()
        {
            return View(DateTime.Today);
        }

        async public Task<ActionResult> DoAccound(DateTime xExeDate)
        {
            var result = await _SVC.DoAccound(xExeDate);
            return Content(result.Message);
        }

        async public Task<ActionResult> DoUnAccound(DateTime xExeDate)
        {
            var result = await _SVC.DoUnAccound(xExeDate);
            return Content(result.Message);
        }
    }
}
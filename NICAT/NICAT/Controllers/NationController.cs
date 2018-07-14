using NICAT.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NICAT.Controllers
{
    public class NationController : Controller
    {
        private GBService _Service;

        public NationController()
            : base()
        {
            _Service = new GBService();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Grid()
        {
            return PartialView(_Service.LookupNation().ToList());
        }

        public ActionResult Create()
        {
            return PartialView(new Nation());
        }

        public ActionResult Edit(string xID)
        {
            return PartialView(_Service.GetNation(xID));
        }

        async public Task<ActionResult> Add(Nation item)
        {
            return Content(await _Service.AddNation(item));
        }

        async public Task<ActionResult> Update(Nation item)
        {
            return Content(await _Service.UpdateNation(item));
        }

        async public Task<ActionResult> Delete(string xID)
        {
            return Content(await _Service.DeleteNation(_Service.GetNation(xID)));
        }
    }
}
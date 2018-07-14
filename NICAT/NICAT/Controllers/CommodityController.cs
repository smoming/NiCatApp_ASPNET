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
    public class CommodityController : Controller
    {
        private GBService _Service;

        public CommodityController()
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
            return PartialView(_Service.LookupCommodity().ToList());
        }

        public ActionResult Create()
        {
            return PartialView(new Commodity());
        }

        public ActionResult Edit(string xID)
        {
            return PartialView(_Service.GetCommodity(xID));
        }

        async public Task<ActionResult> Add(Commodity item)
        {
            return Content(await _Service.AddCommodity(item));
        }

        async public Task<ActionResult> Update(Commodity item)
        {
            return Content(await _Service.UpdateCommodity(item));
        }

        async public Task<ActionResult> Delete(string xID)
        {
            return Content(await _Service.DeleteCommodity(_Service.GetCommodity(xID)));
        }
    }
}
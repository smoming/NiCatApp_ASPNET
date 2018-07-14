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
    public class TradingController : Controller
    {
        private GBService _Service;

        public TradingController()
            : base()
        {
            _Service = new GBService();
        }

        public ActionResult Index()
        {
            //SetViewBag();
            return View(new TradingQueryViewModel() { TradeDate_S = DateTime.Today.AddDays(-7), TradeDate_E = DateTime.Today });
        }

        public ActionResult Grid(TradingQueryViewModel filter)
        {
            return PartialView(_Service.LookupTrading(filter).ToList());
        }

        public ActionResult Create()
        {
            //SetViewBag();
            return PartialView(new Trading() { TradeDate = DateTime.Today });
        }

        public ActionResult Edit(string xTransNo)
        {
            //SetViewBag();
            return PartialView(_Service.GetTrading(xTransNo));
        }

        async public Task<ActionResult> Add(Trading item)
        {
            return Content(await _Service.AddTrading(item));
        }

        async public Task<ActionResult> Update(Trading item)
        {
            return Content(await _Service.UpdateTrading(item));
        }

        async public Task<ActionResult> Delete(string xTransNo)
        {
            return Content(await _Service.DeleteTrading(_Service.GetTrading(xTransNo)));
        }
    }
}
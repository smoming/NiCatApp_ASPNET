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
    public class ShipperController : Controller
    {
        private GBService _Service;

        public ShipperController()
            : base()
        {
            _Service = new GBService();
        }

        public ActionResult Index()
        {
            return View(new ShipperQueryViewModel() { TradeDate_S = DateTime.Today.AddDays(-7), TradeDate_E = DateTime.Today });
        }

        public ActionResult Grid(ShipperQueryViewModel filter)
        {
            return PartialView(_Service.LookupShipper(filter).ToList());
        }

        public ActionResult Create(string xBuyer)
        {
            return PartialView(_Service.LookupTrading(new TradingQueryViewModel() { Buyer = xBuyer, IsShipped = false }).ToList());
        }

        public ActionResult CreateConfirm(string[] xTransNos)
        {
            var xPreData = new List<Trading>();
            if (xTransNos.IsNotEmpty())
            {
                xPreData = _Service.LookupTrading(new TradingQueryViewModel()
                {
                    TransNos = xTransNos.ToList(),
                    IsShipped = false
                }).ToList();
            }

            //SetViewBag();
            TempData["ReadyShip"] = xPreData;
            return PartialView(new Shipper()
            {
                TradeDate = DateTime.Today,
                Buyer = xPreData.IsNotEmpty() ? xPreData.First().Buyer : string.Empty,
                TradeAmount = xPreData.Sum(s => s.TradeAmount)
            });
        }

        public ActionResult Edit(string xTransNo)
        {
            //SetViewBag();
            return PartialView(_Service.GetShipper(xTransNo));
        }

        async public Task<ActionResult> Add(Shipper item)
        {
            return Content(await _Service.AddShipper(item, TempData["ReadyShip"] as IEnumerable<Trading>));
        }

        async public Task<ActionResult> Update(Shipper item)
        {
            return Content(await _Service.UpdateShipper(item));
        }

        async public Task<ActionResult> Delete(string xTransNo)
        {
            return Content(await _Service.DeleteShipper(_Service.GetShipper(xTransNo)));
        }
    }
}
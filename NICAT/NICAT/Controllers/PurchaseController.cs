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
    public class PurchaseController : Controller
    {
        private GBService _Service;

        public PurchaseController()
            : base()
        {
            _Service = new GBService();
        }

        public ActionResult Index()
        {
            return View(new PurchaseQueryViewModel() { TradeDate_S = DateTime.Today.AddDays(-7), TradeDate_E = DateTime.Today });
        }

        public ActionResult Grid(PurchaseQueryViewModel filter)
        {
            return PartialView(_Service.LookupPurchase(filter).ToList());
        }

        public ActionResult Create()
        {
            return PartialView(_Service.LookupOrder(new OrderQueryViewModel() { IsPaid = true, IsPurchased = false }).ToList());
        }

        public ActionResult CreateConfirm(string[] xTransNos)
        {
            var xPreData = new List<Order>();
            if (xTransNos.IsNotEmpty())
            {
                xPreData = _Service.LookupOrder(new OrderQueryViewModel()
                {
                    TransNos = xTransNos.ToList(),
                    IsPaid = true,
                    IsPurchased = false
                }).ToList();
            }

            TempData["ReadyPurchase"] = xPreData;
            return PartialView(new Purchase()
            {
                TradeDate = DateTime.Today
            });
        }

        public ActionResult Edit(string xTransNo)
        {
            return PartialView(_Service.GetPurchase(xTransNo));
        }

        async public Task<ActionResult> Add(Purchase item)
        {
            return Content(await _Service.AddPurchase(item, TempData["ReadyPurchase"] as IEnumerable<Order>));
        }

        async public Task<ActionResult> Update(Purchase item)
        {
            return Content(await _Service.UpdatePurchase(item));
        }

        async public Task<ActionResult> Delete(string xTransNo)
        {
            return Content(await _Service.DeletePurchase(_Service.GetPurchase(xTransNo)));
        }
    }
}
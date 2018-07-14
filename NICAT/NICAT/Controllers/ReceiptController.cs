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
    public class ReceiptController : Controller
    {
        private GBService _Service;

        public ReceiptController()
            : base()
        {
            _Service = new GBService();
        }

        public ActionResult Index()
        {
            return View(new ReceiptQueryViewModel() { TradeDate_S = DateTime.Today.AddDays(-7), TradeDate_E = DateTime.Today });
        }

        public ActionResult Grid(ReceiptQueryViewModel filter)
        {
            return PartialView(_Service.LookupReceipt(filter).ToList());
        }

        public ActionResult Create()
        {
            return PartialView(_Service.LookupOrder(new OrderQueryViewModel() { IsPaid = false }).ToList());
        }

        public ActionResult CreateConfirm(string[] xTransNos)
        {
            var xPreData = new List<Order>();
            if (xTransNos.IsNotEmpty())
            {
                xPreData = _Service.LookupOrder(new OrderQueryViewModel()
                {
                    TransNos = xTransNos.ToList(),
                    IsPaid = false
                }).ToList();
            }

            TempData["ReadyPay"] = xPreData;
            return PartialView(new Receipt()
            {
                TradeDate = DateTime.Today,
                TradeAmount = xPreData.Sum(s => s.TradeAmount)
            });
        }

        public ActionResult Edit(string xTransNo)
        {
            return PartialView(_Service.GetReceipt(xTransNo));
        }

        async public Task<ActionResult> Add(Receipt item)
        {
            return Content(await _Service.AddReceipt(item, TempData["ReadyPay"] as IEnumerable<Order>));
        }

        async public Task<ActionResult> Update(Receipt item)
        {
            return Content(await _Service.UpdateReceipt(item));
        }

        async public Task<ActionResult> Delete(string xTransNo)
        {
            return Content(await _Service.DeleteReceipt(_Service.GetReceipt(xTransNo)));
        }
    }
}
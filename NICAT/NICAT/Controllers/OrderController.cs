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
    public class OrderController : Controller
    {
        private GBService _Service;

        public OrderController()
            : base()
        {
            _Service = new GBService();
        }

        public ActionResult Index()
        {
            return View(new OrderQueryViewModel() { TradeDate_S = DateTime.Today.AddDays(-7), TradeDate_E = DateTime.Today });
        }

        public ActionResult Grid(OrderQueryViewModel filter)
        {
            return PartialView(_Service.LookupOrder(filter).ToList());
        }

        public ActionResult Create()
        {
            return PartialView(new Order() { TradeDate = DateTime.Today });
        }

        public ActionResult Edit(string xTransNo)
        {
            return PartialView(_Service.GetOrder(xTransNo));
        }

        async public Task<ActionResult> Add(Order item)
        {
            return Content(await _Service.AddOrder(item));
        }

        async public Task<ActionResult> Update(Order item)
        {
            return Content(await _Service.UpdateOrder(item));
        }

        async public Task<ActionResult> Delete(string xTransNo)
        {
            return Content(await _Service.DeleteOrder(_Service.GetOrder(xTransNo)));
        }
    }
}
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
    public class DeliveryTypeController : Controller
    {
        private GBService _Server;

        public DeliveryTypeController()
            : base()
        {
            _Server = new GBService();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Grid()
        {
            return PartialView(_Server.LookupDeliveryType().ToList());
        }

        public ActionResult Create()
        {
            return PartialView(new DeliveryType());
        }

        public ActionResult Edit(string xID)
        {
            return PartialView(_Server.GetDeliveryType(xID));
        }

        async public Task<ActionResult> Add(DeliveryType item)
        {
            return Content(await _Server.AddDeliveryType(item));
        }

        async public Task<ActionResult> Update(DeliveryType item)
        {
            return Content(await _Server.UpdateDeliveryType(item));
        }

        async public Task<ActionResult> Delete(string xID)
        {
            return Content(await _Server.DeleteDeliveryType(_Server.GetDeliveryType(xID)));
        }
    }
}
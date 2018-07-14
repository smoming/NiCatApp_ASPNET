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
    public class SupplierController : Controller
    {
        private GBService _Server;

        public SupplierController()
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
            return PartialView(_Server.LookupSupplier().ToList());
        }

        public ActionResult Create()
        {
            return PartialView(new Supplier());
        }

        public ActionResult Edit(string xID)
        {
            return PartialView(_Server.GetSupplier(xID));
        }

        async public Task<ActionResult> Add(Supplier item)
        {
            return Content(await _Server.AddSupplier(item));
        }

        async public Task<ActionResult> Update(Supplier item)
        {
            return Content(await _Server.UpdateSupplier(item));
        }

        async public Task<ActionResult> Delete(string xID)
        {
            return Content(await _Server.DeleteSupplier(_Server.GetSupplier(xID)));
        }
    }
}
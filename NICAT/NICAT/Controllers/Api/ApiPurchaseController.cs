using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NICAT.Models;
using System.Web.Http.Cors;
using NICAT.Models.ApiModel;

namespace NICAT.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ApiPurchaseController : ApiController
    {
        private GBService _SVC = new GBService();

        // GET: api/Purchase
        public List<PurchaseDTOs> GetPurchase(DateTime? StartDate, DateTime? EndDate)
        {
            return MapperConfig.mapper.Map<List<PurchaseDTOs>>(_SVC.LookupPurchase(new PurchaseQueryViewModel()
            {
                TradeDate_S = StartDate,
                TradeDate_E = EndDate
            }));
        }

        // GET: api/Purchase/5
        [ResponseType(typeof(Purchase)), HttpGet]
        public async Task<IHttpActionResult> GetPurchase(string id)
        {
            Purchase purchase = _SVC.GetPurchase(id);
            if (purchase == null)
            {
                return NotFound();
            }

            return Ok(purchase);
        }

        // PUT: api/Purchase/5
        [ResponseType(typeof(void)), HttpPut]
        public async Task<IHttpActionResult> PutPurchase(string id, Purchase purchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != purchase.TransNo)
            {
                return BadRequest();
            }

            try
            {
                await _SVC.UpdatePurchase(purchase);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Purchase
        [ResponseType(typeof(Purchase)), HttpPost]
        public async Task<IHttpActionResult> PostPurchase(string[] transnos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var xPreData = _SVC.LookupOrder(new OrderQueryViewModel() { TransNos = transnos.ToList() });
            Purchase purchase = new Purchase() { TradeDate = DateTime.Today };

            try
            {
                await _SVC.AddPurchase(purchase, xPreData);
            }
            catch (DbUpdateException)
            {
                if (PurchaseExists(purchase.TransNo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = purchase.TransNo }, purchase);
        }

        // DELETE: api/Purchase/5
        [ResponseType(typeof(Purchase)), HttpDelete]
        public async Task<IHttpActionResult> DeletePurchase(string id)
        {
            Purchase purchase = _SVC.GetPurchase(id);
            if (purchase == null)
            {
                return NotFound();
            }

            await _SVC.DeletePurchase(purchase);
            return Ok(purchase);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _SVC.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PurchaseExists(string id)
        {
            return _SVC.GetPurchase(id) != null;
        }
    }
}
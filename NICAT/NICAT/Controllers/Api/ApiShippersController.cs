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
    public class ApiShippersController : ApiController
    {
        private GBService _SVC = new GBService();

        // GET: api/ApiShippers
        public List<ShipperDTOs> GetShipper(DateTime? StartDate, DateTime? EndDate, string Buyer)
        {
            return MapperConfig.mapper.Map<List<ShipperDTOs>>(_SVC.LookupShipper(new ShipperQueryViewModel()
            {
                TradeDate_S = StartDate,
                TradeDate_E = EndDate,
                Buyer = Buyer
            }));
        }

        // GET: api/ApiShippers/5
        [ResponseType(typeof(Shipper)), HttpGet]
        public async Task<IHttpActionResult> GetShipper(string id)
        {
            Shipper shipper = _SVC.GetShipper(id);
            if (shipper == null)
            {
                return NotFound();
            }

            return Ok(shipper);
        }

        // PUT: api/ApiShippers/5
        [ResponseType(typeof(void)), HttpPut]
        public async Task<IHttpActionResult> PutShipper(string id, Shipper shipper)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shipper.TransNo)
            {
                return BadRequest();
            }

            try
            {
                await _SVC.UpdateShipper(shipper);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShipperExists(id))
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

        // POST: api/ApiShippers
        [ResponseType(typeof(Shipper)), HttpPost]
        public async Task<IHttpActionResult> PostShipper(string[] transnos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var xDelivery = _SVC.LookupDeliveryType().First();
            var xPreData = _SVC.LookupTrading(new TradingQueryViewModel() { TransNos = transnos.ToList() });
            Shipper shipper = new Shipper()
            {
                TradeDate = DateTime.Today,
                TradeAmount = xPreData.Sum(s => s.TradeAmount),
                Buyer = xPreData.First().Buyer,
                Delivery = xDelivery.ID,
                Fee = decimal.Zero
            };

            try
            {
                await _SVC.AddShipper(shipper, xPreData);
            }
            catch (DbUpdateException)
            {
                if (ShipperExists(shipper.TransNo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = shipper.TransNo }, shipper);
        }

        // DELETE: api/ApiShippers/5
        [ResponseType(typeof(Shipper)), HttpDelete]
        public async Task<IHttpActionResult> DeleteShipper(string id)
        {
            Shipper shipper = _SVC.GetShipper(id);
            if (shipper == null)
            {
                return NotFound();
            }
            
            await _SVC.DeleteShipper(shipper);
            return Ok(shipper);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _SVC.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShipperExists(string id)
        {
            return _SVC.GetShipper(id) != null;
        }
    }
}
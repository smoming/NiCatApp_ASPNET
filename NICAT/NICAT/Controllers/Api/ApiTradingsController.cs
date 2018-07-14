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
using NICAT.Models.ApiModel;
using System.Web.Http.Cors;

namespace NICAT.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ApiTradingsController : ApiController
    {
        private GBService _SVC = new GBService();

        // GET: api/ApiTradings
        public List<TradingDTOs> GetTrading(DateTime? StartDate, DateTime? EndDate, string Buyer, string CommodityID)
        {
            return MapperConfig.mapper.Map<List<TradingDTOs>>(_SVC.LookupTrading(new TradingQueryViewModel()
            {
                TradeDate_S = StartDate,
                TradeDate_E = EndDate,
                Buyer = Buyer,
                CommodityID = CommodityID
            }));
        }

        [HttpGet, Route("api/ApiTradings/GetUnShipped")]
        public List<TradingDTOs> GetUnShipped(string Buyer)
        {
            var unshipped = _SVC.LookupTrading(new TradingQueryViewModel() { Buyer = Buyer, IsShipped = false }).ToList();
            return MapperConfig.mapper.Map<List<TradingDTOs>>(unshipped);
        }

        // GET: api/ApiTradings/5
        [ResponseType(typeof(Trading)), HttpGet]
        public async Task<IHttpActionResult> GetTrading(string id)
        {
            Trading trading = _SVC.GetTrading(id);
            if (trading == null)
            {
                return NotFound();
            }

            return Ok(trading);
        }

        // PUT: api/ApiTradings/5
        [ResponseType(typeof(void)), HttpPut]
        public async Task<IHttpActionResult> PutTrading(string id, Trading trading)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trading.TransNo)
            {
                return BadRequest();
            }

            try
            {
                await _SVC.UpdateTrading(trading);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TradingExists(id))
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

        // POST: api/ApiTradings
        [ResponseType(typeof(Trading)), HttpPost]
        public async Task<IHttpActionResult> PostTrading(Trading trading)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _SVC.AddTrading(trading);
            }
            catch (DbUpdateException)
            {
                if (TradingExists(trading.TransNo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = trading.TransNo }, trading);
        }

        // DELETE: api/ApiTradings/5
        [ResponseType(typeof(Trading)), HttpDelete]
        public async Task<IHttpActionResult> DeleteTrading(string id)
        {
            Trading trading = _SVC.GetTrading(id);
            if (trading == null)
            {
                return NotFound();
            }

            await _SVC.DeleteTrading(trading);
            return Ok(trading);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _SVC.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TradingExists(string id)
        {
            return _SVC.GetTrading(id) != null;
        }
    }
}
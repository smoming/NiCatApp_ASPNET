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
    public class ApiReceiptController : ApiController
    {
        private GBService _SVC = new GBService();

        // GET: api/ApiReceipt
        public List<ReceiptDTOs> GetReceipt(DateTime? StartDate, DateTime? EndDate)
        {
            return MapperConfig.mapper.Map<List<ReceiptDTOs>>(_SVC.LookupReceipt(new ReceiptQueryViewModel()
            {
                TradeDate_S = StartDate,
                TradeDate_E = EndDate
            }));
        }

        // GET: api/ApiReceipt/5
        [ResponseType(typeof(Receipt)), HttpGet]
        public async Task<IHttpActionResult> GetReceipt(string id)
        {
            Receipt receipt = _SVC.GetReceipt(id);
            if (receipt == null)
            {
                return NotFound();
            }

            return Ok(receipt);
        }

        // PUT: api/ApiReceipt/5
        [ResponseType(typeof(void)), HttpPut]
        public async Task<IHttpActionResult> PutReceipt(string id, Receipt receipt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != receipt.TransNo)
            {
                return BadRequest();
            }

            try
            {
                await _SVC.UpdateReceipt(receipt);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReceiptExists(id))
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

        // POST: api/ApiReceipt
        [ResponseType(typeof(Receipt)), HttpPost]
        public async Task<IHttpActionResult> PostReceipt(string[] transnos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var xPreData = _SVC.LookupOrder(new OrderQueryViewModel() { TransNos = transnos.ToList() });
            Receipt receipt = new Receipt() { TradeDate = DateTime.Today, TradeAmount = xPreData.Sum(s => s.TradeAmount) };

            try
            {
                await _SVC.AddReceipt(receipt, xPreData);
            }
            catch (DbUpdateException)
            {
                if (ReceiptExists(receipt.TransNo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = receipt.TransNo }, receipt);
        }

        // DELETE: api/ApiReceipt/5
        [ResponseType(typeof(Receipt)), HttpDelete]
        public async Task<IHttpActionResult> DeleteReceipt(string id)
        {
            Receipt receipt = _SVC.GetReceipt(id);
            if (receipt == null)
            {
                return NotFound();
            }

            await _SVC.DeleteReceipt(_SVC.GetReceipt(id));
            return Ok(receipt);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _SVC.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReceiptExists(string id)
        {
            return _SVC.GetReceipt(id) != null;
        }
    }
}
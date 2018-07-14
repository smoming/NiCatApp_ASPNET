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
    public class ApiOrdersController : ApiController
    {
        private GBService _SVC = new GBService();

        // GET: api/ApiOrders
        public List<OrderDTOs> GetOrder(DateTime? StartDate, DateTime? EndDate, string CommodityID)
        {
            return MapperConfig.mapper.Map<List<OrderDTOs>>(_SVC.LookupOrder(new OrderQueryViewModel()
            {
                TradeDate_S = StartDate,
                TradeDate_E = EndDate,
                CommodityID = CommodityID
            }));
        }

        // GET: api/ApiOrders/GetUnPaid
        [HttpGet, Route("api/ApiOrders/GetUnPaid")]
        public List<OrderDTOs> GetUnPaid()
        {
            var unpay = _SVC.LookupOrder(new OrderQueryViewModel() { IsPaid = false }).ToList();
            return MapperConfig.mapper.Map<List<OrderDTOs>>(unpay);
        }

        [HttpGet, Route("api/ApiOrders/GetUnPurchase")]
        public List<OrderDTOs> GetUnPurchase()
        {
            var unpurchase = _SVC.LookupOrder(new OrderQueryViewModel() { IsPaid = true, IsPurchased = false }).ToList();
            return MapperConfig.mapper.Map<List<OrderDTOs>>(unpurchase);
        }

        // GET: api/ApiOrders/5
        [ResponseType(typeof(Order)), HttpGet]
        public async Task<IHttpActionResult> GetOrder(string id)
        {
            Order order = _SVC.GetOrder(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/ApiOrders/5
        [ResponseType(typeof(void)), HttpPut]
        public async Task<IHttpActionResult> PutOrder(string id, Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.TransNo)
            {
                return BadRequest();
            }

            try
            {
                await _SVC.UpdateOrder(order);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/ApiOrders
        [ResponseType(typeof(Order)), HttpPost]
        public async Task<IHttpActionResult> PostOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _SVC.AddOrder(order);
            }
            catch (DbUpdateException)
            {
                if (OrderExists(order.TransNo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = order.TransNo }, order);
        }

        // DELETE: api/ApiOrders/5
        [ResponseType(typeof(Order)), HttpDelete]
        public async Task<IHttpActionResult> DeleteOrder(string id)
        {
            Order order = _SVC.GetOrder(id);
            if (order == null)
            {
                return NotFound();
            }

            await _SVC.DeleteOrder(order);
            return Ok(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _SVC.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(string id)
        {
            return _SVC.GetOrder(id) != null;
        }
    }
}
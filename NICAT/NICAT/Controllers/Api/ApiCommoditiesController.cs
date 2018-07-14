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
    public class ApiCommoditiesController : ApiController
    {
        private GBService _SVC = new GBService();

        // GET: api/ApiCommodities
        public List<CommodityDTOs> GetCommodity()
        {
            return MapperConfig.mapper.Map<List<CommodityDTOs>>(_SVC.LookupCommodity());
        }

        // GET: api/ApiCommodities/5
        [ResponseType(typeof(Commodity)), HttpGet]
        public async Task<IHttpActionResult> GetCommodity(string id)
        {
            Commodity commodity = _SVC.GetCommodity(id);
            if (commodity == null)
            {
                return NotFound();
            }

            return Ok(commodity);
        }

        // PUT: api/ApiCommodities/5
        [ResponseType(typeof(void)), HttpPut]
        public async Task<IHttpActionResult> PutCommodity(string id, Commodity commodity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != commodity.ID)
            {
                return BadRequest();
            }

            try
            {
                await _SVC.UpdateCommodity(commodity);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommodityExists(id))
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

        // POST: api/ApiCommodities
        [ResponseType(typeof(Commodity)), HttpPost]
        public async Task<IHttpActionResult> PostCommodity(Commodity commodity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _SVC.AddCommodity(commodity);
            }
            catch (DbUpdateException)
            {
                if (CommodityExists(commodity.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = commodity.ID }, commodity);
        }

        // DELETE: api/ApiCommodities/5
        [ResponseType(typeof(Commodity)), HttpDelete]
        public async Task<IHttpActionResult> DeleteCommodity(string id)
        {
            Commodity commodity = _SVC.GetCommodity(id);
            if (commodity == null)
            {
                return NotFound();
            }

            await _SVC.DeleteCommodity(commodity);
            return Ok(commodity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _SVC.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommodityExists(string id)
        {
            return _SVC.GetCommodity(id) != null;
        }
    }
}
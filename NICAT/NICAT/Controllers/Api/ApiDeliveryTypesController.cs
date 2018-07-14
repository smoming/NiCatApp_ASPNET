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
    public class ApiDeliveryTypesController : ApiController
    {
        private GBService _SVC = new GBService();

        // GET: api/ApiDeliveryTypes
        public List<DeliveryTypeDTOs> GetDeliveryType()
        {
            return MapperConfig.mapper.Map<List<DeliveryTypeDTOs>>(_SVC.LookupDeliveryType());
        }

        // GET: api/ApiDeliveryTypes/5
        [ResponseType(typeof(DeliveryType)), HttpGet]
        public IHttpActionResult GetDeliveryType(string id)
        {
            DeliveryType deliveryType = _SVC.GetDeliveryType(id);
            if (deliveryType == null)
            {
                return NotFound();
            }

            return Ok(deliveryType);
        }

        // PUT: api/ApiDeliveryTypes/5
        [ResponseType(typeof(void)), HttpPut]
        public async Task<IHttpActionResult> PutDeliveryType(string id, DeliveryType deliveryType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != deliveryType.ID)
            {
                return BadRequest();
            }

            try
            {
                await _SVC.UpdateDeliveryType(deliveryType);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryTypeExists(id))
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

        // POST: api/ApiDeliveryTypes
        [ResponseType(typeof(DeliveryType)), HttpPost]
        public async Task<IHttpActionResult> PostDeliveryType(DeliveryType deliveryType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _SVC.AddDeliveryType(deliveryType);
            }
            catch (DbUpdateException)
            {
                if (DeliveryTypeExists(deliveryType.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = deliveryType.ID }, deliveryType);
        }

        // DELETE: api/ApiDeliveryTypes/5
        [ResponseType(typeof(DeliveryType)), HttpDelete]
        public async Task<IHttpActionResult> DeleteDeliveryType(string id)
        {
            DeliveryType deliveryType = _SVC.GetDeliveryType(id);
            if (deliveryType == null)
            {
                return NotFound();
            }

            await _SVC.DeleteDeliveryType(deliveryType);
            return Ok(deliveryType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _SVC.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DeliveryTypeExists(string id)
        {
            return _SVC.GetDeliveryType(id) != null;
        }
    }
}
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
    public class ApiSuppliersController : ApiController
    {
        private GBService _SVC = new GBService();

        // GET: api/ApiSuppliers
        public List<SupplierDTOs> GetSupplier()
        {
            return MapperConfig.mapper.Map<List<SupplierDTOs>>(_SVC.LookupSupplier());
        }

        // GET: api/ApiSuppliers/5
        [ResponseType(typeof(Supplier)), HttpGet]
        public async Task<IHttpActionResult> GetSupplier(string id)
        {
            Supplier supplier = _SVC.GetSupplier(id);
            if (supplier == null)
            {
                return NotFound();
            }

            return Ok(supplier);
        }

        // PUT: api/ApiSuppliers/5
        [ResponseType(typeof(void)), HttpPut]
        public async Task<IHttpActionResult> PutSupplier(string id, Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != supplier.ID)
            {
                return BadRequest();
            }

            try
            {
                await _SVC.UpdateSupplier(supplier);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierExists(id))
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

        // POST: api/ApiSuppliers
        [ResponseType(typeof(Supplier)), HttpPost]
        public async Task<IHttpActionResult> PostSupplier(Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _SVC.AddSupplier(supplier);
            }
            catch (DbUpdateException)
            {
                if (SupplierExists(supplier.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = supplier.ID }, supplier);
        }

        // DELETE: api/ApiSuppliers/5
        [ResponseType(typeof(Supplier)), HttpDelete]
        public async Task<IHttpActionResult> DeleteSupplier(string id)
        {
            Supplier supplier = _SVC.GetSupplier(id);
            if (supplier == null)
            {
                return NotFound();
            }

            await _SVC.DeleteSupplier(supplier);
            return Ok(supplier);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _SVC.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SupplierExists(string id)
        {
            return _SVC.GetSupplier(id) != null;
        }
    }
}
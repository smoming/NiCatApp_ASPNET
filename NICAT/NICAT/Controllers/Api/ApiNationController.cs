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
    public class ApiNationController : ApiController
    {
        private GBService _SVC = new GBService();

        // GET: api/ApiNation
        public List<NationDTOs> GetNation()
        {
            return MapperConfig.mapper.Map<List<NationDTOs>>(_SVC.LookupNation());
        }

        // GET: api/ApiNation/5
        [ResponseType(typeof(Nation)), HttpGet]
        public async Task<IHttpActionResult> GetNation(string id)
        {
            Nation nation = _SVC.GetNation(id);
            if (nation == null)
            {
                return NotFound();
            }

            return Ok(nation);
        }

        // PUT: api/ApiNation/5
        [ResponseType(typeof(void)), HttpPut]
        public async Task<IHttpActionResult> PutNation(string id, Nation nation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nation.ID)
            {
                return BadRequest();
            }

            try
            {
                await _SVC.UpdateNation(nation);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NationExists(nation.ID))
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

        // POST: api/ApiNation
        [ResponseType(typeof(Nation)), HttpPost]
        public async Task<IHttpActionResult> PostNation(Nation nation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _SVC.AddNation(nation);
            }
            catch (DbUpdateException)
            {
                if (NationExists(nation.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = nation.ID }, nation);
        }

        // DELETE: api/ApiNation/5
        [ResponseType(typeof(Nation)), HttpDelete]
        public async Task<IHttpActionResult> DeleteNation(string id)
        {
            Nation nation = _SVC.GetNation(id);
            if (nation == null)
            {
                return NotFound();
            }

            await _SVC.DeleteNation(nation);
            return Ok(nation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _SVC.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NationExists(string id)
        {
            return _SVC.GetNation(id) != null;
        }
    }
}
using NICAT.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace NICAT.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ApiCashFlowController : ApiController
    {
        private GBService _SVC;        

        public ApiCashFlowController()
            : base()
        {
            _SVC = new GBService();
        }

        [HttpPost, Route("api/ApiCashFlow/DoAccound")]
        public async Task<IHttpActionResult> DoAccound(DateTime xExeDate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _SVC.DoAccound(xExeDate);
                if (!result.isSuccess)
                    return BadRequest(result.Message);
                else
                    return Ok();
            }
            catch (Exception)
            {
                throw;
            }

            return StatusCode(HttpStatusCode.BadRequest);
        }

        [HttpPost, Route("api/ApiCashFlow/DoUnAccound")]
        public async Task<IHttpActionResult> DoUnAccound(DateTime xExeDate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _SVC.DoUnAccound(xExeDate);
                if (!result.isSuccess)
                    return BadRequest(result.Message);
                else
                    return Ok();
            }
            catch (Exception)
            {
                throw;
            }

            return StatusCode(HttpStatusCode.BadRequest);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DevelopersAPI.Controllers
{
    [RoutePrefix("developers")]
    public class DevelopersController : ApiController
    {
        private IDeveloperRepository _developersRep;

        public DevelopersController(IDeveloperRepository repo)
        {
            _developersRep = repo;
        }
        [Route("all/"), HttpGet]
        public IHttpActionResult getAll()
        {
            return Ok(_developersRep.getAll());
        }
        [Route("bylevel/{level?}/{type?}"), HttpGet]
        public IHttpActionResult getAll(int level = 0, string type = "")
        {

            return Ok(_developersRep.getAll());
        }
    }
}

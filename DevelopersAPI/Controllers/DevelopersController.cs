using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DevelopersAPI.Controllers
{
    [RoutePrefix("api/developers")]
    public class DevelopersController : ApiController
    {
        private IDeveloperRepository _developersRep;

        public DevelopersController(IDeveloperRepository repo)
        {
            _developersRep = repo;
        }
        [Route(""), HttpGet]
        public IHttpActionResult getAll()
        {
            return Ok(_developersRep.getAll());
        }
        [Route("bylevel/{type?}"), HttpGet]
        public IHttpActionResult getAll(string type = "")
        {
            int level_FromSettings = 0;
            int level = (int.TryParse(ConfigurationManager.AppSettings.Get("developerLevel"), out level_FromSettings)) ? level_FromSettings : 0;
            return Ok(_developersRep.getByLevel(level, type));
        }
    }
}

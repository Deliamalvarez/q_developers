using DevelopersAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace DevelopersAPI.Controllers
{
    [RoutePrefix("api/developers")]
    public class DevelopersController : ApiController
    {
        private IDeveloperRepository _developersRep = new DeveloperRepository();
        public DevelopersController()
        {

        }

        public DevelopersController(IDeveloperRepository repo)
        {
            _developersRep = repo;
        }

        [Route(""), HttpGet]
        public async Task<IEnumerable<Developer>> GetAllAsync()
        {
            return await Task.FromResult(_developersRep.GetAll());
        }

        [Route("sync/"), HttpGet]
        public IEnumerable<Developer> GetAllSync()
        {
            return _developersRep.GetAll();
        }

        [Route("bylevel/{byType?}"), HttpGet]
        public async Task<IEnumerable<Developer>> GetByLevel(Boolean byType = false)
        {
            int level_FromSettings = 0;
            int level = (int.TryParse(ConfigurationManager.AppSettings.Get("developerLevel"), out level_FromSettings)) ? level_FromSettings : 0;
            return await Task.FromResult(_developersRep.GetByLevel(level, byType));
        }
    }
}

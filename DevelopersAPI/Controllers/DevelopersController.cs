using DeveloperModel.DTO;
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
    /*
     * The controller class that handles the required endpoints
     * */
    [RoutePrefix("api/developers")]
    public class DevelopersController : ApiController
    {
        private IDeveloperRepository _developersRep;
        public DevelopersController()
        {
            _developersRep = new DeveloperRepository();
        }

        public DevelopersController(IDeveloperRepository repo)
        {
            _developersRep = repo;
        }
        /// <summary>
        /// Retrieve asynchronously all developers included in given json file
        /// </summary>
        [Route(""), HttpGet]
        public async Task<IEnumerable<Developer>> GetAllAsync()
        {
            return await Task.FromResult(_developersRep.GetAll());
        }
        /// <summary>
        /// Retrieve synchronously all developers included in given json file
        /// </summary>
        [Route("sync/"), HttpGet]
        public IEnumerable<Developer> GetAllSync()
        {
            return _developersRep.GetAll();
        }
        /// <summary>
        /// Retrieve developers with level equal or greater than 8, 
        /// if optional parameter bytype is specified with true, 
        /// it returns as the exercise ask for: only the skills that are of the same type of the already filtered skills
        /// on step 1.
        /// </summary>
        [Route("bylevel/{byType?}"), HttpGet]
        public async Task<IEnumerable<Developer>> GetByLevel(Boolean byType = false)
        {
            int level_FromSettings = 0;
            int level = (int.TryParse(ConfigurationManager.AppSettings.Get("developerLevel"), out level_FromSettings)) ? level_FromSettings : 8;
            return await Task.FromResult(_developersRep.GetByLevel(level, byType));
        }
    }
}

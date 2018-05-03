using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using DeveloperModel.DTO;

namespace LoadDevelopers.Controllers
{
    [RoutePrefix("api/alldevs")]
    public class AllDevsController : ApiController
    {
        private readonly String filepath = ConfigurationManager.AppSettings.Get("developersJSON");

        [Route(""), HttpGet]
        public async Task<IEnumerable<Developer>> GetAllAsync()
        {
            return await LoadJSON();
        }

        private async Task<List<Developer>> LoadJSON()
        {
            var data = String.Empty;
            try
            {
                string path = System.Web.HttpContext.Current.ApplicationInstance.Server.MapPath(filepath);
                using (StreamReader r = new StreamReader(path))
                {
                    data = await r.ReadToEndAsync();

                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Developer>>(data);
        }

    }
}

using DevelopersAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace DevelopersAPI.Controllers
{
    public interface IDeveloperRepository
    {
        IEnumerable<Developer> getAll();
        IEnumerable<Developer> getByLevel(int level, string type = "");
    }
    public class DeveloperRepository: IDeveloperRepository
    {
        private List<Developer> _developers;
        private readonly String filepath = ConfigurationManager.AppSettings.Get("developersJSON");
        public DeveloperRepository()
        {
            _developers = this.LoadJSON();
        }

        private List<Developer> LoadJSON()
        {
            List<Developer> devs = null;
            try
            {
                string path = System.Web.HttpContext.Current.ApplicationInstance.Server.MapPath(filepath);
                using (StreamReader r = new StreamReader(path))
                {
                    var data = r.ReadToEnd();
                    devs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Developer>>(data);
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
            return devs;
        }
        public IEnumerable<Developer> getAll()
        {
            return this._developers;
        }

        public IEnumerable<Developer> getByLevel(int level, string type = "")
        {
            /* var skills = _developers.SelectMany(d => d.skills.Where(s=>s.level >= level)).ToList();
             return _developers.Where(dev => dev.skills.Where(s => skills.Intersect(skills))).Distinct();*/
            var filtered_devs = _developers.Select(d => new Developer()
            {
                age = d.age,
                firstName = d.firstName,
                lastName = d.lastName,
                skills = d.skills.Where(s => s.level >= level).ToList(),
            })
            .Where(devs =>devs.skills.Count > 0);
            if (!String.IsNullOrEmpty(type))
            {
                filtered_devs = filtered_devs.Select(d => new Developer()
                {
                    age = d.age,
                    firstName = d.firstName,
                    lastName = d.lastName,
                    skills = d.skills.Where(s => s.type == type).ToList()
                });
            }
            return filtered_devs;
        }
    }
}
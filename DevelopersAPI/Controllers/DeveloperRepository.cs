using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DeveloperModel.DTO;
using System.Net.Http;
using Newtonsoft.Json;

namespace DevelopersAPI.Controllers
{
    public interface IDeveloperRepository
    {
        IEnumerable<Developer> GetAll();
        IEnumerable<Developer> GetByLevel(int level, Boolean byType = false);
    }
    public class DeveloperRepository: IDeveloperRepository
    {
        private List<Developer> _developers;
        private readonly String filepath = ConfigurationManager.AppSettings.Get("developersJSON");
        public DeveloperRepository(List<Developer> developers = null)
        {
            _developers = developers?? LoadDevs().Result;
        }

        private async Task<List<Developer>> LoadDevs()
        {
            string uri = ConfigurationManager.AppSettings.Get("developerServer").ToString();
            if (!String.IsNullOrEmpty(uri))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(uri);
                    HttpResponseMessage response = client.GetAsync(uri).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        return await Task.FromResult(JsonConvert.DeserializeObject<List<Developer>>(data));
                    }
                }
            }
            return null;
        }
        
        public IEnumerable<Developer> GetAll()
        {
            return this._developers;
        }

        public IEnumerable<Developer> GetByLevel(int level, Boolean byType = false)
        {
            var filtered_devs = _developers.Select(d => new Developer()
            {
                Age = d.Age,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Skills = d.Skills.Where(s => s.Level >= level).OrderByDescending(s=> s.Level).ToList()
                
            })
            .Where(devs =>devs.Skills.Count > 0).ToList();
            if (byType)
            {
                foreach( Developer dev in filtered_devs)
                {
                    List<Skill> skilllsByLevel = dev.Skills.ToList();
                    List<Skill> allSkills = _developers.Where(d => d.FirstName == dev.FirstName && d.LastName == dev.LastName).First().Skills;
                    IEnumerable<string> types = allSkills.Select(s => s.Type).Intersect(skilllsByLevel.Select(s => s.Type));
                    allSkills = allSkills.Where(s => types.Contains(s.Type)).AsEnumerable().OrderByDescending(s => s.Level).GroupBy(s => s.Type).SelectMany(s=>s).ToList();
                    dev.Skills = allSkills;
                }
            }
            return filtered_devs;
        }
    }
}
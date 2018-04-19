using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevelopersAPI.Models
{
    public class Developer
    {
        public String firstName { get; set; }
        public String lastName { get; set; }
        public int age { get; set; }
        public List<Skill> skills { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevelopersAPI.Models
{
    //const string[] types  = ["backend", "frontend"];
    public class Skill
    {
        public String name { get; set; }
        public String type { get; set; }
        public int level { get; set; }
    }
}
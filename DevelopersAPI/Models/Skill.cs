using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevelopersAPI.Models
{
    //const string[] types  = ["backend", "frontend"];
    public class Skill
    {
        public String Name { get; set; }
        public String Type { get; set; }
        public int Level { get; set; }
    }
}
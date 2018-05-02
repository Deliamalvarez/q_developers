using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeveloperModel.DTO
{
    public class Developer
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public int Age { get; set; }
        public List<Skill> Skills { get; set; }
    }
}
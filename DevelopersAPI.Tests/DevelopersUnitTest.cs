using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DevelopersAPI.Controllers;
using System.Threading.Tasks;
using DeveloperModel.DTO;


namespace DevelopersAPI.Tests
{
    [TestClass]
    public class DevelopersUnitTest
    {
        List<Developer> testDevs;
        IDeveloperRepository testRepo;
        DevelopersController controller;
        [TestInitialize]
        public void TestInitialize()
        {
            testDevs = GetDevelopers();
            testRepo = new DeveloperRepository(testDevs);
            controller = new DevelopersController(testRepo);
        }
        [TestMethod]
        public void GetAllDevelopers_ShouldReturnAllDevelopersSync()
        {
            var result = controller.GetAllSync() as List<Developer>;
            Assert.AreEqual(testDevs.Count, result.Count);
        }
        [TestMethod]
        public async Task GetAllDevelopers_ShouldReturnAllDevelopersAsync()
        {
            var result = await controller.GetAllAsync() as List<Developer>;
            Assert.AreEqual(testDevs.Count, result.Count);
        }

        [TestMethod]
        public async Task GetAllDevelopers_ShouldReturnDevelopersWithLevelGreaterThan8()
        {
            var result = await controller.GetByLevel() as List<Developer>;
            Assert.IsNotNull(result);
            Assert.AreEqual(2, (result[0] as Developer).Skills.Count);
            Assert.AreEqual(2, (result[1] as Developer).Skills.Count);
        }

        private List<Developer> GetDevelopers() => new List<Developer>() {
                new Developer()
                {
                    FirstName= "First",
                    LastName= "Dev Test",
                    Age= 34,
                     Skills= new List<Skill>()
                       {
                         new Skill(){Name = "Angular", Level = 8, Type="frontend"},
                         new Skill(){Name = "React", Level = 3, Type="frontend"},
                         new Skill(){Name = "CSS", Level = 10, Type="frontend"}
                        }
                },
                new Developer()
                {
                    FirstName= "Second",
                    LastName= "Dev Test",
                    Age= 34,
                     Skills= new List<Skill>()
                       {
                         new Skill(){Name = "NodeJs", Level = 8, Type="backend"},
                         new Skill(){Name = "React", Level = 3, Type="frontend"},
                         new Skill(){Name = "CSS", Level = 10, Type="frontend"}
                        }
                }
            };
    }
}

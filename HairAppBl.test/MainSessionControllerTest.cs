using NUnit.Framework;
using HairAppBl.Models;
using HairAppBl.Controller;
using System;
using System.Collections.Generic;

namespace HairAppBl.Tests
{
    public class MainSessionControllerTest
    {
        

        [Test]
        public void Init()
        {
            MainSessionController c = new MainSessionController(new Dictionary<string, object>());
            c.Init();
            Assert.True(c.GetAllDefinitions().Count > 0);
        }
        
        
            [Test]
        public void Init()
        {
            MainSessionController c = new MainSessionController(new Dictionary<string, object>());
            c.Init();
            var days = c.GetAllWashingDays();
            var wdd = new WashingDayDefinition();
            wdd.ID = "Foo";
            days.Add(new WashingDayDefinition());
             
            Assert.AreEqual(wdd,c.GetWashingDayById("Foo"));
        }
        







    }
}

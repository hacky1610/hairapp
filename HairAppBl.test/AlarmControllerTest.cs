using NUnit.Framework;
using HairAppBl.Models;
using HairAppBl.Controller;
using System;
using System.Collections.Generic;

namespace HairAppBl.Tests
{
    public class AlarmControllerTest
    {
    
        [Test]
        public void ControllerCanBeLoaded()
        {
            AlarmController ac = new AlarmController();
            var days = ac.GetWashDays();
            Assert.NotNull(days);
        }

     





    }
}

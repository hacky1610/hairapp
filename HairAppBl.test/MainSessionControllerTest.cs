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







    }
}

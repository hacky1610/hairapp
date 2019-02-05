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
            MainSessionController c = new MainSessionController(new Dictionary<string, string>());
            c.Init();
            Assert.NotNull(c.MainSession);
        }







    }
}

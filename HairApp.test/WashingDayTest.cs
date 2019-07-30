using NUnit.Framework;
using HairAppBl.Models;
using HairAppBl.Controller;
using System.Collections.Generic;

namespace HairApp.Tests
{
    public class WashingDayTest
    {
        [SetUp]
        public void SetUp()
        {
            Xamarin.Forms.Mocks.MockForms.Init();
        }

        [Test]
        public void Instantiate()
        {
            var r = new RoutineDefinition();
            r.Name = "";
            var dic = new Dictionary<string, object>();
            var hbl = new HairAppBl.HairAppBl(new HairAppBl.ConsoleLogger(),dic);
            dic.Add("RoutineFrame", null); 
            dic.Add("RoutineContent", null);
            dic.Add("RoutineButton", null);
            new HairApp.Controls.RoutineDefinitionCell(r,hbl,1);
        }

       

    }
}
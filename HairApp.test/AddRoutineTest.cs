using NUnit.Framework;
using HairAppBl.Models;
using HairAppBl.Controller;
using System.Collections.Generic;
using HairAppBl;

namespace HairApp.Tests
{
    public class AddRoutineTest
    {
        [SetUp]
        public void SetUp()
        {
            Xamarin.Forms.Mocks.MockForms.Init();
        }

        [Test]
        public void Instantiate()
        {
            var r = new HairApp.RoutineCellObject(new RoutineDefinition());
            r.Name = "";
            var dic = new Dictionary<string, object>();
            var hbl = new HairAppBl.HairAppBl(new ConsoleLogger(),dic);
            dic.Add("RoutineFrame", null); 
            dic.Add("RoutineContent", null);
            dic.Add("RoutineButton", null);
            dic.Add("RoutineFrameSelect", null);
           
           var c =  new HairApp.Controls.AddRoutineCell(r,hbl);
            Assert.False(c.Checked);
        }

       

    }
}

using NUnit.Framework;
using HairAppBl.Models;
using HairAppBl.Controller;
using System.Collections.Generic;

namespace HairAppBl.Tests
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
            var hbl = new HairAppBl(new ConsoleLogger(),dic);
            dic.Add("RoutineFrame", null); 
            dic.Add("RoutineContent", null);
            dic.Add("RoutineButton", null);
            dic.Add("RoutineFrameSelect", null);
            
            
            new HairApp.Controls.AddRoutineCell(r,hbl);
        }

       

    }
}

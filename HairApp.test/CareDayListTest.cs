using NUnit.Framework;
using HairAppBl.Models;
using HairAppBl.Controller;
using System.Collections.Generic;

namespace HairAppBl.Tests
{
    public class CareDayListTest
    {
        [SetUp]
        public void SetUp()
        {
            Xamarin.Forms.Mocks.MockForms.Init();
        }

        [Test]
        public void Instantiate()
        {
            var dic = new Dictionary<string, object>();
            var hbl = new HairAppBl(new ConsoleLogger(),dic);
            dic.Add("RoutineFrame", null); 
            dic.Add("RoutineContent", null);
            dic.Add("RoutineButton", null);
            dic.Add("RoutineFrameSelect", null);

            //var r = new HairApp.Controls.CareDayList()

            //Assert.False(c.Checked);
        }

       

    }
}

using NUnit.Framework;
using HairAppBl.Models;
using HairAppBl.Controller;
using System;
using System.Collections.Generic;

namespace HairAppBl.Tests
{
    public class FutureDayControllerTest
    {

    
        [Test]
        public void SingleAddIsWorking()
        {
            var dbMock = new DbMock();
            
            FutureDayListController<string> contr = new FutureDayListController<string>();
            contr.Add("A", ScheduleController.GetToday());
            contr.Add("B", ScheduleController.GetToday().AddDays(1));
            contr.Add("C", ScheduleController.GetToday().AddDays(1));

            Assert.AreEqual("A", contr.GetAllDays()[ScheduleController.GetToday()][0]);
            Assert.AreEqual(2, contr.GetAllDays()[ScheduleController.GetToday().AddDays(1)].Count);
        }

 









    }
}

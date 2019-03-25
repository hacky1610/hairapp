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
        public void GetWashingDayById_GetIsWorking()
        {
            MainSessionController c = new MainSessionController(new Dictionary<string, object>());
            c.Init();
            var days = c.GetAllWashingDays();
            var wdd = new WashingDayDefinition();
            wdd.ID = "Foo";
            days.Add(wdd);
             
            Assert.AreEqual(wdd,c.GetWashingDayById("Foo"));
        }
        
        [Test]
        public void Restore_IsWorking()
        {
            MainSessionController c = new MainSessionController(new Dictionary<string, object>());
            c.Init();
            var days = c.GetAllWashingDays();
            var wdd = new WashingDayDefinition();
            wdd.ID = "Foo";
            days.Add(wdd);
            
            c.Save();
            c.GetAllWashingDays().Clear();
            c.Restore();
            Assert.AreEqual(1,c.GetAllWashingDays().Count);
            Assert.AreEqual("Foo",c.GetAllWashingDays()[0].ID);
        }

        [Test]
        public void GetFutureDaysIsWorking()
        {
            MainSessionController c = new MainSessionController(new Dictionary<string, object>());
            c.Init();
            var days = c.GetAllWashingDays();
            var wdd = new WashingDayDefinition();
            wdd.Scheduled.Type = ScheduleDefinition.ScheduleType.Dayly;
            wdd.Scheduled.DaylyPeriod.Period = 1;
            days.Add(wdd);

            var fDays = c.GetFutureDays();

            

            Assert.AreEqual(wdd, c.GetFutureDays()[ScheduleController.GetToday()][0]);
        }









    }
}

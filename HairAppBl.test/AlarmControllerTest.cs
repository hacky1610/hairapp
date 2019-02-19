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
            var dbMock = new DbMock();
            
            AlarmController ac = new AlarmController(dbMock);
            var days = ac.GetWashDays();
            Assert.NotNull(days);
        }

        [Test]
        public void DayCanBeSaved()
        {
            var dbMock = new DbMock();

            AlarmController ac = new AlarmController(dbMock);
            var schedule = new ScheduleDefinition();
            schedule.WeeklyPeriod.Period = 4;
            var day = new ScheduleSqlDefinition(schedule, "Foo", "Bar");
            ac.SaveWashDay(day);
            var restored = ac.Load();
            
            Assert.AreEqual(4, restored["Foo"].Value.WeeklyPeriod.Period);
        }









    }
}

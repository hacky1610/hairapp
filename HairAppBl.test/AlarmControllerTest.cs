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
            
            AlarmController ac = new AlarmController(dbMock,dbMock);
            var days = ac.GetTodayWashDays();
            Assert.NotNull(days);
        }

        [Test]
        public void DayCanBeSaved()
        {
            var dbMock = new DbMock();

            AlarmController ac = new AlarmController(dbMock,dbMock);
            var schedule = new ScheduleDefinition();
            schedule.WeeklyPeriod.Period = 4;
            var day = new ScheduleSqlDefinition(schedule, "Foo", "Bar");
            ac.SaveWashDay(day);
            var restored = ac.LoadScheduleDatabase();
            
            Assert.AreEqual(4, restored["Foo"].Value.WeeklyPeriod.Period);
        }

        [Test]
        public void DayCanBeSavedTwice()
        {
            var dbMock = new DbMock();

            AlarmController ac = new AlarmController(dbMock,dbMock);
            var schedule = new ScheduleDefinition();
            schedule.WeeklyPeriod.Period = 4;
            var day = new ScheduleSqlDefinition(schedule, "Foo", "Bar");
            ac.SaveWashDay(day);
            schedule.WeeklyPeriod.Period = 5;
            ac.SaveWashDay(day);

            var restored = ac.LoadScheduleDatabase();

            Assert.AreEqual(5, restored["Foo"].Value.WeeklyPeriod.Period);
        }


        [Test]
        public void DayCanBeDeleted()
        {
            var dbMock = new DbMock();

            AlarmController ac = new AlarmController(dbMock,dbMock);
            var schedule = new ScheduleDefinition();
            schedule.WeeklyPeriod.Period = 4;
            var day = new ScheduleSqlDefinition(schedule, "Foo", "Bar");
            ac.SaveWashDay(day);
            ac.DeleteWashDay("Foo");
            var restored = ac.LoadScheduleDatabase();
            Assert.AreEqual(0,restored.Count);
        }

        [Test]
        public void WashDaysCanBeRead()
        {
            var dbMock = new DbMock();

            AlarmController ac = new AlarmController(dbMock,dbMock);
            var schedule = new ScheduleDefinition();
            schedule.Type = ScheduleDefinition.ScheduleType.Dayly;
            schedule.DaylyPeriod.Period = 1;
            var day = new ScheduleSqlDefinition(schedule, "Foo", "Bar");
            ac.SaveWashDay(day);
            var days = ac.GetTodayWashDays();
            Assert.AreEqual(1, days.Count);
        }









    }
}

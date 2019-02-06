using NUnit.Framework;
using HairAppBl.Models;
using HairAppBl.Controller;
using System;
using System.Collections.Generic;

namespace HairAppBl.Tests
{
    public class ScheduleControllerTest
    {
    
      
        [Test]
        public void GetNextWeekDay_OneDayBefore()
        {
            var d = new DateTime(2019, 2, 4);
            var res = ScheduleController.GetNextWeekDay(d, DayOfWeek.Tuesday);
            Assert.AreEqual(5, res.Day);
        }

        [Test]
        public void GetNextWeekDay_CurrentDay()
        {
            var d = new DateTime(2019, 2, 4);
            var res = ScheduleController.GetNextWeekDay(d, DayOfWeek.Monday);
            Assert.AreEqual(4, res.Day);
        }

        [Test]
        public void GetScheduledDays_CheckDefault()
        {
            WashingDayDefinition wd = new WashingDayDefinition();
            WashingDayEditorController wdc = new WashingDayEditorController(wd, allRoutines);
            var res = wdc.GetScheduledDays();

            Assert.AreEqual(DayOfWeek.Monday, res[0].DayOfWeek );
            Assert.AreEqual(DayOfWeek.Monday, res[1].DayOfWeek);
        }

        [Test]
        public void GetScheduledDays_CheckOtherWeekDay()
        {
            WashingDayDefinition wd = new WashingDayDefinition();
            WashingDayEditorController wdc = new WashingDayEditorController(wd, allRoutines);
            wdc.GetModel().Scheduled.WeeklyPeriod.WeekDays = new System.Collections.Generic.List<DayOfWeek>
            {
                DayOfWeek.Sunday
            };
      
            var res = wdc.GetScheduledDays();

            Assert.AreEqual(DayOfWeek.Sunday, res[0].DayOfWeek);
            Assert.AreEqual(DayOfWeek.Sunday, res[1].DayOfWeek);
        }

        [Test]
        public void GetScheduledDays_Period2()
        {
            WashingDayDefinition wd = new WashingDayDefinition();
            WashingDayEditorController wdc = new WashingDayEditorController(wd, allRoutines);
            wdc.GetModel().Scheduled.WeeklyPeriod.Period = 2;
            wdc.GetModel().Scheduled.StartDate = new DateTime(2019, 2, 5);

            var res = wdc.GetScheduledDays();

            Assert.True(ScheduleController.IsSameDay(res[0],new DateTime(2019,2,11)));
            Assert.True(ScheduleController.IsSameDay(res[1],new DateTime(2019,2,25)));
        }

        [Test]
        public void GetScheduledDays_Period2_twoDays()
        {
            WashingDayDefinition wd = new WashingDayDefinition();
            WashingDayEditorController wdc = new WashingDayEditorController(wd, allRoutines);
            wdc.GetModel().Scheduled.WeeklyPeriod.WeekDays = new System.Collections.Generic.List<DayOfWeek>
            {
                DayOfWeek.Sunday,
                DayOfWeek.Thursday
            };
            wdc.GetModel().Scheduled.WeeklyPeriod.Period = 2;
            wdc.GetModel().Scheduled.StartDate = new DateTime(2019, 2, 5);

            var res = wdc.GetScheduledDays();


            Assert.True(ScheduleController.ContainsDay(res, new DateTime(2019, 2, 7)));
            Assert.True(ScheduleController.ContainsDay(res, new DateTime(2019, 2, 10)));
            Assert.True(ScheduleController.ContainsDay(res, new DateTime(2019, 2,21)));
            Assert.True(ScheduleController.ContainsDay(res, new DateTime(2019, 2, 24)));
        }
        
        [Test]
        public void IsCareDay_TodayIsCareDay()
        {
            WashingDayDefinition wd = new WashingDayDefinition();
            WashingDayEditorController wdc = new WashingDayEditorController(wd, allRoutines);
            wdc.GetModel().Scheduled.WeeklyPeriod.WeekDays = new System.Collections.Generic.List<DayOfWeek>
            {
                DayOfWeek.Sunday,
                DayOfWeek.Thursday
            };
            wdc.GetModel().Scheduled.WeeklyPeriod.Period = 2;
            wdc.GetModel().Scheduled.StartDate = new DateTime(2019, 2, 5);

            var controller = wdc.GetScheduleController();


            Assert.True(controller.IsCareDay(new DateTime(2019, 2, 7)));
        }
        
         [Test]
        public void IsCareDay_TodayIsNoCareDay()
        {
            WashingDayDefinition wd = new WashingDayDefinition();
            WashingDayEditorController wdc = new WashingDayEditorController(wd, allRoutines);
            wdc.GetModel().Scheduled.WeeklyPeriod.WeekDays = new System.Collections.Generic.List<DayOfWeek>
            {
                DayOfWeek.Sunday,
                DayOfWeek.Thursday
            };
            wdc.GetModel().Scheduled.WeeklyPeriod.Period = 2;
            wdc.GetModel().Scheduled.StartDate = new DateTime(2019, 2, 5);

            var controller = wdc.GetScheduleController();


            Assert.False(controller.IsCareDay(new DateTime(2019, 2, 8)));
        }
        
        [Test]
        public void Time2CareDay_is2()
        {
            WashingDayDefinition wd = new WashingDayDefinition();
            WashingDayEditorController wdc = new WashingDayEditorController(wd, allRoutines);
            wdc.GetModel().Scheduled.WeeklyPeriod.WeekDays = new System.Collections.Generic.List<DayOfWeek>
            {
                DayOfWeek.Sunday,
                DayOfWeek.Thursday
            };
            wdc.GetModel().Scheduled.WeeklyPeriod.Period = 2;
            wdc.GetModel().Scheduled.StartDate = new DateTime(2019, 2, 5);

            var controller = wdc.GetScheduleController();


            Assert.AreEqual(2,controller.Time2NextCareDay(new DateTime(2019, 2, 5)));
        }
        
        [Test]
        public void Time2CareDay_is0()
        {
            WashingDayDefinition wd = new WashingDayDefinition();
            WashingDayEditorController wdc = new WashingDayEditorController(wd, allRoutines);
            wdc.GetModel().Scheduled.WeeklyPeriod.WeekDays = new System.Collections.Generic.List<DayOfWeek>
            {
                DayOfWeek.Sunday,
                DayOfWeek.Thursday
            };
            wdc.GetModel().Scheduled.WeeklyPeriod.Period = 2;
            wdc.GetModel().Scheduled.StartDate = new DateTime(2019, 2, 5);

            var controller = wdc.GetScheduleController();


            Assert.AreEqual(0,controller.Time2NextCareDay(new DateTime(2019, 2, 7)));
        }



     





    }
}

using NUnit.Framework;
using HairAppBl.Models;
using HairAppBl.Controller;
using System;
using System.Collections.Generic;

namespace HairAppBl.Tests
{
    public class ScheduleControllerTest
    {
        System.Collections.Generic.List<RoutineDefinition> allRoutines;
        [SetUp]
        public void Setup()
        {
            allRoutines = new System.Collections.Generic.List<RoutineDefinition>();
            allRoutines.Add(RoutineDefinition.Create("Wash", "wash", "", ""));
            allRoutines.Add(RoutineDefinition.Create("Creme", "Creme", "", ""));
            allRoutines.Add(RoutineDefinition.Create("Cut", "cut", "", ""));
            allRoutines.Add(RoutineDefinition.Create("DoSomething", "dosomething", "", ""));
            allRoutines.Add(RoutineDefinition.Create("Foo", "bar", "", ""));
            allRoutines.Add(RoutineDefinition.Create("Kämmen", "kaemmen", "", ""));
        }

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
        public void GetScheduledDays_CheckOtherWeekDay()
        {
            WashingDayDefinition wd = new WashingDayDefinition();
            AlarmController ac = new AlarmController(null);

            WashingDayEditorController wdc = new WashingDayEditorController(wd, allRoutines,ac);
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
            AlarmController ac = new AlarmController(null);

            WashingDayEditorController wdc = new WashingDayEditorController(wd, allRoutines,ac);
            wdc.GetModel().Scheduled.WeeklyPeriod.Period = 2;
            wdc.GetModel().Scheduled.WeeklyPeriod.WeekDays = new System.Collections.Generic.List<DayOfWeek>
            {
                DayOfWeek.Monday,
            };
            wdc.GetModel().Scheduled.StartDate = new DateTime(2019, 2, 5);

            var res = wdc.GetScheduledDays();

            Assert.True(ScheduleController.IsSameDay(res[0],new DateTime(2019,2,11)));
            Assert.True(ScheduleController.IsSameDay(res[1],new DateTime(2019,2,25)));
        }

        [Test]
        public void GetScheduledDays_Period2_twoDays()
        {
            WashingDayDefinition wd = new WashingDayDefinition();
            AlarmController ac = new AlarmController(null);

            WashingDayEditorController wdc = new WashingDayEditorController(wd, allRoutines,ac);
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
            AlarmController ac = new AlarmController(null);

            WashingDayEditorController wdc = new WashingDayEditorController(wd, allRoutines,ac);
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
            AlarmController ac = new AlarmController(null);

            WashingDayEditorController wdc = new WashingDayEditorController(wd, allRoutines,ac);
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
            AlarmController ac = new AlarmController(null);

            WashingDayEditorController wdc = new WashingDayEditorController(wd, allRoutines,ac);
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
            AlarmController ac = new AlarmController(null);

            WashingDayEditorController wdc = new WashingDayEditorController(wd, allRoutines,ac);
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

        [Test]
        public void Monthly_FirstFridayOfMonth()
        {
            var day = ScheduleController.GetDayFromMonthlyPeriod(DayOfWeek.Friday,ScheduleDefinition.Monthly.ScheduleType.First , 2, 2019);


            Assert.AreEqual(new DateTime(2019,2,1), day);
        }

        [Test]
        public void Monthly_SecondFridayOfMonth()
        {
            var day = ScheduleController.GetDayFromMonthlyPeriod(DayOfWeek.Friday,ScheduleDefinition.Monthly.ScheduleType.Second , 2, 2019);


            Assert.AreEqual(new DateTime(2019, 2, 8), day);
        }

        [Test]
        public void Monthly_ThirdFridayOfMonth()
        {
            var day = ScheduleController.GetDayFromMonthlyPeriod(DayOfWeek.Friday, ScheduleDefinition.Monthly.ScheduleType.Third, 2, 2019);


            Assert.AreEqual(new DateTime(2019, 2, 15), day);
        }

        [Test]
        public void Monthly_FourthdFridayOfMonth()
        {
            var day = ScheduleController.GetDayFromMonthlyPeriod(DayOfWeek.Friday, ScheduleDefinition.Monthly.ScheduleType.Fourth, 2, 2019);


            Assert.AreEqual(new DateTime(2019, 2, 22), day);
        }

        [Test]
        public void Monthly_LastthdFridayOfMonth()
        {
            var day = ScheduleController.GetDayFromMonthlyPeriod(DayOfWeek.Friday, ScheduleDefinition.Monthly.ScheduleType.Last, 2, 2019);


            Assert.AreEqual(new DateTime(2019, 2, 22), day);
        }

        [Test]
        public void Monthly_FirstSaturday_MonthlyPeriod_1()
        {
            WashingDayDefinition wd = new WashingDayDefinition();
            AlarmController ac = new AlarmController(null);

            WashingDayEditorController wdc = new WashingDayEditorController(wd, allRoutines, ac);
            wdc.GetModel().Scheduled.Type = ScheduleDefinition.ScheduleType.Monthly;
            wdc.GetModel().Scheduled.MonthlyPeriod.Period = 1;
            wdc.GetModel().Scheduled.StartDate = new DateTime(2019, 2, 1);
            wdc.GetModel().Scheduled.MonthlyPeriod.Type = ScheduleDefinition.Monthly.ScheduleType.First;
            wdc.GetModel().Scheduled.MonthlyPeriod.WeekDay = DayOfWeek.Saturday;

            var controller = wdc.GetScheduleController();
            var days =  controller.GetScheduledDays();

        }

        [Test]
        public void Monthly_SecondSaturday_BeforeStart_MonthlyPeriod_2()
        {
            WashingDayDefinition wd = new WashingDayDefinition();
            AlarmController ac = new AlarmController(null);

            WashingDayEditorController wdc = new WashingDayEditorController(wd, allRoutines, ac);
            wdc.GetModel().Scheduled.Type = ScheduleDefinition.ScheduleType.Monthly;
            wdc.GetModel().Scheduled.MonthlyPeriod.Period = 2;
            wdc.GetModel().Scheduled.StartDate = new DateTime(2019, 2, 23);
            wdc.GetModel().Scheduled.MonthlyPeriod.Type = ScheduleDefinition.Monthly.ScheduleType.First;
            wdc.GetModel().Scheduled.MonthlyPeriod.WeekDay = DayOfWeek.Saturday;

            var controller = wdc.GetScheduleController();
            var days = controller.GetScheduledDays();
            Assert.AreEqual(new DateTime(2019, 3, 2), days[0]);
            Assert.AreEqual(new DateTime(2019, 5, 4), days[1]);
        }








    }
}

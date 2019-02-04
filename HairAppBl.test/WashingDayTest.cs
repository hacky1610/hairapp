using NUnit.Framework;
using HairAppBl.Models;
using HairAppBl.Controller;
using System;
using System.Collections.Generic;

namespace HairAppBl.Tests
{
    public class WashingDayTest
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
        public void ControllerCanBeLoaded()
        {
            WashingDayDefinition wd = new WashingDayDefinition();
            new WashingDayEditorController(wd, allRoutines);
            Assert.Pass();
        }

        [Test]
        public void ControllerConstructorCheckNullArgument()
        {
            Assert.Throws<System.ArgumentNullException>(delegate { new WashingDayEditorController(null,allRoutines); });
        }

        [Test]
        public void RoutineCanBeAdded()
        {
             WashingDayDefinition wd = new WashingDayDefinition();
            WashingDayEditorController wdc = new WashingDayEditorController(wd,allRoutines);
            var r = allRoutines[0];
            wdc.AddRoutine(r);
            Assert.True(wdc.GetRoutineDefinitions().Contains(r));
            Assert.False(wdc.GetUnusedRoutineDefinitions().Contains(r));
        }
        
         [Test]
        public void RoutineCanBeRemoved()
        {
             WashingDayDefinition wd = new WashingDayDefinition();
            WashingDayEditorController wdc = new WashingDayEditorController(wd,allRoutines);
            var r = allRoutines[0];
            wdc.AddRoutine(r);
            wdc.RemoveRoutine(r);
            Assert.False(wdc.GetRoutineDefinitions().Contains(r));
            Assert.True(wdc.GetUnusedRoutineDefinitions().Contains(r));
        }
        
         [Test]
        public void RoutineCanBeAdded_Order_is_correct()
        {
             WashingDayDefinition wd = new WashingDayDefinition();
            WashingDayEditorController wdc = new WashingDayEditorController(wd,allRoutines);
            wdc.AddRoutine(allRoutines[5]);
            wdc.AddRoutine(allRoutines[1]);
            wdc.AddRoutine(allRoutines[2]);
            wdc.AddRoutine(allRoutines[4]);
            wdc.AddRoutine(allRoutines[3]);
            
            string sortOrder = "";
            foreach(var item in wdc.GetRoutineDefinitions())
            {
                sortOrder += item.Name;
            }

            Assert.AreEqual(wdc.GetRoutineDefinitions()[0], allRoutines[5],sortOrder);
            Assert.AreEqual(wdc.GetRoutineDefinitions()[1], allRoutines[1],sortOrder);
            Assert.AreEqual(wdc.GetRoutineDefinitions()[2], allRoutines[2],sortOrder);
            Assert.AreEqual(wdc.GetRoutineDefinitions()[3], allRoutines[4],sortOrder);
            Assert.AreEqual(wdc.GetRoutineDefinitions()[4], allRoutines[3],sortOrder);
        }

     

        [Test]
        public void AddRoutine_CheckNullArgument()
        {
            WashingDayDefinition wd = new WashingDayDefinition();
            
            WashingDayEditorController wdc = new WashingDayEditorController(wd,allRoutines);
           
            Assert.Throws<System.ArgumentNullException>(delegate { wdc.AddRoutine(null); });
        }

        [Test]
        public void MoveDown()
        {
            WashingDayDefinition wd = new WashingDayDefinition();
            WashingDayEditorController wdc = new WashingDayEditorController(wd,allRoutines);
            var unused = wdc.GetUnusedRoutineDefinitions();
            wdc.AddRoutine(unused[0]);
            wdc.AddRoutine(unused[1]);
            wdc.AddRoutine(unused[2]);
            wdc.MoveDown(unused[0]);
            Assert.AreEqual(wdc.GetRoutineDefinitions()[1], unused[0]);
        }

        [Test]
        public void MoveUp()
        {
            WashingDayDefinition wd = new WashingDayDefinition();
            WashingDayEditorController wdc = new WashingDayEditorController(wd,allRoutines);
            var unused = wdc.GetUnusedRoutineDefinitions();
            wdc.AddRoutine(unused[0]);
            wdc.AddRoutine(unused[1]);
            wdc.AddRoutine(unused[2]);
            wdc.MoveUp(unused[1]);
            Assert.AreEqual(wdc.GetRoutineDefinitions()[0], unused[1]);
        }

        [Test]
        public void GetNextWeekDay_OneDayBefore()
        {
            WashingDayDefinition wd = new WashingDayDefinition();
            WashingDayEditorController wdc = new WashingDayEditorController(wd, allRoutines);
            var d = new DateTime(2019, 2, 4);
            var res = wdc.GetNextWeekDay(d, DayOfWeek.Tuesday);
            Assert.AreEqual(res.Day, 5);
        }

        [Test]
        public void GetNextWeekDay_CurrentDay()
        {
            WashingDayDefinition wd = new WashingDayDefinition();
            WashingDayEditorController wdc = new WashingDayEditorController(wd, allRoutines);
            var d = new DateTime(2019, 2, 4);
            var res = wdc.GetNextWeekDay(d, DayOfWeek.Monday);
            Assert.AreEqual(res.Day, 4);
        }

        [Test]
        public void GetScheduledDays_CheckDefault()
        {
            WashingDayDefinition wd = new WashingDayDefinition();
            WashingDayEditorController wdc = new WashingDayEditorController(wd, allRoutines);
            var res = wdc.GetScheduledDays();

            Assert.AreEqual(res[0].DayOfWeek, DayOfWeek.Monday);
            Assert.AreEqual(res[1].DayOfWeek, DayOfWeek.Monday);
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

            Assert.AreEqual(res[0].DayOfWeek, DayOfWeek.Sunday);
            Assert.AreEqual(res[1].DayOfWeek, DayOfWeek.Sunday);
        }

        [Test]
        public void GetScheduledDays_Period2()
        {
            WashingDayDefinition wd = new WashingDayDefinition();
            WashingDayEditorController wdc = new WashingDayEditorController(wd, allRoutines);
            wdc.GetModel().Scheduled.WeeklyPeriod.Period = 2;
            wdc.GetModel().Scheduled.StartDate = new DateTime(2019, 2, 5);

            var res = wdc.GetScheduledDays();

            Assert.True(WashingDayEditorController.IsSameDay(res[0],new DateTime(2019,2,11)));
            Assert.True(WashingDayEditorController.IsSameDay(res[1],new DateTime(2019,2,25)));
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


            Assert.True(ContainsDay(res, new DateTime(2019, 2, 7)));
            Assert.True(ContainsDay(res, new DateTime(2019, 2, 10)));
            Assert.True(ContainsDay(res, new DateTime(2019, 2,21)));
            Assert.True(ContainsDay(res, new DateTime(2019, 2, 24)));
        }

        private bool ContainsDay(List<DateTime> days, DateTime day)
        {
            foreach(var d in days)
            {
                if (WashingDayEditorController.IsSameDay(d, day))
                    return true;
            }
            return false;
        }






    }
}

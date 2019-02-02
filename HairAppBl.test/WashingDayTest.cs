using NUnit.Framework;
using HairAppBl.Models;
using HairAppBl.Controller;

namespace HairAppBl.Tests
{
    public class WashingDayTest
    {
        [Test]
        public void ControllerCanBeLoaded()
        {
            WashingDayDefinition wd = new WashingDayDefinition();
            new WashingDayEditorController(wd);
            Assert.Pass();
        }

        [Test]
        public void ControllerConstructorCheckNullArgument()
        {
            Assert.Throws<System.ArgumentNullException>(delegate { new WashingDayEditorController(null); });
        }

        [Test]
        public void RoutineCanBeAdded()
        {
             WashingDayDefinition wd = new WashingDayDefinition();
            WashingDayEditorController wdc = new WashingDayEditorController(wd);
            var r = wdc.GetUnusedRoutines()[0];
            wdc.AddRoutine(r);
            Assert.True(wdc.GetRoutineDefinitions().Contains(r));
        }

        [Test]
        public void AddRoutine_UnusedRoutineIsLess()
        {
            WashingDayDefinition wd = new WashingDayDefinition();
            WashingDayEditorController wdc = new WashingDayEditorController(wd);
            var unused = wdc.GetUnusedRoutines();
            var countBefore = unused.Count;
            var r = unused[0];
            wdc.AddRoutine(r);
            Assert.AreEqual(countBefore -1, unused.Count);
            Assert.False(unused.Contains(r));
        }

        [Test]
        public void AddRoutine_CheckNullArgument()
        {
            WashingDayDefinition wd = new WashingDayDefinition();
            WashingDayEditorController wdc = new WashingDayEditorController(wd);
           
            Assert.Throws<System.ArgumentNullException>(delegate { wdc.AddRoutine(null); });
        }

        [Test]
        public void MoveDown()
        {
            WashingDayDefinition wd = new WashingDayDefinition();
            WashingDayEditorController wdc = new WashingDayEditorController(wd);
            var unused = wdc.GetUnusedRoutines();
            var r = unused[0];
            wdc.AddRoutine(r);
            wdc.AddRoutine(unused[0]);
            wdc.AddRoutine(unused[0]);
            wdc.MoveDown(r);
            Assert.AreEqual(wdc.GetRoutineDefinitions()[1], r);
        }

        [Test]
        public void MoveUp()
        {
            WashingDayDefinition wd = new WashingDayDefinition();
            WashingDayEditorController wdc = new WashingDayEditorController(wd);
            var unused = wdc.GetUnusedRoutines();
            wdc.AddRoutine(unused[0]);
            wdc.AddRoutine(unused[0]);
            var r = unused[0];

            wdc.AddRoutine(r);
            wdc.MoveUp(r);
            Assert.AreEqual(wdc.GetRoutineDefinitions()[1], r);
        }


    }
}

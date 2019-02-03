using NUnit.Framework;
using HairAppBl.Models;
using HairAppBl.Controller;

namespace HairAppBl.Tests
{
    public class WashingDayTest
    {
        System.Collections.Generic.List<RoutineDefinition> allRoutines;
        [SetUp]
        public void Setup()
        {
            allRoutines = new System.Collections.Generic.List<RoutineDefinition>();
            allRoutines.Add(new RoutineDefinition());
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
            WashingDayEditorController wdc = new WashingDayEditorController(wd,allRoutines);
            var unused = allRoutines;
            wdc.AddRoutine(unused[0]);
            wdc.AddRoutine(unused[0]);
            var r = unused[0];

            wdc.AddRoutine(r);
            wdc.MoveUp(r);
            Assert.AreEqual(wdc.GetRoutineDefinitions()[1], r);
        }


    }
}

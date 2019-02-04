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


    }
}

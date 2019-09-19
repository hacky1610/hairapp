using NUnit.Framework;
using HairAppBl.Models;
using HairAppBl.Controller;
using System.Collections.Generic;
using Xamarin.Forms;
using HairAppBl;

namespace HairApp.Tests
{
    public class WashingDayEditorTest
    {
        List<RoutineDefinition> allRoutines;
        HairAppBl.HairAppBl  hairAppBl;
        MainSessionController msCOntroller;

        [SetUp]
        public void SetUp()
        {
            Xamarin.Forms.Mocks.MockForms.Init();
            allRoutines = new List<RoutineDefinition>();
            allRoutines.Add(RoutineDefinition.Create("Wash", "wash", "", ""));
            allRoutines.Add(RoutineDefinition.Create("Creme", "Creme", "", ""));
            allRoutines.Add(RoutineDefinition.Create("Cut", "cut", "", ""));
            allRoutines.Add(RoutineDefinition.Create("DoSomething", "dosomething", "", ""));
            allRoutines.Add(RoutineDefinition.Create("Foo", "bar", "", ""));
            allRoutines.Add(RoutineDefinition.Create("Kämmen", "kaemmen", "", ""));

            var dic = new Dictionary<string, object>();
            hairAppBl = new HairAppBl.HairAppBl(new ConsoleLogger(), dic);

            msCOntroller = new MainSessionController(null);


        }

        [Test]
        [Ignore("Ignore a test")]
        public void Instantiate()
        {
            var wdController = new WashingDayEditorController(new WashingDayDefinition(),allRoutines,new AlarmController(null,null,null));

            WashDayEditor d = new WashDayEditor(msCOntroller,wdController, true, hairAppBl);
        }

       

    }
}

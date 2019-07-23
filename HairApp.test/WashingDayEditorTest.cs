using NUnit.Framework;
using HairAppBl.Models;
using HairAppBl.Controller;
using System.Collections.Generic;
using Xamarin.Forms;

namespace HairAppBl.Tests
{
    public class WashingDayEditorTest
    {
        List<RoutineDefinition> allRoutines;
        HairAppBl hairAppBl;
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
            hairAppBl = new HairAppBl(new ConsoleLogger(), dic);

            msCOntroller = new MainSessionController(null);


        }

        [Test]
        public void Instantiate()
        {
            var wdController = new WashingDayEditorController(new WashingDayDefinition(),allRoutines,new AlarmController(null,null,null));

            HairApp.WashDayEditor d = new HairApp.WashDayEditor(msCOntroller,wdController, true, hairAppBl);
        }

       

    }
}
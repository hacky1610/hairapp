using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using HairAppBl.Models;

namespace HairAppBl.Controller
{
    public class MainSessionController: Interfaces.ISession
    {
        readonly IDictionary<string, object> mProperties;
        MainSession MainSession = null;
        public MainSessionController(IDictionary<string, object> props)
        {
            mProperties = props;
        }

        public void Restore()
        {
            var key= typeof(MainSession).ToString();
            if (!mProperties.ContainsKey(key))
                Init();
            else
            {
                string json = (string)mProperties[key];
                MainSession = (MainSession)JsonConvert.DeserializeObject(json, typeof(MainSession));
            }
        }



        public void Save()
        {
            string json = JsonConvert.SerializeObject(MainSession);
            string key = MainSession.GetType().ToString();
            mProperties[key] = json;
        }

        public void SetName(string name)
        {
            MainSession.User = name;
        }

        public List<RoutineDefinition> GetAllDefinitions()
        {
            return MainSession.AllRoutines;
        }

        public List<WashingDayDefinition> GetAllWashingDays()
        {
            return MainSession.WashingDays;
        }

        public WashingDayDefinition GetWashingDayById(string id)
        {
            foreach(var day in MainSession.WashingDays)
            {
                if (day.ID == id)
                    return day;
            }
            return null;
        }

        public void Init()
        {
            MainSession = new MainSession();
            MainSession.AllRoutines.Add(RoutineDefinition.Create("Prepoo", "Prepoo", "", "Please do your Prepoo"));
            MainSession.AllRoutines.Add(RoutineDefinition.Create("Hot Oil Treatment", "HotOilTreatment", "", "Please do your Hot Oil Treatment"));
            MainSession.AllRoutines.Add(RoutineDefinition.Create("Shampoo", "Shampoo", "", "Please wash your hair"));
            MainSession.AllRoutines.Add(RoutineDefinition.Create("Carifying SHampoo", "CarifyingShampoo", "", "Please wash your hair"));
            MainSession.AllRoutines.Add(RoutineDefinition.Create("Conditioner", "Conditioner", "", "Please use your Conditioner"));
            MainSession.AllRoutines.Add(RoutineDefinition.Create("Deep Conditioner", "DeepConditioner", "", "Please use your Deep Conditioner"));
            MainSession.AllRoutines.Add(RoutineDefinition.Create("Protein Treatment", "ProteinTreatment", "", "Please make Protein Treatment"));
            MainSession.AllRoutines.Add(RoutineDefinition.Create("Moisturising Mask", "MoisturisingMask", "", "Please make Moisturising Mask"));
            MainSession.AllRoutines.Add(RoutineDefinition.Create("Leave in Conditioner", "LeaveInConditioner", "", "Please make Leave in Conditioner"));
            MainSession.AllRoutines.Add(RoutineDefinition.Create("Clay", "Clay", "", "Please use Clay"));
            MainSession.AllRoutines.Add(RoutineDefinition.Create("Rinses", "Rinses", "", "Please use Rinses"));
            MainSession.Initialized = true;
        }
    }
}

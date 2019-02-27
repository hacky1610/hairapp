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
        public static event EventHandler<EventArgs> InitAlarms;
        MainSession MainSession = null;

        public bool Initialized {
            get
            {
                return MainSession.Initialized;
            }
            set
            {
                MainSession.Initialized = value;
            }
        }

        public  void SendInitAlarms()
        {
            if (!MainSession.AlarmInitialized)
            {
                InitAlarms(null, new EventArgs());
                MainSession.AlarmInitialized = true;
            }
        }

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

        public int TimeToNextCareDay()
        {
            var diff = Int32.MaxValue;
            foreach(var d in MainSession.WashingDays)
            {
                var c = new ScheduleController(d.Scheduled);
                var t = c.Time2NextCareDay(DateTime.Now);
                if (t < diff)
                    diff = t;
            }
            return diff;
        }

        public Dictionary<DateTime, List<Models.WashingDayDefinition>> GetFutureDays()
        {
            var c = new FutureDayListController<WashingDayDefinition>();
            foreach (var d in MainSession.WashingDays)
            {
                var scheduleController = new ScheduleController(d.Scheduled);
                c.AddMultiple(d,scheduleController.GetScheduledDays());
            }
            return c.GetAllDays();

        }

        public Dictionary<DateTime, List<Models.WashingDayInstance>> GetInstances()
        {
            var c = new FutureDayListController<WashingDayInstance>();
            foreach (var d in MainSession.WashingDays)
            {
                foreach(var i in d.Instances)
                {
                    c.Add(i, i.Day);
                }
               
            }
            return c.GetAllDays();

        }

        public CommingDays NextDay()
        {
            var diff = new CommingDays();

            foreach (var d in MainSession.WashingDays)
            {
                var c = new ScheduleController(d.Scheduled);
                var t = c.Time2NextCareDay(ScheduleController.GetToday());
                if (t <= diff.Time2Wait)
                {
                    diff.Time2Wait = t;
                    diff.Days = new List<WashingDayDefinition> { d };
                }
                else if (t == diff.Time2Wait)
                {
                    diff.Time2Wait = t;
                    diff.Days.Add(d);
                }
            }
            return diff;
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
        }

        public class CommingDays
        {
            public int Time2Wait { get; set; }
            public List<Models.WashingDayDefinition> Days { get; set; }

            public CommingDays()
            {
                Days = new List<WashingDayDefinition>();
                Time2Wait = Int32.MaxValue;
            }

            public CommingDays(List<WashingDayDefinition> days, int time):this()
            {
                Days = days;
                Time2Wait = time;
            }
        }


    }
}

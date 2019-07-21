using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using HairAppBl.Models;
using System.Drawing;
using System.Linq;

namespace HairAppBl.Controller
{
    public class MainSessionController: Interfaces.ISession
    {
        readonly IDictionary<string, object> mProperties;
        MainSession MainSession = null;
        public event EventHandler<EventArgs> DefinitionsEdited;
        public event EventHandler<EventArgs> InstanceEdited;
        public event EventHandler<EventArgs> Saved;

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

        public void SendDefinitionsEdited()
        {
            Save();
            DefinitionsEdited?.Invoke(this, new EventArgs());
        }

        public void SendInstanceEdited()
        {
            Save();
            InstanceEdited?.Invoke(this, new EventArgs());
        }

        public void Save()
        {
            string json = JsonConvert.SerializeObject(MainSession);
            string key = MainSession.GetType().ToString();
            mProperties[key] = json;
            Saved?.Invoke(this, new EventArgs());
        }

        public void SetName(string name)
        {
            MainSession.User = name;
        }

        public List<RoutineDefinition> GetAllDefinitions()
        {
            return MainSession.AllRoutines;
        }

        public void DeleteRoutine(RoutineDefinition r)
        {
            MainSession.AllRoutines.Remove(r);
            Save();

        }

        public List<WashingDayDefinition> GetAllWashingDays()
        {
            return MainSession.WashingDays;
        }

        public void DeleteWashDay(WashingDayDefinition washingDay)
        {
            MainSession.WashingDays.Remove(washingDay);
            SendDefinitionsEdited();
        }

        public List<Color> GetUnusedColors()
        {
            var usedColors = new List<Color>();
            foreach (var wd in GetAllWashingDays())
            {
                usedColors.Add(wd.ItemColor);
            }
            var colors = from b in WashingDayDefinition.Colors where !usedColors.Contains(b) select b;

            return colors.ToList();
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

        public Dictionary<DateTime, List<WashingDayInstance>> GetInstancesByDate()
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

        public List<WashingDayInstance> GetInstances()
        {
            var list = new List<WashingDayInstance>();
            foreach (var d in MainSession.WashingDays)
            {
                list.AddRange(d.Instances);
            }
            return list;
        }

        public List<HairLength> GetHairLength()
        {
            return MainSession.HairLengths;
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

        public String GetCulture()
        {
            if (HasCulture())
                return MainSession.Culture;
            else
                return "fr";
        }

        public void SetCulture(String cul)
        {
            MainSession.Culture = cul;
        }

        public bool HasCulture()
        {
            return MainSession.Culture != String.Empty;
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
        }

        public void InitRoutines()
        {
            MainSession.AllRoutines.Clear();
            MainSession.AllRoutines.Add(RoutineDefinition.Create(Resources.AppResource.Prepoo, "Prepoo", "", Resources.AppResource.PrepooDescription));
            MainSession.AllRoutines.Add(RoutineDefinition.Create(Resources.AppResource.HotOilTreatment, "HotOilTreatment", "", Resources.AppResource.HotOilTreatmentDescription));
            MainSession.AllRoutines.Add(RoutineDefinition.Create(Resources.AppResource.Shampoo, "Shampoo", "", Resources.AppResource.ShampooDescription));
            MainSession.AllRoutines.Add(RoutineDefinition.Create(Resources.AppResource.ClarifyingShampoo, "CarifyingShampoo", "", Resources.AppResource.ClarifyingShampooDescription));
            MainSession.AllRoutines.Add(RoutineDefinition.Create(Resources.AppResource.Conditioner, "Conditioner", "", Resources.AppResource.ConditionerDescription));
            MainSession.AllRoutines.Add(RoutineDefinition.Create(Resources.AppResource.DeepConditioner, "DeepConditioner", "", Resources.AppResource.DeepConditionerDescription));
            MainSession.AllRoutines.Add(RoutineDefinition.Create(Resources.AppResource.ProteinTreatment, "ProteinTreatment", "", Resources.AppResource.ProteinTreatmentDescription));
            MainSession.AllRoutines.Add(RoutineDefinition.Create(Resources.AppResource.MoisturisingMask, "MoisturisingMask", "", Resources.AppResource.MoisturisingMaskDescription));
            MainSession.AllRoutines.Add(RoutineDefinition.Create(Resources.AppResource.LeaveInConditioner, "LeaveInConditioner", "", Resources.AppResource.LeaveInConditionerDescription));
            MainSession.AllRoutines.Add(RoutineDefinition.Create(Resources.AppResource.Clay, "Clay", "", Resources.AppResource.ClayDescription));
            MainSession.AllRoutines.Add(RoutineDefinition.Create(Resources.AppResource.Rinse, "Rinses", "", Resources.AppResource.RinseDescription));

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

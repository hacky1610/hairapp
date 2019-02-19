using System;
using System.Collections.Generic;
using System.Text;

namespace HairAppBl.Models
{
    public class MainSession
    {
        public string User { get; set; }
        public Boolean Initialized { get; set; }

        public List<RoutineDefinition> AllRoutines { get; set; }
        public List<WashingDayDefinition> WashingDays { get; set; }

        public MainSession()
        {
            AllRoutines = new List<RoutineDefinition>();
            WashingDays = new List<WashingDayDefinition>();
            Initialized = false;
        }

   

    }
}

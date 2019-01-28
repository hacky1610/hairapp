using System;
using System.Collections.Generic;
using System.Text;

namespace HairAppBl.Models
{
    public class WashingDayDefinition
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public List<RoutineDefinition> Routines { get; set; }
        public DateTime Created { get; set; }

        public List<RoutineDefinition> UnusedDefitions { get; set; }

        public WashingDayDefinition()
        {
            Routines = new List<RoutineDefinition>();
            UnusedDefitions = new List<RoutineDefinition>();
 
        }



    }
}

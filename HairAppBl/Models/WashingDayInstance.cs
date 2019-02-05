using System;
using System.Collections.Generic;
using System.Text;

namespace HairAppBl.Models
{
    public class WashingDayInstance : WashingDayDBInstance
    {

        public readonly List<RoutineInstance> Routines;
        public WashingDayInstance():base()
        {
            Routines = new List<RoutineInstance>();
        }

        public WashingDayInstance(string wdID, string id,DateTime date, List<RoutineDefinition> routines):base(wdID,id,date)
        {
            Routines = new List<RoutineInstance>();
            foreach (var r in routines)
            {
                Routines.Add(new RoutineInstance(r.Name));
            }
        }


    }
}

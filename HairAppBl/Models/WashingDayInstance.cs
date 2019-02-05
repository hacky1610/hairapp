using System;
using System.Collections.Generic;
using System.Text;

namespace HairAppBl.Models
{
    public class WashingDayInstance : WashingDayDBInstance
    {

        readonly List<RoutineInstance> Routines;
        public WashingDayInstance():base()
        {
            Routines = new List<RoutineInstance>();
        }

        public WashingDayInstance(string wdID, string id,DateTime date, List<RoutineInstance> routines):base(wdID,id,date)
        {
            
            Routines = routines;
        }


    }
}

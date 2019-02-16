using System;
using System.Collections.Generic;
using System.Text;

namespace HairAppBl.Models
{
    public class WashingDayInstance : WashingDayDBInstance
    {

        public readonly List<RoutineInstance> Routines;
        public readonly String WashDayId;
        public readonly String Comment;
        public readonly String Description;
        public  Boolean Saved;
        public WashingDayInstance():base()
        {
            Routines = new List<RoutineInstance>();
        }

        public WashingDayInstance(string wdID, string id,DateTime date, List<RoutineDefinition> routines,string desc):base(wdID,id,date)
        {
            Routines = new List<RoutineInstance>();
            Description = desc;
            Saved = false;
            foreach (var r in routines)
            {
                Routines.Add(new RoutineInstance(r.Name,r.Description));
            }
        }


    }
}

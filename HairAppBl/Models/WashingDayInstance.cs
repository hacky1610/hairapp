using System;
using System.Collections.Generic;
using System.Text;

namespace HairAppBl.Models
{
    public class WashingDayInstance : WashingDayDBInstance
    {

        public readonly List<RoutineInstance> Routines;
        public readonly List<Picture> Pictures;
        public readonly String WashDayId;
        public  String Comment;
        public readonly String Description;
        public  Boolean Saved;
        public  TimeSpan NeededTime;
        public WashingDayInstance():base()
        {
            Routines = new List<RoutineInstance>();
        }

        public WashingDayInstance(string wdID, string id,DateTime date, List<RoutineDefinition> routines,string desc):base(wdID,id,date)
        {
            Routines = new List<RoutineInstance>();
            Pictures = new List<Picture>();
            Description = desc;
            Saved = false;
            NeededTime = new TimeSpan();
            foreach (var r in routines)
            {
                Routines.Add(new RoutineInstance(r.Name,r.Description));
            }
        }


    }
}

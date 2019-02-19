using System;
using System.Collections.Generic;
using System.Text;

namespace HairAppBl.Models
{
    public class ScheduleDefinition
    {
        public DateTime StartDate{ get; set; }
        public ScheduleType Type{ get; set; }

        public Dayly DaylyPeriod { get; set; }
        public Weekly WeeklyPeriod { get; set; }
        public Monthly MonthlyPeriod { get; set; }
        public Yearly YearlyPeriod { get; set; }

        public ScheduleDefinition()
        {
            StartDate = DateTime.Now;
            Type = ScheduleType.Weekly;
            DaylyPeriod = new Dayly();
            WeeklyPeriod = new Weekly();
            MonthlyPeriod = new Monthly();
            YearlyPeriod = new Yearly();
        }


        public enum ScheduleType
        {
            Dayly,
            Weekly,
            Monthly,
            Yearly,
        }
        
        
        public class Dayly
        {
            public int Period { get; set; }
            public Dayly()
            {
                Period = 1;   
            }
         }
        
        public class Weekly
        {
            public int Period { get; set; }
            public List<DayOfWeek> WeekDays { get; set; }

            public Weekly()
            {
                Period = 1;
                WeekDays = new List<DayOfWeek>();
            }

        }

        public class Monthly
        {
            public int Period { get; set; }
            public DayOfWeek WeekDay { get; set; }
            public ScheduleType Type { get; set; }

            public Monthly()
            {
                Period = 1;
                WeekDay = DayOfWeek.Monday;
                Type = ScheduleType.First;
            }


            public enum ScheduleType
            {
                First,
                Second,
                Third,
                Fourth,
                Last
            }
        }
        
          public class Yearly
        {
            public int Period { get; set; }
           
         }

      

     

    }


}

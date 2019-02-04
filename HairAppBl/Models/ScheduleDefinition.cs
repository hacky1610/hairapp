using System;
using System.Collections.Generic;
using System.Text;

namespace HairAppBl.Models
{
    public class ScheduleDefinition
    {
        public DateTime StartDate{ get; set; }
        public ScheduleType Type{ get; set; }

        public Weekly WeeklyPeriod { get; set; }
        public Monthly MonthlyPeriod { get; set; }


        public ScheduleDefinition()
        {
            StartDate = DateTime.Now;
            Type = ScheduleType.Weekly;
            WeeklyPeriod = new Weekly();
            MonthlyPeriod = new Monthly();
        }


        public enum ScheduleType
        {
            Weekly,
            Monthly
        }

        public class Weekly
        {
            public int Period { get; set; }
            public List<DayOfWeek> WeekDays { get; set; }

            public Weekly()
            {
                Period = 1;
                WeekDays = new List<DayOfWeek>{
                    DayOfWeek.Monday
                };
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

    }
}

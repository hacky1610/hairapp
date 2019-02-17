using System;
using System.Collections.Generic;
using System.Text;
using HairAppBl.Models;

namespace HairAppBl.Controller
{
    public class ScheduleController
    {
        readonly ScheduleDefinition mSchedule;
        public ScheduleController(ScheduleDefinition sd)
        {
            if(sd == null)
                throw new ArgumentNullException("sd");

            this.mSchedule = sd;
        }

        public List<DateTime> GetScheduledDays()
        {
            var days = new List<DateTime>();
            if (mSchedule.Type == ScheduleDefinition.ScheduleType.Dayly)
            {
                var start = mSchedule.StartDate;
                for (int i = 0; i < 200; i++)
                {
                    days.Add(start);
                    start = start.AddDays( mSchedule.DaylyPeriod.Period);
                }
            }
            else if (mSchedule.Type == ScheduleDefinition.ScheduleType.Weekly)
            {
                foreach(var d in mSchedule.WeeklyPeriod.WeekDays)
                {
                    var start = GetNextWeekDay(mSchedule.StartDate,d);
                    for (int i = 0; i < (50 / mSchedule.WeeklyPeriod.Period) ; i++)
                    {
                        days.Add(start);
                        start = start.AddDays(7 * mSchedule.WeeklyPeriod.Period);
                    }

                }

            }

            return days;
        }
        
        public Boolean IsCareDay(DateTime currentDay)
        {
            
            return ScheduleController.ContainsDay(GetScheduledDays(),currentDay);
            
        }
        
        public int Time2NextCareDay(DateTime currentDay)
        {
            int diffDays = Int32.MaxValue;
            foreach(var d in GetScheduledDays())
            {
               var diffTimeSpan =  d.Subtract (currentDay);
               if(diffTimeSpan.Days >= 0 && diffTimeSpan.Days < diffDays)
                    diffDays = diffTimeSpan.Days;
            }
            return diffDays;
            
        }
        
        public String GetSchedule()
        {
            if (mSchedule.Type == ScheduleDefinition.ScheduleType.Dayly)
            {
                return $"Every {mSchedule.DaylyPeriod.Period} days.";
            }
            else if (mSchedule.Type == ScheduleDefinition.ScheduleType.Weekly)
            {
                var days = String.Empty;
                foreach (var d in mSchedule.WeeklyPeriod.WeekDays)
                    days += $" {d},";
                days.TrimEnd(',');
                return $"Every{days} each {mSchedule.WeeklyPeriod.Period} week.";

            }
            return "";
        }

        public static Boolean ContainsDay(List<DateTime> days, DateTime day)
        {
            foreach(var d in days)
            {
                if (ScheduleController.IsSameDay(d, day))
                    return true;
            }
            return false;
        }

        public static DateTime GetNextWeekDay(DateTime d, DayOfWeek day)
        {
            DateTime date = d;
            if (date.DayOfWeek == day)
                return date;

            while (date.DayOfWeek != day)
                date = date.AddDays(1);

            return date;
        }

        public static Boolean IsSameDay(DateTime x, DateTime y)
        {
            return ((x.Year == y.Year) && (x.Month == y.Month) && (x.Day == y.Day));
        }

        public static DateTime GetToday()
        {
            return new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day);
        }

        public static List<ScheduleDefinition.ScheduleTypeObject> CreateScheduleTypeList()
        {
            var typeList = new List<ScheduleDefinition.ScheduleTypeObject>();
            typeList.Add(new ScheduleDefinition.ScheduleTypeObject(ScheduleDefinition.ScheduleType.Dayly, "Daily"));
            typeList.Add(new ScheduleDefinition.ScheduleTypeObject(ScheduleDefinition.ScheduleType.Weekly, "Weekly"));
            typeList.Add(new ScheduleDefinition.ScheduleTypeObject(ScheduleDefinition.ScheduleType.Monthly, "Monthly"));
            typeList.Add(new ScheduleDefinition.ScheduleTypeObject(ScheduleDefinition.ScheduleType.Yearly, "Yearly"));
            return typeList;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using HairAppBl.Models;

namespace HairAppBl.Controller
{
    public class ScheduleController
    {
        readonly WashingDayDefinition mWashingDay;
        public ScheduleController(WashingDayDefinition wd)
        {
            if(wd == null)
                throw new ArgumentNullException("wd");

            this.mWashingDay = wd;
        }

        public List<DateTime> GetScheduledDays()
        {
            var days = new List<DateTime>();
            var schedule = mWashingDay.Scheduled;
            if(schedule.Type == ScheduleDefinition.ScheduleType.Weekly)
            {
                foreach(var d in schedule.WeeklyPeriod.WeekDays)
                {
                    var start = GetNextWeekDay(schedule.StartDate,d);
                    for (int i = 0; i < (50 / schedule.WeeklyPeriod.Period) ; i++)
                    {
                        days.Add(start);
                        start = start.AddDays(7 * schedule.WeeklyPeriod.Period);
                    }

                }

            }

            return days;
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
    }
}

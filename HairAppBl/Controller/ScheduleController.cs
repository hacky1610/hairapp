using System;
using System.Collections.Generic;
using System.Linq;
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
            else if (mSchedule.Type == ScheduleDefinition.ScheduleType.Monthly)
            {

                var start = mSchedule.StartDate;
                var first = GetDayFromMonthlyPeriod(mSchedule.MonthlyPeriod.WeekDay,
                                            mSchedule.MonthlyPeriod.Type,
                                            start.Month,
                                            start.Year);
                if (first < start)
                    start = start.AddMonths(1);
                for (int i = 0; i < 36; i++)
                {
                    var d = GetDayFromMonthlyPeriod(mSchedule.MonthlyPeriod.WeekDay,
                                            mSchedule.MonthlyPeriod.Type,
                                            start.Month,
                                            start.Year);
                    days.Add(d);
                    start = start.AddMonths(mSchedule.MonthlyPeriod.Period);
                }


            }

            return days;
        }

        public static DateTime GetDayFromMonthlyPeriod(DayOfWeek day, ScheduleDefinition.Monthly.ScheduleType dayInMonth, int month, int year)
        {
            var start = new DateTime(year, month, 1);
            var occurence = 0;
            DateTime lastDay = new DateTime();
            while(start.Month == month)
            {
                if(start.DayOfWeek == day)
                {
                    occurence++;
                    if (dayInMonth == ScheduleDefinition.Monthly.ScheduleType.First && occurence == 1 ||
                        dayInMonth == ScheduleDefinition.Monthly.ScheduleType.Second && occurence == 2 ||
                        dayInMonth == ScheduleDefinition.Monthly.ScheduleType.Third && occurence == 3 ||
                        dayInMonth == ScheduleDefinition.Monthly.ScheduleType.Fourth && occurence == 4)
                        return start;

                    lastDay = start;

                }

                start = start.AddDays(1);
            }
            return lastDay;
        }
        
        public Boolean IsCareDay(DateTime currentDay)
        {
            
            return ContainsDay(GetScheduledDays(),currentDay);
            
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
                days = days.TrimEnd(',');
                return $"Every{days} each {mSchedule.WeeklyPeriod.Period} week.";

            }
            else if (mSchedule.Type == ScheduleDefinition.ScheduleType.Monthly)
            {
                var occurenceList = CreateMonthOccurenceTypeList();
                var occurenceItem = from item in occurenceList where item.Type == mSchedule.MonthlyPeriod.Type select item;

                var weekDayList = CreateDayOfWeekList();
                var day = from item in weekDayList where item.Type == mSchedule.MonthlyPeriod.WeekDay select item;


                return $"At {occurenceList.First().Name} {day.First().Name} every {mSchedule.MonthlyPeriod.Period} month.";

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

        public static List<TypeNameObject<ScheduleDefinition.ScheduleType>> CreateScheduleTypeList()
        {
            var typeList = new List<TypeNameObject<ScheduleDefinition.ScheduleType>>();
            typeList.Add(new TypeNameObject<ScheduleDefinition.ScheduleType>(ScheduleDefinition.ScheduleType.Dayly, "Daily"));
            typeList.Add(new TypeNameObject<ScheduleDefinition.ScheduleType>(ScheduleDefinition.ScheduleType.Weekly, "Weekly"));
            typeList.Add(new TypeNameObject<ScheduleDefinition.ScheduleType>(ScheduleDefinition.ScheduleType.Monthly, "Monthly"));
            typeList.Add(new TypeNameObject<ScheduleDefinition.ScheduleType>(ScheduleDefinition.ScheduleType.Yearly, "Yearly"));
            return typeList;
        }

        public static List<TypeNameObject<ScheduleDefinition.Monthly.ScheduleType>> CreateMonthOccurenceTypeList()
        {
            var typeList = new List<TypeNameObject<ScheduleDefinition.Monthly.ScheduleType>>();
            typeList.Add(new TypeNameObject<ScheduleDefinition.Monthly.ScheduleType>(ScheduleDefinition.Monthly.ScheduleType.First, "First"));
            typeList.Add(new TypeNameObject<ScheduleDefinition.Monthly.ScheduleType>(ScheduleDefinition.Monthly.ScheduleType.Second, "Second"));
            typeList.Add(new TypeNameObject<ScheduleDefinition.Monthly.ScheduleType>(ScheduleDefinition.Monthly.ScheduleType.Third, "Third"));
            typeList.Add(new TypeNameObject<ScheduleDefinition.Monthly.ScheduleType>(ScheduleDefinition.Monthly.ScheduleType.Fourth, "Fourth"));
            typeList.Add(new TypeNameObject<ScheduleDefinition.Monthly.ScheduleType>(ScheduleDefinition.Monthly.ScheduleType.Last, "Last"));
            return typeList;
        }

        public static List<TypeNameObject<DayOfWeek>> CreateDayOfWeekList()
        {
            var typeList = new List<TypeNameObject<DayOfWeek>>();
            typeList.Add(new TypeNameObject<DayOfWeek>(DayOfWeek.Monday, "Monday"));
            typeList.Add(new TypeNameObject<DayOfWeek>(DayOfWeek.Tuesday, "Tuesday"));
            typeList.Add(new TypeNameObject<DayOfWeek>(DayOfWeek.Wednesday, "Wednesday"));
            typeList.Add(new TypeNameObject<DayOfWeek>(DayOfWeek.Thursday, "Thursday"));
            typeList.Add(new TypeNameObject<DayOfWeek>(DayOfWeek.Friday, "Friday"));
            typeList.Add(new TypeNameObject<DayOfWeek>(DayOfWeek.Saturday, "Saturday"));
            typeList.Add(new TypeNameObject<DayOfWeek>(DayOfWeek.Sunday, "Sunday"));
            return typeList;
        }
    }
}

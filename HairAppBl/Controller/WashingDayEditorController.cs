using System;
using System.Collections.Generic;
using System.Text;
using HairAppBl.Models;

namespace HairAppBl.Controller
{
    public class WashingDayEditorController
    {
        readonly WashingDayDefinition mWashingDay;
        readonly List<RoutineDefinition> mAllRoutines;
        public WashingDayEditorController(WashingDayDefinition wd, List<RoutineDefinition> allroutines)
        {
            if(wd == null)
                throw new ArgumentNullException("wd");

            this.mWashingDay = wd;
            this.mAllRoutines = allroutines;
        }

        public WashingDayDefinition GetModel()
        {
            return this.mWashingDay;
        }   

        public void AddRoutine(RoutineDefinition routine)
        {
            if(routine == null)
                throw new ArgumentNullException("routine");
            if(routine.ID == string.Empty)
                throw new ArgumentException($"Routine ID is empty");


            this.mWashingDay.Routines.Add(routine.ID);
        }

        public void RemoveRoutine(RoutineDefinition routine)
        {
            if (routine == null)
                throw new ArgumentNullException("Routine is null");
            if (routine.ID == string.Empty)
                throw new ArgumentException($"Routine ID is empty");

            this.mWashingDay.Routines.Remove(routine.ID);

        }

        public void MoveUp(RoutineDefinition routine)
        {
            if (routine == null)
                throw new ArgumentNullException("Routine is null");
            if (routine.ID == string.Empty)
                throw new ArgumentException($"Routine ID is empty");

            var currentIndex = this.mWashingDay.Routines.IndexOf(routine.ID);
            if (currentIndex == 0)
                return;

            this.mWashingDay.Routines.Remove(routine.ID);
            this.mWashingDay.Routines.Insert(currentIndex - 1, routine.ID);
        }

        public void MoveDown(RoutineDefinition routine)
        {
            if (routine == null)
                throw new ArgumentNullException("Routine is null");
            if (routine.ID == string.Empty)
                throw new ArgumentException($"Routine ID is empty");

            var currentIndex = this.mWashingDay.Routines.IndexOf(routine.ID);
            if (currentIndex == this.mWashingDay.Routines.Count -1)
                return;

            this.mWashingDay.Routines.Remove(routine.ID);
            this.mWashingDay.Routines.Insert(currentIndex + 1, routine.ID);
        }

        public List<RoutineDefinition> GetRoutineDefinitions()
        {
            var tempList = new List<RoutineDefinition>();
            foreach(var r in this.mWashingDay.Routines)
            {
                 tempList.Add(GetRoutineById(r));
            }
            return tempList;
        }

        private RoutineDefinition GetRoutineById(string id )
        {
            foreach (var r in this.mAllRoutines)
            {
                if (r.ID == id)
                    return r;
            }
            return null;
        }

        public List<RoutineDefinition> GetUnusedRoutineDefinitions()
        {
            var tempList = new List<RoutineDefinition>();
            foreach (var r in mAllRoutines)
            {
                if (!this.mWashingDay.Routines.Contains(r.ID))
                    tempList.Add(r);
            }
            return tempList;
        }

        public void SaveInstances()
        {

            var instances = new List<WashingDayDBInstance>
            {
                new WashingDayDBInstance(GetModel().ID, Guid.NewGuid().ToString(), DateTime.Now),
                new WashingDayDBInstance(GetModel().ID, Guid.NewGuid().ToString(), DateTime.Now),
                new WashingDayDBInstance(GetModel().ID, Guid.NewGuid().ToString(), DateTime.Now)
            };

            var table = new DbTable<WashingDayDBInstance>(DataBase.Instance);
            table.ExecQuery($"DELETE * FROM WashingDayInstance WHERE WashDayID = '{GetModel().ID}'").Wait();

            table.SaveItemsAsync(instances).Wait();


        }

        private List<RoutineInstance> GetRoutineInstances()
        {
            var routines = new List<RoutineInstance>();
            foreach(var routine in GetRoutineDefinitions())
            {
                var r = new RoutineInstance();
                r.Name = routine.Name;
                routines.Add(r);
            }
            return routines;
        }

        public WashingDayInstance GetWashingDayInstance()
        {
            var instance = new WashingDayInstance();

            return instance;
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

        public DateTime GetNextWeekDay(DateTime d, DayOfWeek day)
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

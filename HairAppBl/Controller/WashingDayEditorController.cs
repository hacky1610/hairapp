using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HairAppBl.Models;

namespace HairAppBl.Controller
{
    public class WashingDayEditorController
    {
        readonly WashingDayDefinition mWashingDay;
        readonly ScheduleController mScheduleController;
        readonly List<RoutineDefinition> mAllRoutines;
        readonly AlarmController mAlarmController;
        public WashingDayEditorController(WashingDayDefinition wd, List<RoutineDefinition> allroutines,AlarmController ac)
        {
            if(wd == null)
                throw new ArgumentNullException("wd");

            this.mWashingDay = wd;
            this.mAllRoutines = allroutines;
            this.mScheduleController = new ScheduleController(wd.Scheduled);
            this.mAlarmController = ac;
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
                throw new ArgumentNullException("routine");
            if (routine.ID == string.Empty)
                throw new ArgumentException($"Routine ID is empty");

            this.mWashingDay.Routines.Remove(routine.ID);

        }

        public void MoveUp(RoutineDefinition routine)
        {
            if (routine == null)
                throw new ArgumentNullException("routine");
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

        public RoutineDefinition GetRoutineById(string id )
        {
            foreach (var r in this.mAllRoutines)
            {
                if (r.ID == id)
                    return r;
            }
            return null;
        }

        public WashingDayInstance GetWashingDayInstance(DateTime date)
        {
            foreach(var i in mWashingDay.Instances)
            {
                if (ScheduleController.IsSameDay(date, i.Day))
                    return i;
            }

            return new WashingDayInstance(mWashingDay.ID, Guid.NewGuid().ToString(), date, GetRoutineDefinitions(), mWashingDay.Description);
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

        public void SaveInstances(string id,string name)
        {
            var sqlItem = new ScheduleSqlDefinition(mWashingDay.Scheduled,id,name);
            mAlarmController.SaveWashDay(sqlItem);

        }

        
         public List<DateTime> GetScheduledDays()
        {
            return mScheduleController.GetScheduledDays();
        }
        
        
        public ScheduleController  GetScheduleController()
        {
            return mScheduleController;
        }

        public String GetSchedule()
        {
           return mScheduleController.GetSchedule();
        }
          
    }
}

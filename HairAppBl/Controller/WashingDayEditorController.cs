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
        public WashingDayEditorController(WashingDayDefinition wd, List<RoutineDefinition> allroutines)
        {
            if(wd == null)
                throw new ArgumentNullException("wd");

            this.mWashingDay = wd;
            this.mAllRoutines = allroutines;
            this.mScheduleController = new ScheduleController(wd.Scheduled);
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

        public void SaveInstances(string id)
        {
            var sqlItem = new ScheduleSqlDefinition(mWashingDay.Scheduled,id);
            var am = new AlarmController();
            am.SaveWashDay(sqlItem);

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
            return mScheduleController.GetScheduledDays();
        }
        
        
        public ScheduleController  GetScheduleController()
        {
            return mScheduleController;
        }
          
    }
}

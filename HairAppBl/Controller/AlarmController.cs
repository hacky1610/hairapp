using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HairAppBl.Interfaces;
using HairAppBl.Models;
using Newtonsoft.Json;

namespace HairAppBl.Controller
{
    public class AlarmController
    {
        private readonly IDataBase scheduleDatabase;
        private readonly IDataBase alarmHistoryDatabase;
        public AlarmController(IDataBase scheduleDb, IDataBase alarmDb)
        {
            this.scheduleDatabase = scheduleDb;
            this.alarmHistoryDatabase = alarmDb;
        }

        public void SaveWashDay(ScheduleSqlDefinition def)
        {

            var list = LoadScheduleDatabase();
            if (list.ContainsKey(def.ID))
                list.Remove(def.ID);
            list.Add(def.ID, def);
             
             this.scheduleDatabase.Save(list);
        }

        public void SetAlarmShown(String id)
        {

            var list = LoadAlarmHistory();
            if (!list.ContainsKey(id))
            {
                list.Add(id, new AlarmHistory());
            }
            list[id].LastAlarm = ScheduleController.GetToday();

            this.alarmHistoryDatabase.Save(list);
        }

        public void SetReminderShown(String id)
        {

            var list = LoadAlarmHistory();
            if (!list.ContainsKey(id))
            {
                list.Add(id, new AlarmHistory());
            }
            list[id].LastReminder = ScheduleController.GetToday();

            this.alarmHistoryDatabase.Save(list);
        }


        public Dictionary<string, ScheduleSqlDefinition> LoadScheduleDatabase()
        {
            try
            {               
                return  this.scheduleDatabase.Load<Dictionary<string, ScheduleSqlDefinition>>();
            }
            catch(Exception)
            {
                return new Dictionary<string, ScheduleSqlDefinition>();
            }
        }

        public Dictionary<string, AlarmHistory> LoadAlarmHistory()
        {
            try
            {
                return this.alarmHistoryDatabase.Load<Dictionary<string, AlarmHistory>>();
            }
            catch (Exception)
            {
                return new Dictionary<string, AlarmHistory>();
            }
        }

        public bool AlarmShown(string id)
        {
            var list = LoadAlarmHistory();

            if (list.ContainsKey(id))
            {
                var val = list[id];
                if (val.LastAlarm == ScheduleController.GetToday())
                    return true;
            }

            return false;
        }

        public bool ReminderShown(string id)
        {
            var list = LoadAlarmHistory();

            if (list.ContainsKey(id))
            {
                var val = list[id];
                if (val.LastReminder == ScheduleController.GetToday())
                    return true;
            }

            return false;
        }

        public  List<ScheduleSqlDefinition> GetTodayWashDays()
        {
            var wdId = new List<ScheduleSqlDefinition>();
            var list = LoadScheduleDatabase();

            foreach (var schedule in list.Values)
            {
                var s = schedule.GetDefinition();
                var controller = new ScheduleController(s);
                if(controller.IsCareDay(DateTime.Now))
                {
                    if(!AlarmShown(schedule.ID))
                         wdId.Add(schedule);
                }
            }
            return wdId;    
        }

        public List<ScheduleSqlDefinition> GetReminderWashDays()
        {
            List<ScheduleSqlDefinition> wdId = new List<ScheduleSqlDefinition>();
            Dictionary<string, ScheduleSqlDefinition> list = LoadScheduleDatabase();

            foreach (var schedule in list.Values)
            {
                var s = schedule.GetDefinition();
                var controller = new ScheduleController(s);
                if (controller.IsCareDay(ScheduleController.GetToday().AddDays(1)))
                {
                    if (!ReminderShown(schedule.ID))
                        wdId.Add(schedule);
                }
            }
            return wdId;
        }

        public void DeleteWashDay(string id)
        {
            var washDays = LoadScheduleDatabase();
            if (washDays.ContainsKey(id))
                washDays.Remove(id);
            this.scheduleDatabase.Save(washDays);


        }

        public static long GetAlarmTime()
        {
            var s = DateTime.Now;
            s = s.AddMinutes(15);

            var utcTime = TimeZoneInfo.ConvertTimeToUtc(s);
            var epochDif = (new DateTime(1970, 1, 1) - DateTime.MinValue).TotalSeconds;
            return utcTime.AddSeconds(-epochDif).Ticks / 10000;
        }




    }
}

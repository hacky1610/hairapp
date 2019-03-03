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
        private readonly IDataBase database;
        public AlarmController(IDataBase db)
        {
            this.database = db;
        }

        public void SaveWashDay(ScheduleSqlDefinition def)
        {

            Dictionary<string,ScheduleSqlDefinition> list = Load();
            if (list.ContainsKey(def.ID))
                list.Remove(def.ID);
            list.Add(def.ID, def);
             
             this.database.Save(list);
        }


        public Dictionary<string, ScheduleSqlDefinition> Load()
        {
            try
            {               
                return  this.database.Load<Dictionary<string, ScheduleSqlDefinition>>();
            }
            catch(Exception e)
            {
                return new Dictionary<string, ScheduleSqlDefinition>();
            }
        }

        public  List<ScheduleSqlDefinition> GetWashDays()
        {
            List<ScheduleSqlDefinition> wdId = new List<ScheduleSqlDefinition>();
            Dictionary<string, ScheduleSqlDefinition> list = Load();

            foreach (var schedule in list.Values)
            {
                var s = schedule.GetDefinition();
                var controller = new ScheduleController(s);
                if(controller.IsCareDay(DateTime.Now))
                {
                    wdId.Add(schedule);
                }
            }
            return wdId;    
        }

        public void DeleteWashDay(string id)
        {
            var washDays = Load();
            if (washDays.ContainsKey(id))
                washDays.Remove(id);
            this.database.Save(washDays);


        }

        public static long GetAlarmTime()
        {
            var s = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);
            if (s < DateTime.Now)
                s = s.AddDays(1);

            var utcTime = TimeZoneInfo.ConvertTimeToUtc(s);
            var epochDif = (new DateTime(1970, 1, 1) - DateTime.MinValue).TotalSeconds;
            return utcTime.AddSeconds(-epochDif).Ticks / 10000;
        }

        public static long Get24Houres()
        {
            return 60001 * 60 * 24;
        }
    }
}

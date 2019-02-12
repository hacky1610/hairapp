using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HairAppBl.Interfaces;
using HairAppBl.Models;
using Newtonsoft.Json;

namespace HairAppBl.Controller
{
    public class AlarmController
    {
        private readonly String dbFile;
        public AlarmController()
        {
            this.dbFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "schedules.json");
        }

        public void SaveWashDay(ScheduleSqlDefinition def)
        {

            Dictionary<string,ScheduleSqlDefinition> list = Load();
            if (list.ContainsKey(def.ID))
                list.Remove(def.ID);
            list.Add(def.ID, def);
             
            string json = JsonConvert.SerializeObject(list);
            File.WriteAllText(dbFile, json);
        }


        public Dictionary<string, ScheduleSqlDefinition> Load()
        {
            try
            {
                List<string> wdId = new List<string>();
                var json = File.ReadAllText(dbFile);
                return (Dictionary<string, ScheduleSqlDefinition>)JsonConvert.DeserializeObject(json, typeof(Dictionary<string, ScheduleSqlDefinition>));
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
    }
}

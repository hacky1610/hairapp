using System;
using System.Collections.Generic;
using System.Text;
using HairAppBl.Interfaces;
using HairAppBl.Models;

namespace HairAppBl.Controller
{
    public class AlarmController
    {
        private readonly IDataBase mDB; 
        public AlarmController(IDataBase dataBase)
        {
            this.mDB = dataBase;
         
        }

        public  List<string> GetWashDays()
        {
            List<string> wdId = new List<string>();
            var table = new DbTable<ScheduleSqlDefinition>(DataBase.Instance);
            var allSchedules = table.GetItemsAsync();
            allSchedules.Wait();
            foreach(var schedule in allSchedules.Result)
            {
                var s = schedule.GetDefinition();
                var controller = new ScheduleController(s);
                if(controller.IsCareDay(DateTime.Now))
                {
                    wdId.Add(schedule.ID);
                }
            }
            return wdId;
           
        }
    }
}

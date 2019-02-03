using System;
using System.Collections.Generic;
using System.Text;
using HairAppBl.Interfaces;

namespace HairAppBl.Controller
{
    public class AlarmController
    {
        private IDataBase mDB; 
        public AlarmController(IDataBase dataBase)
        {
            this.mDB = dataBase;
         
        }

        public  Models.WashingDayInstance GetWashDay()
        {
            var table = new DbTable<Models.WashingDayInstance>(mDB);
            var allDays = table.GetItemsAsync();
            allDays.Wait();
            var today = DateTime.Now;
            foreach(var day in allDays.Result)
            {
                if (day.Day.Year == today.Year && day.Day.Month == today.Month && day.Day.Day == today.Day)
                    return day;
            }
            return null;
        }
    }
}

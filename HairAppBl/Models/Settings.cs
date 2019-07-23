using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HairAppBl.Models
{
    public class AlarmHistory
    {
        public DateTime LastAlarm { get;  set; }
        public DateTime LastReminder { get;  set; }


        public AlarmHistory()
        {
            LastAlarm = new DateTime(1990, 1, 1);
            LastReminder = new DateTime(1990, 1, 1);
        }

        public AlarmHistory(DateTime lastAlarm, DateTime lastTeminder):this()
        {
            LastAlarm = lastAlarm;
            LastReminder = LastReminder;
        }
    }
}

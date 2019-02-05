using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HairAppBl.Models
{
    public class ScheduleSqlDefinition
    {
        public string ID { get; set; }
        public string Value { get; set; }

        public ScheduleSqlDefinition() { }

        public ScheduleSqlDefinition(ScheduleDefinition def, string washdayId)
        {
            ID = washdayId;
            string json = JsonConvert.SerializeObject(def);
            Value = json;
        }

        public ScheduleDefinition GetDefinition()
        {
             return  (ScheduleDefinition)JsonConvert.DeserializeObject(Value, typeof(ScheduleDefinition));
            
        }

    }
}

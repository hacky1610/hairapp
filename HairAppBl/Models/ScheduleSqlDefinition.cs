using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HairAppBl.Models
{
    public class ScheduleSqlDefinition
    {
        public string ID { get; set; }
        public ScheduleDefinition Value { get; set; }

        public ScheduleSqlDefinition() { }

        public ScheduleSqlDefinition(ScheduleDefinition def, string washdayId)
        {
            ID = washdayId;
            Value = def;
        }

        public ScheduleDefinition GetDefinition()
        {
             return  Value;
            
        }

    }
}

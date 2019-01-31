using System;
using System.Collections.Generic;
using System.Text;

namespace HairAppBl.Models
{
    public class RoutineDefinition
    {
        public string Name { get; set; }
        public string ID { get; set; }

        public string Type { get; set; }
        public string Description { get; set; }

        public static readonly  List<string> RoutineTypes = new List<string>() {"Prepoo","HotOilTreatment"};




        public static RoutineDefinition Create(string name, string id, string type, string description)
        {
            var r = new RoutineDefinition();
            r.Name = name;
            r.Description = description;
            r.ID = id;
            r.Type = type;
            return r;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Text;

namespace HairAppBl.Models
{
    public class WashingDayDefinition
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public List<string> Routines { get; set; }
        public DateTime Created { get; set; }


        public WashingDayDefinition()
        {
            Routines = new List<string>();
            Name = "Care day";
 
        }



    }
}

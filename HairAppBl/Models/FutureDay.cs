using System;
using System.Collections.Generic;
using System.Text;

namespace HairAppBl.Models
{
    public class FutureDay
    {
        public DateTime Date { get; set; }
        public List<WashingDayDefinition> Definitions  { get; set; }

        public FutureDay()
        {
            Definitions = new List<WashingDayDefinition>();
        }

    }

}

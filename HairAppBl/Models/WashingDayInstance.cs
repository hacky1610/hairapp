using System;
using System.Collections.Generic;
using System.Text;

namespace HairAppBl.Models
{
    public class WashingDayInstance : Interfaces.IDbObject
    {
        public string WashDayID { get; set; }
        public string ID { get; set; }
        public DateTime Day { get; set; }

        public WashingDayInstance()
        {

        }

        public WashingDayInstance(string wdID, string id, DateTime d)
        {
            WashDayID = wdID;
            ID = id;
            Day = d;
        }
    }
}

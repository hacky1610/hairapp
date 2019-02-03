﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HairAppBl.Models
{
    public class WashingDayDBInstance : Interfaces.IDbObject
    {
        public string WashDayID { get; set; }
        public string ID { get; set; }
        public DateTime Day { get; set; }

        public WashingDayDBInstance()
        {

        }

        public WashingDayDBInstance(string wdID, string id, DateTime d)
        {
            WashDayID = wdID;
            ID = id;
            Day = d;
        }
    }
}

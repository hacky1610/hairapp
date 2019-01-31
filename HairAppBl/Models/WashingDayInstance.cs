using System;
using System.Collections.Generic;
using System.Text;

namespace HairAppBl.Models
{
    public class WashingDayInstance : Interfaces.IDbObject
    {
        public string ID { get; set; }
        public string Comment { get; set; }
        public DateTime Day { get; set; }

        public WashingDayInstance()
        {

        }
    }
}

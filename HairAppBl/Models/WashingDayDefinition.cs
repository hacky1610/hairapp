using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace HairAppBl.Models
{
    public class WashingDayDefinition
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public List<string> Routines { get; set; }
        public DateTime Created { get; set; }
        public ScheduleDefinition Scheduled { get; set; }
        public Color ItemColor { get; set; }



        public WashingDayDefinition()
        {
            Routines = new List<string>();
            Created = DateTime.Now;
            Scheduled = new ScheduleDefinition();
            Random rnd = new Random();
            ItemColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
        }



    }
}

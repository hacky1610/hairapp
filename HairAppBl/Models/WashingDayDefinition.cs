using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace HairAppBl.Models
{
    public class WashingDayDefinition
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ID { get; set; }
        public List<string> Routines { get; set; }
        public List<WashingDayInstance> Instances { get; set; }
        public DateTime Created { get; set; }
        public ScheduleDefinition Scheduled { get; set; }
        public Color ItemColor { get; set; }

        private static List<Color> GetColors()
        {
            var cols = new List<Color>();
            int red = 255;
            int green = 0;
            int blue = 0;

            for(; green < 255; green= green+ 15)
            {
                cols.Add(Color.FromArgb(red, green, blue));
            }
            for (; red > 0; red= red - 15)
            {
                cols.Add(Color.FromArgb(red, green, blue));
            }
            for (; blue < 255; blue= blue + 15)
            {
                cols.Add(Color.FromArgb(red, green, blue));
            }
            for (; green > 0; green= green- 15)
            {
                cols.Add(Color.FromArgb(red, green, blue));
            }
            for (; red < 255; red= red +15)
            {
                cols.Add(Color.FromArgb(red, green, blue));
            }


            return cols;
        }

        public static List<Color> Colors = GetColors();
        

        public WashingDayDefinition()
        {
            Routines = new List<string>();
            Instances = new List<WashingDayInstance>();
            Created = DateTime.Now;
            Scheduled = new ScheduleDefinition();
            Random rnd = new Random();
            ItemColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
        }



    }
}

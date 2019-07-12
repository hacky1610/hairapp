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

        public static List<Color> Colors = new List<Color>()
        {
            Color.Red,
            Color.Blue,
            Color.Brown,
            Color.Purple,
            Color.Plum,
            Color.Green,
            Color.Yellow,
            Color.Orange,
            Color.Orchid,
            Color.SteelBlue,
            Color.Black,
            Color.LawnGreen,
            Color.ForestGreen,
            Color.LimeGreen,
            Color.DarkRed,
            Color.OrangeRed,
            Color.GreenYellow,
            Color.LightGoldenrodYellow,
            Color.CadetBlue,
            Color.LightBlue,
            Color.Purple,
            Color.Olive
        };
        

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

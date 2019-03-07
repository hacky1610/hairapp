using OxyPlot;
using System;
using System.Collections.Generic;
using System.Text;

namespace HairApp.Models
{
    public class ChartLine
    {
        public List<DataPoint> Points { get; private set; }
        public String Name { get; private set; }

        public ChartLine(string name, List<DataPoint> points )
        {
            Name = name;
            Points = points;
        }
    }
}

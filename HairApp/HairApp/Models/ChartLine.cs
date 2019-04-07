using OxyPlot;
using System;
using System.Collections.Generic;

namespace HairApp.Models
{
    public class ChartLine
    {
        public List<DataPoint> Points { get; private set; }
        public String Name { get; private set; }

        public OxyColor Color { get; private set; }

        public ChartLine(string name, List<DataPoint> points, OxyColor color )
        {
            Name = name;
            Points = points;
            Color = color;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using HairApp.Models;
using HairAppBl.Models;
using OxyPlot;
using OxyPlot.Axes;

namespace HairAppBl.Controller
{
    public class HairChartController
    {
        readonly List<HairLength> mHairLengths;
        public HairChartController(List<HairLength> hairLengths)
        {
            if(hairLengths == null)
                throw new ArgumentNullException("hairLengths");

            this.mHairLengths = hairLengths;
        }

        public ChartLine GetBackLengths()
        {
            var list = new List<DataPoint>();
            foreach(var l in mHairLengths)
            {
                list.Add(new DataPoint(DateTimeAxis.ToDouble(l.Day), l.Back));
            }

            return new ChartLine("Back",list);
        }

        public ChartLine GetSideLengths()
        {
            var list = new List<DataPoint>();
            foreach (var l in mHairLengths)
            {
                list.Add(new DataPoint(DateTimeAxis.ToDouble(l.Day), l.Side));
            }

            return new ChartLine("Side", list);
        }

        public List<ChartLine> GetLengths()
        {
            return new List<ChartLine> { GetBackLengths(), GetSideLengths() };
        }

    }
}

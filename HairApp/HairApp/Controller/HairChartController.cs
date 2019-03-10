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
        Dictionary<DataPoint, HairLength> mPointHairLength;
        ChartLine mBackLength;
        ChartLine mSideLength;
        ChartLine mFrontLength;

        public HairChartController(List<HairLength> hairLengths)
        {
            if(hairLengths == null)
                throw new ArgumentNullException("hairLengths");

            mPointHairLength = new Dictionary<DataPoint, HairLength>();

            this.mHairLengths = hairLengths;

            SetBackLengths();
            SetSideLengths();
            SetFrontLengths();
        }


        public HairLength GetHairLengthByPoint(DataPoint point)
        {
            return mPointHairLength[point];
        }


        private void SetBackLengths()
        {
            var list = new List<DataPoint>();
            foreach(var l in mHairLengths)
            {
                var dp = new DataPoint(DateTimeAxis.ToDouble(l.Day), l.Back);
                list.Add(dp);
                mPointHairLength.Add(dp, l);
            }

            mBackLength = new ChartLine("Back",list);
        }

        private void SetSideLengths()
        {
            var list = new List<DataPoint>();
            foreach (var l in mHairLengths)
            {
                var dp = new DataPoint(DateTimeAxis.ToDouble(l.Day), l.Side);
                list.Add(dp);
                mPointHairLength.Add(dp, l);
            }

            mSideLength = new ChartLine("Side", list);
        }

        private void SetFrontLengths()
        {
            var list = new List<DataPoint>();
            foreach (var l in mHairLengths)
            {
                var dp = new DataPoint(DateTimeAxis.ToDouble(l.Day), l.Front);
                list.Add(dp);
                mPointHairLength.Add(dp, l);
            }

            mFrontLength = new ChartLine("Front", list);
        }


        public List<DataPoint> GetSelectPoints(HairLength hl)
        {
            List<DataPoint> points = new List<DataPoint>();
            points.Add(new DataPoint(DateTimeAxis.ToDouble(hl.Day), hl.Back));
            points.Add(new DataPoint(DateTimeAxis.ToDouble(hl.Day), hl.Side));
            points.Add(new DataPoint(DateTimeAxis.ToDouble(hl.Day), hl.Front));
            return points;
        }

        public List<ChartLine> GetCharts()
        {
            return new List<ChartLine> { mBackLength,mSideLength, mFrontLength };
        }

        public List<HairLength> GetLengths()
        {
            return mHairLengths;
        }

    }
}

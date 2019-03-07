using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using HairAppBl.Models;
using HairAppBl.Controller;
using static HairApp.WashDayEditor;
using System.Threading.Tasks;
using OxyPlot.Xamarin.Forms;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using System.Linq;
using HairApp.Models;
using OxyPlot.Annotations;

namespace HairApp.Controls
{
    /// <summary>
    /// For custom renderer on Android (only)
    /// </summary>

    public class HairChartView : Frame
    {
        PlotModel mModel;

        public HairChartView(HairAppBl.Interfaces.IHairBl hairbl, List<ChartLine> lines)
        {

            mModel = new PlotModel();
            var allPoints = new List<DataPoint>();
            foreach (var l in lines)
            {
                allPoints.AddRange(l.Points);
            }
            //Y Axis
            var maxY = allPoints.Max(p => p.Y) + 5;
            var minY = allPoints.Min(p => p.Y) - 5;

            var yAxis = new LinearAxis() { Position = AxisPosition.Right, Minimum = minY, Maximum = maxY , StringFormat = "00cm" };

            //X Axis
            var minX = allPoints.Min(p => p.X) - 3;
            var maxX = allPoints.Max(p => p.X) + 3;
            var xAxis = new DateTimeAxis() { Position = AxisPosition.Bottom, Minimum = minX, Maximum = maxX , StringFormat = "dd.MM.yy" };

            mModel.Axes.Add(yAxis);
            mModel.Axes.Add(xAxis);

            mModel.IsLegendVisible = true;

            foreach (var l in lines)
            {
                var ls = new LineSeries();
                ls.TrackerFormatString = "{4}cm";
                ls.MarkerType = MarkerType.Circle;
                ls.MarkerStrokeThickness = 3;
                ls.TouchStarted += Ls_TouchStarted;
                ls.Title = l.Name;
                ls.SelectionMode = SelectionMode.Single;
                ls.StrokeThickness = 4;
                ls.Points.AddRange(l.Points);

                mModel.Series.Add(ls);
            }
          


            this.Content = new PlotView
            {
                Model = mModel,
                HeightRequest = 400,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
            };
        }

        private void Ls_TouchStarted(object sender, OxyTouchEventArgs e)
        {
            var ls = (LineSeries)sender;
            var p = ls.GetNearestPoint(e.Position, true);

            var cm = p.DataPoint.Y;
            var date = DateTimeAxis.ToDateTime(p.DataPoint.X);

            var labelAnnotation = new TextAnnotation
            {
                Text = $"{date.ToString("dd MMMM yyyy")} - {String.Format("{0:0}", cm)}cm",
                TextPosition = new DataPoint(p.DataPoint.X , p.DataPoint.Y + 2),
                FontWeight = 4,
                Background = OxyColor.FromRgb(211, 211, 211),
                StrokeThickness = 0
                
            };

            var pointAnnotation = new PointAnnotation
            {
                Shape = MarkerType.Diamond,
                Size = 6,
                X = p.DataPoint.X,
                Y = p.DataPoint.Y,
            };
           
           
            mModel.Annotations.Clear();
            mModel.Annotations.Add(labelAnnotation);
            mModel.Annotations.Add(pointAnnotation);
            mModel.InvalidatePlot(false);
        }

  
    }

}

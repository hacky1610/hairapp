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
        HairChartController mController;
        StackLayout mPictContainer;

        public HairChartView(HairAppBl.Interfaces.IHairBl hairbl, HairChartController controller)
        {

            mModel = new PlotModel();
            mController = controller;
            mPictContainer = new StackLayout();
            mPictContainer.Orientation = StackOrientation.Horizontal;

            var allPoints = new List<DataPoint>();
            foreach (var l in mController.GetCharts())
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

            foreach (var l in mController.GetCharts())
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

            foreach (var hl in mController.GetLengths())
            {
                mPictContainer.Children.Add(new HairLengthImage(hairbl,hl)
                {
                    Source = hl.Picture,
                });
            }



            this.Content = new StackLayout
            {
                Children =
                {
                    new PlotView
                    {
                        Model = mModel,
                        HeightRequest = 400,
                        VerticalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                    },
                    new ScrollView
                    {
                        Orientation = ScrollOrientation.Horizontal,
                        Content = mPictContainer
                    }
                }
            };

        }

  

        private void Ls_TouchStarted(object sender, OxyTouchEventArgs e)
        {
            var ls = (LineSeries)sender;
            var nierestPoint = ls.GetNearestPoint(e.Position, true);

            //var date = DateTimeAxis.ToDateTime(nierestPoint.DataPoint.X);

            DataPoint closest = new DataPoint();
            double distance = Double.MaxValue;
            foreach (var point in ls.Points)
            {
                var curDiff = (point.X - nierestPoint.DataPoint.X );
                if (curDiff < 0)
                    curDiff = curDiff * -1; 
                if (curDiff < distance)
                {
                    distance = curDiff;
                    closest = point;
                }
            }

            var hl = mController.GetHairLengthByPoint(closest);

            var labelAnnotation = new TextAnnotation
            {
                Text = $"{hl.Day.ToString("dd MMMM yyyy")} - {String.Format("{0:0}", closest.Y)}cm",
                TextPosition = new DataPoint(closest.X, closest.Y + 2),
                FontWeight = 4,
                Background = OxyColor.FromRgb(211, 211, 211),
                StrokeThickness = 0
                
            };

            var pointAnnotation = new PointAnnotation
            {
                Shape = MarkerType.Diamond,
                Size = 6,
                X = closest.X,
                Y = closest.Y,
            };
           
           
            mModel.Annotations.Clear();
            mModel.Annotations.Add(labelAnnotation);
            mModel.Annotations.Add(pointAnnotation);
            mModel.InvalidatePlot(false);

            foreach(var image in mPictContainer.Children)
            {
                var hairImage = (HairLengthImage)image;
                if (hairImage.HairLength == hl)
                {
                    hairImage.Select();
                    hairImage.Focus();
                }
                else
                    hairImage.Deselect();

            }
       
        }

  
    }

}

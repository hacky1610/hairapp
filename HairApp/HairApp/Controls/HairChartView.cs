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
        ScrollView mScrollContainer;
        Label mDateLabel = new Label();
        Label mBackLengthLabel = new Label();
        Label mSideLengthLabel = new Label();
        Label mFRontLengthLabel = new Label();

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

            if (allPoints.Any())
            {
                //Y Axis
                var maxY = allPoints.Max(p => p.Y) + 5;
                var minY = allPoints.Min(p => p.Y) - 5;

                var yAxis = new LinearAxis() { Position = AxisPosition.Right, Minimum = minY, Maximum = maxY, StringFormat = "00cm" };

                //X Axis
                var minX = allPoints.Min(p => p.X) - 3;
                var maxX = allPoints.Max(p => p.X) + 3;
                var xAxis = new DateTimeAxis() { Position = AxisPosition.Bottom, Minimum = minX, Maximum = maxX, StringFormat = "dd.MM.yy" };

                mModel.Axes.Add(yAxis);
                mModel.Axes.Add(xAxis);
            }

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

            var lengths = mController.GetLengths().OrderBy(x => x.Day);
            foreach (var hl in lengths)
            {
                var image = new HairLengthImage(hairbl, hl)
                {
                    Source = hl.Picture,
                };
                image.Clicked += Image_Clicked;
                mPictContainer.Children.Add(image);
            }

            mScrollContainer = new ScrollView
            {
                Orientation = ScrollOrientation.Horizontal,
                Content = mPictContainer,
                HeightRequest = 100,
            };

            if (lengths.Any())
            {
                FillData(lengths.Last());
                SelectImage((HairLengthImage)mPictContainer.Children.Last());
                SelectAllPoints(lengths.Last());
            }


            Padding = new Thickness(0, 0, 0, 0);
            VerticalOptions = LayoutOptions.FillAndExpand;
            BackgroundColor = Color.Transparent;
            this.Content = new StackLayout
            {
                BackgroundColor = Color.Transparent,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    new Frame
                    {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        CornerRadius = 4,
                        Content = new StackLayout
                        {
                            VerticalOptions = LayoutOptions.FillAndExpand,
                            Children =
                            {
                                  new PlotView
                                    {
                                        Model = mModel,
                                        VerticalOptions = LayoutOptions.FillAndExpand,
                                        HorizontalOptions = LayoutOptions.Fill,
                                    },
                                    mScrollContainer
                            }
                        }
                    },
                    new Frame
                    {
                        CornerRadius = 4,
                        Content = GetDataFields()
                    }
                    
                }
            };

        }

        private void Image_Clicked(object sender, EventArgs e)
        {
            var image = (HairLengthImage)sender;
            ClearSelection();
            SelectImage(image);
            SelectAllPoints(image.HairLength);
            FillData(image.HairLength);
        }

        private void ClearSelection()
        {
            foreach (var image in mPictContainer.Children)
            {
                var hairImage = (HairLengthImage)image;
                hairImage.Deselect();
            }
        }

        private StackLayout GetDataFields()
        {
            return new StackLayout
            {
                Children =
                {
                    GetRow("Date",mDateLabel),
                    GetRow("Back lenght",mBackLengthLabel),
                    GetRow("Side lenght",mSideLengthLabel),
                    GetRow("Front lenght",mFRontLengthLabel)
                }
            };
        }

        private StackLayout GetRow(String labelText, Label label)
        {
            return new StackLayout
            {
                Children =
                {
                    new Label{Text= labelText},
                    label
                },
                Orientation = StackOrientation.Horizontal
            };
        }

        private void FillData(HairLength hl)
        {
            mDateLabel.Text = hl.Day.ToString("dd MMMM yyyy");
            mBackLengthLabel.Text = $"{hl.Back}cm";
            mSideLengthLabel.Text = $"{hl.Side}cm";
            mFRontLengthLabel.Text = $"{hl.Front}cm";
        }

        private void SelectImage(HairLengthImage image)
        {
            image.Select();
            image.Focus();
            mScrollContainer.ScrollToAsync(image.X, 0, true);
        }

        private PointAnnotation SetSelectPoint(DataPoint point)
        {
            return new PointAnnotation
            {
                Shape = MarkerType.Diamond,
                Size = 6,
                X = point.X,
                Y = point.Y
            };
        }

        private void SelectAllPoints(HairLength hl)
        {
            mModel.Annotations.Clear();
            foreach (var selection in mController.GetSelectPoints(hl))
            {
                mModel.Annotations.Add(SetSelectPoint(selection));

            }
            mModel.InvalidatePlot(false);
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


            mModel.Annotations.Clear();
            //mModel.Annotations.Add(labelAnnotation);
            mModel.Annotations.Add(SetSelectPoint(closest));
            mModel.InvalidatePlot(false);

            foreach(var image in mPictContainer.Children)
            {
                var hairImage = (HairLengthImage)image;
                if (hairImage.HairLength == hl)
                {
                    SelectImage(hairImage);
                }
                else
                    hairImage.Deselect();

            }

            FillData(hl);
       
        }

  
    }

}

using System;
using System.Collections.Generic;
using Xamarin.Forms;
using HairAppBl.Models;
using HairAppBl.Controller;
using OxyPlot.Xamarin.Forms;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using System.Linq;
using OxyPlot.Annotations;
using HairApp.Resources;

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
        ImageButton mEditButton = new ImageButton();
        HairLength mCurrentHairLength = null;

        public event EventHandler<EditEventArgs> Edited;

        public HairChartView(HairAppBl.Interfaces.IHairBl hairbl, HairChartController controller)
        {

            mModel = new PlotModel();
            mController = controller;
            mPictContainer = new StackLayout();
            mPictContainer.Orientation = StackOrientation.Horizontal;

            mEditButton = Common.GetButton("edit.png", hairbl);
            mEditButton.Clicked += (sender, e) =>
            {
                Edited?.Invoke(this, new EditEventArgs(mCurrentHairLength));

            };

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
                ls.Color = l.Color;

                mModel.Series.Add(ls);
            }
            mModel.IsLegendVisible = false;

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
                    new StackLayout
                    {
                        Children =
                        {
                            GetRow(AppResources.Date,mDateLabel),
                            mEditButton
                        },
                        Orientation = StackOrientation.Horizontal
                    },
                 
                    GetRow(AppResources.BackLenght,mBackLengthLabel,HairChartController.BackLineColor),
                    GetRow(AppResources.SideLenght,mSideLengthLabel,HairChartController.SideLineColor),
                    GetRow(AppResources.FrontLength,mFRontLengthLabel,HairChartController.FrontLineColor),
                }
            };
        }

        private StackLayout GetRow(String labelText, Label label, OxyColor col)
        {
            var colFrame = new Frame();
            colFrame.Padding = new Thickness(2, 2, 2, 2);
            colFrame.BackgroundColor = Color.FromRgb(col.R,col.G,col.B);
            colFrame.WidthRequest = 15;
            return new StackLayout
            {
                Children =
                {
                    colFrame,
                    new Label{Text= labelText},
                    label
                },
                Orientation = StackOrientation.Horizontal
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
            mCurrentHairLength = hl;
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

    public class EditEventArgs : EventArgs
    {
        public readonly HairLength HairLenght;
        public EditEventArgs(HairLength hl)
        {
            HairLenght = hl;
        }
    }

}

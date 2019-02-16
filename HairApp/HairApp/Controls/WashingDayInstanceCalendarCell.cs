using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using HairAppBl.Models;
using HairAppBl.Controller;
using static HairApp.WashDayEditor;

namespace HairApp.Controls
{
    /// <summary>
    /// For custom renderer on Android (only)
    /// </summary>

    public class WashingDayInstanceCalendarCell : ViewCell
    {
        Label text;
        
        private HairAppBl.Interfaces.IHairBl mHairBl;
        private readonly WashingDayInstance Instance;
        private readonly WashingDayDefinition Definition;
        private StackLayout mDetailsFrame;
        public event EventHandler<WashingDayCellEventArgs> Openclicked;



        public WashingDayInstanceCalendarCell(WashingDayInstance instance,WashingDayDefinition def, HairAppBl.Interfaces.IHairBl hairbl)
        {
            this.mHairBl = hairbl;
            this.Instance = instance;
            this.Definition = def;

            text = new Label
            {
                Text = Definition.Name,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };


            var moreInfoButton = GetButton("info.png");
            moreInfoButton.Clicked += (sender, e) =>
            {
                mDetailsFrame.IsVisible = !mDetailsFrame.IsVisible;
            };

            var commentLabel = Common.GetCalendarDetailsRow("comment.png",new Label
            {
                Text = instance.Comment,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
            },hairbl);
            commentLabel.IsVisible = !String.IsNullOrWhiteSpace(instance.Comment);

            var showMore = new Button
            {
                Text = "show more",
                BackgroundColor = Color.Transparent,
                TextColor = Color.Blue
            };
            showMore.Clicked += ShowMoreButton_Clicked;

            var routineList = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
            };

            foreach (var r in instance.Routines)
            {
                var check = new XLabs.Forms.Controls.CheckBox();
                check.Checked = r.Checked;
                check.IsEnabled = false;
                var row = new StackLayout { Orientation = StackOrientation.Horizontal };
                row.Children.Add(check);
                row.Children.Add(new Label { Text = r.Name });
                routineList.Children.Add(row);
            }

            var routineFrame = Common.GetCalendarDetailsRow("list.png", routineList, hairbl);
            var neededTime = Common.GetCalendarDetailsRow("time.png", new Label { Text = $"{instance.NeededTime.TotalMinutes} minutes"}, hairbl);

            mDetailsFrame = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Style = (Style)hairbl.Resources["DetailsFrame"],
                IsVisible = false,
                Children = { commentLabel, routineFrame, neededTime, showMore}
            };

           


            var frame = new Frame
            {
                Style = (Style)hairbl.Resources["RoutineFrame"],
                Content = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Children =
                    {
                        new StackLayout
                        {
                            Style = (Style)hairbl.Resources["RoutineContent"],
                            Orientation = StackOrientation.Horizontal,

                            Children = { text, moreInfoButton }
                        },
                        mDetailsFrame
                        
                    }
                }
            };
            View = frame;
                
              
        }

        private void ShowMoreButton_Clicked(object sender, EventArgs e)
        {
            Openclicked(this, new WashingDayCellEventArgs(Instance, Definition));
        }

        private ImageButton GetButton(string image)
        {
            return new ImageButton
            {
                Style = (Style)mHairBl.Resources["RoutineButton"],
                HorizontalOptions = LayoutOptions.EndAndExpand,
                Source = image

            };
        }

        public class WashingDayCellEventArgs : EventArgs
        {
            public readonly WashingDayInstance Instance;
            public readonly WashingDayDefinition Definition;

            public WashingDayCellEventArgs(WashingDayInstance instance, WashingDayDefinition definition )
            {
                this.Definition = definition;
                this.Instance = instance;
            }
        }




    }

}

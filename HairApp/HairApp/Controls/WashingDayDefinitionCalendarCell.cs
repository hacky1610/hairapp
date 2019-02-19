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

    public class WashingDayDefinitionCalendarCell : ViewCell
    {
        Label text;
        private HairAppBl.Interfaces.IHairBl mHairBl;
        public WashingDayEditorController WdController { get; set; }
        private StackLayout mDetailsFrame;
        public event EventHandler<WashingDayCellEventArgs> Edited;

        public WashingDayDefinitionCalendarCell(WashingDayEditorController controller, HairAppBl.Interfaces.IHairBl hairbl)
        {
            this.mHairBl = hairbl;
            this.WdController = controller;
            var def = WdController.GetModel();

            text = new Label
            {
                Text = def.Name,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };


            var moreInfoButton = GetButton("info.png");
            moreInfoButton.Clicked += (sender, e) =>
            {
                mDetailsFrame.IsVisible = !mDetailsFrame.IsVisible;
            };

            var descriptionLabel = Common.GetCalendarDetailsRow("description.png", new Label
            {
                Text = def.Description,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                IsVisible = !String.IsNullOrWhiteSpace(def.Description)
            },hairbl);

            var scheduleLabel = Common.GetCalendarDetailsRow("schedule.png", new Label
            {
                Text = WdController.GetSchedule(),
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
            }, hairbl);

            var editButton = new Button
            {
                Text = "edit",
                BackgroundColor = Color.Transparent,
                TextColor = Color.Blue
            };
            editButton.Clicked += EditButton_Clicked;

            var routineList = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
            };

            var listContainer = Common.GetCalendarDetailsRow("list.png",routineList, hairbl);

            foreach (var r in def.Routines)
            {
                routineList.Children.Add(new Label { Text = WdController.GetRoutineById(r).Name });
            }

            mDetailsFrame = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Style = (Style)hairbl.Resources["DetailsFrame"],
                IsVisible = false,
                Children = { descriptionLabel,scheduleLabel, listContainer , editButton}
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

        private void EditButton_Clicked(object sender, EventArgs e)
        {
            Edited(this, new WashingDayCellEventArgs(WdController));
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
            public readonly WashingDayEditorController Controller;

            public WashingDayCellEventArgs(WashingDayEditorController controller)
            {
                this.Controller = controller;
            }
        }




    }

}

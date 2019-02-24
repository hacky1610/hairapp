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

    public class WashingDayDefinitionControl : ViewCell
    {
        Label mLabelText;
        private HairAppBl.Interfaces.IHairBl mHairBl;
        public WashingDayEditorController WdController { get; set; }
        private StackLayout mDetailsFrame;
        public event EventHandler<WashingDayCellEventArgs> Edited;

        public WashingDayDefinitionControl(WashingDayEditorController controller, HairAppBl.Interfaces.IHairBl hairbl)
        {
            this.mHairBl = hairbl;
            this.WdController = controller;
            var def = WdController.GetModel();

            mLabelText = new Label
            {
                Text = def.Name,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };


            var moreInfoButton = Common.GetButton("info.png",hairbl);
            moreInfoButton.Clicked += (sender, e) =>
            {
                if (!mDetailsFrame.IsVisible)
                    ShowDetailsAnimation();
                else
                    HideDetailsAnimation();
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

                            Children = { mLabelText, moreInfoButton }
                        },
                        mDetailsFrame
                        
                    }
                }
            };
            View = frame;
                
          
        }

        private void ShowDetailsAnimation()
        {
            mDetailsFrame.IsVisible = true;
            var animation = new Animation(v => mDetailsFrame.HeightRequest = v,0, 500);
            animation.Commit(mDetailsFrame, "ShowDetails", 16, 800,Easing.SinIn,(v,c) => { mDetailsFrame.HeightRequest = -1; });
        }

        private void HideDetailsAnimation()
        {
            var animation = new Animation(v => mDetailsFrame.HeightRequest = v, 500, 0);
            animation.Commit(mDetailsFrame, "ShowDetails", 16, 800, Easing.SinIn, (v, c) => { mDetailsFrame.IsVisible = false; });
        }

        private void EditButton_Clicked(object sender, EventArgs e)
        {
            Edited(this, new WashingDayCellEventArgs(WdController));
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

using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using HairAppBl.Models;
using HairAppBl.Controller;

namespace HairApp.Controls
{
    /// <summary>
    /// For custom renderer on Android (only)
    /// </summary>

    public class WashingDayCell : ViewCell
    {
        StackLayout startCareDayContainer;
        Label mNameLabel;
        Label timeText;

        public event EventHandler<EventArgs> Removed;
        public event EventHandler<EventArgs> Edited;
        public event EventHandler<WashingDayCellEventArgs> StartCareDay;
        private HairAppBl.Interfaces.IHairBl mHairBl;
        public WashingDayDefinition WashingDayDefinition { get; set; }


        public WashingDayCell(WashingDayDefinition def, HairAppBl.Interfaces.IHairBl hairbl)
        {
            this.mHairBl = hairbl;
            this.WashingDayDefinition = def;
            var c = new ScheduleController(def.Scheduled);
            var t = c.Time2NextCareDay(ScheduleController.GetToday());

            mNameLabel = new Label
            {
                Text = WashingDayDefinition.Name,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                Select();
            };

            //StartCareDay
            var startImage = new Image { Source = "start.png" };

            var startLabel = new Label { Text = "Start care day" };


            startCareDayContainer = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.EndAndExpand,
                IsVisible = false,
                Children = { startLabel, startImage }
            };

            var startCareDayTab = new TapGestureRecognizer();
            startCareDayTab.Tapped += (s, e) => {
                StartCareDay?.Invoke(this, new WashingDayCellEventArgs(WashingDayDefinition));
            };

            startCareDayContainer.GestureRecognizers.Add(startCareDayTab);
            startCareDayContainer.IsVisible = t == 0;

            //Time label
            var text = "today";
            if (t > 0)
                text = $"in {t} days";
            timeText = new Label
            {
                Text = text
            };



            var frame = new Frame
            {
                Style = (Style)hairbl.Resources["RoutineFrame"],
                Content = new StackLayout
                {
                    Style = (Style)hairbl.Resources["RoutineContent"],
                    Orientation = StackOrientation.Horizontal,

                    Children = {
                    new StackLayout {
                        Orientation = StackOrientation.Vertical,
                        Children = { mNameLabel }
                    },
                    timeText,
                    startCareDayContainer
                    
                }
                }
            };
            frame.GestureRecognizers.Add(tapGestureRecognizer);
            View = frame;
                
              
        }


        public void Select()
        {

        }

        public void Deselect()
        {

        }

        private void SendRemoved()
        {
            Removed?.Invoke(this, new EventArgs());
        }

        private void SendEdited()
        {
            Edited?.Invoke(this, new EventArgs());
        }

        public class WashingDayCellEventArgs : EventArgs
        {
            public readonly WashingDayDefinition Definition;

            public WashingDayCellEventArgs(WashingDayDefinition definition)
            {
                this.Definition = definition;
            }
        }


    }

}

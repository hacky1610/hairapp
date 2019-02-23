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
        ImageButton editButton;
        ImageButton deleteButton;
        StackLayout buttonGroup;
        Label text;
        Label timeText;

        public event EventHandler<EventArgs> Removed;
        public event EventHandler<EventArgs> Edited;
        private HairAppBl.Interfaces.IHairBl mHairBl;
        public WashingDayDefinition WashingDayDefinition { get; set; }


        public WashingDayCell(WashingDayDefinition def, HairAppBl.Interfaces.IHairBl hairbl)
        {
            this.mHairBl = hairbl;
            this.WashingDayDefinition = def;
            var c = new ScheduleController(def.Scheduled);
            var t = c.Time2NextCareDay(ScheduleController.GetToday());

            text = new Label
            {
                Text = WashingDayDefinition.Name,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                Select();
            };


            editButton = GetButton("edit.png");
            editButton.Clicked += (sender, e) =>
            {
                SendEdited();
            };

            deleteButton = GetButton("remove.png");
            deleteButton.Clicked += (sender, e) =>
            {
                SendRemoved();
            };

            buttonGroup = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.EndAndExpand,
                IsVisible = false,
                Children = { editButton, deleteButton }
            };

            timeText = new Label
            {
                Text = $"in {t} days"
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
                        Children = { text }
                    },
                    timeText,
                    buttonGroup
                    
                }
                }
            };
            frame.GestureRecognizers.Add(tapGestureRecognizer);
            View = frame;
                
              
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



        public void Select()
        {
            this.buttonGroup.IsVisible = true;
            this.timeText.IsVisible = false;

        }

        public void Deselect()
        {
            this.buttonGroup.IsVisible = false;
            this.timeText.IsVisible = true;

        }

        private void SendRemoved()
        {
            Removed?.Invoke(this, new EventArgs());
        }

        private void SendEdited()
        {
            Edited?.Invoke(this, new EventArgs());
        }


    }

}

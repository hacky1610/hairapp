﻿using System;
using Xamarin.Forms;
using HairAppBl.Controller;
using HairApp.Controller;
using HairApp.Resources;

namespace HairApp.Controls
{
    /// <summary>
    /// For custom renderer on Android (only)
    /// </summary>

    public class WashingDayDefinitionHomeControl : WashingDayDefinitionControl
    {
        public event EventHandler<WashingDayCellEventArgs> StartCareDay;

        public WashingDayDefinitionHomeControl(WashingDayEditorController controller, HairAppBl.Interfaces.IHairBl hairbl):base(controller, hairbl)
        {
            var c = new ScheduleController(controller.GetModel().Scheduled);
            var t = c.Time2NextCareDay(ScheduleController.GetToday());

            //StartCareDay
            var startImage = new Image { Source = "start.png" , HeightRequest = 15};

            var startLabel = new Label { Text = AppResources.StartCareDay };


            var startCareDayContainer = new Frame
            {
                Content = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                   
                    Children = { startLabel, startImage }
                },
                CornerRadius = 8,
                HorizontalOptions = LayoutOptions.EndAndExpand,
                BackgroundColor = Color.DarkGray,
                Padding = new Thickness(3, 3, 3, 3)
            };
           

            var startCareDayTab = new TapGestureRecognizer();
            startCareDayTab.Tapped += (s, e) => {
                StartCareDay?.Invoke(this, new WashingDayCellEventArgs(controller));
            };
            startCareDayContainer.GestureRecognizers.Add(startCareDayTab);

            if (t == 0)
                HeaderExtensionRight = startCareDayContainer;

            var text = AppResources.Today;
            if (t > 0)
                text = AppResources.InDays.Replace("{count}", t.ToString());
            HeaderExtensionLeft = new Label
            {
                Text = text
            };



        }

        public void ShowHelp()
        {
            var tt = new ToolTipController(HeaderExtensionLeft, "Click here to show more info");
            tt.Show();
        }
      


    }

}

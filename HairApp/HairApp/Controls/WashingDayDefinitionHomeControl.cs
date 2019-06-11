using System;
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
            var startImage = new Image { Source = "start.png" , HeightRequest = 22};

            var startCareDayTab = new TapGestureRecognizer();
            startCareDayTab.Tapped += (s, e) => {
                StartCareDay?.Invoke(this, new WashingDayCellEventArgs(controller));
            };
            startImage.GestureRecognizers.Add(startCareDayTab);

            if (t == 0)
                HeaderExtensionRight = startImage;

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

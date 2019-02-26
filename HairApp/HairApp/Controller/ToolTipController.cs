using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HairApp.Controller
{
    class ToolTipController
    {
        Forms9Patch.BubblePopup bubble;
        public ToolTipController(View v, string text)
        {
            bubble = new Forms9Patch.BubblePopup(v, true);
            bubble.Content = new Label { Text = text};
            bubble.PointerDirection = Forms9Patch.PointerDirection.Down;
            bubble.PageOverlayColor = Color.Transparent;
        }

        public void Show()
        {
            bubble.IsVisible = true;
            var aTimer = new System.Timers.Timer(6000);
            aTimer.Elapsed += (s, ev) =>
            {
                Device.BeginInvokeOnMainThread(() => {
                    bubble.IsVisible = false;
                });
            };
            aTimer.Enabled = true;

        }


    }
}

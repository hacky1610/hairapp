using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HairApp.Controls
{
    class Common
    {
        public static StackLayout GetCalendarDetailsRow(string icon, View content, HairAppBl.Interfaces.IHairBl hairbl)
        {
            var row = new StackLayout { Orientation = StackOrientation.Horizontal };
            row.Children.Add(new Image { Source = icon, Style = (Style)hairbl.Resources["CalendarDetailsImage"], });

            var frame = new Frame
            {
                Style = (Style)hairbl.Resources["CalendarDetailsFrame"],
                Content = content
            };
            row.Children.Add(frame);

            return row;
        }
    }
}

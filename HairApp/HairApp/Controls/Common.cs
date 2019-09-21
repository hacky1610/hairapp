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

        public static ImageButton GetButton(string image, HairAppBl.Interfaces.IHairBl hairbl)
        {
            return new ImageButton
            {
                Style = (Style)hairbl.Resources["RoutineButton"],
                HorizontalOptions = LayoutOptions.EndAndExpand,
                Source = image

            };
        }

        public static Entry GetEntry()
        {
            return new Entry
            {
                Keyboard = Keyboard.Numeric,
                WidthRequest = 40
            };
        }
    }
}

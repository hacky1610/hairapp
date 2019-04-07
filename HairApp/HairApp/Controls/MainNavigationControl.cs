using Xamarin.Forms;

namespace HairApp.Controls
{
    /// <summary>
    /// For custom renderer on Android (only)
    /// </summary>

    public class MainNavigationControl : ViewCell
    {

        public readonly ImageButton LeftButton;
        public readonly ImageButton MiddleButton;
        public readonly ImageButton RightButton;
        public MainNavigationControl(HairAppBl.Interfaces.IHairBl hairbl)
        {
            var grid = new Grid();
            grid.HorizontalOptions = LayoutOptions.FillAndExpand;
            
            grid.RowDefinitions.Add(new RowDefinition { Height = 50 });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            LeftButton = new ImageButton { Source = "home.png",
                                        Style = (Style)hairbl.Resources["HomeButton"]
            };
            MiddleButton = new ImageButton { Source = "calendar.png",
                        Style = (Style)hairbl.Resources["HomeButton"]
            };
            RightButton = new ImageButton {
                Source = "chart.png",
                Style = (Style)hairbl.Resources["HomeButton"]
            };

            grid.Children.Add(LeftButton, 0, 0);
            grid.Children.Add(MiddleButton, 1, 0);
            grid.Children.Add(RightButton, 2, 0);
            View = new Frame { Content = grid};

        }

 
    }
}

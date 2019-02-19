using HairAppBl.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XLabs.Forms;

namespace HairApp.Controls
{
    /// <summary>
    /// For custom renderer on Android (only)
    /// </summary>

    public class NavigationControl : ViewCell
    {

        public readonly Button LeftButton;
        public readonly Button RightButton;
        public NavigationControl(string leftButtonText,string rightButtonText)
        {
            var grid = new Grid();
            grid.BackgroundColor = Color.FromHex("E56D0D");
            grid.HorizontalOptions = LayoutOptions.FillAndExpand;
            
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            LeftButton = new Button { Text = leftButtonText, HorizontalOptions = LayoutOptions.Fill , BackgroundColor = Color.Transparent};
            RightButton = new Button { Text = rightButtonText, HorizontalOptions = LayoutOptions.Fill, BackgroundColor = Color.Transparent };

            grid.Children.Add(LeftButton, 0, 0);
            grid.Children.Add(RightButton, 1, 0);
            View = grid;

        }

 
    }
}

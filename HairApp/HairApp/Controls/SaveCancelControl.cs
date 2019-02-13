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

    public class SaveCancelControl : ViewCell
    {

        public readonly Button OkButton;
        public readonly Button CancelButton;
        public SaveCancelControl()
        {
            var grid = new Grid();
            grid.BackgroundColor = Color.LightGray;
            grid.HorizontalOptions = LayoutOptions.FillAndExpand;
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            OkButton = new Button { Text = "Save", HorizontalOptions = LayoutOptions.Fill , BackgroundColor = Color.Transparent};
            CancelButton = new Button { Text = "Cancel", HorizontalOptions = LayoutOptions.Fill, BackgroundColor = Color.Transparent };

            grid.Children.Add(CancelButton, 0, 0);
            grid.Children.Add(OkButton, 1, 0);
            View = grid;

        }

 
    }
}

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

        Button OkButton;
        Button CancelButton;
        public SaveCancelControl()
        {
            var grid = new Grid();
            grid.HorizontalOptions = LayoutOptions.FillAndExpand;
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto});
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto});
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            OkButton = new Button { Text = "Save", HorizontalOptions = LayoutOptions.Fill , BackgroundColor = Color.Transparent};
            CancelButton = new Button { Text = "Cancel", HorizontalOptions = LayoutOptions.Fill, BackgroundColor = Color.Transparent };

            grid.Children.Add(CancelButton, 0, 0);
            grid.Children.Add(OkButton, 1, 0);
            View = grid;

        }

 
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HairAppBl.Controller;
using Rg.Plugins.Popup.Extensions;
using HairApp.Resources;
using Xamarin.Forms;

namespace HairApp.Dialogs
{
    public partial class SelectColorDialog : Rg.Plugins.Popup.Pages.PopupPage
    {
        private HairAppBl.Interfaces.IHairBl mHairbl;
        public Color SelectedColor { get; private set; }


        public SelectColorDialog(MainSessionController msc)
        {
            InitializeComponent();


            var grid = new Grid();

            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            var currentRow = 0;
            var currentCol = 0;
            var maxCol = grid.ColumnDefinitions.Count;


            foreach(var c in msc.GetUnusedColors())
            {
                var colorButton = new Controls.ColorButton(c);
                colorButton.Clicked += ColorButton_Clicked;
                grid.Children.Add(colorButton,currentCol++, currentRow);
                if (currentCol % maxCol == 0)
                {
                    currentRow++;
                    currentCol = 0;
                }
            }

            colorButtons.Content = grid;

        }

        private void ColorButton_Clicked(object sender, EventArgs e)
        {
            var cb = (Controls.ColorButton)sender;
            SelectedColor = cb.Color;
            Navigation.PopPopupAsync();

        }
    }


     }

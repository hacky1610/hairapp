using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using HairAppBl.Models;

namespace HairApp.Controls
{
    /// <summary>
    /// For custom renderer on Android (only)
    /// </summary>

    public class WashingDayDefinitionCalendarCell : ViewCell
    {
        ImageButton editButton;
        ImageButton deleteButton;
        StackLayout buttonGroup;
        Label text;
        
        public event EventHandler<EventArgs> Removed;
        public event EventHandler<EventArgs> Edited;
        private HairAppBl.Interfaces.IHairBl mHairBl;
        public WashingDayDefinition WashingDayDefinition { get; set; }
        private StackLayout mDetailsFrame;


        public WashingDayDefinitionCalendarCell(WashingDayDefinition def, HairAppBl.Interfaces.IHairBl hairbl)
        {
            this.mHairBl = hairbl;
            this.WashingDayDefinition = def;

            text = new Label
            {
                Text = WashingDayDefinition.Name,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };


            var moreInfoButton = GetButton("info.png");
            moreInfoButton.Clicked += (sender, e) =>
            {
                mDetailsFrame.IsVisible = !mDetailsFrame.IsVisible;
            };

            var descriptionLabel = new Label
            {
                Text = def.Description,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                IsVisible = !String.IsNullOrWhiteSpace(def.Description)
            };

            mDetailsFrame = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Style = (Style)hairbl.Resources["DetailsFrame"],
                IsVisible = false,
                Children = { descriptionLabel }
            };

            var frame = new Frame
            {
                Style = (Style)hairbl.Resources["RoutineFrame"],
                Content = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Children =
                    {
                        new StackLayout
                        {
                            Style = (Style)hairbl.Resources["RoutineContent"],
                            Orientation = StackOrientation.Horizontal,

                            Children = { text, moreInfoButton }
                        },
                        mDetailsFrame
                        
                    }
                }
            };
            View = frame;
                
              
        }

        private ImageButton GetButton(string image)
        {
            return new ImageButton
            {
                Style = (Style)mHairBl.Resources["RoutineButton"],
                HorizontalOptions = LayoutOptions.EndAndExpand,
                Source = image

            };
        }




    }

}

using HairAppBl.Controller;
using HairAppBl.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XLabs.Forms;
using System.Linq;

namespace HairApp.Controls
{

    public class HairLengthControl : StackLayout
    {
        HairAppBl.Interfaces.IHairBl mHairbl;
        Entry mBackEntry;
        Entry mSideEntry;
        Entry mFrontEntry;

        public HairLengthControl( HairAppBl.Interfaces.IHairBl hairbl)
        {
            mHairbl = hairbl;

            mBackEntry = GetEntry();
            mSideEntry = GetEntry();
            mFrontEntry = GetEntry();
            Children.Add(GetRow(mBackEntry,"Back"));
            Children.Add(GetRow(mSideEntry, "Side"));
            Children.Add(GetRow(mFrontEntry, "Front"));
        }

        private Entry GetEntry()
        {
            return new Entry
            {
                Keyboard = Keyboard.Numeric,
                WidthRequest = 40
            };
        }

        private StackLayout GetRow(Entry entry, String label)
        {
            return new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Center,
                Children =
                {
                    new Label{Text = label, FontSize = 20, VerticalOptions = LayoutOptions.CenterAndExpand},
                    entry,
                    new Label{Text = "cm", FontSize = 20, VerticalOptions = LayoutOptions.CenterAndExpand}
                }
            };
        }
    }
}

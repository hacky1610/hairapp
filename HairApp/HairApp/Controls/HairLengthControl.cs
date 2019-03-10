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
        Image mPicture;
        String mCurrentPhoto;

        public HairLengthControl( HairAppBl.Interfaces.IHairBl hairbl)
        {
            mHairbl = hairbl;

            mBackEntry = GetEntry();
            mSideEntry = GetEntry();
            mFrontEntry = GetEntry();
            var takePhoto = new Button { Text = "Take photo" };
            takePhoto.Clicked += TakePhoto_Clicked;

            mPicture = new Image { HeightRequest = 100, IsVisible = false };
            Children.Add(GetRow(mBackEntry,"Back"));
            Children.Add(GetRow(mSideEntry, "Side"));
            Children.Add(GetRow(mFrontEntry, "Front"));
            Children.Add(mPicture);
            Children.Add(takePhoto);
        }

        private async void TakePhoto_Clicked(object sender, EventArgs e)
        {
            var c = new Controller.CameraController();
            var file = await c.SelectPhoto();
            mCurrentPhoto = file.AlbumPath;
            mPicture.IsVisible = true;
            mPicture.Source = ImageSource.FromResource(file.AlbumPath);
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

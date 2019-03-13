using HairAppBl.Controller;
using HairAppBl.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XLabs.Forms;
using System.Linq;
using Plugin.Media.Abstractions;
using HairApp.Controller;

namespace HairApp.Controls
{

    public class HairLengthControl : StackLayout
    {
        HairAppBl.Interfaces.IHairBl mHairbl;
        public event EventHandler<EventArgs> TakeOrPicPhotoClicked;
        DatePicker mDatePicker;
        Entry mBackEntry;
        Entry mSideEntry;
        Entry mFrontEntry;
        Image mPicture;
        String mCurrentPhoto;

        public HairLengthControl( HairAppBl.Interfaces.IHairBl hairbl)
        {
            mHairbl = hairbl;
            mDatePicker = new DatePicker();
            mDatePicker.Date = ScheduleController.GetToday();
            mBackEntry = GetEntry();
            mSideEntry = GetEntry();
            mFrontEntry = GetEntry();
            var takePhoto = new Button { Text = "Take photo" };
            takePhoto.Clicked += TakePhoto_Clicked;

            mPicture = new Image { HeightRequest = 100, IsVisible = false };
            Children.Add(mDatePicker);
            Children.Add(GetRow(mBackEntry,"Back"));
            Children.Add(GetRow(mSideEntry, "Side"));
            Children.Add(GetRow(mFrontEntry, "Front"));
            Children.Add(mPicture);
            Children.Add(takePhoto);
        }

        private void TakePhoto_Clicked(object sender, EventArgs e)
        {
            TakeOrPicPhotoClicked?.Invoke(this, new EventArgs());
        }

        public HairLength GetHairLength()
        {
            var hl = new HairLength("");
            hl.Back = GetLengthValue(mBackEntry);
            hl.Side = GetLengthValue(mSideEntry);
            hl.Front = GetLengthValue(mFrontEntry);
            hl.Picture = mCurrentPhoto;
            hl.Day = mDatePicker.Date;
            

            return hl;
        }

        public int GetLengthValue(Entry entry)
        {
            return entry.Text != null ? Int32.Parse(entry.Text) : 0;
        }

        public void SetPicture(MediaFile file)
        {
            if (file != null)
            {
                mPicture.IsVisible = true;
                mCurrentPhoto = file.Path;

                mPicture.Source = CameraController.LoadImage(file);
            }
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

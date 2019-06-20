using HairAppBl.Controller;
using HairAppBl.Models;
using System;
using Xamarin.Forms;
using Plugin.Media.Abstractions;
using HairApp.Controller;
using HairApp.Resources;

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
        HairLength mHairLength;

        public HairLengthControl()
        {
            mHairLength = new HairLength();
        }

        public HairLengthControl(HairAppBl.Interfaces.IHairBl hairbl, HairLength hl = null):this()
        {
            if (hl != null)
                mHairLength = hl;

            mHairbl = hairbl;
            mDatePicker = new DatePicker();
            mDatePicker.Date = ScheduleController.GetToday();
            mBackEntry = GetEntry();
            mSideEntry = GetEntry();
            mFrontEntry = GetEntry();
            var takePhoto = new Button { Text = AppResources.TakePic};
            takePhoto.Clicked += TakePhoto_Clicked;

            mPicture = new Image { HeightRequest = 100, IsVisible = false };

            if (hl != null)
            {
                mBackEntry.Text = hl.Back.ToString();
                mSideEntry.Text = hl.Side.ToString();
                mFrontEntry.Text = hl.Front.ToString();
                if(!string.IsNullOrEmpty(hl.Picture))
                {
                    mPicture.Source = hl.Picture;
                    mCurrentPhoto = hl.Picture;
                    mPicture.IsVisible = true;
                }
                mDatePicker.Date = hl.Day;
            }


            Children.Add(mDatePicker);
            Children.Add(GetRow(mBackEntry,AppResources.BackLenght));
            Children.Add(GetRow(mSideEntry, AppResources.SideLenght));
            Children.Add(GetRow(mFrontEntry, AppResources.FrontLength));
            Children.Add(mPicture);
            Children.Add(takePhoto);
        }

        private void TakePhoto_Clicked(object sender, EventArgs e)
        {
            TakeOrPicPhotoClicked?.Invoke(this, new EventArgs());
        }

        public HairLength GetHairLength()
        {
            mHairLength.Back = GetLengthValue(mBackEntry);
            mHairLength.Side = GetLengthValue(mSideEntry);
            mHairLength.Front = GetLengthValue(mFrontEntry);
            mHairLength.Picture = mCurrentPhoto;
            mHairLength.Day = mDatePicker.Date;
            

            return mHairLength;
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

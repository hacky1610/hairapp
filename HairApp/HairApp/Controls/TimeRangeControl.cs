using HairAppBl.Controller;
using HairAppBl.Models;
using System;
using Xamarin.Forms;
using Plugin.Media.Abstractions;
using HairApp.Controller;
using HairApp.Resources;
using HairAppBl.Interfaces;

namespace HairApp.Controls
{

    public class TimeRangeControl : StackLayout
    {
        #region Members
        IHairBl mHairbl;
        Entry mHour;
        Entry mMinute;
        DateTime mTime;
        #endregion

        #region Constructor
        public TimeRangeControl(IHairBl hairbl)
        {
            Orientation = StackOrientation.Horizontal;
            mHairbl = hairbl;
            var hours = new StackLayout();
            var minutes = new StackLayout();
            hours.Children.Add(new Label()
            {
                Text = "Hours"
            });
            minutes.Children.Add(new Label()
            {
                Text = "Minutes"
            });

            mHour = Common.GetEntry();
            mMinute = Common.GetEntry();

            hours.Children.Add(mHour);
            minutes.Children.Add(mMinute);
            Children.Add(hours);
            Children.Add(minutes);
        }
        #endregion

        #region Functions
        public void SetTime(TimeSpan time)
        {
            mMinute.Text = time.Minutes.ToString();
            mHour.Text = time.Hours.ToString();
        }

        public TimeSpan GetTime()
        {
            return new TimeSpan(int.Parse(mHour.Text), int.Parse(mMinute.Text),0);
        }

        #endregion

        private StackLayout GetRow(Entry entry, string label)
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

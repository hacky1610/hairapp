using System;
using Rg.Plugins.Popup.Extensions;
using HairApp.Resources;
using HairAppBl.Interfaces;

namespace HairApp.Dialogs
{
    public partial class TimeRangePicker : Rg.Plugins.Popup.Pages.PopupPage
    {
        #region Members
        IHairBl mHairbl;
        Controls.TimeRangeControl mTimeRangeControl;
        public event EventHandler<TimeSpanDialogEventArgs> OkClicked;
        #endregion

        #region Constructor
        public TimeRangePicker(IHairBl hairbl,TimeSpan span)
        {
            InitializeComponent();
            mHairbl = hairbl;

            mTimeRangeControl = new Controls.TimeRangeControl(hairbl);
            mTimeRangeControl.SetTime(span);
            mChooseTimeRangeContainer.Content = mTimeRangeControl;

            OKButton.Clicked += OKButton_Clicked;
            CancelButton.Clicked += CancelButton_Clicked;

            //Resources
            mChooseTimeLabel.Text = AppResources.NeededTime;
            CancelButton.Text = AppResources.Cancel;
            OKButton.Text = AppResources.Save;
        }
        #endregion

        #region Functions
        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }

        private void OKButton_Clicked(object sender, EventArgs e)
        {
            OkClicked?.Invoke(this, new TimeSpanDialogEventArgs(mTimeRangeControl.GetTime()));

            Navigation.PopPopupAsync();
        }
        #endregion

        public class TimeSpanDialogEventArgs: EventArgs
        {
            public TimeSpan Time;

            public TimeSpanDialogEventArgs(TimeSpan span)
            {
                Time = span;
            }
        }
    }
}

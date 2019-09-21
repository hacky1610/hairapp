﻿using System;
using Rg.Plugins.Popup.Extensions;
using HairApp.Resources;
using HairAppBl.Interfaces;
using HairAppBl.Models;

namespace HairApp.Dialogs
{
    public partial class AddHairLengthDialog : Rg.Plugins.Popup.Pages.PopupPage
    {
        #region
        IHairBl mHairbl;
        Controls.HairLengthControl mHairLengthControl;
        bool AddNewHairLength;
        public event EventHandler<AddHairLengthDialogEventArgs> OkClicked;
        #endregion

        public AddHairLengthDialog(IHairBl hairbl,HairLength hl = null)
        {
            InitializeComponent();
            mHairbl = hairbl;
            AddNewHairLength = hl == null;

            mHairLengthControl = new Controls.HairLengthControl(hairbl,hl);
            hairLengthContainer.Content = mHairLengthControl;
            mHairLengthControl.TakeOrPicPhotoClicked += Hlc_TakeOrPicPhotoClicked;

            OKButton.Clicked += OKButton_Clicked;
            CancelButton.Clicked += CancelButton_Clicked;

            //Resources
            mEnterHairLengthLabel.Text = AppResources.EnterYourHairLength;
            CancelButton.Text = AppResources.Cancel;
            OKButton.Text = AppResources.Save;
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }

        private void OKButton_Clicked(object sender, EventArgs e)
        {
            OkClicked?.Invoke(this, new AddHairLengthDialogEventArgs(mHairLengthControl.GetHairLength(),AddNewHairLength));

            Navigation.PopPopupAsync();
        }

        private async void Hlc_TakeOrPicPhotoClicked(object sender, EventArgs e)
        {
            var choose = new ChoosePictureDialog(null);
            choose.PictureChoosen += Choose_PictureChoosen;
            await Navigation.PushPopupAsync(choose);
        }

        private void Choose_PictureChoosen(object sender, ChoosePictureDialog.PictureChoosenEventArgs e)
        {
            mHairLengthControl.SetPicture(e.File);
        }

        public class AddHairLengthDialogEventArgs: EventArgs
        {
            public readonly HairAppBl.Models.HairLength HairLength;
            public bool AddNewHairLength;

            public AddHairLengthDialogEventArgs(HairAppBl.Models.HairLength hl, bool addNew)
            {
                HairLength = hl;
                AddNewHairLength = addNew;
            }
        }
    }
}

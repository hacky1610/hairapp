﻿using System;
using Rg.Plugins.Popup.Extensions;
using HairApp.Resources;

namespace HairApp
{
    public partial class AddHairLengthDialog : Rg.Plugins.Popup.Pages.PopupPage
    {
        private HairAppBl.Interfaces.IHairBl mHairbl;
        HairApp.Controls.HairLengthControl mHairLengthControl;
        public event EventHandler<AddHairLengthDialogEventArgs> OkClicked;


        public AddHairLengthDialog(HairAppBl.Interfaces.IHairBl hairbl)
        {
            InitializeComponent();
            mHairbl = hairbl;

            mHairLengthControl = new HairApp.Controls.HairLengthControl(hairbl);
            hairLengthContainer.Content = mHairLengthControl;
            mHairLengthControl.TakeOrPicPhotoClicked += Hlc_TakeOrPicPhotoClicked;

            OKButton.Clicked += OKButton_Clicked;

            //Resources
            mEnterHairLengthLabel.Text = AppResources.EnterYourHairLength;
            CancelButton.Text = AppResources.Cancel;
            OKButton.Text = AppResources.Save;
        }

        private void OKButton_Clicked(object sender, EventArgs e)
        {
            OkClicked?.Invoke(this, new AddHairLengthDialogEventArgs(mHairLengthControl.GetHairLength()));

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

            public AddHairLengthDialogEventArgs(HairAppBl.Models.HairLength hl)
            {
                HairLength = hl;
            }
        }
    }
}

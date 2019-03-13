using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HairAppBl.Controller;
using Rg.Plugins.Popup.Extensions;

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
        }

        private void OKButton_Clicked(object sender, EventArgs e)
        {
            OkClicked?.Invoke(this, new AddHairLengthDialogEventArgs(mHairLengthControl.GetHairLength()));

            Navigation.PopPopupAsync();
        }

        private async void Hlc_TakeOrPicPhotoClicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Take Picture?", "Choose!", "Take new picture", "Select from album");
            var c = new Controller.CameraController();
            Plugin.Media.Abstractions.MediaFile file;
            if (answer)
            {
                file = await c.TakePhoto();
            }
            else
            {
                file = await c.SelectPhoto();
            }
            mHairLengthControl.SetPicture(file);


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

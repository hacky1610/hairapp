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
    public partial class ChoosePictureDialog : Rg.Plugins.Popup.Pages.PopupPage
    {
        private HairAppBl.Interfaces.IHairBl mHairbl;
        public event EventHandler<PictureChoosenEventArgs> PictureChoosen;
        private Controller.CameraController cameraController = new Controller.CameraController();

        public ChoosePictureDialog(HairAppBl.Interfaces.IHairBl hairbl)
        {
            InitializeComponent();
            mHairbl = hairbl;
            TakePictureButton.Clicked += TakePictureButton_Clicked;
            SelectPictureButton.Clicked += SelectPictureButton_Clicked;

            SelectPicContainer.Scale = 0;
            SelectPicContainer.ScaleTo(1, 500);
            TakePicContainer.Scale = 0;
            TakePicContainer.ScaleTo(1, 500);
        }

        private async void SelectPictureButton_Clicked(object sender, EventArgs e)
        {
            var file = await cameraController.SelectPhoto();
            PictureChoosen?.Invoke(this, new PictureChoosenEventArgs(file));
            await Navigation.PopPopupAsync();
        }

        private async void TakePictureButton_Clicked(object sender, EventArgs e)
        {
            var file = await cameraController.TakePhoto();
            PictureChoosen?.Invoke(this, new PictureChoosenEventArgs(file));

            await Navigation.PopPopupAsync();

        }

        public class PictureChoosenEventArgs : EventArgs
        {
            public Plugin.Media.Abstractions.MediaFile File { get; private set; }

            public PictureChoosenEventArgs(Plugin.Media.Abstractions.MediaFile  file)
            {
                File = file;
            }
        }
    }







    }

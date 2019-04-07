using System;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;
using HairApp.Resources;

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

            //Resources
            mTakeNewPicLabel.Text = AppResources.TakePic;
            mSelectFromAlbumLabel.Text = AppResources.SelectFromAlbum;
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

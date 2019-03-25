﻿using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HairApp.Controller
{
    class CameraController
    {
        public CameraController()
        {

        }

        public static ImageSource LoadImage(MediaFile file)
        {
            return ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }

        public async Task<MediaFile> TakePhoto()
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                return null;
            }

            return await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "HairApp",
                SaveToAlbum = true,
                CompressionQuality = 75,
                CustomPhotoSize = 50,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 2000,
                DefaultCamera = CameraDevice.Rear
            });
        }



        public async Task<MediaFile> SelectPhoto()
        {
            return await CrossMedia.Current.PickPhotoAsync();
               
        }

 

    }
}

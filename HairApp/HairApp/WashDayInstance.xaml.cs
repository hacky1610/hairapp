﻿using System;
using System.Collections.Generic;
using System.Linq;
using Rg.Plugins.Popup.Extensions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HairApp.Controls;
using HairAppBl.Models;
using HairApp.Resources;

namespace HairApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WashDayInstance : ContentPage
	{
        private List<RoutineInstanceCell> mRoutineListControls = new List<RoutineInstanceCell>();
        private WashingDayInstance mInstance;
        private WashingDayDefinition mDefinition;
        private HairAppBl.Interfaces.IHairBl mHairbl;

        public WashDayInstance(WashingDayDefinition definition, WashingDayInstance instance, HairAppBl.Interfaces.IHairBl hairbl)
		{
			InitializeComponent ();
       
            mInstance =  instance;
            mDefinition = definition;
            mHairbl = hairbl;

            InitFields();
        }

        private void InitFields()
        {
            //Title
            mLabelTitle.Text = AppResources.DoYourCareDay + $" {mDefinition.Name}";

            //Description
            DescriptionFrame.IsVisible = false;
            if (!string.IsNullOrEmpty(mDefinition.Description))
            {
                Description.Text = mDefinition.Description;
                DescriptionFrame.IsVisible = true;
            }

            //Save close
            var saveClose = new Controls.NavigationControl(AppResources.Cancel, AppResources.Save,mHairbl);
            SaveButtonContainer.Content = saveClose.View;

            saveClose.RightButton.Clicked += OKButton_Clicked;
            saveClose.LeftButton.Clicked += CancelButton_Clicked;

            RefreshList();

            //Needed Time
            UsedTime.Time = mInstance.NeededTime;

            //Comment
            mAddCommentButton.Clicked += AddComment_Clicked;
            mCommentEntry.Text = mInstance.Comment;
            mCommentFrame.IsVisible = false;
            if (!String.IsNullOrEmpty(mInstance.Comment)) ShowComment();

            //Take pic
            takePicButton.Clicked += TakePicture_Clicked;

            PictureListContainer.IsVisible = mInstance.Pictures.Any();
            foreach (var pic in mInstance.Pictures)
                AddPicToAlbum(ImageSource.FromFile(pic.Path));

            //Resources
            mTakePicLabel.Text = AppResources.TakePic;
            mAddCommentButton.Text = AppResources.AddComment;
            mNeededTimeLabel.Text = AppResources.NeededTime;
        }

        private async void TakePicture_Clicked(object sender, EventArgs e)
        {
            var choose = new ChoosePictureDialog(null);
            choose.PictureChoosen += Choose_PictureChoosen; ;
            await Navigation.PushPopupAsync(choose);
        }

        private void Choose_PictureChoosen(object sender, ChoosePictureDialog.PictureChoosenEventArgs e)
        {
            if (e.File == null)
                return;

            mInstance.Pictures.Add(new Picture(e.File.Path));
            PictureListContainer.IsVisible = true;
            AddPicToAlbum(ImageSource.FromStream(() =>
            {
                var stream = e.File.GetStream();
                e.File.Dispose();
                return stream;
            }));
        }

        private void AddPicToAlbum(ImageSource source)
        {
            var picView = new Image { HeightRequest = 100 , Margin = new Thickness(10,10,10,10)};
            picView.Source = source;
            PictureList.Children.Add(picView);
        }

        private void AddComment_Clicked(object sender, EventArgs e)
        {
            ShowComment();
        }

        private void ShowComment()
        {
            mAddCommentButton.IsVisible = false;
            mCommentFrame.IsVisible = true;
        }

        private  void OKButton_Clicked(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            if (!mInstance.Saved)
            {
                mDefinition.Instances.Add(mInstance);
                mInstance.Saved = true;
            }

            mInstance.Comment = mCommentEntry.Text;
            mInstance.NeededTime = UsedTime.Time;

            App.MainSession.SendInstanceEdited();

            App.Current.MainPage.Navigation.PopAsync();
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            Navigation.PopAsync();
        }

        private void RefreshList()
        {
            this.RoutineList.Children.Clear();
            this.mRoutineListControls.Clear();
            foreach (var r in mInstance.Routines)
            {
                var c = new Controls.RoutineInstanceCell(r,App.BL);
                this.RoutineList.Children.Add(c.View);
                this.mRoutineListControls.Add(c);
            }
        }

        public class WashDayInstanceEventArgs : EventArgs
        {
            public Boolean Created { get; set; }
            public WashingDayInstance Instance { get; set; }

            public WashDayInstanceEventArgs(WashingDayInstance instance, Boolean create)
            {
                this.Instance = instance;
                this.Created = create;
            }
        }


    }
}
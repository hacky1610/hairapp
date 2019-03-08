using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HairApp.Controls;
using HairAppBl.Controller;
using HairAppBl.Models;
using Plugin.Media.Abstractions;
using Plugin.Media;

namespace HairApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WashDayInstance : ContentPage
	{
        private List<RoutineInstanceCell> mRoutineListControls = new List<RoutineInstanceCell>();
        private WashingDayInstance mInstance;
        private WashingDayDefinition mDefinition;

        public WashDayInstance(WashingDayDefinition definition, WashingDayInstance instance)
		{
			InitializeComponent ();
       
            mInstance =  instance;
            mDefinition = definition;

            InitFields();
        }

        private void InitFields()
        {
            //Description
            DescriptionFrame.IsVisible = false;
            if (!string.IsNullOrEmpty(mDefinition.Description))
            {
                Description.Text = mDefinition.Description;
                DescriptionFrame.IsVisible = true;
            }

            //Save close
            var saveClose = new Controls.NavigationControl("Cancel", "Save");
            SaveButtonContainer.Content = saveClose.View;

            saveClose.RightButton.Clicked += OKButton_Clicked;
            saveClose.LeftButton.Clicked += CancelButton_Clicked;

            RefreshList();

            //Needed Time
            UsedTime.Time = mInstance.NeededTime;

            //Comment
            AddComment.Clicked += AddComment_Clicked;
            Comment.Text = mInstance.Comment;
            CommentFrame.IsVisible = false;
            if (!String.IsNullOrEmpty(mInstance.Comment)) ShowComment();

            //Take pic
            takePicButton.Clicked += TakePicture_Clicked;

            PictureListContainer.IsVisible = mInstance.Pictures.Any();
            foreach (var pic in mInstance.Pictures)
                AddPicToAlbum(ImageSource.FromFile(pic.Path));
        }


        private async void TakePicture_Clicked(object sender, EventArgs e)
        {
            var c = new Controller.CameraController();
            var file = await c.TakePhoto();
            mInstance.Pictures.Add(new Picture(file.Path));
            PictureListContainer.IsVisible = true;
            AddPicToAlbum(ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
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
            AddComment.IsVisible = false;
            CommentFrame.IsVisible = true;
        }

        private  void OKButton_Clicked(object sender, EventArgs e)
        {
            if (!mInstance.Saved)
            {
                mDefinition.Instances.Add(mInstance);
                mInstance.Saved = true;
            }

            mInstance.Comment = Comment.Text;
            mInstance.NeededTime = UsedTime.Time;

            App.MainSession.SendInstanceEdited();

             App.Current.MainPage.Navigation.PopAsync();
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
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
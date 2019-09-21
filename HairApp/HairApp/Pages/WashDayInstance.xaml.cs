using System;
using System.Collections.Generic;
using System.Linq;
using Rg.Plugins.Popup.Extensions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HairApp.Controls;
using HairAppBl.Models;
using HairApp.Resources;
using HairApp.Dialogs;
using HairAppBl.Interfaces;

namespace HairApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WashDayInstance : ContentPage
	{
        #region Members
        List<RoutineInstanceCell> mRoutineListControls = new List<RoutineInstanceCell>();
        WashingDayInstance mInstance;
        WashingDayDefinition mDefinition;
        IHairBl mHairbl;

        #endregion

        #region Constructor
        public WashDayInstance(WashingDayDefinition definition, WashingDayInstance instance, IHairBl hairbl)
		{
			InitializeComponent ();
       
            mInstance =  instance;
            mDefinition = definition;
            mHairbl = hairbl;

            InitFields();
        }
        #endregion

        #region Functions
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
            var saveClose = new NavigationControl(AppResources.Cancel, AppResources.Save,mHairbl);
            SaveButtonContainer.Content = saveClose.View;

            saveClose.RightButton.Clicked += OKButton_Clicked;
            saveClose.LeftButton.Clicked += CancelButton_Clicked;

            RefreshList();

            //Needed Time
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                var timePicker = new TimeRangePicker(mHairbl, mInstance.NeededTime);
                timePicker.OkClicked += TimeTicker_OkClicked;
                Navigation.PushPopupAsync(timePicker);
            };
            UsedTime.GestureRecognizers.Add(tapGestureRecognizer);

            //Comment
            mAddCommentButton.Clicked += AddComment_Clicked;
            mCommentEntry.Text = mInstance.Comment;
            mCommentFrame.IsVisible = false;
            if (!string.IsNullOrEmpty(mInstance.Comment)) ShowComment();

            //Take pic
            takePicButton.Clicked += TakePicture_Clicked;

            PictureListContainer.IsVisible = mInstance.Pictures.Any();
            foreach (var pic in mInstance.Pictures)
                AddPicToAlbum(ImageSource.FromFile(pic.Path));

            //Resources
            mTakePicLabel.Text = AppResources.TakePic;
            mAddCommentButton.Text = AppResources.AddComment;
            mNeededTimeLabel.Text = AppResources.NeededTime;
            UsedTime.Text = AppResources.ClickToEnterTime;
        }

        private async void TakePicture_Clicked(object sender, EventArgs e)
        {
            var choose = new ChoosePictureDialog(null);
            choose.PictureChoosen += Choose_PictureChoosen; ;
            await Navigation.PushPopupAsync(choose);
        }

        private void TimeTicker_OkClicked(object sender, TimeRangePicker.TimeSpanDialogEventArgs e)
        {
            mInstance.NeededTime = e.Time;
            if(e.Time.Hours ==0 )
                UsedTime.Text = $"{e.Time.Minutes} {AppResources.Minutes}";
            else
                UsedTime.Text = $"{e.Time.Hours}:{string.Format(String.Format("{0:D2}", e.Time.Minutes))} {AppResources.Hours}";

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
            //mInstance.NeededTime = UsedTime.Time;

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
                var c = new RoutineInstanceCell(r,App.BL);
                this.RoutineList.Children.Add(c.View);
                this.mRoutineListControls.Add(c);
            }
        }
        #endregion

        public class WashDayInstanceEventArgs : EventArgs
        {
            public bool Created { get; set; }
            public WashingDayInstance Instance { get; set; }

            public WashDayInstanceEventArgs(WashingDayInstance instance, bool create)
            {
                this.Instance = instance;
                this.Created = create;
            }
        }


    }
}
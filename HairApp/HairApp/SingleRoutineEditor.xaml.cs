using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HairAppBl.Controller;
using Rg.Plugins.Popup.Extensions;
using HairApp.Resources;
using HairAppBl.Models;

namespace HairApp
{
    public partial class SingleRoutineEditor : Rg.Plugins.Popup.Pages.PopupPage
    {
        private HairAppBl.Interfaces.IHairBl mHairbl;
        private RoutineDefinition mRoutine;
        private Controls.RoutineDefinitionEditCell mRoutineControl;

        public SingleRoutineEditor()
        {
            InitializeComponent();

            //Resources
            OKButton.Text = AppResources.OK;
            mEditRoutineLabel.Text = AppResources.EditRoutine; 

        }

        public SingleRoutineEditor(RoutineDefinition routine, HairAppBl.Interfaces.IHairBl hairBl) :this()
        {
            mRoutine = routine;
            mHairbl = hairBl;
            RefreshList();
            OKButton.Clicked += OKButton_Clicked;

        }

        private void OKButton_Clicked(object sender, EventArgs e)
        {
            mRoutineControl.Save();
            Navigation.PopPopupAsync();
        }


        private void RefreshList()
        {
            mRoutineControl = new Controls.RoutineDefinitionEditCell(mRoutine, mHairbl);
            RoutineContentView.Content = mRoutineControl.View;

        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        // ### Methods for supporting animations in your popup page ###

        // Invoked before an animation appearing
        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();
        }

        // Invoked after an animation appearing
        protected override void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();
        }

        // Invoked before an animation disappearing
        protected override void OnDisappearingAnimationBegin()
        {
            base.OnDisappearingAnimationBegin();
        }

        // Invoked after an animation disappearing
        protected override void OnDisappearingAnimationEnd()
        {
            base.OnDisappearingAnimationEnd();
        }

        protected override Task OnAppearingAnimationBeginAsync()
        {
            return base.OnAppearingAnimationBeginAsync();
        }

        protected override Task OnAppearingAnimationEndAsync()
        {
            return base.OnAppearingAnimationEndAsync();
        }

        protected override Task OnDisappearingAnimationBeginAsync()
        {
            return base.OnDisappearingAnimationBeginAsync();
        }

        protected override Task OnDisappearingAnimationEndAsync()
        {
            return base.OnDisappearingAnimationEndAsync();
        }

        // ### Overrided methods which can prevent closing a popup page ###

        // Invoked when a hardware back button is pressed
        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            return base.OnBackButtonPressed();
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
            return base.OnBackgroundClicked();
        }
    }

}

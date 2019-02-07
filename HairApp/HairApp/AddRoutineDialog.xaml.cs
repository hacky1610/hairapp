﻿using System;
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
	public partial class AddRoutineDialog : Rg.Plugins.Popup.Pages.PopupPage
    {
        private WashingDayEditorController mWashingDayEditorController;
        private List<RoutineCellObject> mRoutines = new List<RoutineCellObject>();

        public AddRoutineDialog()
        {
            InitializeComponent();

        }

        public AddRoutineDialog(WashingDayEditorController controller)
        {
            this.mWashingDayEditorController = controller;
            InitializeComponent();
            foreach(var routine in controller.GetUnusedRoutineDefinitions())
            {
                var routineObject = new RoutineCellObject(routine);
                mRoutines.Add(routineObject);
            }

            this.RoutineList.ItemsSource = mRoutines;
            this.RoutineList.ItemTemplate = new DataTemplate(typeof(Controls.AddRoutineCell)); // has context actions defined

            AddButton.Clicked += AddButton_Clicked;

            // Using ItemTapped
            //this.RoutineList.ItemTapped += async (sender, e) => {
            //    await DisplayAlert("Tapped", e.Item + " row was tapped", "OK");
            //};

            // If using ItemSelected
            //			listView.ItemSelected += (sender, e) => {
            //				if (e.SelectedItem == null) return;
            //				Debug.WriteLine("Selected: " + e.SelectedItem);
            //				((ListView)sender).SelectedItem = null; // de-select the row
            //			};

        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            foreach(var r in mRoutines)
            {
                if(r.Checked)
                    this.mWashingDayEditorController.AddRoutine(r.RoutineObject);
            }
            // Close the last PopupPage int the PopupStack
           Navigation.PopPopupAsync();
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
    
    class RoutineCellObject
    {
        public string Name { get; set; }
        public HairAppBl.Models.RoutineDefinition RoutineObject { get; set; }
        public Boolean Checked { get; set; }


        public RoutineCellObject(HairAppBl.Models.RoutineDefinition routine)
        {
            Name = routine.Name;
            RoutineObject = routine;
        }


    }
}

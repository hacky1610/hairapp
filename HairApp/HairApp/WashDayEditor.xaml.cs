﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HairApp.Controls;
using HairAppBl.Controller;
using HairAppBl.Models;
using static HairAppBl.Models.ScheduleDefinition;

namespace HairApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WashDayEditor : ContentPage
	{
        private WashingDayEditorController mWashingDayEditorController;
        private List<WashingDayEditorCell> mRoutineListControls = new List<WashingDayEditorCell>();
        private Boolean mCreate;
        private HairAppBl.Interfaces.IHairBl mHairbl;

        //Events
        public event EventHandler<WashDayEditorEventArgs> OkClicked;

        public WashDayEditor (WashingDayEditorController wdController,Boolean create, HairAppBl.Interfaces.IHairBl hairbl)
	    {
	        InitializeComponent ();
            mHairbl = hairbl;
            this.mCreate = create;
            this.mWashingDayEditorController = wdController;

            RefreshList();

            var saveClose = new Controls.NavigationControl("Cancel","Save");
            SaveButtonContainer.Content = saveClose.View;

            this.AddRoutine.Clicked += AddRoutine_Clicked;
            saveClose.RightButton.Clicked += OKButton_Clicked;
            saveClose.LeftButton.Clicked += CancelButton_Clicked;
	    
	        InitFields();
        }
	
	    private void InitFields()
	    {
            var model = mWashingDayEditorController.GetModel();

            //Title
            this.WashDayNameEntry.Placeholder = "Title";
            this.WashDayNameEntry.Text = mWashingDayEditorController.GetModel().Name;

            //Description
            this.Description.Placeholder = "Description";
            this.AddDescription.Clicked += AddDescription_Clicked;
            if(!String.IsNullOrWhiteSpace(model.Description))
            {
                AddDescription.IsVisible = false;
                Description.IsVisible = true;
                Description.Text = model.Description;
            }

            //Schedule
            var typeList = ScheduleController.CreateScheduleTypeList();

            OpenTypeButton.Source = "combo.png";
            OpenTypeButton.Clicked += OpenTypeButton_Clicked;

            TypeSelection.ItemsSource = typeList;
            TypeSelection.ItemDisplayBinding = new Binding("Name");

            SelectScheduleTypeView(model.Scheduled.Type);

            if (model.Scheduled.Type == ScheduleDefinition.ScheduleType.Dayly)
                TypeSelection.SelectedIndex = 0;
            if (model.Scheduled.Type == ScheduleDefinition.ScheduleType.Weekly)
                TypeSelection.SelectedIndex = 1;
            if (model.Scheduled.Type == ScheduleDefinition.ScheduleType.Monthly)
                TypeSelection.SelectedIndex = 2;
            if (model.Scheduled.Type == ScheduleDefinition.ScheduleType.Yearly)
                TypeSelection.SelectedIndex = 3;

            TypeSelection.SelectedIndexChanged += TypeSelection_SelectedIndexChanged;


            this.StartDatePicker.MinimumDate = DateTime.Now;
            var schedule = model.Scheduled;

            this.StartDatePicker.Date = schedule.StartDate;

            //Dayly
            mEntryDaylyPeriod.Text = schedule.DaylyPeriod.Period.ToString();

            //Weekly
            foreach (var d in schedule.WeeklyPeriod.WeekDays)
                setWeekDay(d);

            if (!schedule.WeeklyPeriod.WeekDays.Any())
                setWeekDay(DayOfWeek.Monday);

            mEntryWeeklyPeriod.Items.Add("1");
            mEntryWeeklyPeriod.Items.Add("2");
            mEntryWeeklyPeriod.Items.Add("3");
            mEntryWeeklyPeriod.Items.Add("4");
            mEntryWeeklyPeriod.Items.Add("5");

            mEntryWeeklyPeriod.SelectedIndex = schedule.WeeklyPeriod.Period - 1;

        }

        private void TypeSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = (ScheduleTypeObject)((Picker)sender).SelectedItem;
            SelectScheduleTypeView(selectedItem.Type);
        }

        private void SelectScheduleTypeView(ScheduleType type)
        {
            DaylySection.IsVisible = false;
            WeeklySection.IsVisible = false;
            MonthlySection.IsVisible = false;
            YearlySection.IsVisible = false;

            if (type == ScheduleType.Dayly)
                DaylySection.IsVisible = true;
            else if (type == ScheduleType.Weekly)
                WeeklySection.IsVisible = true;
            else if (type == ScheduleType.Monthly)
                MonthlySection.IsVisible = true;
            else if (type == ScheduleType.Yearly)
                YearlySection.IsVisible = true;

        }

        private void OpenTypeButton_Clicked(object sender, EventArgs e)
        {
            TypeSelection.Focus();
        }

        private void AddDescription_Clicked(object sender, EventArgs e)
        {
            ShowDescription();
        }

        private void ShowDescription()
        {
            AddDescription.IsVisible = false;
            Description.IsVisible = true;
        }

        private bool SaveFields()
        {
            if (String.IsNullOrWhiteSpace(WashDayNameEntry.Text))
            {
                DisplayAlert("Something is missing ", "You forgot to enter a title", "OK");
                WashDayNameEntry.Focus();
                return false;
            }

            //Title
            mWashingDayEditorController.GetModel().Name = WashDayNameEntry.Text;

            //Description
            mWashingDayEditorController.GetModel().Description = Description.Text;

            //Schedule
            var schedule = mWashingDayEditorController.GetModel().Scheduled;
            schedule.StartDate = StartDatePicker.Date;
            schedule.Type = ((ScheduleTypeObject)TypeSelection.SelectedItem).Type;

            //Dayly
            schedule.DaylyPeriod.Period = Convert.ToInt32(mEntryDaylyPeriod.Text);

            //Weekly
            schedule.WeeklyPeriod.WeekDays = getWeekDays();
            schedule.WeeklyPeriod.Period = Convert.ToInt32(mEntryWeeklyPeriod.SelectedItem);

            return true;
        }

        private void setWeekDay(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Friday:
                    this.mCheckBoxFriday.Checked = true;
                    break;
                case DayOfWeek.Monday:
                    this.mCheckBoxMonday.Checked = true;
                    break;
                case DayOfWeek.Saturday:
                    this.mCheckBoxSaturday.Checked = true;
                    break;
                case DayOfWeek.Sunday:
                    this.mCheckBoxSunday.Checked = true;
                    break;
                case DayOfWeek.Thursday:
                    this.mCheckBoxThursday.Checked = true;
                    break;
                case DayOfWeek.Tuesday:
                    this.mCheckBoxTuesday.Checked = true;
                    break;
                case DayOfWeek.Wednesday:
                    this.mCheckBoxWednesday.Checked = true;
                    break;
            }
        }

        private List<DayOfWeek> getWeekDays()
        {
            var days = new List<DayOfWeek>();
            if (this.mCheckBoxFriday.Checked)
                days.Add(DayOfWeek.Friday);
            if (this.mCheckBoxMonday.Checked)
                days.Add(DayOfWeek.Monday);
            if (this.mCheckBoxTuesday.Checked)
                days.Add(DayOfWeek.Tuesday);
            if (this.mCheckBoxWednesday.Checked)
                days.Add(DayOfWeek.Wednesday);
            if (this.mCheckBoxThursday.Checked)
                days.Add(DayOfWeek.Thursday);
            if (this.mCheckBoxSaturday.Checked)
                days.Add(DayOfWeek.Saturday);
            if (this.mCheckBoxSunday.Checked)
                days.Add(DayOfWeek.Sunday);
            return days;

        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();

        }

        private void OKButton_Clicked(object sender, EventArgs e)
        {
            if (!SaveFields())
                return;
            mWashingDayEditorController.SaveInstances(mWashingDayEditorController.GetModel().ID, mWashingDayEditorController.GetModel().Name);
            Navigation.PopAsync();
            OkClicked?.Invoke(this, new WashDayEditorEventArgs(mWashingDayEditorController.GetModel(), mCreate));
        }

        private void AddRoutine_Clicked(object sender, EventArgs e)
        {
            var diaog = new AddRoutineDialog(this.mWashingDayEditorController,mHairbl);
            diaog.Disappearing += Diaog_Disappearing;
            // Open a PopupPage
            Navigation.PushPopupAsync(diaog);

        }

        private void Diaog_Disappearing(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void RefreshList()
        {
            this.RoutineList.Children.Clear();
            this.mRoutineListControls.Clear();
            foreach (var r in mWashingDayEditorController.GetRoutineDefinitions())
            {
                var c = new Controls.WashingDayEditorCell(r,App.BL);
                c.Removed += Routine_Removed;
                c.MovedDown += Routine_MovedDown;
                c.MovedUp += Routine_MovedUp;
                this.RoutineList.Children.Add(c.View);
                this.mRoutineListControls.Add(c);
            }
        }

        private WashingDayEditorCell GetRoutineControl(RoutineDefinition routine)
        {
            foreach(var c in mRoutineListControls)
            {
                if (c.Routine == routine)
                    return c;
            }
            return null;
        }

        private void Routine_MovedDown(object sender, EventArgs e)
        {
            var item = ((WashingDayEditorCell)sender);
            this.mWashingDayEditorController.MoveDown(item.Routine);
            RefreshList();
            GetRoutineControl(item.Routine).Select();
        }

        private void Routine_MovedUp(object sender, EventArgs e)
        {
            var item = ((WashingDayEditorCell)sender);
            this.mWashingDayEditorController.MoveUp(item.Routine);
            RefreshList();
            GetRoutineControl(item.Routine).Select();

        }

        private void Routine_Removed(object sender, EventArgs e)
        {
            var item = ((WashingDayEditorCell)sender);
            this.mWashingDayEditorController.RemoveRoutine(item.Routine);
            RefreshList();
        }

        public class WashDayEditorEventArgs:EventArgs
        {
            public Boolean Created { get; set; }
            public WashingDayDefinition Definition { get; set; }

            public WashDayEditorEventArgs(WashingDayDefinition def, Boolean create)
            {
                this.Definition = def;
                this.Created = create;
            }
        }
    }
}

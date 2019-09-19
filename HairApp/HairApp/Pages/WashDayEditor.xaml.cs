using System;
using System.Collections.Generic;
using System.Linq;
using Rg.Plugins.Popup.Extensions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HairApp.Controls;
using HairAppBl.Controller;
using HairAppBl.Models;
using static HairAppBl.Models.ScheduleDefinition;
using HairApp.Resources;
using HairApp.Dialogs;

namespace HairApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WashDayEditor : ContentPage
	{
        private WashingDayEditorController mWashingDayEditorController;
        private MainSessionController mMainSessionController;
        private List<RoutineDefinitionCell> mRoutineListControls = new List<RoutineDefinitionCell>();
        private Boolean mCreate;
        private HairAppBl.Interfaces.IHairBl mHairbl;


        public WashDayEditor (MainSessionController mainSession, WashingDayEditorController wdController,Boolean create, HairAppBl.Interfaces.IHairBl hairbl)
	    {
	        InitializeComponent ();

            mHairbl = hairbl;
            mCreate = create;
            mWashingDayEditorController = wdController;
            mMainSessionController = mainSession;

            RefreshList();

            var saveClose = new NavigationControl(AppResources.Cancel, AppResources.Save,hairbl);
            SaveButtonContainer.Content = saveClose.View;

            //Events
            this.AddRoutine.Clicked += AddRoutine_Clicked;
            saveClose.RightButton.Clicked += OKButton_Clicked;
            saveClose.LeftButton.Clicked += CancelButton_Clicked;

            var colorButtonClicked = new TapGestureRecognizer();
            colorButtonClicked.Tapped += ColorClicked_Tapped;
            colorButton.GestureRecognizers.Add(colorButtonClicked);
	    
	        InitFields();

            //Resources
            mDefineYourCareDayLabel.Text = AppResources.DefineYourCareDay;
            AddRoutine.Text = AppResources.AddRoutine;
            mScheduleLabel.Text = AppResources.Schedule;
            mWeekSelectionEveryLabel.Text = AppResources.Every;
            mWeekSelectionEveryWeek.Text = AppResources.Weeks;
            mDaysSelectionEveryLabel.Text = AppResources.Every;
            mDaysSelectionDaysLabel.Text = AppResources.Days;
            mStartDateLabel.Text = AppResources.StartDate;
            mRoutinesLabel.Text = AppResources.Routines;
            AddDescription.Text = AppResources.AddDescription;

            mCheckBoxMonday.DefaultText = AppResources.Monday;
            mCheckBoxTuesday.DefaultText = AppResources.Tuesdays;
            mCheckBoxWednesday.DefaultText = AppResources.Wednesday;
            mCheckBoxThursday.DefaultText = AppResources.Thursday;
            mCheckBoxFriday.DefaultText = AppResources.Friday;
            mCheckBoxSaturday.DefaultText = AppResources.Saturday;
            mCheckBoxSunday.DefaultText = AppResources.Sunday;

        }

        private void ColorClicked_Tapped(object sender, EventArgs e)
        {
            var colDialog = new SelectColorDialog(mMainSessionController);
            colDialog.Disappearing += ColDialog_Disappearing;
            Navigation.PushPopupAsync(colDialog);
        }

        private void ColDialog_Disappearing(object sender, EventArgs e)
        {
            var c = ((SelectColorDialog)sender).SelectedColor;
            colorButton.BackgroundColor = c;
            mWashingDayEditorController.GetModel().ItemColor = c;
        }

        private void InitFields()
	    {
            var model = mWashingDayEditorController.GetModel();

            //Title
            this.WashDayNameEntry.Placeholder = "Title";
            this.WashDayNameEntry.Text = mWashingDayEditorController.GetModel().Name;

            //Color
            colorButton.BackgroundColor = mWashingDayEditorController.GetModel().ItemColor;

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

            var i = from s in typeList where s.Type == model.Scheduled.Type select s;
            TypeSelection.SelectedItem = i.First();

            TypeSelection.SelectedIndexChanged += TypeSelection_SelectedIndexChanged;


            var schedule = model.Scheduled;
            this.StartDatePicker.MinimumDate = schedule.StartDate;
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

            //Monthly
            mEntryMonthPeriod_1.Text = schedule.MonthlyPeriod.Period.ToString();
            var occurenceList = ScheduleController.CreateMonthOccurenceTypeList();
            mPickerOcurenceInMonth.ItemsSource = occurenceList;
            mPickerOcurenceInMonth.ItemDisplayBinding = new Binding("Name");

            var occurenceItem = from s in occurenceList where s.Type == schedule.MonthlyPeriod.Type select s;
            mPickerOcurenceInMonth.SelectedItem = occurenceItem.First();

            var weekDayList = ScheduleController.CreateDayOfWeekList();
            mPickerDayInWeek.ItemsSource = weekDayList;
            mPickerDayInWeek.ItemDisplayBinding = new Binding("Name");

            var weekDayItem = from s in weekDayList where s.Type == schedule.MonthlyPeriod.WeekDay select s;
            mPickerDayInWeek.SelectedItem = weekDayItem.First();
        }

        private void TypeSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = (TypeNameObject<ScheduleDefinition.ScheduleType>)((Picker)sender).SelectedItem;
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
                DisplayAlert(AppResources.MissingValueTitle, AppResources.ForgotTitle, AppResources.OK);
                WashDayNameEntry.Focus();
                return false;
            }

            if (!mWashingDayEditorController.GetRoutineDefinitions().Any())
            {
                DisplayAlert(AppResources.MissingValueTitle, AppResources.ForgotRoutine, AppResources.OK);
                return false;
            }


            //Title
            mWashingDayEditorController.GetModel().Name = WashDayNameEntry.Text;

            //Description
            mWashingDayEditorController.GetModel().Description = Description.Text;

            //Schedule
            var schedule = mWashingDayEditorController.GetModel().Scheduled;
            schedule.StartDate = StartDatePicker.Date;
            schedule.Type = ((TypeNameObject<ScheduleType>)TypeSelection.SelectedItem).Type;

            //Dayly
            schedule.DaylyPeriod.Period = Convert.ToInt32(mEntryDaylyPeriod.Text);

            //Weekly
            schedule.WeeklyPeriod.WeekDays = getWeekDays();
            schedule.WeeklyPeriod.Period = Convert.ToInt32(mEntryWeeklyPeriod.SelectedItem);

            //Monthly
            schedule.MonthlyPeriod.Period = Convert.ToInt32(mEntryMonthPeriod_1.Text);
            schedule.MonthlyPeriod.Type = ((TypeNameObject<Monthly.ScheduleType>)mPickerOcurenceInMonth.SelectedItem).Type;
            schedule.MonthlyPeriod.WeekDay = ((TypeNameObject<DayOfWeek>)mPickerDayInWeek.SelectedItem).Type;

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
            this.IsEnabled = false;
            Navigation.PopAsync();
        }

        private void OKButton_Clicked(object sender, EventArgs e)
        {
            if (!SaveFields())
                return;

            this.IsEnabled = false;
            if (mCreate)
                mMainSessionController.GetAllWashingDays().Add(mWashingDayEditorController.GetModel());

            mMainSessionController.SendDefinitionsEdited();
            mWashingDayEditorController.SaveInstances(mWashingDayEditorController.GetModel().ID, mWashingDayEditorController.GetModel().Name);
            Navigation.PopAsync();
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
            RefreshList(true);
        }

        private void RefreshList(bool selectLast = false)
        {
            this.RoutineList.Children.Clear();
            this.mRoutineListControls.Clear();
            var count = 1;
            foreach (var r in mWashingDayEditorController.GetRoutineDefinitions())
            {
                var c = new RoutineDefinitionCell(r,App.BL, count++);
                c.Removed += Routine_Removed;
                c.MovedDown += Routine_MovedDown;
                c.MovedUp += Routine_MovedUp;
                c.Selected += Routine_Selected;
                c.Edited += Routine_Edited;
                this.RoutineList.Children.Add(c.View);
                this.mRoutineListControls.Add(c);
            }

            if(selectLast)
            {
                if (this.mRoutineListControls.Any())
                    mRoutineListControls.Last().Select();
            }
        }

        private void Routine_Edited(object sender, EventArgs e)
        {
            RoutineDefinitionCell s = (RoutineDefinitionCell)sender;
            var routineEditor = new SingleRoutineEditor(s.Routine,mHairbl);
            routineEditor.Disappearing += RoutineEditor_Disappearing; ;
            Navigation.PushPopupAsync(routineEditor);
        }

        private void RoutineEditor_Disappearing(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void Routine_Selected(object sender, EventArgs e)
        {
            foreach(var rd in mRoutineListControls)
            {
                if (sender != rd)
                    rd.Deselect();
            }
        }

        private RoutineDefinitionCell GetRoutineControl(RoutineDefinition routine)
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
            var item = ((RoutineDefinitionCell)sender);
            this.mWashingDayEditorController.MoveDown(item.Routine);
            RefreshList();
            GetRoutineControl(item.Routine).Select();
        }

        private void Routine_MovedUp(object sender, EventArgs e)
        {
            var item = ((RoutineDefinitionCell)sender);
            this.mWashingDayEditorController.MoveUp(item.Routine);
            RefreshList();
            GetRoutineControl(item.Routine).Select();

        }

        private void Routine_Removed(object sender, EventArgs e)
        {
            var item = ((RoutineDefinitionCell)sender);
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

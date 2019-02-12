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

namespace HairApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WashDayEditor : ContentPage
	{
        private WashingDayEditorController mWashingDayEditorController;
        private List<WashingDayEditorCell> mRoutineListControls = new List<WashingDayEditorCell>();
        public event EventHandler<WashDayEditorEventArgs> OkClicked;
        private Boolean mCreate;
        HairAppBl.Interfaces.IHairBl mHairbl;


        public WashDayEditor (WashingDayDefinition def,Boolean create, HairAppBl.Interfaces.IHairBl hairbl)
		{
			InitializeComponent ();
            mHairbl = hairbl;
       
            var washingDayDefinition =def;
            this.mCreate = create;
	    var ac = new AlarmController(new FileDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "schedules.json");));
            this.mWashingDayEditorController = new WashingDayEditorController(washingDayDefinition, App.MainSession.GetAllDefinitions(),ac);
            RefreshList();

            this.AddRoutine.Clicked += AddRoutine_Clicked;
            this.OKButton.Clicked += OKButton_Clicked;
            this.CancelButton.Clicked += CancelButton_Clicked;
	    
	        InitFields();
        }
	
	    private void InitFields()
	    {
            this.WashDayNameEntry.Placeholder = "Title";
            this.WashDayNameEntry.Text = mWashingDayEditorController.GetModel().Name;

            this.StartDatePicker.MinimumDate = DateTime.Now;

            var schedule = mWashingDayEditorController.GetModel().Scheduled;

            this.StartDatePicker.Date = schedule.StartDate;

            foreach (var d in schedule.WeeklyPeriod.WeekDays)
                setWeekDay(d);

            mEntryWeeklyPeriod.Text = schedule.WeeklyPeriod.Period.ToString() ;
		
	    }

        private void SaveFields()
        {
            var schedule = mWashingDayEditorController.GetModel().Scheduled;

            mWashingDayEditorController.GetModel().Name = WashDayNameEntry.Text;
            schedule.WeeklyPeriod.WeekDays = getWeekDays();
            schedule.WeeklyPeriod.Period = Convert.ToInt32(mEntryWeeklyPeriod.Text);
            schedule.StartDate = StartDatePicker.Date;
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
            SaveFields();
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

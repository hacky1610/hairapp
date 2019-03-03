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
using XamForms.Controls;
using static HairApp.Controls.WashingDayDefinitionControl;

namespace HairApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CalendarPage : ContentPage
	{
        Dictionary<DateTime, List<WashingDayDefinition>> mFutureDays;
        Dictionary<DateTime, List<WashingDayInstance>> mInstances;
        MainSessionController mMainSessionController;
        AlarmController mAlarmController;
        DateTime mLastDate;
        Calendar mCalendar;

        public CalendarPage(MainSessionController controller, AlarmController ac)
		{
			InitializeComponent();

            mFutureDays = controller.GetFutureDays();
            mInstances = controller.GetInstancesByDate();
            mMainSessionController = controller;
            mAlarmController = ac;

            mCalendar = new Calendar
            {
                BorderColor = Color.White,
                BorderWidth = 3,
                BackgroundColor = Color.White,
                StartDay = DayOfWeek.Monday,
                StartDate = DateTime.Now,
                SelectedBorderColor = Color.Black,
                SelectedBackgroundColor = Color.Gray

            };
            mCalendar.SpecialDates.Clear();
            mCalendar.SelectedDate = DateTime.Now;
            RefreshList(ScheduleController.GetToday());
            mLastDate = mCalendar.StartDate.AddDays(40);

            FillFutureDays(mCalendar);
            FillInstances(mCalendar);

            mCalendar.RightArrowClicked += NextMonth;

            mCalendar.DateClicked += Cal_DateClicked;
            CalendarFrame.Content = mCalendar;
        }

        private void NextMonth(object sender, DateTimeEventArgs e)
        {
            mLastDate = mLastDate.AddDays(40);
            FillFutureDays(mCalendar);
        }

        private void FillFutureDays(Calendar cal)
        {
            foreach (var day in mFutureDays)
            {
                if (day.Key > mLastDate)
                    continue;
                if (mInstances.ContainsKey(day.Key))
                    continue;

                var pattern = new BackgroundPattern(1);
                pattern.Pattern = new List<Pattern>();
                float height = 1.0f;
                foreach (var i in day.Value)
                {

                    pattern.Pattern.Add(new Pattern() { WidthPercent = 1f, HightPercent = height / day.Value.Count, Color = i.ItemColor });
                }

                var specialDate = new SpecialDate(day.Key)
                {
                    BackgroundColor = Color.Blue,
                    Selectable = true,
                    BackgroundPattern = pattern,
                };
                cal.SpecialDates.Add(specialDate);
            }
        }

        private void FillInstances(Calendar cal)
        {
            foreach (var day in mInstances)
            {

                var specialDate = new SpecialDate(day.Key)
                {
                    BackgroundColor = Color.Blue,
                    Selectable = true,
                    BackgroundImage = "done.png",
                };
                cal.SpecialDates.Add(specialDate);
            }
        }

        private void LeftButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void Cal_DateClicked(object sender, DateTimeEventArgs e)
        {
            RefreshList(e.DateTime);
        }

        private void RefreshList(DateTime date)
        {
            PlanedWashDaysContainer.IsVisible = false;
            DoneWashDaysContainer.IsVisible = false;

            this.PlanedWashDays.Children.Clear();
            if (mFutureDays.ContainsKey(date))
            {
                PlanedWashDaysContainer.IsVisible = true;
                foreach (var d in mFutureDays[date])
                {
                    var wdController = new WashingDayEditorController(d, App.MainSession.GetAllDefinitions(), mAlarmController);
                    var c = new WashingDayDefinitionControl(wdController, App.BL);
                    c.Edited += WashingDayEdited;
                    this.PlanedWashDays.Children.Add(c.View);
                }
            }

            this.DoneWashDays.Children.Clear();
            if (mInstances.ContainsKey(date))
            {
                PlanedWashDaysContainer.IsVisible = false;
                DoneWashDaysContainer.IsVisible = true;
                foreach (var d in mInstances[date])
                {
                    var def = mMainSessionController.GetWashingDayById(d.WashDayID);
                    var c = new WashingDayInstanceCalendarCell(d,def, App.BL);
                    c.Openclicked += C_Openclicked;
                    this.DoneWashDays.Children.Add(c.View);
                }
            }
        }

        private void C_Openclicked(object sender, WashingDayInstanceCalendarCell.WashingDayCellEventArgs e)
        {
            Navigation.PushAsync(new WashDayInstance(e.Definition, e.Instance));

        }

        private void WashingDayEdited(object sender, WashingDayCellEventArgs e)
        {
            Navigation.PushAsync(new WashDayEditor(e.Controller, false, App.BL));

        }
    }
}

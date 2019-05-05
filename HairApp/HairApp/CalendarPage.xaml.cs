using System;
using System.Collections.Generic;
using HairApp.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HairApp.Controls;
using HairAppBl.Controller;
using XamForms.Controls;
using static HairApp.Controls.WashingDayDefinitionControl;

namespace HairApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CalendarPage : ContentPage
	{
        MainSessionController mMainSessionController;
        AlarmController mAlarmController;
        DateTime mLastDate;
        Calendar mCalendar;

        public CalendarPage(MainSessionController controller, AlarmController ac)
		{
			InitializeComponent();

            mMainSessionController = controller;
            mAlarmController = ac;

            //Calendar
            InitCalendar();

            //Resources
            mPlanedCareDaysLabel.Text = AppResources.PlanedCareDays;
            mFinshedCareDaysLabel.Text = AppResources.FinishedCareDays;

            mMainSessionController.DefinitionsEdited += MMainSessionController_DefinitionsEdited;
            mMainSessionController.InstanceEdited += MMainSessionController_InstanceEdited;
        }

        private void InitCalendar()
        {
            mCalendar = new Calendar
            {
                BorderColor = Color.White,
                BorderWidth = 3,
                BackgroundColor = Color.White,
                StartDay = DayOfWeek.Monday,
                StartDate = DateTime.Now,
                SelectedBorderColor = Color.Black,
                SelectedBackgroundColor = Color.Gray,

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

        private void MMainSessionController_InstanceEdited(object sender, EventArgs e)
        {
            Redraw();
        }

        private void MMainSessionController_DefinitionsEdited(object sender, EventArgs e)
        {
            Redraw();
        }

        private void Redraw()
        {
            mCalendar.SpecialDates.Clear();
            FillFutureDays(mCalendar);
            FillInstances(mCalendar);
            if (mCalendar.SelectedDate == null)
                mCalendar.SelectedDate = DateTime.Now;

            RefreshList(mCalendar.SelectedDate.Value);
            mCalendar.ForceRedraw();
        }


        private void NextMonth(object sender, DateTimeEventArgs e)
        {
            mLastDate = mLastDate.AddDays(40);
            FillFutureDays(mCalendar);
        }

        private void FillFutureDays(Calendar cal)
        {
            var instances = mMainSessionController.GetInstancesByDate();
            foreach (var day in mMainSessionController.GetFutureDays())
            {
                if (day.Key > mLastDate)
                    continue;
                if (instances.ContainsKey(day.Key))
                    continue;
                if (day.Key < ScheduleController.GetToday())
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
                    BackgroundColor = Color.Transparent,
                    Selectable = true,
                    BackgroundPattern = pattern,
                };
                cal.SpecialDates.Add(specialDate);
            }
        }

        private void FillInstances(Calendar cal)
        {
            foreach (var day in mMainSessionController.GetInstancesByDate())
            {

                var specialDate = new SpecialDate(day.Key)
                {
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
            var futureDays = mMainSessionController.GetFutureDays();
            var instances = mMainSessionController.GetInstancesByDate();
            if (futureDays.ContainsKey(date))
            {
                PlanedWashDaysContainer.IsVisible = true;
                foreach (var d in futureDays[date])
                {
                    var wdController = new WashingDayEditorController(d, App.MainSession.GetAllDefinitions(), mAlarmController);
                    var c = new WashingDayDefinitionControl(wdController, App.BL);
                    c.Edited += WashingDayEdited;
                    c.Removed += C_Removed;
                    this.PlanedWashDays.Children.Add(c.View);
                }
            }

            this.DoneWashDays.Children.Clear();
            if (instances.ContainsKey(date))
            {
                PlanedWashDaysContainer.IsVisible = false;
                DoneWashDaysContainer.IsVisible = true;
                foreach (var d in instances[date])
                {
                    var def = mMainSessionController.GetWashingDayById(d.WashDayID);
                    var c = new WashingDayInstanceCalendarCell(d,def, App.BL);
                    c.Openclicked += C_Openclicked;
                    c.ImageClicked += C_ImageClicked;
                    this.DoneWashDays.Children.Add(c.View);
                }
            }
        }

        private async void C_Removed(object sender, WashingDayCellEventArgs e)
        {
            var answer  = await DisplayAlert(AppResources.DeleteWashDay, AppResources.DeleteWashdayConfirmation, AppResources.Delete, AppResources.Cancel);
            if (answer)
            {
                mAlarmController.DeleteWashDay(e.Controller.GetModel().ID);
                mMainSessionController.DeleteWashDay(e.Controller.GetModel());
                Redraw();
            }
        
        }

        private void C_ImageClicked(object sender, WashingDayInstanceCalendarCell.ImageClickedEventArgs e)
        {
            Navigation.PushAsync(new PicturePage(e.Source));

        }

        private void C_Openclicked(object sender, WashingDayInstanceCalendarCell.WashingDayCellEventArgs e)
        {
            Navigation.PushAsync(new WashDayInstance(e.Definition, e.Instance));

        }

        private void WashingDayEdited(object sender, WashingDayCellEventArgs e)
        {
            Navigation.PushAsync(new WashDayEditor(mMainSessionController, e.Controller, false, App.BL));

        }
    }
}

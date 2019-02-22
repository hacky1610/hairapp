﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using HairAppBl;
using HairAppBl.Controller;

namespace HairApp
{
    public partial class MainPage : ContentPage
    {
        private AlarmController mAlarmController;
        public MainPage()
        {
            App.BL.Logger.Call("MainPage");
            InitializeComponent();


            OpenWashDayOverview.Source = "edit.png";
            OpenWashDayOverview.Clicked += OpenWashingDayOverview_Clicked;
            ShowCalendar.Source = "calendar.png";
            OpenStatistic.Source = "chart.png";


            ShowCalendar.Clicked += ShowCalendar_Clicked;

            var fileDb = new FileDB(Constants.SchedulesStorageFile);
            this.mAlarmController = new AlarmController(fileDb);
        }

        private void ShowCalendar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CalendarPage(App.MainSession,App.MainSession.GetFutureDays(),App.MainSession.GetInstances()));
        }

        private void OpenPageIfNeeded()
        {
            if(!App.MainSession.Initialized)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PushAsync(new IntroPage(App.MainSession.GetAllWashingDays(), App.BL, mAlarmController), true);
                });
               return;
            }

            if (!String.IsNullOrEmpty(App.washdayToShow))
            {
                var day = App.MainSession.GetWashingDayById(App.washdayToShow);
                var contr = new WashingDayEditorController(day, App.MainSession.GetAllDefinitions(), mAlarmController);
                var wdInstance = new HairAppBl.Models.WashingDayInstance(App.washdayToShow, Guid.NewGuid().ToString(), ScheduleController.GetToday(), contr.GetRoutineDefinitions(), day.Description);
                App.washdayToShow = String.Empty;

                Device.BeginInvokeOnMainThread(() => {
                    Navigation.PushAsync(new WashDayInstance(day, wdInstance), true);
                });
                ;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            OpenPageIfNeeded();


            OpenCareDay.IsVisible = false;
           var timeToNexDay =   App.MainSession.NextDay();
            if (!timeToNexDay.Days.Any())
            {
                TimeToNextCareDay.Text = "No Care day configured";
                ValsImage.Source = "nocareday.jpg";

            }
            else if (timeToNexDay.Days.Count == 1)
            {
                if (timeToNexDay.Time2Wait == 0)
                {
                    TimeToNextCareDay.Text = $"Today is care day: {timeToNexDay.Days[0].Name}";
                    OpenCareDay.IsVisible = true;
                    OpenCareDay.Text = $"Lets do {timeToNexDay.Days[0].Name}";
                    OpenCareDay.Command = new Command<string>(OpenCareDayCommand);
                    OpenCareDay.CommandParameter = timeToNexDay.Days[0].ID;
                    ValsImage.Source = "caredaytoday.jpg";

                }
                else
                {
                    ValsImage.Source = "caredaysoon.jpg";
                    TimeToNextCareDay.Text = $"Next care day {timeToNexDay.Days[0].Name} is in {timeToNexDay.Time2Wait} days";
                }
            }
            else
            {
                if (timeToNexDay.Time2Wait == 0)
                {
                    TimeToNextCareDay.Text = "Today is care day";
                }
                else
                {
                    TimeToNextCareDay.Text = $"Next care day is in {timeToNexDay.Time2Wait} days";
                }
            }
        }

        private void OpenCareDayCommand(string id)
        {
            var day = App.MainSession.GetWashingDayById(id);
            var contr = new WashingDayEditorController(day, App.MainSession.GetAllDefinitions(),this.mAlarmController);
            var wdInstance = contr.GetWashingDayInstance(ScheduleController.GetToday());

            var instancePage = new WashDayInstance(day,wdInstance);
            instancePage.OkClicked += InstancePage_OkClicked;

            Navigation.PushAsync(instancePage, true);
        }


        private void InstancePage_OkClicked(object sender, WashDayInstance.WashDayInstanceEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TestPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TestPage());
        }

        private void OpenWashingDayOverview_Clicked(object sender, EventArgs e)
        {
            App.BL.Logger.Call("ChangeScreen_Clicked");

            Navigation.PushAsync(new WashDayOverview(App.MainSession.GetAllWashingDays(), App.BL,mAlarmController));
        }

    }
}

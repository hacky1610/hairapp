using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using HairAppBl;
using HairAppBl.Controller;
using Rg.Plugins.Popup.Extensions;

namespace HairApp
{
    public partial class MainPage : ContentPage
    {
        private AlarmController mAlarmController;
        private Controls.CareDayList mCareDayList;

        public MainPage()
        {
            App.BL.Logger.Call("MainPage");
            InitializeComponent();

            //Home Menu
            Home.Source = "home.png";
            ShowCalendar.Source = "calendar.png";
            OpenStatistic.Source = "chart.png";

            //Button Events
            ShowCalendar.Clicked += ShowCalendar_Clicked;
            mAddCareDayButton.Clicked += MAddCareDayButton_Clicked;
            openSettingsButton.Clicked += OpenSettingsButton_Clicked;

            var fileDb = new FileDB(Constants.SchedulesStorageFile);
            this.mAlarmController = new AlarmController(fileDb);

            mCareDayList = new Controls.CareDayList(App.MainSession.GetAllWashingDays(), App.BL, mAlarmController);
            CareDayListFrame.Content = mCareDayList;
        }

        private void OpenSettingsButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LogView(App.BL.Logger));

        }

        private void MAddCareDayButton_Clicked(object sender, EventArgs e)
        {
            mCareDayList.AddWashDay();
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
                    var page = new IntroPage(App.MainSession.GetAllWashingDays(), App.BL, mAlarmController);
                    page.PageClosed += IntroPage_Closed;
                    Navigation.PushAsync(page, true);
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

            Device.BeginInvokeOnMainThread(() => {
                //var helpController = new Controller.HelpController();
                //helpController.Add("Foo", "Des", "Tooltip", mAddCareDayButton);
                //var diaog = new HelpPage(helpController);
                // Open a PopupPage
                //Navigation.PushPopupAsync(diaog);
            });
        }

        private void IntroPage_Closed(object sender, EventArgs e)
        {
            mInfo.IsVisible = true;
            var animation = new Animation(v => mAddCareDayButton.Scale = v, 1, 1.2);
            animation.Commit(mAddCareDayButton, "SimpleAnimation", 16, 1000, Easing.Linear, (v, c) => mAddCareDayButton.Scale = 1, () => true);

            
            var aTimer = new System.Timers.Timer(10000);
            aTimer.Elapsed += (s,ev)=>
            {
                Device.BeginInvokeOnMainThread(() => {
                    var animation1 = new Animation(v => mInfo.Scale = v, 1, 0.0);
                    animation1.Commit(this, "SimpleAnimation1", 16, 1000, Easing.Linear, (v, c) => { mInfo.Scale = 0; mInfo.IsVisible = false; },() => false);
                    AnimationExtensions.AbortAnimation(mAddCareDayButton, "SimpleAnimation");
                });
            };
            aTimer.Enabled = true;
        }

  

        protected override void OnAppearing()
        {
            base.OnAppearing();
            OpenPageIfNeeded();

         
         

            mCareDayList.RefreshList();
           var timeToNexDay =   App.MainSession.NextDay();
            if (!timeToNexDay.Days.Any())
            {
                TimeToNextCareDay.Text = "No Care day configured";
                ValsImage.Source = "nocareday.jpg";

            }
            else 
            {
                if (timeToNexDay.Time2Wait == 0)
                {
                    TimeToNextCareDay.Text = $"Today is care day";
                    ValsImage.Source = "caredaytoday.jpg";
                }
                else
                {
                    ValsImage.Source = "caredaysoon.jpg";
                    TimeToNextCareDay.Text = $"Next care day {timeToNexDay.Days[0].Name} is in {timeToNexDay.Time2Wait} days";
                }
            }
  
        }

        private void InstancePage_OkClicked(object sender, WashDayInstance.WashDayInstanceEventArgs e)
        {
            throw new NotImplementedException();
        }




    }
}

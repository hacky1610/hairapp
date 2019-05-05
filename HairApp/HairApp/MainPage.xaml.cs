using System;
using System.Linq;
using Xamarin.Forms;
using HairAppBl.Controller;
using HairApp.Controls;
using HairApp.Resources;

namespace HairApp
{
    public partial class MainPage : ContentPage
    {
        private AlarmController mAlarmController;
        private CareDayList mCareDayList;
        public event EventHandler<EventArgs> ListRefreshed;

        public MainPage(AlarmController ac)
        {
            App.BL.Logger.Call("MainPage");
            InitializeComponent();

            this.mAlarmController = ac;

            mCareDayList = new CareDayList(App.MainSession, App.BL, mAlarmController, this);
            CareDayListFrame.Content = mCareDayList;

            mAddCareDayButton.Clicked += MAddCareDayButton_Clicked;

            var openLog = new TapGestureRecognizer();
            openLog.NumberOfTapsRequired = 2;
            openLog.Tapped+= (s, e) => {
                Navigation.PushAsync(new LogView(App.BL.Logger));
            };
            ValsImage.GestureRecognizers.Add(openLog);

            //Resources
            mListOfAllCareDaysLabel.Text = AppResources.ListOfAllCareDays;
        }

        private void OpenSettingsButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LogView(App.BL.Logger));

        }

        private void MAddCareDayButton_Clicked(object sender, EventArgs e)
        {
            mCareDayList.AddWashDay();
        }

        private void OpenPageIfNeeded()
        {
            if(!App.MainSession.Initialized)
            {
               /* Device.BeginInvokeOnMainThread(() =>
                {
                    var page = new IntroPage(App.MainSession.GetAllWashingDays(), App.BL, mAlarmController);
                    page.PageClosed += IntroPage_Closed;
                    Navigation.PushAsync(page, true);
                });
               return;*/
            }

            if (!String.IsNullOrEmpty(App.washdayToShow))
            {
                var day = App.MainSession.GetWashingDayById(App.washdayToShow);
                var contr = new WashingDayEditorController(day, App.MainSession.GetAllDefinitions(), mAlarmController);
                var wdInstance = contr.GetWashingDayInstance(ScheduleController.GetToday());
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
                TimeToNextCareDay.Text = AppResources.NoCareDay;
                ValsImage.Source = "nocareday.jpg";

            }
            else 
            {
                if (timeToNexDay.Time2Wait == 0)
                {
                    TimeToNextCareDay.Text = AppResources.CareDayToday;
                    ValsImage.Source = "caredaytoday.jpg";
                }
                else
                {
                    ValsImage.Source = "caredaysoon.jpg";
                    var text = AppResources.NextCareDay.Replace("{name}", timeToNexDay.Days[0].Name);
                    TimeToNextCareDay.Text = text.Replace("{days}", timeToNexDay.Time2Wait.ToString());


                }
            }
  
        }

        private void InstancePage_OkClicked(object sender, WashDayInstance.WashDayInstanceEventArgs e)
        {
            throw new NotImplementedException();
        }




    }
}

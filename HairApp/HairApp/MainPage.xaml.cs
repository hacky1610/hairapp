using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
            TestPage.Clicked += TestPage_Clicked;
            
            var fileDb = new FileDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "schedules.json"));
	        this.mAlarmController = new AlarmController(fileDb);

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            OpenCareDay.IsVisible = false;
           var timeToNexDay =   App.MainSession.NextDay();
            if (!timeToNexDay.Days.Any())
                TimeToNextCareDay.Text = "No Care day configured";
            else if (timeToNexDay.Days.Count == 1)
            {
                if (timeToNexDay.Time2Wait == 0)
                {
                    TimeToNextCareDay.Text = $"Today is care day: {timeToNexDay.Days[0].Name}";
                    OpenCareDay.IsVisible = true;
                    OpenCareDay.Text = $"Lets do {timeToNexDay.Days[0].Name}";
                    OpenCareDay.Command = new Command<string>(OpenCareDayCommand);
                    OpenCareDay.CommandParameter = timeToNexDay.Days[0].ID;

                }
                else
                {
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
            var contr = new HairAppBl.Controller.WashingDayEditorController(day, App.MainSession.GetAllDefinitions(), this.mAlarmController);
            var wdInstance = new HairAppBl.Models.WashingDayInstance(id, Guid.NewGuid().ToString(), DateTime.Now, contr.GetRoutineDefinitions());
             Navigation.PushAsync(new WashDayInstance(wdInstance));
        }

        private void TestPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TestPage());
        }

        private void OpenWashingDayOverview_Clicked(object sender, EventArgs e)
        {
            App.BL.Logger.Call("ChangeScreen_Clicked");

            Navigation.PushAsync(new WashDayOverview(App.MainSession.GetAllWashingDays(), App.BL,this.mAlarmController));
        }

    }
}

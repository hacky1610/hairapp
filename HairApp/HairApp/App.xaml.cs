using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HairAppBl;
using HairAppBl.Interfaces;
using HairAppBl.Controller;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using HairApp.Interfaces;
using HairApp.Resources;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace HairApp
{
    public partial class App : Application
    {
        public static String washdayToShow;

        public App()
        {
            InitializeComponent();

            //Init Culture
            //var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            /*var ci = new System.Globalization.CultureInfo("fr");
            AppResources.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);*/

            //Init Alarms
            DependencyService.Get<IAlarm>().Init();
            DependencyService.Get<IAlarm>().InitReminder();


            BL = new HairAppBl.HairAppBl(new FileLogger(), Current.Resources);
            App.MainSession = new MainSessionController(Current.Properties);
            App.MainSession.Saved += MainSession_Saved;
            Session.Register(App.MainSession);
            Session.Restore();


            var fileDb = new FileDB(Constants.SchedulesStorageFile);
            var ac = new AlarmController(fileDb);

            MainPage = new NavigationPage(new MainTabPage(BL,MainSession,ac));

        }

        private void MainSession_Saved(object sender, EventArgs e)
        {
            Current.SavePropertiesAsync();
        }

        public void InitException()
        {
            AppCenter.Start("android=8321cd36-8954-4649-97f7-c8eb9019d46e;" +
                  "uwp={Your UWP App secret here};" +
                  "ios=5fa34f6c-5c63-4560-acbc-9d560e6e34b2",
                  typeof(Analytics), typeof(Crashes));

        }

        public void SendException(Exception e)
        {
            Crashes.TrackError(e);
        }

  

        public App(String washdayId):this()
        {
            washdayToShow = washdayId;
        }

        public INavigation GetNavigation()
        {
            return MainPage.Navigation;
        }

        public static IHairBl BL { get; set; }
        public static MainSessionController MainSession { get; set; }

        protected override void OnStart()
        {
        }   

        protected override void OnSleep()
        {
            Session.Save();
        }


        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HairAppBl;
using HairAppBl.Interfaces;
using HairAppBl.Controller;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace HairApp
{
    public partial class App : Application
    {
        public static String washdayToShow;

        public App()
        {
            InitializeComponent();

            App.BL = new HairAppBl.HairAppBl(new FileLogger(),Application.Current.Resources);
            App.MainSession = new MainSessionController(Current.Properties);
            Session.Register(App.MainSession);
            Session.Restore();

            var logger = new HairAppBl.ConsoleLogger();

            MainPage = new NavigationPage( new MainPage());
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

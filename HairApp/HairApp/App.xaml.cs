using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HairAppBl;
using HairAppBl.Interfaces;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace HairApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            App.BL = new HairAppBl.HairAppBl(new ConsoleLogger(),Application.Current.Resources);
            App.MainSession = new HairAppBl.Controller.MainSessionController(Current.Properties);
            HairAppBl.Controller.Session.Register(App.MainSession);
            HairAppBl.Controller.Session.Restore();

            var logger = new HairAppBl.ConsoleLogger();

            MainPage = new NavigationPage( new MainPage());

        }

        public INavigation GetNavigation()
        {
            return MainPage.Navigation;
        }

        public static HairAppBl.Interfaces.IHairBl BL { get; set; }
        public static HairAppBl.Controller.MainSessionController MainSession { get; set; }

        protected override void OnStart()
        {
    
        }

        protected override void OnSleep()
        {
            HairAppBl.Controller.Session.Save();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

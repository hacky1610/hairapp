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
            var logger = new HairAppBl.ConsoleLogger();

            MainPage = new NavigationPage( new MainPage());
        }

        public static HairAppBl.Interfaces.IHairBl BL { get; set; }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using HairAppBl;

namespace HairApp
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            App.BL.Logger.Call("MainPage");
            InitializeComponent();

            Init.Clicked += Init_Clicked;

            OpenWashDayOverview.Source = "calendar.png";
            OpenWashDayOverview.Clicked += OpenWashingDayOverview_Clicked;



        }

        private void Init_Clicked(object sender, EventArgs e)
        {
            var ac = new HairAppBl.Controller.AlarmController(HairAppBl.DataBase.Instance);
            App.MainSession.Init();

        }

        private async void OpenWashingDayOverview_Clicked(object sender, EventArgs e)
        {
            App.BL.Logger.Call("ChangeScreen_Clicked");

            Navigation.PushAsync(new WashDayOverview(App.MainSession.GetAllWashingDays()));
        }

    }
}

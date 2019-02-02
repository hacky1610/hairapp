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
            
            ChangeScreen.Clicked += ChangeScreen_Clicked;

            Application.Current.Properties["Foo"] = "Bar";

      

        }

        private async void ChangeScreen_Clicked(object sender, EventArgs e)
        {
            App.BL.Logger.Call("ChangeScreen_Clicked");

            var ac = new HairAppBl.Controller.AlarmController( HairAppBl.DataBase.Instance);
            ac.FillDb();

            Navigation.PushAsync(new WashDayEditor());
        }

    }
}

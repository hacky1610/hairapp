using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using HairAppBl;
using Plugin.Notifications;

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

            var t = new HairAppBl.Models.WashingDayInstance();
            t.ID = "Bat1";
            t.Comment = "Foo";
            t.Day = DateTime.Now;
            var table = new DbTable<HairAppBl.Models.WashingDayInstance>(DataBase.Instance);
            await table.SaveItemAsync(t);


            var t2 = await table.GetItemsAsync();


            Navigation.PushAsync(new WashDayEditor());
        }

    }
}

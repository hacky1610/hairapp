﻿using System;
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
            Init.Clicked += Init_Clicked;

            

        }

        private void Init_Clicked(object sender, EventArgs e)
        {
            var ac = new HairAppBl.Controller.AlarmController(HairAppBl.DataBase.Instance);
            ac.FillDb();
        }

        private async void ChangeScreen_Clicked(object sender, EventArgs e)
        {
            App.BL.Logger.Call("ChangeScreen_Clicked");

      

            Navigation.PushAsync(new WashDayEditor());
        }

    }
}

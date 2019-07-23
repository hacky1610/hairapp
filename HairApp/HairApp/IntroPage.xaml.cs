using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HairAppBl.Controller;
using HairAppBl.Models;
using HairApp.Resources;
using HairApp.Interfaces;

namespace HairApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IntroPage : ContentPage
	{
        private MainSessionController mMainSessionController;
        private AlarmController mAlarmController;
        public String Culture { get; private set; }

        public IntroPage(MainSessionController mainSessionController,AlarmController ac)
	    {
	        InitializeComponent ();
            mMainSessionController = mainSessionController;
            mAlarmController = ac;
            FrenchButton.Clicked += FrenchButton_Clicked;
            GermanButton.Clicked += GermanButton_Clicked;
            EnglishButton.Clicked += EnglishButton_Clicked;
        }

        private void SetLanguage(String lang)
        {
            mMainSessionController.SetCulture(lang);
            mAlarmController.SetCulture(lang);
            var ci = new System.Globalization.CultureInfo(lang);
            AppResources.Culture = ci;
            DependencyService.Get<ILocalize>()?.SetLocale(ci);


            Navigation.PopAsync();
        }

        private void EnglishButton_Clicked(object sender, EventArgs e)
        {
            SetLanguage("en-US");


        }

        private void GermanButton_Clicked(object sender, EventArgs e)
        {
            SetLanguage("de");

        }

        private void FrenchButton_Clicked(object sender, EventArgs e)
        {
            SetLanguage("fr");

        }
    }
}

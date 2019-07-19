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
        public event EventHandler<EventArgs> PageClosed;
        private MainSessionController mMainSessionController;


        public IntroPage(MainSessionController mainSessionController)
	    {
	        InitializeComponent ();
            mMainSessionController = mainSessionController;
            FrenchButton.Clicked += FrenchButton_Clicked;
            GermanButton.Clicked += GermanButton_Clicked;
            EnglishButton.Clicked += EnglishButton_Clicked;
        }

        private void SetLanguage(String lang)
        {
            mMainSessionController.SetCulture(lang);
            var ci = new System.Globalization.CultureInfo(lang);
            AppResources.Culture = ci;
            DependencyService.Get<ILocalize>()?.SetLocale(ci);
            Navigation.PopAsync();
        }

        private void EnglishButton_Clicked(object sender, EventArgs e)
        {
            SetLanguage("en");
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

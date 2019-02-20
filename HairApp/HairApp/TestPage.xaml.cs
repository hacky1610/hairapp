using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HairApp.Controls;
using HairAppBl.Controller;
using HairAppBl.Models;

namespace HairApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TestPage : ContentPage
	{
        HairAppBl.Interfaces.IHairBl mHairbl;


        public TestPage()
		{
			InitializeComponent();
            Init.Clicked += Init_Clicked;

            ShowLog.Clicked += ShowLog_Clicked;
        }

        private void ShowLog_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LogView(App.BL.Logger));

        }

        private void Init_Clicked(object sender, EventArgs e)
        {
            App.MainSession.Init();

        }

    }
}

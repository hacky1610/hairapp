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
using XamForms.Controls;
using static HairApp.Controls.WashingDayDefinitionControl;

namespace HairApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PicturePage : ContentPage
	{

        public PicturePage(ImageSource source)
		{
			InitializeComponent();

            var navi = new Controls.NavigationControl("Home", "");
            NavigationContainer.Content = navi.View;
            navi.LeftButton.Clicked += LeftButton_Clicked; ;

            mImageView.Source = source;
        }

        private void LeftButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
      
    }
}

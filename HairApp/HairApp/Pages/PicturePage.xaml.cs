using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HairApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PicturePage : ContentPage
	{

        public PicturePage(ImageSource source, HairAppBl.Interfaces.IHairBl hairbl)
		{
			InitializeComponent();

            var navi = new Controls.NavigationControl("Home", "",hairbl);
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

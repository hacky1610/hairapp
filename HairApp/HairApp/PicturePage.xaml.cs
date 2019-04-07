using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

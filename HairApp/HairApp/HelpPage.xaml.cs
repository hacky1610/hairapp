using System;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms.Xaml;
using HairApp.Controller;

namespace HairApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HelpPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        HelpController mHelpController;
        public HelpPage()
        {
            InitializeComponent();
            HelpText.Clicked += HelpText_Clicked;
        }

        private void HelpText_Clicked(object sender, EventArgs e)
        {
            mHelpController.SHow(0);
            Navigation.PushPopupAsync(this);
        }

        public HelpPage(HelpController hController):this()
        {
            mHelpController = hController;
        }

    }
}

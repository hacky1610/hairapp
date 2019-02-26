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

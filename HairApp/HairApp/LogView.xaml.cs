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
    public partial class LogView : ContentPage
    {
        HairAppBl.Interfaces.ILogger mLogger;
        public LogView(HairAppBl.Interfaces.ILogger logger)
        {
            InitializeComponent();
            LogContent.Text = logger.ReadLog();
            ClearButton.Clicked += ClearButton_Clicked;
            mLogger = logger;
        }

        private void ClearButton_Clicked(object sender, EventArgs e)
        {
            mLogger.Clear();
            LogContent.Text = String.Empty;
        }
    }
}

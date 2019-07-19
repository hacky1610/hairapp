using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            Init.Clicked += Init_Clicked;
            mLogger = logger;
        }

        private void Init_Clicked(object sender, EventArgs e)
        {
            App.MainSession.SetCulture(String.Empty);

        }

        private void ClearButton_Clicked(object sender, EventArgs e)
        {
            mLogger.Clear();
            LogContent.Text = String.Empty;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using HairAppBl.Interfaces;
using HairAppBl.Controller;
using Rg.Plugins.Popup.Extensions;
using HairApp.Controls;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace HairApp
{
    public class MainTabPage : XLabs.Forms.Controls.ExtendedTabbedPage
    {

        public MainTabPage(IHairBl bl, MainSessionController mainSessionController, AlarmController ac):base()
        {
            App.BL.Logger.Call("MainPage");
            
            var mainPage = new MainPage(ac);
            mainPage.Icon = "home.png";

            var calPage = new CalendarPage(mainSessionController, ac);
            calPage.Icon = "calendar.png";

            var hPage = new HistoryPage(bl, mainSessionController);
            hPage.Icon = "chart.png";

            Children.Add(mainPage);
            Children.Add(calPage);
            Children.Add(hPage);
            BarBackgroundColor = Color.Brown;
            BarTintColor = Color.Cyan;

            NavigationPage.SetHasNavigationBar(this, false);
            //SetToolbarPlacement(ToolbarPlacement.Bottom);

        }


    }
}

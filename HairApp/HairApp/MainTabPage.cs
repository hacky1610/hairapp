using Xamarin.Forms;
using HairAppBl.Interfaces;
using HairAppBl.Controller;

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
            hPage.Icon = "checklist.png";

            var cPage = new ChartPage(bl, mainSessionController);
            cPage.Icon = "chart.png";

            Children.Add(mainPage);
            Children.Add(calPage);
            Children.Add(hPage);
            Children.Add(cPage);
            BarBackgroundColor = Color.Brown;
            BarTintColor = Color.Cyan;

            NavigationPage.SetHasNavigationBar(this, false);
            //SetToolbarPlacement(ToolbarPlacement.Bottom);

        }


    }
}

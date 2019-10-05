using HairApp.Pages;
using HairAppBl.Controller;
using HairAppBl.Interfaces;
using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace HairApp.Controls
{
    public class CustomTabBar : Xamarin.Forms.TabbedPage
    {
        public void Foo()
        {

        }

        public CustomTabBar(IHairBl bl, MainSessionController mainSessionController, AlarmController ac) : base()
        {
            ToolbarItems.Add(new ToolbarItem("Settings","settingstitle.png",new Action(Foo),ToolbarItemOrder.Primary));


            NavigationPage.SetTitleView(this, CreateTitleView(new Image() { Source = "title.png", HeightRequest = 43 }));

            SelectedTabColor = Color.FromHex((String)bl.Resources["MainColor"]);
            UnselectedTabColor = Color.DarkGray;
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            var navigationPage =  new MainPage(ac,bl);
            navigationPage.IconImageSource = "home.png";
            navigationPage.Title = "";
            var navigationPage2 = new CalendarPage(mainSessionController, ac,bl);
            navigationPage2.IconImageSource = "calendar.png";
            navigationPage2.Title = "";
            var navigationPage3 = new HistoryPage(bl, mainSessionController);
            navigationPage3.IconImageSource = "checklist.png";
            navigationPage3.Title = "";
            var navigationPage4 = new ChartPage(bl, mainSessionController);
            navigationPage4.IconImageSource = "chart.png";
            navigationPage4.Title = "";
            Children.Add(navigationPage);
            Children.Add(navigationPage2);
            Children.Add(navigationPage3);
            Children.Add(navigationPage4);

           // NavigationPage.SetHasNavigationBar(this, false);
        }

        public static View CreateTitleView(View view)
        {
            view.VerticalOptions = LayoutOptions.CenterAndExpand;

            var titleView = new StackLayout
            {
                Children = { view },
                Padding = new Thickness(10, 10,30,10)
            };

            return titleView;
        }
    }
}

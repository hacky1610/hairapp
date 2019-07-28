﻿using Xamarin.Forms;
using HairAppBl.Interfaces;
using HairAppBl.Controller;
using HairApp.Interfaces;

namespace HairApp
{
    public class MainTabPage : XLabs.Forms.Controls.ExtendedTabbedPage
    {
        MainSessionController mMainsSesion;
        AlarmController mAlarmController;
        IHairBl mHairBl;

        public MainTabPage(IHairBl bl, MainSessionController mainSessionController, AlarmController ac):base()
        {
            App.BL.Logger.Call("MainPage");
            mMainsSesion = mainSessionController;
            mAlarmController = ac;
            mHairBl = bl;


            if (!mainSessionController.HasCulture())
            {
                var intro = new IntroPage(mainSessionController, ac);
                intro.Disappearing += Intro_Disappearing;
                Navigation.PushAsync(intro);
            }
            else
            {
                var ci = new System.Globalization.CultureInfo(mainSessionController.GetCulture());
                DependencyService.Get<ILocalize>().SetLocale(ci);
                Init();
            }
            
        
            //SetToolbarPlacement(ToolbarPlacement.Bottom);

        }

        private void Intro_Disappearing(object sender, System.EventArgs e)
        {
            Init();
            mMainsSesion.InitRoutines();
        }

        private void Init()
        {
            var mainPage = new MainPage(mAlarmController,mHairBl);
            mainPage.Icon = "home.png";

            var calPage = new CalendarPage(mMainsSesion, mAlarmController,mHairBl);
            calPage.Icon = "calendar.png";

            var hPage = new HistoryPage(mHairBl, mMainsSesion);
            hPage.Icon = "checklist.png";

            var cPage = new ChartPage(mHairBl, mMainsSesion);
            cPage.Icon = "chart.png";

            Children.Add(mainPage);
            Children.Add(calPage);
            Children.Add(hPage);
            Children.Add(cPage);
            BarBackgroundColor = Color.Brown;
            BarTintColor = Color.Cyan;

            NavigationPage.SetHasNavigationBar(this, false);
        }

    }
}

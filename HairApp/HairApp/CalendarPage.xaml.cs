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
using XamForms.Controls;

namespace HairApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CalendarPage : ContentPage
	{
        Dictionary<DateTime, List<HairAppBl.Models.WashingDayDefinition>> mFutureDays;

        public CalendarPage(Dictionary<DateTime, List<HairAppBl.Models.WashingDayDefinition>> futureDays)
		{
			InitializeComponent();

            var navi = new Controls.NavigationControl("Home", "");
            NavigationContainer.Content = navi.View;
            navi.LeftButton.Clicked += LeftButton_Clicked; ;

            mFutureDays = futureDays;
            var cal = new Calendar
            {
                BorderColor = Color.White,
                BorderWidth = 3,
                BackgroundColor = Color.White,
                StartDay = DayOfWeek.Monday,
                StartDate = DateTime.Now,
                SelectedBorderColor = Color.Black,
                SelectedBackgroundColor = Color.Gray

            };
            cal.SpecialDates.Clear();

            foreach (var day in futureDays)
            {
                var pattern = new BackgroundPattern(1);
                pattern.Pattern = new List<Pattern>();
                float height = 1.0f;
                foreach(var i in day.Value)
                {

                    pattern.Pattern.Add(new Pattern() { WidthPercent = 1f, HightPercent = height/day.Value.Count, Color = i.ItemColor });
                }

                var specialDate = new SpecialDate(day.Key)
                {
                    BackgroundColor = Color.Blue,
                    Selectable = true,
                    BackgroundPattern = pattern,
                };
                cal.SpecialDates.Add(specialDate);
            }

            cal.DateClicked += Cal_DateClicked;
            CalendarFrame.Content = cal;
        }

        private void LeftButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void Cal_DateClicked(object sender, DateTimeEventArgs e)
        {
            RefreshList(e.DateTime);
        }

        private void RefreshList(DateTime date)
        {
            this.Washdaylist.Children.Clear();
            if (mFutureDays.ContainsKey(date))
            {
                foreach (var d in mFutureDays[date])
                {
                    var c = new Controls.WashingDayCell(d, App.BL);
                    this.Washdaylist.Children.Add(c.View);
                }
            }

           
        }
    }
}

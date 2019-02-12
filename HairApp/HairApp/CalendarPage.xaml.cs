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
        HairAppBl.Interfaces.IHairBl mHairbl;


        public CalendarPage(Dictionary<DateTime, List<HairAppBl.Models.WashingDayDefinition>> futureDays)
		{
			InitializeComponent();
            var cal = new Calendar
            {
                BorderColor = Color.Pink,
                BorderWidth = 3,
                BackgroundColor = Color.Pink,
                StartDay = DayOfWeek.Monday,
                StartDate = DateTime.Now,
                 
            };

            foreach(var day in futureDays)
            {
                var specialDate = new SpecialDate(day.Key)
                {
                    BackgroundColor = Color.Blue,
                    Selectable = true,
                };
                cal.SpecialDates.Add(specialDate);
            }

            cal.SpecialDates.Add(new SpecialDate(DateTime.Now.AddDays(2)) {
                BackgroundColor = Color.Blue,
                Selectable = true,
                BackgroundPattern = new BackgroundPattern(1)
                {
                    Pattern = new List<Pattern>
                            {
                                new Pattern{ WidthPercent = 1f, HightPercent = 0.25f, Color = Color.Red},
                                new Pattern{ WidthPercent = 1f, HightPercent = 0.25f, Color = Color.Purple},
                                new Pattern{ WidthPercent = 1f, HightPercent = 0.25f, Color = Color.Green},
                                new Pattern{ WidthPercent = 1f, HightPercent = 0.25f, Color = Color.Yellow}
                            }
                }
            });
            CalendarFrame.Content = cal;
        }

    

    }
}

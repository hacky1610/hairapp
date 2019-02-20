﻿using System;
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
using static HairApp.Controls.WashingDayDefinitionCalendarCell;

namespace HairApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CalendarPage : ContentPage
	{
        Dictionary<DateTime, List<WashingDayDefinition>> mFutureDays;
        Dictionary<DateTime, List<WashingDayInstance>> mInstances;
        MainSessionController mMainSessionController;

        public CalendarPage(MainSessionController controller, Dictionary<DateTime, List<WashingDayDefinition>> futureDays, Dictionary<DateTime, List<WashingDayInstance>> instances)
		{
			InitializeComponent();

            var navi = new Controls.NavigationControl("Home", "");
            NavigationContainer.Content = navi.View;
            navi.LeftButton.Clicked += LeftButton_Clicked; ;

            mFutureDays = futureDays;
            mInstances = instances;
            mMainSessionController = controller;

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
            cal.SelectedDate = DateTime.Now;
            RefreshList(ScheduleController.GetToday());

            FillFutureDays(cal);
            FillInstances(cal);

            cal.DateClicked += Cal_DateClicked;
            CalendarFrame.Content = cal;
        }

        private void FillFutureDays(Calendar cal)
        {
            foreach (var day in mFutureDays)
            {
                if (mInstances.ContainsKey(day.Key))
                    continue;

                var pattern = new BackgroundPattern(1);
                pattern.Pattern = new List<Pattern>();
                float height = 1.0f;
                foreach (var i in day.Value)
                {

                    pattern.Pattern.Add(new Pattern() { WidthPercent = 1f, HightPercent = height / day.Value.Count, Color = i.ItemColor });
                }

                var specialDate = new SpecialDate(day.Key)
                {
                    BackgroundColor = Color.Blue,
                    Selectable = true,
                    BackgroundPattern = pattern,
                };
                cal.SpecialDates.Add(specialDate);
            }
        }

        private void FillInstances(Calendar cal)
        {
            foreach (var day in mInstances)
            {

                var specialDate = new SpecialDate(day.Key)
                {
                    BackgroundColor = Color.Blue,
                    Selectable = true,
                    BackgroundImage = "done.png",
                };
                cal.SpecialDates.Add(specialDate);
            }
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
            PlanedWashDaysContainer.IsVisible = false;
            DoneWashDaysContainer.IsVisible = false;

            this.PlanedWashDays.Children.Clear();
            if (mFutureDays.ContainsKey(date))
            {
                PlanedWashDaysContainer.IsVisible = true;
                foreach (var d in mFutureDays[date])
                {
                    var wdController = new HairAppBl.Controller.WashingDayEditorController(d, App.MainSession.GetAllDefinitions(), null);
                    var c = new WashingDayDefinitionCalendarCell(wdController, App.BL);
                    c.Edited += WashingDayEdited;
                    this.PlanedWashDays.Children.Add(c.View);
                }
            }

            this.DoneWashDays.Children.Clear();
            if (mInstances.ContainsKey(date))
            {
                PlanedWashDaysContainer.IsVisible = false;
                DoneWashDaysContainer.IsVisible = true;
                foreach (var d in mInstances[date])
                {
                    var def = mMainSessionController.GetWashingDayById(d.WashDayID);
                    var c = new WashingDayInstanceCalendarCell(d,def, App.BL);
                    c.Openclicked += C_Openclicked;
                    this.DoneWashDays.Children.Add(c.View);
                }
            }
        }

        private void C_Openclicked(object sender, WashingDayInstanceCalendarCell.WashingDayCellEventArgs e)
        {
            Navigation.PushAsync(new WashDayInstance(e.Definition, e.Instance));

        }

        private void WashingDayEdited(object sender, WashingDayCellEventArgs e)
        {
            Navigation.PushAsync(new WashDayEditor(e.Controller, false, App.BL));

        }
    }
}

﻿using HairAppBl.Controller;
using HairAppBl.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;
using HairApp.Resources;

namespace HairApp.Controls
{

    public class CareDayList : StackLayout
    {
        List<WashingDayDefinition> mWashingDays;
        HairAppBl.Interfaces.IHairBl mHairbl;
        AlarmController mAlarmController;
        StackLayout mWashDayList;
        WashingDayDefinitionHomeControl helpItem;
        MainSessionController mMainSessionController;
        ContentPage mPage;

        public CareDayList(MainSessionController mainSessionController, HairAppBl.Interfaces.IHairBl hairbl, AlarmController ac, ContentPage page)
        {
            mMainSessionController = mainSessionController;
            mWashingDays = mainSessionController.GetAllWashingDays();
            mHairbl = hairbl;
            mAlarmController = ac;
            mPage = page;

            mMainSessionController.DefinitionsEdited += MMainSessionController_DefinitionsEdited;

            mWashDayList = new StackLayout { Orientation = StackOrientation.Vertical , HorizontalOptions = LayoutOptions.FillAndExpand };

            Children.Add(mWashDayList);
            RefreshList();
        }

        private void MMainSessionController_DefinitionsEdited(object sender, EventArgs e)
        {
            RefreshList();
        }

        public void AddWashDay()
        {
            var def = new WashingDayDefinition();
            def.ID = Guid.NewGuid().ToString();
            var contr = new WashingDayEditorController(def, App.MainSession.GetAllDefinitions(), this.mAlarmController);
            var editor = new WashDayEditor(mMainSessionController, contr, true, mHairbl);
            Navigation.PushAsync(editor);
        }

        public void RefreshList()
        {
            this.mWashDayList.Children.Clear();

            var orderedList = mWashingDays.OrderBy(x => ((new ScheduleController(x.Scheduled)).Time2NextCareDay(ScheduleController.GetToday())));
            foreach (var r in orderedList)
            {
                var wdController = new WashingDayEditorController(r, App.MainSession.GetAllDefinitions(), mAlarmController);

                var c = new WashingDayDefinitionHomeControl(wdController, App.BL);
                c.Removed += WashDay_Removed;
                c.Edited += WashDay_Edited;
                c.StartCareDay += WashDay_StartCareDay;
                this.mWashDayList.Children.Add(c.View);

                helpItem = c;
            }
        }

        public void ShowHelp()
        {
            helpItem?.ShowHelp();
        }

        private void WashDay_StartCareDay(object sender, WashingDayDefinitionControl.WashingDayCellEventArgs e)
        {
            var wdInstance = e.Controller.GetWashingDayInstance(ScheduleController.GetToday());

            var instancePage = new WashDayInstance(e.Controller.GetModel(), wdInstance,mHairbl);
            Navigation.PushAsync(instancePage, true);
        }

        private void WashDay_Edited(object sender, WashingDayDefinitionControl.WashingDayCellEventArgs e)
        {
            var contr = new WashingDayEditorController(e.Controller.GetModel(), App.MainSession.GetAllDefinitions(), this.mAlarmController);
            var editor = new WashDayEditor(mMainSessionController, contr, false, mHairbl);
            Navigation.PushAsync(editor);
        }

        private async void WashDay_Removed(object sender, WashingDayDefinitionControl.WashingDayCellEventArgs e)
        {
            var answer = await mPage.DisplayAlert(AppResources.DeleteWashDay, AppResources.DeleteWashdayConfirmation, AppResources.Delete, AppResources.Cancel);
            if (answer)
            {
                mAlarmController.DeleteWashDay(e.Controller.GetModel().ID);
                mMainSessionController.DeleteWashDay(e.Controller.GetModel());
                RefreshList();
            }
        }



    }
}

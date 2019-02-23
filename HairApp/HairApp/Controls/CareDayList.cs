using HairAppBl.Controller;
using HairAppBl.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XLabs.Forms;

namespace HairApp.Controls
{

    public class CareDayList : StackLayout
    {
        List<WashingDayDefinition> mWashingDays;
        HairAppBl.Interfaces.IHairBl mHairbl;
        AlarmController mAlarmController;
        StackLayout mWashDayList;


        public CareDayList(List<WashingDayDefinition> washingDays, HairAppBl.Interfaces.IHairBl hairbl, AlarmController ac)
        {

            mWashingDays = washingDays;
            mHairbl = hairbl;
            mAlarmController = ac;

            //WashdayList
            mWashDayList = new StackLayout { Orientation = StackOrientation.Vertical , HorizontalOptions = LayoutOptions.FillAndExpand };

            this.Children.Add(mWashDayList);
            RefreshList();
        }

        public void AddWashDay()
        {
            var def = new WashingDayDefinition();
            def.ID = Guid.NewGuid().ToString();
            var contr = new WashingDayEditorController(def, App.MainSession.GetAllDefinitions(), this.mAlarmController);
            var editor = new WashDayEditor(contr, true, mHairbl);
            editor.OkClicked += Editor_OkClicked;
            Navigation.PushAsync(editor);

        }

        private void Editor_OkClicked(object sender, WashDayEditor.WashDayEditorEventArgs e)
        {
            if (e.Created)
                mWashingDays.Add(e.Definition);
            RefreshList();
        }

        public void RefreshList()
        {
            this.mWashDayList.Children.Clear();
            foreach (var r in this.mWashingDays)
            {
                var c = new WashingDayCell(r, App.BL);
                c.Removed += WashDay_Removed;
                c.Edited += WashDay_Edited;
                this.mWashDayList.Children.Add(c.View);
            }
        }

        private void WashDay_Edited(object sender, EventArgs e)
        {
            var item = ((WashingDayCell)sender);
            var contr = new WashingDayEditorController(item.WashingDayDefinition, App.MainSession.GetAllDefinitions(), this.mAlarmController);
            var editor = new WashDayEditor(contr, false, mHairbl);
            editor.OkClicked += Editor_OkClicked;
            Navigation.PushAsync(editor);
        }

        private void WashDay_Removed(object sender, EventArgs e)
        {
            var item = ((WashingDayCell)sender);
            mWashingDays.Remove(item.WashingDayDefinition);
            mAlarmController.DeleteWashDay(item.WashingDayDefinition.ID);
            RefreshList();
        }



    }
}

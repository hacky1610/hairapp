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

namespace HairApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WashDayOverview : ContentPage
	{
        List<WashingDayDefinition> mWashingDays;
        HairAppBl.Interfaces.IHairBl mHairbl;

        public WashDayOverview(List<WashingDayDefinition> washingDays, HairAppBl.Interfaces.IHairBl  hairbl)
		{
			InitializeComponent ();
            mWashingDays = washingDays;
            mHairbl = hairbl;       
            var washingDayDefinition = new WashingDayDefinition();
            RefreshList();
            AddWashday.Clicked += AddWashday_Clicked;
        }

        private void AddWashday_Clicked(object sender, EventArgs e)
        {
            var def = new WashingDayDefinition();
            def.ID = Guid.NewGuid().ToString();
            var editor = new WashDayEditor(def,true,mHairbl);
            editor.OkClicked += Editor_OkClicked;
            Navigation.PushAsync(editor);

        }

        private void Editor_OkClicked(object sender, WashDayEditor.WashDayEditorEventArgs e)
        {
            if (e.Created)
                mWashingDays.Add(e.Definition);
            RefreshList();
        }

        private void RefreshList()
        {
            this.Washdaylist.Children.Clear();
            foreach (var r in this.mWashingDays)
            {
                var c = new Controls.WashingDayCell(r,App.BL);
                c.Removed += WashDay_Removed;
                c.Edited += WashDay_Edited;
                this.Washdaylist.Children.Add(c.View);
            }
        }

        private void WashDay_Edited(object sender, EventArgs e)
        {
            var item = ((WashingDayCell)sender);
            var editor = new WashDayEditor(item.WashingDayDefinition, false,mHairbl);
            editor.OkClicked += Editor_OkClicked;
            Navigation.PushAsync(editor);
        }

        private void WashDay_Removed(object sender, EventArgs e)
        {
            var item = ((WashingDayCell)sender);
            mWashingDays.Remove(item.WashingDayDefinition);
            RefreshList();
        }
    }
}
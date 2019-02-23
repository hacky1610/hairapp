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

namespace HairApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WashDayOverview : ContentPage
	{
        List<WashingDayDefinition> mWashingDays;
        HairAppBl.Interfaces.IHairBl mHairbl;
	    AlarmController mAlarmController;

        public WashDayOverview(List<WashingDayDefinition> washingDays, HairAppBl.Interfaces.IHairBl  hairbl, AlarmController ac)
	    {
	        InitializeComponent ();
            mWashingDays = washingDays;
            mHairbl = hairbl;       
	         mAlarmController = ac;
            var washingDayDefinition = new WashingDayDefinition();
            RefreshList();
            AddWashday.Clicked += AddWashday_Clicked;

            var navi = new Controls.NavigationControl("Home", "");
            NavigationContainer.Content = navi.View;
            navi.LeftButton.Clicked += LeftButton_Clicked;
        }

        private void LeftButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void AddWashday_Clicked(object sender, EventArgs e)
        {
            var def = new WashingDayDefinition();
	        def.ID = Guid.NewGuid().ToString();
	        var contr = new WashingDayEditorController(def, App.MainSession.GetAllDefinitions(), this.mAlarmController);                           
            var editor = new WashDayEditor(contr,true,mHairbl);
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
	        var contr = new WashingDayEditorController(item.WashingDayDefinition, App.MainSession.GetAllDefinitions(), this.mAlarmController);               
            var editor = new WashDayEditor(contr, false,mHairbl);
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

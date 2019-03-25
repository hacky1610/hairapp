using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HairApp.Controls;
using HairAppBl.Controller;
using HairAppBl.Models;

namespace HairApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IntroPage : ContentPage
	{
        List<WashingDayDefinition> mWashingDays;
        HairAppBl.Interfaces.IHairBl mHairbl;
	    AlarmController mAlarmController;
        public event EventHandler<EventArgs> PageClosed;


        public IntroPage(List<WashingDayDefinition> washingDays, HairAppBl.Interfaces.IHairBl hairbl, AlarmController ac)
	    {
	        InitializeComponent ();
            mWashingDays = washingDays;
            mHairbl = hairbl;
            mAlarmController = ac;
            AddWashday.Clicked += AddWashday_Clicked;
        }

        private void AddWashday_Clicked(object sender, EventArgs e)
        {
            var def = new WashingDayDefinition();
            def.ID = Guid.NewGuid().ToString();
            var contr = new WashingDayEditorController(def, App.MainSession.GetAllDefinitions(), this.mAlarmController);
           // var editor = new WashDayEditor(contr, true, mHairbl);
            //Navigation.PushAsync(editor);

        }

        private void Editor_CancelClicked(object sender, EventArgs e)
        {
            App.MainSession.Initialized = true;
            PageClosed?.Invoke(this, new EventArgs());
            Navigation.PopAsync(true);
        }

        private void Editor_OkClicked(object sender, WashDayEditor.WashDayEditorEventArgs e)
        {
            /* mWashingDays.Add(e.Definition);
            App.MainSession.Initialized = true;
            PageClosed?.Invoke(this, new EventArgs());

            Navigation.PopAsync(true);*/
        }
    }
}

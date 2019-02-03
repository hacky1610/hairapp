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
	public partial class WashDayInstance : ContentPage
	{
        private WashingDayEditorController mWashingDayEditorController;
        private List<WashingDayEditorCell> mRoutineListControls = new List<WashingDayEditorCell>();
		public WashDayInstance(WashingDayDefinition def)
		{
			InitializeComponent ();
       
            var washingDayDefinition = def;
            this.mWashingDayEditorController = new WashingDayEditorController(washingDayDefinition,App.MainSession.GetAllDefinitions());
        }

        

        private void RefreshList()
        {
            this.RoutineList.Children.Clear();
            this.mRoutineListControls.Clear();
            foreach (var r in this.mWashingDayEditorController.GetRoutineDefinitions())
            {
                var c = new Controls.WashingDayEditorCell(r,App.BL);
                this.RoutineList.Children.Add(c.View);
                this.mRoutineListControls.Add(c);
            }
        }


    }
}
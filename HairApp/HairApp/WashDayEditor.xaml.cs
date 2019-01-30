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
	public partial class WashDayEditor : ContentPage
	{
        private WashingDayEditorController mWashingDayEditorController;
        private List<WashingDayEditorCell> mRoutineListControls = new List<WashingDayEditorCell>();
		public WashDayEditor ()
		{
			InitializeComponent ();
       
            var washingDayDefinition = new WashingDayDefinition();
            this.mWashingDayEditorController = new WashingDayEditorController(washingDayDefinition);
            RefreshList();
            this.AddRoutine.Clicked += AddRoutine_Clicked;
        }


        private void AddRoutine_Clicked(object sender, EventArgs e)
        {
            var diaog = new AddRoutineDialog(this.mWashingDayEditorController);
            diaog.Disappearing += Diaog_Disappearing;
            // Open a PopupPage
            Navigation.PushPopupAsync(diaog);

        }

        private void Diaog_Disappearing(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void RefreshList()
        {
            this.RoutineList.Children.Clear();
            this.mRoutineListControls.Clear();
            foreach (var r in this.mWashingDayEditorController.GetRoutineDefinitions())
            {
                var c = new Controls.WashingDayEditorCell(r,App.BL);
                c.Removed += Routine_Removed;
                c.MovedDown += Routine_MovedDown;
                c.MovedUp += Routine_MovedUp;
                this.RoutineList.Children.Add(c.View);
                this.mRoutineListControls.Add(c);
            }
        }

        private WashingDayEditorCell GetRoutineControl(RoutineDefinition routine)
        {
            foreach(var c in mRoutineListControls)
            {
                if (c.Routine == routine)
                    return c;
            }
            return null;
        }

        private void Routine_MovedDown(object sender, EventArgs e)
        {
            var item = ((WashingDayEditorCell)sender);
            this.mWashingDayEditorController.MoveDown(item.Routine);
            RefreshList();
            GetRoutineControl(item.Routine).Select();
        }

        private void Routine_MovedUp(object sender, EventArgs e)
        {
            var item = ((WashingDayEditorCell)sender);
            this.mWashingDayEditorController.MoveUp(item.Routine);
            RefreshList();
            GetRoutineControl(item.Routine).Select();

        }

        private void Routine_Removed(object sender, EventArgs e)
        {
            var item = ((WashingDayEditorCell)sender);
            this.mWashingDayEditorController.RemoveRoutine(item.Routine);
            RefreshList();
        }
    }
}
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
        private List<RoutineInstanceCell> mRoutineListControls = new List<RoutineInstanceCell>();
        private WashingDayInstance mInstance;
        private WashingDayDefinition mDefinition;
        public event EventHandler<WashDayInstanceEventArgs> OkClicked;

        public WashDayInstance(WashingDayDefinition definition, WashingDayInstance instance)
		{
			InitializeComponent ();
       
            mInstance =  instance;
            mDefinition = definition;

            var saveClose = new Controls.NavigationControl("Cancel", "Save");
            SaveButtonContainer.Content = saveClose.View;

            saveClose.RightButton.Clicked += OKButton_Clicked;
            saveClose.LeftButton.Clicked += CancelButton_Clicked;
            RefreshList();
        }

        private void OKButton_Clicked(object sender, EventArgs e)
        {
            mDefinition.Instances.Add(mInstance);
            Navigation.PopAsync();
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void RefreshList()
        {
            this.RoutineList.Children.Clear();
            this.mRoutineListControls.Clear();
            foreach (var r in mInstance.Routines)
            {
                var c = new Controls.RoutineInstanceCell(r,App.BL);
                this.RoutineList.Children.Add(c.View);
                this.mRoutineListControls.Add(c);
            }
        }

        public class WashDayInstanceEventArgs : EventArgs
        {
            public Boolean Created { get; set; }
            public WashingDayInstance Instance { get; set; }

            public WashDayInstanceEventArgs(WashingDayInstance instance, Boolean create)
            {
                this.Instance = instance;
                this.Created = create;
            }
        }


    }
}
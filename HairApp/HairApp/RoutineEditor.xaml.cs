using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HairApp.Controls;
using HairAppBl.Controller;
using HairAppBl.Models;
using static HairAppBl.Models.ScheduleDefinition;
using HairApp.Resources;

namespace HairApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RoutineEditor : ContentPage
	{
        private List<RoutineDefinitionEditCell> mRoutineListControls = new List<RoutineDefinitionEditCell>();
        private HairAppBl.Interfaces.IHairBl mHairbl;


        public RoutineEditor(MainSessionController mainSession, HairAppBl.Interfaces.IHairBl hairbl)
	    {
	        InitializeComponent ();


            mHairbl = hairbl;

            var saveClose = new Controls.NavigationControl(AppResources.Cancel, AppResources.Save);
            SaveButtonContainer.Content = saveClose.View;

            saveClose.RightButton.Clicked += OKButton_Clicked;
            saveClose.LeftButton.Clicked += CancelButton_Clicked;
	    
            RefreshList();
        }

        private void RefreshList()
        {
            this.RoutineList.Children.Clear();
            mRoutineListControls.Clear();
            foreach (var r in App.MainSession.GetAllDefinitions())
            {
                var c = new RoutineDefinitionEditCell(r, mHairbl);
                mRoutineListControls.Add(c);
                this.RoutineList.Children.Add(c.View);
            }
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();

        }

        private void OKButton_Clicked(object sender, EventArgs e)
        {
            foreach (var r in mRoutineListControls)
            {
                r.Save();
            }
            Navigation.PopAsync();
        }

  

      
    }
}

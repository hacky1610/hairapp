using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HairApp.Controls;
using HairAppBl.Controller;
using HairApp.Resources;

namespace HairApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RoutineEditor : ContentPage
	{
        private List<RoutineDefinitionEditCell> mRoutineListControls = new List<RoutineDefinitionEditCell>();
        private HairAppBl.Interfaces.IHairBl mHairbl;


        public RoutineEditor(MainSessionController mainSession, HairAppBl.Interfaces.IHairBl hairbl,HairAppBl.Models.RoutineDefinition select = null)
	    {
	        InitializeComponent ();


            mHairbl = hairbl;

            var saveClose = new NavigationControl(AppResources.Cancel, AppResources.Save,hairbl);
            SaveButtonContainer.Content = saveClose.View;

            saveClose.RightButton.Clicked += OKButton_Clicked;
            saveClose.LeftButton.Clicked += CancelButton_Clicked;
            mAddRoutineButton.Clicked += MAddRoutineButton_Clicked;
            RefreshList(select);

            //Ressources
            mHeading.Text = AppResources.RoutinEditorHeading;
        }

        private void MAddRoutineButton_Clicked(object sender, EventArgs e)
        {
            App.MainSession.GetAllDefinitions().Add(new HairAppBl.Models.RoutineDefinition());
            RefreshList();
        }

        private void RefreshList(HairAppBl.Models.RoutineDefinition select = null)
        {
            this.RoutineList.Children.Clear();
            mRoutineListControls.Clear();
            foreach (var r in App.MainSession.GetAllDefinitions())
            {
                var c = new RoutineDefinitionEditCell(r, mHairbl);
                c.Removed += Routine_Removed;
                c.Selected += C_Selected;
                mRoutineListControls.Add(c);
                this.RoutineList.Children.Add(c.View);
            }

        }

        private void DeselectAll()
        {
            foreach(RoutineDefinitionEditCell item in mRoutineListControls)
            {
                item.Deselect();
            }
        }

        private void C_Selected(object sender, EventArgs e)
        {
            DeselectAll();
            ((RoutineDefinitionEditCell)sender).Select();
        }

        private void Routine_Removed(object sender, EventArgs e)
        {
            App.MainSession.DeleteRoutine(((RoutineDefinitionEditCell)sender).Routine);
            RefreshList();
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

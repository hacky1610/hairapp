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
using XamForms.Controls;
using static HairApp.Controls.WashingDayDefinitionControl;
using SkiaSharp;

namespace HairApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HistoryPage : ContentPage
	{
        MainSessionController mMainSessionController;

        public HistoryPage(HairAppBl.Interfaces.IHairBl hairbl,MainSessionController controller)
		{
			InitializeComponent();

            mMainSessionController = controller;
            mMainSessionController.InstanceEdited += MMainSessionController_InstanceEdited;

            RefreshList();
          
        }

        private void MMainSessionController_InstanceEdited(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void RefreshList()
        {

            this.DoneWashDays.Children.Clear();

            foreach (var d in mMainSessionController.GetInstances())
            {
                var def = mMainSessionController.GetWashingDayById(d.WashDayID);
                var c = new WashingDayInstanceCalendarCell(d,def, App.BL);
                c.HeaderExtensionRight = new Label
                {
                    Text = d.Day.ToLongDateString()
                };
                c.Openclicked += C_Openclicked;
                c.ImageClicked += C_ImageClicked;
                this.DoneWashDays.Children.Add(c.View);
            }
            
        }

        private void C_ImageClicked(object sender, WashingDayInstanceCalendarCell.ImageClickedEventArgs e)
        {
            Navigation.PushAsync(new PicturePage(e.Source));
        }

        private void C_Openclicked(object sender, WashingDayInstanceCalendarCell.WashingDayCellEventArgs e)
        {
            Navigation.PushAsync(new WashDayInstance(e.Definition, e.Instance));

        }
       
    }
}

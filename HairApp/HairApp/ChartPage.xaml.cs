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
using static HairApp.Controls.WashingDayDefinitionControl;

namespace HairApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChartPage : ContentPage
	{
        MainSessionController mMainSessionController;
        HairAppBl.Interfaces.IHairBl mHairBl;
        List<HairLength> hairLengths = new List<HairLength>();

        public ChartPage(HairAppBl.Interfaces.IHairBl hairbl,MainSessionController controller)
		{
			InitializeComponent();

            mMainSessionController = controller;
            mHairBl = hairbl;

            mMainSessionController.InstanceEdited += MMainSessionController_InstanceEdited;

            AddHairLengthButton.Clicked += AddHairLengthButton_Clicked;


            RefreshList();
          
        }

        private void AddHairLengthButton_Clicked(object sender, EventArgs e)
        {
            var dialog = new AddHairLengthDialog(mHairBl);
            dialog.OkClicked += Dialog_OkClicked;
            Navigation.PushPopupAsync(dialog);
        }

        private void Dialog_OkClicked(object sender, AddHairLengthDialog.AddHairLengthDialogEventArgs e)
        {
            hairLengths.Add(e.HairLength);
            RefreshList();
        }

        private void MMainSessionController_InstanceEdited(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void RefreshList()
        {

       


            var controller = new HairChartController(hairLengths);


           ChartContainer.Content = new HairChartView(mHairBl, controller);


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

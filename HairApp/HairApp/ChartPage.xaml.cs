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
            Navigation.PushPopupAsync(new AddHairLengthDialog(mHairBl));
        }

        private void MMainSessionController_InstanceEdited(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void RefreshList()
        {
            var list = new List<HairLength>();
            list.Add(new HairLength("")
            {
                   Back = 30,
                   Front = 10,
                   Side = 5,
                   Day = DateTime.Now.AddDays(-122),
                   Picture = "/storage/emulated/0/DCIM/HairApp/0.jpg"
            });
            list.Add(new HairLength("")
            {
                Back = 35,
                Front = 15,
                Side = 6,
                Day = DateTime.Now.AddDays(-80),
                Picture = "/storage/emulated/0/DCIM/HairApp/1.jpg"

            });
            list.Add(new HairLength("")
            {
                Back = 36,
                Front = 10,
                Side = 8,
                Day = DateTime.Now.AddDays(-66),

            });
            list.Add(new HairLength("")
            {
                Back = 40,
                Front = 10,
                Side = 12,
                Day = DateTime.Now.AddDays(-43),
                Picture = "/storage/emulated/0/DCIM/HairApp/2.jpg"


            });
            list.Add(new HairLength("")
            {
                Back = 50,
                Front = 10,
                Side = 15,
                Day = DateTime.Now.AddDays(-20),
                Picture = "/storage/emulated/0/DCIM/HairApp/4.jpg"

            });
            list.Add(new HairLength("")
            {
                Back = 55,
                Front = 10,
                Side = 20,
                Day = DateTime.Now,
                Picture = "/storage/emulated/0/DCIM/HairApp/5.jpg"

            });


            var controller = new HairChartController(list);


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

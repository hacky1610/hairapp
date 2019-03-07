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
	public partial class HistoryPage : ContentPage
	{
        MainSessionController mMainSessionController;
        HairAppBl.Interfaces.IHairBl mHairBl;

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
            var instances = mMainSessionController.GetInstances();
            var orderedList = instances.OrderBy(x => x.Day);

            foreach (var d in orderedList)
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

            var list = new List<HairLength>();
            list.Add(new HairLength("")
            {
                   Back = 30,
                   Front = 10,
                   Side = 5,
                   Day = DateTime.Now.AddDays(-122)
            });
            list.Add(new HairLength("")
            {
                Back = 35,
                Front = 15,
                Side = 6,
                Day = DateTime.Now.AddDays(-80)
            });
            list.Add(new HairLength("")
            {
                Back = 36,
                Front = 10,
                Side = 8,
                Day = DateTime.Now.AddDays(-66)
            });
            list.Add(new HairLength("")
            {
                Back = 40,
                Front = 10,
                Side = 12,
                Day = DateTime.Now.AddDays(-43)
            });
            list.Add(new HairLength("")
            {
                Back = 50,
                Front = 10,
                Side = 15,
                Day = DateTime.Now.AddDays(-20)
            });
            list.Add(new HairLength("")
            {
                Back = 55,
                Front = 10,
                Side = 20,
                Day = DateTime.Now
            });


            var controller = new HairChartController(list);


            ChartContainer.Content = new HairChartView(mHairBl, controller.GetLengths());


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

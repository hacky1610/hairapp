using System;
using System.Linq;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HairApp.Controls;
using HairAppBl.Controller;
using HairApp.Resources;

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
            mHairBl = hairbl;

            mMainSessionController.InstanceEdited += MMainSessionController_InstanceEdited;

            RefreshList();

            //Resources
            mFinishedCareDaysLabel.Text = AppResources.FinishedCareDays;
          
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
        }

        private void C_ImageClicked(object sender, WashingDayInstanceCalendarCell.ImageClickedEventArgs e)
        {
            Navigation.PushAsync(new PicturePage(e.Source,mHairBl));
        }

        private void C_Openclicked(object sender, WashingDayInstanceCalendarCell.WashingDayCellEventArgs e)
        {
            Navigation.PushAsync(new WashDayInstance(e.Definition, e.Instance,mHairBl));

        }
       
    }
}

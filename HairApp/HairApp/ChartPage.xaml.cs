using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HairApp.Controls;
using HairAppBl.Controller;
using HairAppBl.Models;
using HairApp.Resources;

namespace HairApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChartPage : ContentPage
	{
        MainSessionController mMainSessionController;
        HairAppBl.Interfaces.IHairBl mHairBl;
        List<HairLength> mHairLengths;

        public ChartPage(HairAppBl.Interfaces.IHairBl hairbl,MainSessionController controller)
		{
			InitializeComponent();

            mMainSessionController = controller;
            mHairBl = hairbl;
            mHairLengths = mMainSessionController.GetHairLength();

            mMainSessionController.InstanceEdited += MMainSessionController_InstanceEdited;

            mAddHairLengthButton.Clicked += AddHairLengthButton_Clicked;

            //Resources
            mAddHairLengthButton.Text = AppResources.AddHairLength;
            mHairLengthStatisticLabel.Text = AppResources.HairlengthStatistic;

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
            mHairLengths.Add(e.HairLength);
            RefreshList();
        }

        private void MMainSessionController_InstanceEdited(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void RefreshList()
        {
            var controller = new HairChartController(mHairLengths);
            ChartContainer.Content = new HairChartView(mHairBl, controller);
        }
    }
}

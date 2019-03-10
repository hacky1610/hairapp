using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HairAppBl.Controller;
using Rg.Plugins.Popup.Extensions;

namespace HairApp
{
	public partial class AddHairLengthDialog : Rg.Plugins.Popup.Pages.PopupPage
    {
        private HairAppBl.Interfaces.IHairBl mHairbl;
             


        public AddHairLengthDialog(HairAppBl.Interfaces.IHairBl hairbl)
        {
            InitializeComponent();
            mHairbl = hairbl;

            hairLengthContainer.Content = new HairApp.Controls.HairLengthControl(hairbl);

        }

   


    }
}

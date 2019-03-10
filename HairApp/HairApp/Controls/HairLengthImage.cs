using HairAppBl.Controller;
using HairAppBl.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XLabs.Forms;
using System.Linq;

namespace HairApp.Controls
{

    public class HairLengthImage : Image
    {
        HairAppBl.Interfaces.IHairBl mHairbl;
        Entry mBackEntry;
        Entry mSideEntry;
        Entry mFrontEntry;
        public HairLength HairLength { get;private set; }

        public HairLengthImage(HairAppBl.Interfaces.IHairBl hairbl, HairLength hl)
        {
            mHairbl = hairbl;
            HairLength = hl;
            HeightRequest = 70;
        }

        public void Select()
        {
            HeightRequest = 100;
        }

        public void Deselect()
        {
            HeightRequest = 70;
        }
    }
}

﻿using HairAppBl.Controller;
using HairAppBl.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XLabs.Forms;
using System.Linq;

namespace HairApp.Controls
{

    public class HairLengthImage : ImageButton
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
            BackgroundColor = Color.Transparent;
        }

        public void Select()
        {
            var animation1 = new Animation(v => HeightRequest = v, 70, 100);
            animation1.Commit(this, "SimpleAnimation2", 16, 200, Easing.Linear, (v, c) => { }, () => false);

        }

        public void Deselect()
        {
            this.HeightRequest = 70;
        }
    }
}

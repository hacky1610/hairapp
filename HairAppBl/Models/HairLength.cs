﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HairAppBl.Models
{
    public class HairLength
    {
        public int  Back;
        public int  Side;
        public int  Front;
        public DateTime  Day;
        public readonly String ID;
        public String Picture;
        public event EventHandler<EventArgs> Selected;


        public HairLength(string id)
        {
        }

        public void SendSelected()
        {
            Selected?.Invoke(this, new EventArgs());
        }

    }

}

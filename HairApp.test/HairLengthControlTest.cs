using HairAppBl;
using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text;

namespace HairApp.Tests
{
    class HairLengthControlTest
    {
        HairAppBl.HairAppBl hbl;
        [SetUp]
        public void SetUp()
        {
            Xamarin.Forms.Mocks.MockForms.Init();
            var dic = new Dictionary<string, object>();
            hbl = new HairAppBl.HairAppBl(new ConsoleLogger(), dic);
        }

        [Test]
        public void SetPicture_ArgumentNull()
        {
          
            var hlc = new HairApp.Controls.HairLengthControl(hbl);
            hlc.SetPicture(null);
        }


    }
}

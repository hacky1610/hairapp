using NUnit.Framework;
using HairAppBl.Models;
using HairAppBl.Controller;
using System.Collections.Generic;

namespace HairAppBl.Tests
{
    public class ColorButtonTest
    {
        [SetUp]
        public void SetUp()
        {
            Xamarin.Forms.Mocks.MockForms.Init();
        }

        [Test]
        public void CheckColor()
        {
            var r = new HairApp.Controls.ColorButton(Xamarin.Forms.Color.Red);
            Assert.AreEqual(Xamarin.Forms.Color.Red, r.Color);
            Assert.AreEqual(Xamarin.Forms.Color.Red, r.BackgroundColor);
        }

       

    }
}

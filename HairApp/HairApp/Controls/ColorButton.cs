using HairAppBl.Models;
using System;
using Xamarin.Forms;

namespace HairApp.Controls
{

    public class ColorButton : Frame
    {
        public event EventHandler<EventArgs> Clicked;
        public Color Color { get; private set; }

        public ColorButton(Color color)
        {
            HeightRequest = 20;
            WidthRequest = 20;
            CornerRadius = 40;
            HorizontalOptions = new LayoutOptions(LayoutAlignment.Center, false);
            
            BackgroundColor = color;
            Color = color;
            Selected = false;

            var tb = new TapGestureRecognizer();
            tb.Tapped += Tb_Tapped;
            this.GestureRecognizers.Add(tb);
        }

        private void Tb_Tapped(object sender, EventArgs e)
        {
            Selected = !Selected;
            Clicked?.Invoke(this, new EventArgs());
        }

        public bool Selected{
            get ; private set; }

       
    }
}

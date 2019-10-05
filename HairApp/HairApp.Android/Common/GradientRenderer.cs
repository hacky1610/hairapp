using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using HairApp.Controls;
using HairApp.Droid.Common;
using Android.Content;

[assembly: ExportRenderer(typeof(GradientColorStack), typeof(GradientRenderer))]

namespace HairApp.Droid.Common
{
    public class GradientRenderer : VisualElementRenderer<StackLayout>
    {
        private String StartColor { get; set; }
        private String EndColor { get; set; }


        public GradientRenderer(Context c):base(c)
        {

        }

        protected override void DispatchDraw(global::Android.Graphics.Canvas canvas)
        {
            #region for Vertical Gradient
            var gradient = new Android.Graphics.LinearGradient(0, 0, 0, Height,
            #endregion

            #region for Horizontal Gradient
            //var gradient = new Android.Graphics.LinearGradient(0, 0, Width, 0,
            #endregion

                   Color.FromHex(StartColor).ToAndroid(),
                   Color.FromHex(EndColor).ToAndroid(),
                   Android.Graphics.Shader.TileMode.Mirror);

            var paint = new Android.Graphics.Paint()
            {
                Dither = true,
            };
            paint.SetShader(gradient);
            canvas.DrawPaint(paint);
            base.DispatchDraw(canvas);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }
            try
            {
                var stack = e.NewElement as GradientColorStack;
                this.StartColor = stack.StartColor;
                this.EndColor = stack.EndColor;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"ERROR:", ex.Message);
            }
        }
    }
}
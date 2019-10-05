using Android.Content;
using Android.Support.Design.BottomAppBar;
using Android.Support.Design.Internal;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using HairApp.Controls;
using HairApp.Droid.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using RelativeLayout = Android.Widget.RelativeLayout;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(CustomTabBar), typeof(CustomBottomBarRenderer))]
namespace HairApp.Droid.Controls
{
    public class CustomBottomBarRenderer : TabbedPageRenderer
    {

        public CustomBottomBarRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);
            var bottomNavigationView = (GetChildAt(0) as RelativeLayout).GetChildAt(1) as BottomNavigationView;
            bottomNavigationView.SetBackgroundColor(Android.Graphics.Color.ParseColor("#ffffff"));
            bottomNavigationView.SetClipChildren(true);
            bottomNavigationView.SetPadding(10, 10, 10, 10);
        }
    }
}

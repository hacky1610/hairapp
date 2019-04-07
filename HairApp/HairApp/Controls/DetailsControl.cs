using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace HairApp.Controls
{
    /// <summary>
    /// For custom renderer on Android (only)
    /// </summary>

    public class DetailsControl : ViewCell
    {
        private Label mLabelText;
        private HairAppBl.Interfaces.IHairBl mHairBl;
        private StackLayout mDetailsFrame;
        private Frame mColorFrame;
        private ContentView mHeaderLeft;
        private ContentView mHeaderRight;


        protected Color Color
        {
            get
            {
                return mColorFrame.BackgroundColor;
            }
            set
            {
                mColorFrame.BackgroundColor = value;
            }
        }
        protected string HeaderName {
            get
            {
                return mLabelText.Text;
            }
            set
            {
                mLabelText.Text = value;
            }
        }

        protected View HeaderExtensionLeft
        {
            get
            {
                return mHeaderLeft.Content;
            }
            set
            {
                mHeaderLeft.Content = value;
            }
        }

        public View HeaderExtensionRight
        {
            get
            {
                return mHeaderRight.Content;
            }
            set
            {
                mHeaderRight.Content = value;
            }
        }

        protected IList<View> DetailsContent
        {
            get
            {
                return mDetailsFrame.Children;
            }
           
   
        }

        public DetailsControl(HairAppBl.Interfaces.IHairBl hairbl)
        {
            this.mHairBl = hairbl;

            //Header
            mColorFrame = new Frame
            {
                CornerRadius = 4,
                Padding = new Thickness(0, 0, 0, 0),
                HeightRequest = 10,
                WidthRequest = 15
             
            };

            mLabelText = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };
            mHeaderLeft = new ContentView
            {
                HorizontalOptions = LayoutOptions.Start
            };
            mHeaderRight = new ContentView
            {
                HorizontalOptions = LayoutOptions.EndAndExpand
            };


            //var moreInfoButton = Common.GetButton("info.png",hairbl);
            //moreInfoButton.Clicked +=  (sender, e) =>
            //{
            //    if (!mDetailsFrame.IsVisible)
            //        ShowDetailsAnimation();
            //    else
            //        HideDetailsAnimation();
            //};


            mDetailsFrame = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Style = (Style)hairbl.Resources["DetailsFrame"],
                IsVisible = false,
                Opacity = 0
            };


            var frame = new Frame
            {
                Style = (Style)hairbl.Resources["RoutineFrame"],
                Content = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Children =
                    {
                        new StackLayout
                        {
                            Style = (Style)hairbl.Resources["RoutineContent"],
                            Orientation = StackOrientation.Horizontal,

                            Children = {mColorFrame, mLabelText,mHeaderLeft,mHeaderRight }
                        },
                        mDetailsFrame
                        
                    }
                }
            };
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                if (!mDetailsFrame.IsVisible)
                    ShowDetailsAnimation();
                else
                    HideDetailsAnimation();
                
            };
            frame.GestureRecognizers.Add(tapGestureRecognizer);

            View = frame;
                
          
        }

        private async void ShowDetailsAnimation()
        {
            mDetailsFrame.IsVisible = true;
            await Task.WhenAll(mDetailsFrame.FadeTo(1, 400), mDetailsFrame.RotateXTo(360, 500));

        }

        private async void HideDetailsAnimation()
        {
            await Task.WhenAll(mDetailsFrame.FadeTo(0, 400), mDetailsFrame.RotateXTo(0, 500));
            mDetailsFrame.IsVisible = false;

        }

    }

}

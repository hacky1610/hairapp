using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HairApp.Controls
{
    /// <summary>
    /// For custom renderer on Android (only)
    /// </summary>

    class AddRoutineCell : ViewCell
    {
        RoutineCellObject cellObject;
        public AddRoutineCell()
        {
            var label1 = new Label
            {
                Text = "Label 1",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };
            label1.SetBinding(Label.TextProperty, new Binding("Name"));

            Tapped += AddRoutineCell_Tapped;

            View = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Padding = new Thickness(15, 5, 5, 15),
                Children = {
                    new StackLayout {
                        Orientation = StackOrientation.Vertical,
                        Children = { label1 }
                    },
                }
            };
        }

        private void AddRoutineCell_Tapped(object sender, EventArgs e)
        {
            this.cellObject.Select();
            
        }

        protected override void OnBindingContextChanged()
        {
            cellObject = (RoutineCellObject)this.BindingContext;
            base.OnBindingContextChanged();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XLabs.Forms;

namespace HairApp.Controls
{
    /// <summary>
    /// For custom renderer on Android (only)
    /// </summary>

    class AddRoutineCell : ViewCell
    {
        RoutineCellObject cellObject;
        XLabs.Forms.Controls.CheckBox mCheckBox;
        public Boolean Checked
        {
            get
            {
                return mCheckBox.Checked;
            }
        }

        public AddRoutineCell()
        {
            mCheckBox = new XLabs.Forms.Controls.CheckBox();
            mCheckBox.CheckedChanged += MCheckBox_CheckedChanged;

            var label1 = new Label
            {
                Text = "Label 1",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };
            label1.SetBinding(Label.TextProperty, new Binding("Name"));

            View = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Padding = new Thickness(15, 5, 5, 15),
                Children = {
                    new StackLayout {
                        Orientation = StackOrientation.Horizontal,
                        Children = { mCheckBox, label1 }
                    },
                }
            };
        }

        private void MCheckBox_CheckedChanged(object sender, XLabs.EventArgs<bool> e)
        {
            cellObject.Checked = e.Value;
        }

        protected override void OnBindingContextChanged()
        {
            cellObject = (RoutineCellObject)this.BindingContext;
            base.OnBindingContextChanged();
        }
    }
}

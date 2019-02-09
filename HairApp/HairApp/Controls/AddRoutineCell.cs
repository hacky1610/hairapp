using HairAppBl.Models;
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

    public class AddRoutineCell : ViewCell
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

        public AddRoutineCell(RoutineCellObject rcObject, HairAppBl.Interfaces.IHairBl hairbl)
        {
            cellObject = rcObject;
            
            mCheckBox = new XLabs.Forms.Controls.CheckBox();
            mCheckBox.CheckedChanged += MCheckBox_CheckedChanged;

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                mCheckBox.Checked = !mCheckBox.Checked;
            };

            var label1 = new Label
            {
                Text = rcObject.Name,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };

                var frame = new Frame
                {
                    Style = (Style)hairbl.Resources["RoutineFrame"],
                    Content = new StackLayout
                    {
                        Style = (Style)hairbl.Resources["RoutineContent"],
                        Orientation = StackOrientation.Horizontal,

                        Children = { mCheckBox,label1 }
                    }
                 };
            frame.GestureRecognizers.Add(tapGestureRecognizer);
            View = frame;

        }

        private void MCheckBox_CheckedChanged(object sender, XLabs.EventArgs<bool> e)
        {
            cellObject.Checked = e.Value;
        }

    }
}

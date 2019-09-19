using HairApp.Dialogs;
using System;
using Xamarin.Forms;

namespace HairApp.Controls
{

    public class AddRoutineCell : ViewCell
    {
        #region Members
        RoutineCellObject cellObject;
        XLabs.Forms.Controls.CheckBox mCheckBox;
        #endregion

        #region Properties
        public Boolean Checked
        {
            get
            {
                return mCheckBox.Checked;
            }
        }
        #endregion

        #region Constructor
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
                    Style = (Style)hairbl.Resources["RoutineFrameSelect"],
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
        #endregion

        #region Functions
        private void MCheckBox_CheckedChanged(object sender, XLabs.EventArgs<bool> e)
        {
            cellObject.Checked = e.Value;
        }
        #endregion
    }
}

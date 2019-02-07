using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using HairAppBl.Models;

namespace HairApp.Controls
{
    /// <summary>
    /// For custom renderer on Android (only)
    /// </summary>

    public class RoutineInstanceCell : ViewCell
    {
        private Label text;
        private XLabs.Forms.Controls.CheckBox mCheckBox;
        private HairAppBl.Interfaces.IHairBl mHairBl;
        private RoutineInstance mRoutine;

        public RoutineInstanceCell(RoutineInstance instance, HairAppBl.Interfaces.IHairBl hairbl)
        {
            this.mHairBl = hairbl;
            mRoutine = instance;

            mCheckBox = new XLabs.Forms.Controls.CheckBox();
            mCheckBox.CheckedChanged += MCheckBox_CheckedChanged;

            text = new Label
            {
                Text = instance.Name,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                Select();
            };

            var frame = new Frame
            {
                Style = (Style)hairbl.Resources["RoutineFrame"],
                Content = new StackLayout
                {
                    Style = (Style)hairbl.Resources["RoutineContent"],
                    Orientation = StackOrientation.Horizontal,

                    Children = {
                    new StackLayout {
                        Orientation = StackOrientation.Vertical,
                        Children = { text }
                    },

                }
                }
            };
            frame.GestureRecognizers.Add(tapGestureRecognizer);
            View = frame;


        }

        private void MCheckBox_CheckedChanged(object sender, XLabs.EventArgs<bool> e)
        {
            mRoutine.Checked = e.Value;
        }

        private ImageButton GetButton(string image)
        {
            return new ImageButton
            {
                Style = (Style)mHairBl.Resources["RoutineButton"],
                HorizontalOptions = LayoutOptions.EndAndExpand,
                Source = image

            };
        }



        public void Select()
        {

        }

        public void Deselect()
        {
        }


    }

}

using System;
using Xamarin.Forms;
using HairAppBl.Models;
using HairApp.Resources;

namespace HairApp.Controls
{
    /// <summary>
    /// For custom renderer on Android (only)
    /// </summary>

    public class RoutineInstanceCell : ViewCell
    {
        private XLabs.Forms.Controls.CheckBox mCheckBox;
        private HairAppBl.Interfaces.IHairBl mHairBl;
        private RoutineInstance mRoutine;
        private Editor mComment;
        private StackLayout mDetailsFrame;

        public RoutineInstanceCell(RoutineInstance instance, HairAppBl.Interfaces.IHairBl hairbl)
        {
            mRoutine = instance;
            mHairBl = hairbl;

            mCheckBox = new XLabs.Forms.Controls.CheckBox();
            mCheckBox.CheckedChanged += MCheckBox_CheckedChanged;
            mCheckBox.Checked = instance.Checked;

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                ShowMore();
            };

            var commentButton = Common.GetButton("comment.png",hairbl);
            commentButton.Clicked += (sender, e) =>
            {
                mDetailsFrame.IsVisible = !mDetailsFrame.IsVisible;
            };

            var nameLabel = new Label
            {
                Text = instance.Name,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };

            var descriptionLabel = new Label
            {
                Text = instance.Description,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                IsVisible = !String.IsNullOrWhiteSpace(instance.Description)
            };


            mComment = new Editor
            {
                HeightRequest= 120,
                Placeholder = AppResources.AddComment,
                Text = mRoutine.Comment
            };
            mComment.TextChanged += MComment_TextChanged;

            mDetailsFrame = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Style = (Style)hairbl.Resources["DetailsFrame"],
                IsVisible = false,
                Children = { descriptionLabel, mComment }
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

                             Children = { mCheckBox, nameLabel, commentButton}
                        },
                        mDetailsFrame
                    }
                 
                }
            };
            frame.GestureRecognizers.Add(tapGestureRecognizer);
            View = frame;
        }

        private void MComment_TextChanged(object sender, TextChangedEventArgs e)
        {
            mRoutine.Comment = e.NewTextValue;
        }

        private void ShowMore()
        {

        }

        private void MCheckBox_CheckedChanged(object sender, XLabs.EventArgs<bool> e)
        {
            mRoutine.Checked = e.Value;
        }

        public void Select()
        {

        }

        public void Deselect()
        {
        }


    }

}

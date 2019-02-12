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
    public class ListButton : Button { }

    public class WashingDayEditorCell : ViewCell
    {
        ImageButton editButton;
        ImageButton deleteButton;
        ImageButton upButton;
        ImageButton downButton;
        StackLayout buttonGroup;
        Label text;
        
        public event EventHandler<EventArgs> Removed;
        public event EventHandler<EventArgs> MovedUp;
        public event EventHandler<EventArgs> MovedDown;
        private HairAppBl.Interfaces.IHairBl mHairBl;

        public RoutineDefinition Routine { get; set; }

        public WashingDayEditorCell(RoutineDefinition routine, HairAppBl.Interfaces.IHairBl hairbl)
        {
            this.mHairBl = hairbl;
            this.Routine = routine;
            text = new Label
            {
                Text = Routine.Name,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                Select();
            };


            editButton = GetButton("edit.png");
            editButton.Clicked += (sender, e) =>
            {
                var b = (Button)sender;
            };

            deleteButton = GetButton("remove.png");
            deleteButton.Clicked += (sender, e) =>
            {
                SendRemoved();
            };

            downButton = GetButton("down.png");
            downButton.Clicked += (sender, e) =>
            {
                SendMoveDown();

            };

            upButton = GetButton("up.png");
            upButton.Clicked += (sender, e) =>
            {
                SendMoveUp();
            };

            buttonGroup = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.EndAndExpand,
                IsVisible = false,
                Children = { downButton,upButton,editButton, deleteButton }
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
                    buttonGroup
                    
                }
                }
            };
            frame.GestureRecognizers.Add(tapGestureRecognizer);
            View = frame;
                
              
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
            this.buttonGroup.IsVisible = true;

        }

        public void Deselect()
        {
            this.buttonGroup.IsVisible = false;
        }

        private void SendRemoved()
        {
            if (Removed != null)
                Removed(this,new EventArgs());
        }

        private void SendMoveUp()
        {
            if (MovedUp != null)
                MovedUp(this, new EventArgs());
        }

        private void SendMoveDown()
        {
            if (MovedDown != null)
                MovedDown(this, new EventArgs());
        }
    }

}

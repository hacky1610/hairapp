using System;
using Xamarin.Forms;
using HairAppBl.Models;

namespace HairApp.Controls
{
    /// <summary>
    /// For custom renderer on Android (only)
    /// </summary>
    public class ListButton : Button { }

    public class RoutineDefinitionCell : ViewCell
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
        public event EventHandler<EventArgs> Selected;
        public event EventHandler<EventArgs> Edited;
        private HairAppBl.Interfaces.IHairBl mHairBl;

        public RoutineDefinition Routine { get; set; }

        public RoutineDefinitionCell(RoutineDefinition routine, HairAppBl.Interfaces.IHairBl hairbl, int number)
        {
            this.mHairBl = hairbl;
            this.Routine = routine;
            text = new Label
            {
                Text = $"{number}) {Routine.Name}",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                Select();
            };


            editButton = Common.GetButton("edit.png", hairbl);
            editButton.Clicked += (sender, e) =>
            {
                SendEdit();
            };

            deleteButton = Common.GetButton("remove.png", hairbl);
            deleteButton.Clicked += (sender, e) =>
            {
                SendRemoved();
            };

            downButton = Common.GetButton("down.png", hairbl);
            downButton.Clicked += (sender, e) =>
            {
                SendMoveDown();

            };

            upButton = Common.GetButton("up.png",hairbl);
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

        public void Select()
        {
            Selected?.Invoke(this, new EventArgs());
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

        private void SendEdit()
        {
            Edited.Invoke(this, new EventArgs());
        }

        private void SendMoveDown()
        {
            if (MovedDown != null)
                MovedDown(this, new EventArgs());
        }
    }

}

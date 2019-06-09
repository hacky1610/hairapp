using System;
using Xamarin.Forms;
using HairAppBl.Models;

namespace HairApp.Controls
{
    /// <summary>
    /// For custom renderer on Android (only)
    /// </summary>

    public class RoutineDefinitionEditCell : ViewCell
    {
        ImageButton deleteButton;
        StackLayout buttonGroup;
        Entry title;
        Entry description;

        public event EventHandler<EventArgs> Removed;
        public event EventHandler<EventArgs> Selected;
        private HairAppBl.Interfaces.IHairBl mHairBl;

        public RoutineDefinition Routine { get; set; }

        public RoutineDefinitionEditCell(RoutineDefinition routine, HairAppBl.Interfaces.IHairBl hairbl)
        {
            this.mHairBl = hairbl;
            this.Routine = routine;
            var labelTitle = new Label
            {
                Text = Resources.AppResources.Title
            };
            var labelDescription = new Label
            {
                Text = Resources.AppResources.Description
            };

            title = new Entry
            {
                Text = Routine.Name,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };
            description = new Entry
            {
                Text = Routine.Description,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };


            deleteButton = Common.GetButton("remove.png", hairbl);
            deleteButton.Clicked += (sender, e) =>
            {
                SendRemoved();
            };

            buttonGroup = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.EndAndExpand,
                IsVisible = false,
                Children = {  deleteButton }
            };

            var frame = new Frame
            {
                Style = (Style)hairbl.Resources["RoutineFrame"],
                Content = new StackLayout
                {
                    Style = (Style)hairbl.Resources["RoutineContent"],
                    Orientation = StackOrientation.Vertical,

                    Children = {
                        labelTitle,
                    new StackLayout {
                        Orientation = StackOrientation.Horizontal,
                        Children = { title, buttonGroup }
                    },
                    labelDescription,
                    description
                    
                    
                }
                }
            };
            var editClicked = new TapGestureRecognizer();
            editClicked.Tapped += EditClicked_Tapped; ;

            frame.GestureRecognizers.Add(editClicked);
            View = frame;
        }

        private void EditClicked_Tapped(object sender, EventArgs e)
        {
            Selected?.Invoke(this, new EventArgs());
        }

        public void Save()
        {
            Routine.Name = title.Text;
            Routine.Description = description.Text;
        }

        public void Select()
        {
            deleteButton.IsVisible = true;
        }

        public void Deselect()
        {
            deleteButton.IsVisible = false;
        }


        private void SendRemoved()
        {
            if (Removed != null)
                Removed(this,new EventArgs());
        }

    }

}

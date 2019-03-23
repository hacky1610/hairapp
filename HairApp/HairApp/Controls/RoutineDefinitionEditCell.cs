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

    public class RoutineDefinitionEditCell : ViewCell
    {
        ImageButton deleteButton;
        StackLayout buttonGroup;
        Entry title;
        Entry description;

        public event EventHandler<EventArgs> Removed;
        private HairAppBl.Interfaces.IHairBl mHairBl;

        public RoutineDefinition Routine { get; set; }

        public RoutineDefinitionEditCell(RoutineDefinition routine, HairAppBl.Interfaces.IHairBl hairbl)
        {
            this.mHairBl = hairbl;
            this.Routine = routine;
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
                    new StackLayout {
                        Orientation = StackOrientation.Horizontal,
                        Children = { title, buttonGroup }
                    },
                    description
                    
                    
                }
                }
            };
            View = frame;
        }

        public void Save()
        {
            Routine.Name = title.Text;
            Routine.Description = description.Text;
        }


        private void SendRemoved()
        {
            if (Removed != null)
                Removed(this,new EventArgs());
        }

    }

}

using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using HairAppBl.Models;
using HairAppBl.Controller;
using static HairApp.WashDayEditor;

namespace HairApp.Controls
{
    /// <summary>
    /// For custom renderer on Android (only)
    /// </summary>

    public class WashingDayDefinitionControl : DetailsControl
    {
        public WashingDayEditorController WdController { get; set; }
        public event EventHandler<WashingDayCellEventArgs> Edited;
        public event EventHandler<WashingDayCellEventArgs> Removed;


        public WashingDayDefinitionControl(WashingDayEditorController controller, HairAppBl.Interfaces.IHairBl hairbl):base(hairbl)
        {
            this.WdController = controller;
            var def = WdController.GetModel();
            HeaderName = def.Name;
            Color = def.ItemColor;

            //HeaderExtensionLeft = new Label { Text = "Foo" };

            var descriptionLabel = Common.GetCalendarDetailsRow("description.png", new Label
            {
                Text = def.Description,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                IsVisible = !String.IsNullOrWhiteSpace(def.Description)
            },hairbl);
            descriptionLabel.IsVisible = !String.IsNullOrWhiteSpace(def.Description);

            var scheduleLabel = Common.GetCalendarDetailsRow("schedule.png", new Label
            {
                Text = WdController.GetSchedule(),
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
            }, hairbl);

            var editButton = new Button
            {
                Text = "edit",
                BackgroundColor = Color.Transparent,
                TextColor = Color.Blue
            };
            editButton.Clicked += EditButton_Clicked;

            var routineList = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
            };

            var listContainer = Common.GetCalendarDetailsRow("list.png",routineList, hairbl);

            foreach (var r in def.Routines)
            {
                routineList.Children.Add(new Label { Text = WdController.GetRoutineById(r).Name });
            }

            DetailsContent.Add(descriptionLabel);
            DetailsContent.Add(scheduleLabel);
            DetailsContent.Add(listContainer);
            DetailsContent.Add(editButton);
        }
      
        private void EditButton_Clicked(object sender, EventArgs e)
        {
            Edited(this, new WashingDayCellEventArgs(WdController));
        }

        public class WashingDayCellEventArgs : EventArgs
        {
            public readonly WashingDayEditorController Controller;

            public WashingDayCellEventArgs(WashingDayEditorController controller)
            {
                this.Controller = controller;
            }
        }




    }

}

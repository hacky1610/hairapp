using System;
using Xamarin.Forms;
using HairAppBl.Controller;
using HairApp.Resources;

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


            var editLabel = new Label
            {
                Text = AppResources.Edit,
                Style = (Style)hairbl.Resources["DetailsActionLabel"]
            };

            var editControl = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new Image
                    {
                        Source = "edit.png",
                         Style = (Style)hairbl.Resources["DetailsActionImage"]
                    },
                    editLabel
                }
            };
            var editClicked = new TapGestureRecognizer();
            editClicked.Tapped += EditButton_Clicked;

            editControl.GestureRecognizers.Add(editClicked);

            var deleteLabel = new Label
            {
                Text = AppResources.Remove,
                Style = (Style)hairbl.Resources["DetailsActionLabel"]
            };

            var deleteControl = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new Image
                    {
                        Source = "remove.png",
                         Style = (Style)hairbl.Resources["DetailsActionImage"]
                    },
                    deleteLabel
                }
            };
            var deleteClicked = new TapGestureRecognizer();
            deleteClicked.Tapped += DeleteButton_Clicked;

            deleteControl.GestureRecognizers.Add(deleteClicked);

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
            DetailsContent.Add(new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Center,
                Spacing = 30,
                Children = {
                    editControl,deleteControl }
            });
        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            Removed(this, new WashingDayCellEventArgs(WdController));
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

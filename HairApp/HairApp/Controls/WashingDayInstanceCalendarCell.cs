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

    public class WashingDayInstanceCalendarCell : DetailsControl
    {
        private readonly WashingDayInstance Instance;
        private readonly WashingDayDefinition Definition;
        public event EventHandler<WashingDayCellEventArgs> Openclicked;

        public WashingDayInstanceCalendarCell(WashingDayInstance instance,WashingDayDefinition def, HairAppBl.Interfaces.IHairBl hairbl):base(hairbl)
        {
            this.Instance = instance;
            this.Definition = def;
            this.HeaderName = Definition.Name;

            var commentLabel = Common.GetCalendarDetailsRow("comment.png",new Label
            {
                Text = instance.Comment,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
            },hairbl);
            commentLabel.IsVisible = !String.IsNullOrWhiteSpace(instance.Comment);

            var picContainer = new ScrollView { Orientation = ScrollOrientation.Horizontal };
            var picListView = new StackLayout { Orientation = StackOrientation.Vertical };
            picContainer.Content = picListView;

            foreach (var pic in instance.Pictures)
                picListView.Children.Add(new Image { HeightRequest = 60, Source = ImageSource.FromFile(pic.Path) });

            var pictureList = Common.GetCalendarDetailsRow("camera.png", picContainer, hairbl);
            pictureList.IsVisible = instance.Pictures.Count > 0;

            var showMore = new Button
            {
                Text = "show more",
                BackgroundColor = Color.Transparent,
                TextColor = Color.Blue
            };
            showMore.Clicked += ShowMoreButton_Clicked;

            var routineList = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
            };

            foreach (var r in instance.Routines)
            {
                var check = new XLabs.Forms.Controls.CheckBox();
                check.Checked = r.Checked;
                check.IsEnabled = false;
                var row = new StackLayout { Orientation = StackOrientation.Horizontal };
                row.Children.Add(check);
                row.Children.Add(new Label { Text = r.Name });
                routineList.Children.Add(row);
            }

            var routineFrame = Common.GetCalendarDetailsRow("list.png", routineList, hairbl);
            var neededTime = Common.GetCalendarDetailsRow("time.png", new Label { Text = $"{instance.NeededTime.TotalMinutes} minutes"}, hairbl);

            DetailsContent.Add(commentLabel);
            DetailsContent.Add(routineFrame);
            DetailsContent.Add(neededTime);
            DetailsContent.Add(pictureList);
            DetailsContent.Add(showMore);
        }

        private void ShowMoreButton_Clicked(object sender, EventArgs e)
        {
            Openclicked(this, new WashingDayCellEventArgs(Instance, Definition));
        }

        public class WashingDayCellEventArgs : EventArgs
        {
            public readonly WashingDayInstance Instance;
            public readonly WashingDayDefinition Definition;

            public WashingDayCellEventArgs(WashingDayInstance instance, WashingDayDefinition definition )
            {
                this.Definition = definition;
                this.Instance = instance;
            }
        }




    }

}

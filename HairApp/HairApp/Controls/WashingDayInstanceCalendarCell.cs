using System;
using Xamarin.Forms;
using HairAppBl.Models;

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
        public event EventHandler<ImageClickedEventArgs> ImageClicked;

        public WashingDayInstanceCalendarCell(WashingDayInstance instance,WashingDayDefinition def, HairAppBl.Interfaces.IHairBl hairbl):base(hairbl)
        {
            this.Instance = instance;
            this.Definition = def;
            this.HeaderName = Definition.Name;
            Color = Definition.ItemColor;

            var commentLabel = Common.GetCalendarDetailsRow("comment.png",new Label
            {
                Text = instance.Comment,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
            },hairbl);
            commentLabel.IsVisible = !String.IsNullOrWhiteSpace(instance.Comment);

            var picContainer = new ScrollView { Orientation = ScrollOrientation.Horizontal };
            var picListView = new StackLayout { Orientation = StackOrientation.Horizontal };
            picContainer.Content = picListView;

            foreach (var pic in instance.Pictures)
            {
                var img = new ImageButton { HeightRequest = 60, Source = ImageSource.FromFile(pic.Path), BackgroundColor = Color.Transparent };
                img.Clicked += Img_Clicked;
                picListView.Children.Add(img);
            }
          
            var pictureList = Common.GetCalendarDetailsRow("camera.png", picContainer, hairbl);
            pictureList.IsVisible = instance.Pictures.Count > 0;

            var showMore = new Button
            {
                Text = Resources.AppResources.ShowMore,
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

        private void Img_Clicked(object sender, EventArgs e)
        {
            var img = (ImageButton)sender;
            ImageClicked?.Invoke(this, new ImageClickedEventArgs(img.Source));
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

        public class ImageClickedEventArgs : EventArgs
        {
            public readonly ImageSource Source;

            public ImageClickedEventArgs(ImageSource source)
            {
                this.Source = source;
            }
        }




    }

}

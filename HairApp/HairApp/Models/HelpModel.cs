namespace HairApp.Models
{
    public class HelpModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tooltip { get; set; }
        public Xamarin.Forms.View View{ get; set; }
        public HelpModel(string name, string description, string tooltip, Xamarin.Forms.View v)
        {
            Name = name;
            Description = description;
            Tooltip = tooltip;
            View = v;
        }
    }
}

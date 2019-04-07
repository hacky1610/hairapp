using System.Collections.Generic;
using Xamarin.Forms;

namespace HairApp.Controller
{
    public class HelpController
    {
        private List<Models.HelpModel> mHelpList = new List<Models.HelpModel>();

        public HelpController()
        {
        }

        public void Add(string name,string description, string tooltip, View v)
        {
            var h = new Models.HelpModel(name, description, tooltip, v);
            mHelpList.Add(h);
        }

        public Models.HelpModel GetHelp(int id)
        {
           return mHelpList[0];
        }

        public void SHow(int id)
        {
            var item = mHelpList[id];
            var tt = new ToolTipController(item.View, item.Tooltip);
            tt.Show();
        }


    }
}

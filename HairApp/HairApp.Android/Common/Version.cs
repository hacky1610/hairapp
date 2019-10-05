using Xamarin.Forms;
using HairApp.Interfaces;
using Android.Content.PM;

[assembly: Dependency(typeof(HairApp.Droid.Common.Version))]
namespace HairApp.Droid.Common
{
    public class Version : IVersion
    {
        private PackageInfo GetPackageManager()
        {
            var context = global::Android.App.Application.Context;
            PackageManager manager = context.PackageManager;
            return manager.GetPackageInfo(context.PackageName, 0);
        }

        public string GetVersion()
        {
            return GetPackageManager().VersionName;
        }

        public int GetBuild()
        {
            return GetPackageManager().VersionCode;
        }

    }
}


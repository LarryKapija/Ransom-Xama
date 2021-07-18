using Android.Content;
using System;
using System.Threading.Tasks;
using Application = Android.App.Application;

namespace RansomApp.Droid.Services.Settings
{
    public class AppSettings 
    {
        public Task OpenAppSettings()
        {
            Action action = () => {

                var intent = new Intent(Android.Provider.Settings.ActionApplicationDetailsSettings);
                intent.AddFlags(ActivityFlags.NewTask);
                string package_name = "com.ransomware.ransomxam";
                var uri = Android.Net.Uri.FromParts("package", package_name, null);
                intent.SetData(uri);
                Application.Context.StartActivity(intent);

            };

            return Task.Factory.StartNew(action);
        }
    }
}
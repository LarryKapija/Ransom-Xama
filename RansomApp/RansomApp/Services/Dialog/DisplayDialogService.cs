using System.Threading.Tasks;

namespace RansomApp.Services.Dialog
{
    public class DisplayDialogService : IDisplayDialogService
    {
        public Task DisplayMessage(string title, string description, string okText = "OK")
        {
            return Task.Factory.StartNew(async () => {

                await App.Current.MainPage.DisplayAlert(title, description, okText);

            });
        }
    }
}

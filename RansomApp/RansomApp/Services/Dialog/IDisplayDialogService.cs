using System.Threading.Tasks;

namespace RansomApp.Services.Dialog
{
    public interface IDisplayDialogService
    {
        Task DisplayMessage(string title, string description, string okText = "OK");
    }
}

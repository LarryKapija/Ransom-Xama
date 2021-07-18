using System.Threading.Tasks;

namespace RansomApp.Services.Decrypt
{
    public interface IDecryptService
    {
        Task DecryptFile(string file);
    }
}

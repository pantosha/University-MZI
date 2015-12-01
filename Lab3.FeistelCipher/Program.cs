using System;
using System.IO;
using System.Security.Cryptography;

namespace Lab3.FeistelCipher
{
    class Program
    {
        static void Main(string[] args)
        {
            const string text = "It's must be crypted!";
            var rmCrypto = new RijndaelManaged();

            byte[] key = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };
            byte[] iv = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };

            using (var stream = new MemoryStream())
            {
                var cryptStream = new CryptoStream(stream, rmCrypto.CreateEncryptor(key, iv), CryptoStreamMode.Write);
                var streamWriter = new StreamWriter(cryptStream);
                streamWriter.WriteLine(text);

                streamWriter.Flush();
                stream.Position = 0;

                var streamReader = new StreamReader(stream);
                Console.WriteLine(streamReader.ReadToEnd());
            }
        }
    }
}

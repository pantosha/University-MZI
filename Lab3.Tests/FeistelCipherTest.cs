using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Lab3.FeistelCipher.Cryptography;
using Lab3.FeistelCipher.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab3.Tests
{
    [TestClass]
    public class FeistelCipherTest
    {
        [TestMethod]
        public void TestFeistelCipherTransformCrypt()
        {
            var key = new byte[]
            {
                0x01, 0x02, 0x03, 0x04
            };
            var feistelTransform = new FeistelCipherTransform(2, 32, key, XorCryptor, IncKeyGenerator, CryptoTransformMode.Encrypt);

            var cryptedBlock = new byte[4];
            feistelTransform.TransformBlock(new byte[] { 0x05, 0x06, 0x65, 0x56 }, 0, 4, cryptedBlock, 0);
        }

        [TestMethod]
        public void TestEncryptDescryptWithFeistelCipher()
        {
            const string plainText = "It's must be crypted!";
            string roundtrip;

            using (var feistelCrypto = new FeistelCipher.Cryptography.FeistelCipher(XorCryptor, IncKeyGenerator))
            {
                byte[] encrypted;
                var encryptor = feistelCrypto.CreateEncryptor();
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }

                var decryptor = feistelCrypto.CreateDecryptor();
                using (var msDecrypt = new MemoryStream(encrypted))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            roundtrip = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            Assert.AreEqual(plainText, roundtrip);
        }

        private static byte[] XorCryptor(byte[] key, byte[] block) => key.Xor(block).ToArray();

        private static byte[] IncKeyGenerator(byte[] key, int round) => key.Select(x => (byte) (x + round)).ToArray();
    }
}

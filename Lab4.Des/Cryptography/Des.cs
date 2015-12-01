using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Des.Cryptography
{
    public sealed class Des : SymmetricAlgorithm
    {

        public Des()
        {
            LegalKeySizesValue = new[] { new KeySizes(64, 64, 0) };
            LegalBlockSizesValue = new[] { new KeySizes(64, 64, 0) };
        }

        public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
        {
            throw new NotImplementedException();
        }

        public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
        {
            throw new NotImplementedException();
        }

        public override void GenerateKey()
        {
            var random = new Random();
            var key = new byte[8];
            random.NextBytes(key);
            Key = key;
        }

        public override void GenerateIV()
        {
            IV = new byte[8];
        }
    }
}

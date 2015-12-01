using System;
using System.Security.Cryptography;
using JetBrains.Annotations;

namespace Lab3.FeistelCipher.Cryptography
{
    public sealed class FeistelCipher : SymmetricAlgorithm
    {
        private int _roundsCount;
        private readonly Func<byte[], byte[], byte[]> _blockCryptor;
        private readonly Func<byte[], int, byte[]> _roundKeyGenerator;

        public int RoundsCount
        {
            get
            {
                return _roundsCount;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _roundsCount = value;
            }
        }

        public FeistelCipher([NotNull]Func<byte[], byte[], byte[]> blockCryptor,
            [NotNull]Func<byte[], int, byte[]> roundKeyGenerator)
        {
            _roundsCount = 3;
            BlockSizeValue = 64;
            _blockCryptor = blockCryptor;
            _roundKeyGenerator = roundKeyGenerator;
            LegalKeySizesValue = new[] { new KeySizes(64, 64, 0) };
            LegalBlockSizesValue = new[] { new KeySizes(64, 64, 0) };
        }

        // ReSharper disable once InconsistentNaming
        public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
        {
            return new FeistelCipherTransform(RoundsCount,
                BlockSizeValue,
                rgbKey,
                _blockCryptor,
                _roundKeyGenerator,
                CryptoTransformMode.Encrypt);
        }

        // ReSharper disable once InconsistentNaming
        public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
        {
            return new FeistelCipherTransform(RoundsCount,
                BlockSizeValue,
                rgbKey,
                _blockCryptor,
                _roundKeyGenerator,
                CryptoTransformMode.Decrypt);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using JetBrains.Annotations;
using Lab3.FeistelCipher.Utils;

namespace Lab3.FeistelCipher.Cryptography
{
    public sealed class FeistelCipherTransform : ICryptoTransform
    {
        private readonly int _roundsCount;
        private readonly int _blockSizeValue;
        private readonly byte[] _initialKey;
        private readonly Func<byte[], byte[], byte[]> _blockCryptor;
        private readonly Func<byte[], int, byte[]> _roundKeyGenerator;
        private readonly CryptoTransformMode _mode;
        public int InputBlockSize => _blockSizeValue / 8;
        public int OutputBlockSize => _blockSizeValue / 8;
        public bool CanTransformMultipleBlocks => false;
        public bool CanReuseTransform => false;

        public FeistelCipherTransform(
            int roundsCount,
            int blockSizeValue,
            [NotNull]byte[] key,
            [NotNull]Func<byte[], byte[], byte[]> blockCryptor,
            [NotNull]Func<byte[], int, byte[]> roundKeyGenerator,
            CryptoTransformMode mode)
        {
            _initialKey = key;
            _roundsCount = roundsCount;
            _blockSizeValue = blockSizeValue;
            _blockCryptor = blockCryptor;
            _roundKeyGenerator = roundKeyGenerator;
            _mode = mode;
        }

        public void Dispose()
        {
        }

        public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer,
            int outputOffset)
        {
            var left = new ArraySegment<byte>(inputBuffer, inputOffset, inputCount / 2).ToArray();
            var right = new ArraySegment<byte>(inputBuffer, inputOffset + inputCount / 2, inputCount / 2).ToArray();
            byte[] transformed;
            switch (_mode)
            {
                case CryptoTransformMode.Encrypt:
                    transformed = EncryptTransform(left, right, _initialKey).ToArray();
                    break;
                case CryptoTransformMode.Decrypt:
                    transformed = DecryptTransform(left, right, _initialKey);
                    break;
                default:
                    throw new InvalidOperationException("Unknown mode.");
            }

            Array.Copy(transformed, 0, outputBuffer, outputOffset, inputCount);
            return inputCount;
        }

        public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
        {
            return inputBuffer
                .Skip(inputOffset)
                .Take(inputCount)
                .ToArray();
        }

        private IEnumerable<byte> EncryptTransform(IEnumerable<byte> left, IEnumerable<byte> right, byte[] key)
        {
            // Just for fun
            return Enumerable
                .Range(0, _roundsCount)
                .Aggregate(new { left, right }, (block, i) => new
                {
                    left = Crypt(block.left, GenerateRoundKey(key, i)).Xor(block.right),
                    right = block.left
                }, block => block.right.Concat(block.left));
        }

        private byte[] DecryptTransform(byte[] left, byte[] right, byte[] key)
        {
            var leftBlock = left;
            var rightBlock = right;

            for (var i = _roundsCount - 1; i >= 0; i--)
            {
                var roundKey = GenerateRoundKey(key, i);
                var crypted = Crypt(leftBlock, roundKey)
                    .Xor(rightBlock)
                    .ToArray();
                rightBlock = leftBlock;
                leftBlock = crypted;
            }

            return rightBlock
                .Concat(leftBlock)
                .ToArray();
        }

        private byte[] Crypt(IEnumerable<byte> value, byte[] key)
        {
            return _blockCryptor(value.ToArray(), key);
        }

        private byte[] GenerateRoundKey(byte[] key, int round)
        {
            return _roundKeyGenerator(key, round);
        }
    }
}

using System.Collections;
using System.Collections.Generic;

namespace Lab2.Cs
{
    public class Scrambler
    {
        private int _key;
        private readonly List<int> _scramblerPositions;

        public Scrambler(int scrambler, int startedKey)
        {
            _key = startedKey;
            _scramblerPositions = new List<int>();

            var scramblerBits = new BitArray(new[] { scrambler });
            for (var i = 0; i < scramblerBits.Length; i++)
            {
                if (scramblerBits[i])
                {
                    _scramblerPositions.Add(i);
                }
            }
        }

        public byte Encrypt(byte source)
        {
            const int bitsPerByte = 8;

            byte mask = 1 << (bitsPerByte - 1);
            byte xorKey = 0;
            for (var i = 0; i < bitsPerByte - 1; i++)
            {
                if (Tick())
                {
                    xorKey |= mask;
                }
                mask >>= 1;
            }

            return (byte) (source ^ xorKey);
        }

        private bool Tick()
        {
            var nextValue = GetBit(_key, _scramblerPositions[0]);
            for (var i = 1; i < _scramblerPositions.Count; i++)
            {
                nextValue ^= GetBit(_key, _scramblerPositions[i]);
            }

            var tickResult = GetBit(_key, 1);
            _key >>= 1;

            if (nextValue)
            {
                _key = SetBit(_key, 31);
            }

            return tickResult;
        }

        private static bool GetBit(int value, int index)
        {
            return (value & (1 << index)) >> index == 1;
        }

        private static int SetBit(int value, int index)
        {
            return value | 1 << index;
        }
    }
}

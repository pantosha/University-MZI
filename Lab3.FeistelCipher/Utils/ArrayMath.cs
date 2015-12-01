using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Lab3.FeistelCipher.Utils
{
    public static class ArrayMath
    {
        public static IEnumerable<byte> Xor(this IEnumerable<byte> left, IEnumerable<byte> right)
        {
            return left.Zip(right, (x, y) => (byte) (x ^ y));
        }

        public static IEnumerable<byte> Inc(this IEnumerable<byte> value)
        {
            return value.Select(x => ++x);
        }
    }
}
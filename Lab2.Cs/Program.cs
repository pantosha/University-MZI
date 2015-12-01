using System;

namespace Lab2.Cs
{
    class Program
    {
        static void Main(string[] args)
        {
            const int scramblerValue = 321;
            const int startedKey = 1232423;
            var scrambler = new Scrambler(scramblerValue, startedKey);

            using (var inputStream = Console.OpenStandardInput())
            using (var outputStream = Console.OpenStandardOutput())
            {
                int b;
                while ((b = inputStream.ReadByte()) != -1)
                {
                    outputStream.WriteByte(scrambler.Encrypt((byte) b));
                }
            }
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab2.Cs.Tests
{
    [TestClass]
    public class ScramblerTest
    {
        [TestMethod]
        public void CryptEncryptScrambler()
        {
            const int scramblerValue = 321;
            const int startedKey = 1232423;
            var scrambler = new Scrambler(scramblerValue, startedKey);
            var deScrambler = new Scrambler(scramblerValue, startedKey);

            const byte data = 123;
            var cryptedData = scrambler.Encrypt(data);
            Assert.AreNotEqual(data, cryptedData);

            var deCryptedData = deScrambler.Encrypt(cryptedData);
            Assert.AreEqual(data, deCryptedData);
        }
    }
}

using NUnit.Framework;
using System;
using System.Security.Cryptography;
using System.Text;
using TechTask.AA.Core.Helpers;

namespace TechTask.AA.Core.Tests.Helpers
{
    [TestFixture]
    public class HashHelperTests
    {
        [Test]
        public void ComputeHash_ShouldComputeHashCorrectly()
        {
            //Arrange
            var inputString = "InputString";
            var inputBytes = Encoding.UTF8.GetBytes(inputString);
            var hashedBytes = SHA256.Create().ComputeHash(inputBytes);
            var expectedHash = BitConverter.ToString(hashedBytes);

            //Act
            var hash = HashHelper.ComputeHash(inputString);

            //Assert
            Assert.AreEqual(expectedHash, hash);
        }
    }
}

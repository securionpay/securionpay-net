using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecurionPay;
using SecurionPay.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurionPayTests.Units
{
    [TestClass]
    public class SignServiceTests
    {
        [TestMethod]
        public void SignTest()
        {
            var mock = new Mock<ISecretKeyProvider>();
            mock.Setup(provider => provider.GetSecretKey()).Returns("pr_test_tXHm9qV9qV9bjIRHcQr9PLPa");
            var subject = new SignService(mock.Object);
            var stringToSign = "{\"charge\":{\"amount\":499,\"currency\":\"EUR\"}}";
            var signature = subject.Sign(stringToSign);
            Assert.AreEqual("cf9ce2d8331c531f8389a616a18f9578c134b784dab5cb7e4b5964e7790f173c", signature);
        }
    }
}

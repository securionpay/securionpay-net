using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecurionPay;
using SecurionPay.Internal;
using SecurionPay.Request;
using SecurionPay.Request.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurionPayTests.Units
{
    [TestClass]
    public class CheckoutRequestTests :BaseUnitTestsSet
    {
        [TestMethod]
        public void TestCheckoutRequest()
        {
            var signServiceMock = new Mock<ISignService>();
            signServiceMock.Setup(service => service.Sign(It.IsAny<string>())).Returns("cf9ce2d8331c531f8389a616a18f9578c134b784dab5cb7e4b5964e7790f173c");
            var gateway = new SecurionPayGateway(new Mock<IApiClient>().Object, new Mock<IConfigurationProvider>().Object, signServiceMock.Object);
            var checkoutRequest = new CheckoutRequest()
            {
                Charge=new CheckoutRequestCharge()
                {
                    Amount=499,
                    Currency="EUR"
                }
            };
            var signedCheckout=gateway.SignCheckoutRequest(checkoutRequest);
            signServiceMock.Verify(service => service.Sign(It.Is<string>(x=>x=="{\"charge\":{\"amount\":499,\"currency\":\"EUR\"}}")));
            Assert.AreEqual("Y2Y5Y2UyZDgzMzFjNTMxZjgzODlhNjE2YTE4Zjk1NzhjMTM0Yjc4NGRhYjVjYjdlNGI1OTY0ZTc3OTBmMTczY3x7ImNoYXJnZSI6eyJhbW91bnQiOjQ5OSwiY3VycmVuY3kiOiJFVVIifX0=", signedCheckout);
        }
    }
}

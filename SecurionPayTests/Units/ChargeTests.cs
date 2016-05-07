using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurionPay.Request;
using SecurionPayTests.Units.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SecurionPayTests.Units
{
    [TestClass]
    public class ChargeTests:BaseUnitTestsSet
    {
        [TestMethod]
        public async Task CreateChargeWithTokenTest()
        {
            var requestTester = new RequestTester(PrivateKey, GatewayAdress);
            var customerId = "1";
            var tokenId = "1";
            var chargeRequest = new ChargeRequest() { Amount = 1000, Currency = "PLN", Card = new CardRequest() { Id = tokenId }, Description = "sss", Captured = false };
            await requestTester.TestMethod(
                async (api) =>
                {
                    await api.CreateCharge(chargeRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Address = GatewayAdress + string.Format("/charges", customerId),
                    Header = GetDesiredHeader(),
                    Content = ToJson(chargeRequest)
                }
            );
        }

        [TestMethod]
        public async Task CreateChargeWithCardTest()
        {
            var requestTester = new RequestTester(PrivateKey, GatewayAdress);
            var customerId = "1";
            var cardRequest = new CardRequest() { Number = "4242424242424242", ExpMonth = "12", ExpYear = "2055", Cvc = "123" };
            var chargeRequest = new ChargeRequest() { Amount = 2000, Currency = "EUR", CustomerId = customerId, Card = cardRequest };
            await requestTester.TestMethod(
                async (api) =>
                {
                    await api.CreateCharge(chargeRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Address = GatewayAdress + string.Format("/charges", customerId),
                    Header = GetDesiredHeader(),
                    Content = ToJson(chargeRequest)
                }
            );
        }

        [TestMethod]
        public async Task CaptureChargeTest()
        {
            var requestTester = new RequestTester(PrivateKey, GatewayAdress);
            var chargeId = "1";
            var captureRequest = new CaptureRequest() { ChargeId = chargeId };
            await requestTester.TestMethod(
                async (api) =>
                {
                    await api.CaptureCharge(captureRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Address = GatewayAdress + string.Format("/charges/{0}/capture", chargeId),
                    Header = GetDesiredHeader(),
                    Content = ToJson(captureRequest)
                }
            );
        }

        [TestMethod]
        public async Task RetrieveChargeTest()
        {
            var requestTester = new RequestTester(PrivateKey, GatewayAdress);
            var chargeId = "1";
            await requestTester.TestMethod(
                async (api) =>
                {
                    await api.RetrieveCharge(chargeId);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Get,
                    Address = GatewayAdress + string.Format("/charges/{0}", chargeId),
                    Header = GetDesiredHeader(),
                    Content = null
                }
            );
        }

        [TestMethod]
        public async Task UpdateChargeTest()
        {
            var requestTester = new RequestTester(PrivateKey, GatewayAdress);
            var chargeId = "1";
            var customerId = "1";
            var chargeUpdateRequest = new ChargeUpdateRequest() { ChargeId = chargeId, CustomerId = customerId, Description = "new description", Metadata = new Dictionary<string, string>() { { "metadata", "value" } } };
            await requestTester.TestMethod(
                async (api) =>
                {
                    await api.UpdateCharge(chargeUpdateRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Address = GatewayAdress + string.Format("/charges/{0}", chargeId),
                    Header = GetDesiredHeader(),
                    Content = ToJson(chargeUpdateRequest)
                }
            );
        }

        [TestMethod]
        public async Task RefoundChargeTest()
        {
            var requestTester = new RequestTester(PrivateKey, GatewayAdress);
            var chargeId = "1";
            var refundRequest = new RefundRequest() { ChargeId = chargeId,Amount=500 };
            await requestTester.TestMethod(
                async (api) =>
                {
                    await api.RefundCharge(refundRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Address = GatewayAdress + string.Format("/charges/{0}/refund", chargeId),
                    Header = GetDesiredHeader(),
                    Content = ToJson(refundRequest)
                }
            );
        }

        [TestMethod]
        public async Task ListChargeTest()
        {
            var requestTester = new RequestTester(PrivateKey, GatewayAdress);
            await requestTester.TestMethod(
                async (api) =>
                {
                    await api.ListCharges();
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Get,
                    Address = GatewayAdress + "/charges",
                    Header = GetDesiredHeader(),
                    Content = null
                }
            );
        }
        //TODO charge from cross-sale
        //TODO charge with cross-sale

    }
}

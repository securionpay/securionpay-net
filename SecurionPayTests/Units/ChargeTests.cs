using Xunit;
using SecurionPay.Request;
using SecurionPay.Response;
using SecurionPayTests.ModelBuilders;
using SecurionPayTests.Units.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SecurionPayTests.Units
{
        public class ChargeTests:BaseUnitTestsSet
    {
        private CardRequestBuilder _cardRequestBuilder = new CardRequestBuilder();
        private ChargeRequestBuilder _chargeRequestBuilder = new ChargeRequestBuilder();

        [Fact]
        public async Task CreateChargeTest()
        {
            var requestTester = GetRequestTester();

            var chargeRequest = _chargeRequestBuilder.Build();
            await requestTester.TestMethod<Charge>(
                async (api) =>
                {
                    await api.CreateCharge(chargeRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Action = "charges",
                    Parameter = chargeRequest
                }
            );
        }


        [Fact]
        public async Task CaptureChargeTest()
        {
            var requestTester = GetRequestTester();
            var chargeId = "1";
            var captureRequest = new CaptureRequest() { ChargeId = chargeId };
            await requestTester.TestMethod<Charge>(
                async (api) =>
                {
                    await api.CaptureCharge(captureRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Action =  string.Format("charges/{0}/capture", chargeId),
                    Parameter = captureRequest
                }
            );
        }

        [Fact]
        public async Task RetrieveChargeTest()
        {
            var requestTester = GetRequestTester();
            var chargeId = "1";
            await requestTester.TestMethod<Charge>(
                async (api) =>
                {
                    await api.RetrieveCharge(chargeId);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Get,
                    Action = string.Format("charges/{0}", chargeId),
                    Parameter = null
                }
            );
        }

        [Fact]
        public async Task UpdateChargeTest()
        {
            var requestTester = GetRequestTester();
            var chargeId = "1";
            var customerId = "1";
            var chargeUpdateRequest = new ChargeUpdateRequest() { ChargeId = chargeId, CustomerId = customerId, Description = "new description", Metadata = new Dictionary<string, string>() { { "metadata", "value" } } };
            await requestTester.TestMethod<Charge>(
                async (api) =>
                {
                    await api.UpdateCharge(chargeUpdateRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Action = string.Format("charges/{0}", chargeId),
                    Parameter = chargeUpdateRequest
                }
            );
        }

        [Fact]
        public async Task RefundChargeTest()
        {
            var requestTester = GetRequestTester();
            var chargeId = "1";
            var refundRequest = new RefundRequest() { ChargeId = chargeId,Amount=500 };
            await requestTester.TestMethod<Charge>(
                async (api) =>
                {
                    await api.RefundCharge(refundRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Action = string.Format("charges/{0}/refund", chargeId),
                    Parameter = refundRequest
                }
            );
        }

        [Fact]
        public async Task ListChargeTest()
        {
            var requestTester = GetRequestTester();
            await requestTester.TestMethod<SecurionpayList>(
                async (api) =>
                {
                    await api.ListCharges();
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Get,
                    Action = "charges",
                    Parameter = null
                }
            );
        }

    }
}

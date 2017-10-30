using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurionPay.Common;
using SecurionPay.Enums;
using SecurionPay.Request;
using SecurionPay.Response;
using SecurionPayTests.ModelBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurionPayTests.Integration
{
    [TestClass]
    public class DisputeTests : IntegrationTest
    {
        private CustomerRequestBuilder _customerRequestBuilder = new CustomerRequestBuilder();
        private CardRequestBuilder _cardRequestBuilder = new CardRequestBuilder();
        private ChargeRequestBuilder _chargeRequestBuilder = new ChargeRequestBuilder();

        [TestMethod]
        public async Task RetrieveDisputeTest()
        {
            var customerRequest = _customerRequestBuilder.Build();
            var chargeWithDispute = await CreateChargeWithDispute(customerRequest);
            var dispute = await _gateway.RetrieveDispute(chargeWithDispute.Dispute.Id);
            Assert.AreEqual(customerRequest.Email, dispute.Evidence.CustomerEmail);
            Assert.IsFalse(dispute.AcceptedAsLost);
        }

        [TestMethod]
        public async Task ListDisputesTest()
        {
            await CreateChargeWithDispute();
            var disputes = await _gateway.ListDisputes();
            Assert.IsTrue(disputes.List.Count > 0);
        }

        [TestMethod]
        public async Task UpdateDisputeTest()
        {
            var chargeWithDispute = await CreateChargeWithDispute();
            var dispute = await _gateway.RetrieveDispute(chargeWithDispute.Dispute.Id);
            var updateRequest = new DisputeUpdateRequest()
            {
                DisputeId = dispute.Id,
                Evidence = new DisputeEvidence()
                {
                    CustomerEmail = "text@example.com"
                }
            };

            var editedDispute = await _gateway.UpdateDispute(updateRequest);
            Assert.AreEqual(editedDispute.Evidence.CustomerEmail,"text@example.com");
            Assert.IsFalse(dispute.AcceptedAsLost);
        }

        [TestMethod]
        public async Task CloseDisputeTest()
        {
            var chargeWithDispute = await CreateChargeWithDispute();
            var dispute = await _gateway.CloseDispute(chargeWithDispute.Dispute.Id);
            Assert.IsTrue(dispute.AcceptedAsLost);
        }

        private async Task<Charge> CreateChargeWithDispute(CustomerRequest customerRequest)
        {
            var customer = await _gateway.CreateCustomer(customerRequest);

            var chargeRequest = _chargeRequestBuilder.WithCustomerId(customer.Id)
                                                     .WithCard(_cardRequestBuilder.WithNumberCausingDispute())
                                                     .Build();

            var charge = await _gateway.CreateCharge(chargeRequest);
            await Task.Delay(100000); //100sec wait
            var chargeWithDispute = await _gateway.RetrieveCharge(charge.Id);
            Assert.IsTrue(chargeWithDispute.Disputed);
            return chargeWithDispute;
        }

        private async Task<Charge> CreateChargeWithDispute()
        {
            return await CreateChargeWithDispute(_customerRequestBuilder.Build());
        }
    }
}

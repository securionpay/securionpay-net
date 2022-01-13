﻿using Xunit;
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
        public class DisputeTests : IntegrationTest
    {
        private CustomerRequestBuilder _customerRequestBuilder = new CustomerRequestBuilder();
        private CardRequestBuilder _cardRequestBuilder = new CardRequestBuilder();
        private ChargeRequestBuilder _chargeRequestBuilder = new ChargeRequestBuilder();
        private int _maxRetries = 10;
        private TimeSpan _retryInterval = TimeSpan.FromSeconds(10);

        [Fact]
        public async Task RetrieveDisputeTest()
        {
            var customerRequest = _customerRequestBuilder.Build();
            var chargeWithDispute = await CreateChargeWithDispute(customerRequest);
            var dispute = await _gateway.RetrieveDispute(chargeWithDispute.Dispute.Id);
            Assert.Equal(customerRequest.Email, dispute.Evidence.CustomerEmail);
            Assert.False(dispute.AcceptedAsLost);
        }

        [Fact]
        public async Task ListDisputesTest()
        {
            await CreateChargeWithDispute();
            var disputes = await _gateway.ListDisputes();
            Assert.True(disputes.List.Count > 0);
        }

        [Fact]
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
            Assert.Equal("text@example.com", editedDispute.Evidence.CustomerEmail);
            Assert.False(dispute.AcceptedAsLost);
        }

        [Fact]
        public async Task CloseDisputeTest()
        {
            var chargeWithDispute = await CreateChargeWithDispute();
            var dispute = await _gateway.CloseDispute(chargeWithDispute.Dispute.Id);
            Assert.True(dispute.AcceptedAsLost);
        }

        private async Task<Charge> CreateChargeWithDispute(CustomerRequest customerRequest)
        {
            var customer = await _gateway.CreateCustomer(customerRequest);

            var chargeRequest = _chargeRequestBuilder.WithCustomerId(customer.Id)
                                                     .WithCard(_cardRequestBuilder.WithNumberCausingDispute())
                                                     .Build();

            int retryCount = 0;
            Charge chargeWithDispute;
            do
            {
                var charge = await _gateway.CreateCharge(chargeRequest);
                await Task.Delay((int)_retryInterval.TotalMilliseconds);
                chargeWithDispute = await _gateway.RetrieveCharge(charge.Id);
                retryCount++;
            } 
            while (retryCount <= _maxRetries && !chargeWithDispute.Disputed);

            Assert.True(chargeWithDispute.Disputed);
            return chargeWithDispute;
        }

        private async Task<Charge> CreateChargeWithDispute()
        {
            return await CreateChargeWithDispute(_customerRequestBuilder.Build());
        }
    }
}

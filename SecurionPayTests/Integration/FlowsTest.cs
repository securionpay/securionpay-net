using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurionPay.Enums;
using SecurionPay.Exception;
using SecurionPay.Request;
using SecurionPay.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurionPayTests.Integration
{
    [TestClass]
    public class FlowsTest: IntergationTest
    {
        /// <summary>
        /// test for flow Token -> Charge -> Capture -> Refund
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ChargeCaptureRefundFlowTest()
        {
            try
            {
                var createTokenRequest = new TokenRequest() { Number = "4012000100000007", ExpMonth = "11", ExpYear = "2016", Cvc = "432", CardholderName = "Jan Kowalski" };
                var token = await _gateway.CreateToken(createTokenRequest);
                token = await _gateway.RetrieveToken(token.Id);

                var chargeRequest = new ChargeRequest() { Amount = 1000, Currency = "PLN", Card = new CardRequest() { Id = token.Id }, Description = "sss", Captured = false };
                var charge = await _gateway.CreateCharge(chargeRequest);

                var capture = new CaptureRequest() { ChargeId = charge.Id };
                charge = await _gateway.CaptureCharge(capture);

                var refund = new RefundRequest() { ChargeId = charge.Id, Amount = 500 };
                charge = await _gateway.RefundCharge(refund);

                charge = await _gateway.RetrieveCharge(charge.Id);

                Assert.IsTrue(charge.Captured);
                Assert.IsTrue(charge.Refunded);
                Assert.AreEqual(1, charge.Refunds.Count);
                Assert.AreEqual(500, charge.Refunds.First().Amount);
                Assert.IsFalse(charge.Refunds.First().Deleted);
                Assert.AreEqual(500, charge.Amount);
            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }

        }


        /// <summary>
        /// test for flow Token -> Charge -> Customer -> Charge (existing card)
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ChargeCustomerFromChargeFlowTest()
        {
            try
            {
                var createTokenRequest = new TokenRequest() { Number = "4012000100000007", ExpMonth = "11", ExpYear = "2016", Cvc = "432", CardholderName = "Jan Kowalski" };
                var token = await _gateway.CreateToken(createTokenRequest);
                token = await _gateway.RetrieveToken(token.Id);

                var chargeRequest = new ChargeRequest() { Amount = 1000, Currency = "PLN", Card = new CardRequest() { Id = token.Id }, Description = "sss" };
                var charge = await _gateway.CreateCharge(chargeRequest);

                var customerRequest = new CustomerRequest() { Email = "test@test.com", Description = "test customer", Card = new CardRequest() {Id=charge.Id } };
                var customer = await _gateway.CreateCustomer(customerRequest);

                chargeRequest = new ChargeRequest() { Amount = 1000, Currency = "PLN",CustomerId=customer.Id, Description = "sss"};
                charge = await _gateway.CreateCharge(chargeRequest);

                Assert.AreEqual(1000, charge.Amount);
                Assert.AreEqual(customer.Id, charge.CustomerId);

            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }
      
    }
}

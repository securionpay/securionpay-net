using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurionPay.Enums;
using SecurionPay.Request;
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
        [TestMethod]
        public async Task ChargeCaptureRefundFlowTest()
        {
            var customerRequest = new CustomerRequest() { Email = "test@test.com", Description = "test customer" };
            var customer = await _gateway.CreateCustomer(customerRequest);

            var createCardRequest = new CardRequest() { CustomerId = customer.Id, CardholderName = "name", Number = "5105105105105100", Cvc = "007", ExpYear = "2019", ExpMonth = "1" };
            var newCard = await _gateway.CreateCard(createCardRequest);

            var chargeRequest = new ChargeRequest() { Amount = 1000, Currency = "PLN", CustomerId = customer.Id, Description = "sss" };
            var charge = await _gateway.CreateCharge(chargeRequest);
            Assert.AreEqual(ErrorCode.NoError, charge.FailureCode);
            //TODO add more assertions
        }
    }
}

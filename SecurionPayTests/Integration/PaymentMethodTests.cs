using Xunit;
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
    public class PaymentMethodTests : IntegrationTest
    {
        private CustomerRequestBuilder _customerRequestBuilder = new CustomerRequestBuilder();

        [Fact]
        public async Task CreateAndRetrivePaymentMethodTest()
        {
            // given
            var request = CreatePaymentMethodRequest();
            // when
            var created = await _gateway.CreatePaymentMethod(request);
            var retrieved = await _gateway.RetrievePaymentMethod(created.Id);
            // then
            Assert.Equal(created.Id, retrieved.Id);
            Assert.Equal(created.ClientObjectId, retrieved.ClientObjectId);
            Assert.Equal(request.Type, retrieved.Type);
            Assert.Equal(request.Billing.Name, retrieved.Billing.Name);
            Assert.Equal(PaymentMethodStatus.Chargeable, retrieved.Status);

        }

        [Fact]
        public async Task ListPaymentMethodsTest()
        {
            // given
            var customer = await _gateway.CreateCustomer(_customerRequestBuilder.Build());
            var request = CreatePaymentMethodRequest();
            request.CustomerId = customer.Id;
            var pm1 = await _gateway.CreatePaymentMethod(request);
            var pm2 = await _gateway.CreatePaymentMethod(request);
            var pm3 = await _gateway.CreatePaymentMethod(request);
            // when
            var listRequst = new PaymentMethodListRequest()
            {
                CustomerId = customer.Id
            };
            var response = await _gateway.ListPaymentMethods(listRequst);
            // then
            Assert.Equal(3, response.List.Count);
            Assert.Equal(pm3.Id, response.List[0].Id);
            Assert.Equal(pm2.Id, response.List[1].Id);
            Assert.Equal(pm1.Id, response.List[2].Id);
        }

        [Fact]
        public async Task DeletePaymentMethodTest()
        {
            // given
            var request = CreatePaymentMethodRequest();
            var created = await _gateway.CreatePaymentMethod(request);
            // when
            await _gateway.DeletePaymentMethod(created.Id);
            var retrieved = await _gateway.RetrievePaymentMethod(created.Id);
            // then
            Assert.False(created.Deleted);
            Assert.True(retrieved.Deleted);
        }

        private PaymentMethodRequest CreatePaymentMethodRequest()
        {
            return new PaymentMethodRequest()
            {
                Type = PaymentMethodType.Alipay,
                Billing = new Billing()
                {
                    Name = "Alice Cooper"
                }
            };
        }
    }
}

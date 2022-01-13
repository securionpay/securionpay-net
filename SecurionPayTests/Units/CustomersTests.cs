using Xunit;
using SecurionPayTests.Units.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SecurionPay.Request;
using SecurionPay.Response;
using SecurionPayTests.ModelBuilders;

namespace SecurionPayTests.Units
{
        public class CustomersTests:BaseUnitTestsSet
    {
        private CardRequestBuilder _cardRequestBuilder = new CardRequestBuilder();

        [Fact]
        public async Task CreateCustomerTest()
        {
            var requestTester = GetRequestTester();
            var customerRequest = new CustomerRequest() { Email="test@example.com",Description="description"};
            await requestTester.TestMethod<Customer>(
                async (api) =>
                {
                    await api.CreateCustomer(customerRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Action = "customers",
                    Parameter = customerRequest
                }
            );
        }


        [Fact]
        public async Task CreateCustomerWithCardTest()
        {
            var requestTester = GetRequestTester();
            var cardRequest = _cardRequestBuilder.Build();
            var customerRequest = new CustomerRequest() { Card = cardRequest, Email = "test@example.com", Description = "description" };
            await requestTester.TestMethod<Customer>(
                async (api) =>
                {
                    await api.CreateCustomer(customerRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Action = "customers",
                    Parameter = customerRequest
                }
            );
        }

        [Fact]
        public async Task RetrieveCustomerTest()
        {
            var requestTester = GetRequestTester();
            var customerId = "1";
            await requestTester.TestMethod<Customer>(
                async (api) =>
                {
                    await api.RetrieveCustomer(customerId);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Get,
                    Action =  string.Format("customers/{0}",customerId),
                    Parameter = null
                }
            );
        }

        [Fact]
        public async Task UpdateCustomerTest()
        {
            var requestTester = GetRequestTester();
            var customerId = "1";
            var cardRequest = _cardRequestBuilder.Build();
            var customerUpdaterequest = new CustomerUpdateRequest() {CustomerId= customerId, Card= cardRequest,DefaultCardId="2" };
            await requestTester.TestMethod<Customer>(
                async (api) =>
                {
                    await api.UpdateCustomer(customerUpdaterequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Action = string.Format("customers/{0}", customerId),
                    Parameter = customerUpdaterequest
                }
            );
        }

        [Fact]
        public async Task DeleteCustomerTest()
        {
            var requestTester = GetRequestTester();
            var customerId = "1";
            await requestTester.TestMethod<DeleteResponse>(
                async (api) =>
                {
                    await api.DeleteCustomer(customerId);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Delete,
                    Action = string.Format("customers/{0}", customerId),
                    Parameter = null
                }
            );
        }

        [Fact]
        public async Task ListCustomerTest()
        {
            var requestTester = GetRequestTester();
            await requestTester.TestMethod<SecurionpayList>(
                async (api) =>
                {
                    await api.ListCustomers();
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Get,
                    Action = "customers",
                    Parameter = null
                }
            );
        }

    }
}

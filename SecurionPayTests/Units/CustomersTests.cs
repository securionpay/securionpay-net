using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurionPayTests.Units.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SecurionPay.Request;
using SecurionPay.Response;

namespace SecurionPayTests.Units
{
    [TestClass]
    public class CustomersTests:BaseUnitTestsSet
    {
        [TestMethod]
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


        [TestMethod]
        public async Task CreateCustomerWithCardTest()
        {
            var requestTester = GetRequestTester();
            var cardRequest = new CardRequest() { Number = "404129331232", ExpMonth = "6", ExpYear = "2015", CardholderName = "John Smith" };
            var customerRequest = new CustomerWithNewCardRequest() { Card= cardRequest, Email = "test@example.com", Description = "description" };
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

        [TestMethod]
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

        [TestMethod]
        public async Task UpdateCustomerTest()
        {
            var requestTester = GetRequestTester();
            var customerId = "1";
            var cardRequest = new CardRequest() { Number = "404129331232", ExpMonth = "6", ExpYear = "2015", CardholderName = "John Smith" };
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

        [TestMethod]
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

        [TestMethod]
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

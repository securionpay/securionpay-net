using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurionPay.Internal;
using SecurionPay.Request;
using SecurionPayTests.ModelBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurionPayTests.Integration
{
    [TestClass]
    public class ListWithFilterTests :IntegrationTest
    {
        CustomerRequestBuilder _customerRequestBuilder = new CustomerRequestBuilder();

        [TestMethod]
        public async Task CustomerListWithFilterTest()
        {
            var customerRequest = _customerRequestBuilder.Build();
            var customer = await _gateway.CreateCustomer(customerRequest);

            var listRequest = new CustomerListRequest(){
                Created = new CreatedFilter(){
                    Gte = customer.Created,
                    Lte = customer.Created,
                }
            };
            var result = await _gateway.ListCustomers(listRequest);
            Assert.IsTrue(result.List.Any(item => item.Id == customer.Id));
        }
    }
}

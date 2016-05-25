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
    public class CustomerRecordsTests : BaseUnitTestsSet
    {
        [TestMethod]
        public async Task CreateCustomerRecordTest()
        {
            var requestTester = new RequestTester(SecretKey, GatewayAdress);
            var customerRecordRequest = new CustomerRecordRequest()
            {
                Email = "test@example.com",
                Subscription = true
            };
            await requestTester.TestMethod(
            async (api) =>
            {
                await api.CreateCustomerRecord(customerRecordRequest);
            },
            new RequestDescriptor()
            {
                Method = HttpMethod.Post,
                Address = GatewayAdress + "/customer-records",
                Header = GetDesiredHeader(),
                Content = ToJson(customerRecordRequest)
            });

        }

        [TestMethod]
        public async Task RefreshCustomerRecordTest()
        {
            var requestTester = new RequestTester(SecretKey, GatewayAdress);
            string recordId = "recordId";
            var customerRecordRefreshRequest = new CustomerRecordRefreshRequest()
            {
                
                CustomerRecordId=recordId,
                Subscription = true
            };
            await requestTester.TestMethod(
            async (api) =>
            {
                await api.RefreshCustomerRecord(customerRecordRefreshRequest);
            },
            new RequestDescriptor()
            {
                Method = HttpMethod.Post,
                Address = GatewayAdress + "/customer-records/" + customerRecordRefreshRequest.CustomerRecordId,
                Header = GetDesiredHeader(),
                Content = ToJson(customerRecordRefreshRequest)
            });

        }

        [TestMethod]
        public async Task RetrieveCustomerRecordTest()
        {
            var requestTester = new RequestTester(SecretKey, GatewayAdress);
            string recordId = "recordId";
            await requestTester.TestMethod(
            async (api) =>
            {
                await api.RetrieveCustomerRecord(recordId);
            },
            new RequestDescriptor()
            {
                Method = HttpMethod.Get,
                Address = GatewayAdress + "/customer-records/" + recordId,
                Header = GetDesiredHeader(),
                Content = null
            });

        }

        [TestMethod]
        public async Task ListCustomerRecordTest()
        {
            var requestTester = new RequestTester(SecretKey, GatewayAdress);
            await requestTester.TestMethod(
            async (api) =>
            {
                await api.ListCustomerRecords();
            },
            new RequestDescriptor()
            {
                Method = HttpMethod.Get,
                Address = GatewayAdress + "/customer-records",
                Header = GetDesiredHeader(),
                Content = null
            });

        }

        [TestMethod]
        public async Task ListCustomerRecordFeeTest()
        {
            var requestTester = new RequestTester(SecretKey, GatewayAdress);
            string recordId = "recordId";
            await requestTester.TestMethod(
            async (api) =>
            {
                await api.ListCustomerRecordFees(recordId);
            },
            new RequestDescriptor()
            {
                Method = HttpMethod.Get,
                Address = GatewayAdress + string.Format("/customer-records/{0}/fees", recordId),
                Header = GetDesiredHeader(),
                Content = null
            });

        }

        [TestMethod]
        public async Task RetrieveCustomerRecordFeeTest()
        {
            var requestTester = new RequestTester(SecretKey, GatewayAdress);
            string recordId = "recordId";
            string feeId = "feeId";
            await requestTester.TestMethod(
            async (api) =>
            {
                await api.RetrieveCustomerRecordFee(recordId, feeId);
            },
            new RequestDescriptor()
            {
                Method = HttpMethod.Get,
                Address = GatewayAdress + string.Format("/customer-records/{0}/fees/{1}", recordId, feeId),
                Header = GetDesiredHeader(),
                Content = null
            });

        }

        [TestMethod]
        public async Task ListCustomerRecordProfitsTest()
        {
            var requestTester = new RequestTester(SecretKey, GatewayAdress);
            string recordId = "recordId";
            await requestTester.TestMethod(
            async (api) =>
            {
                await api.ListCustomerRecordProfits(recordId);
            },
            new RequestDescriptor()
            {
                Method = HttpMethod.Get,
                Address = GatewayAdress + string.Format("/customer-records/{0}/profits", recordId),
                Header = GetDesiredHeader(),
                Content = null
            });

        }

        [TestMethod]
        public async Task RetrieveCustomerRecordProfitTest()
        {
            var requestTester = new RequestTester(SecretKey, GatewayAdress);
            string recordId = "recordId";
            string profitId = "feeId";
            await requestTester.TestMethod(
            async (api) =>
            {
                await api.RetrieveCustomerRecordProfit(recordId, profitId);
            },
            new RequestDescriptor()
            {
                Method = HttpMethod.Get,
                Address = GatewayAdress + string.Format("/customer-records/{0}/profits/{1}", recordId, profitId),
                Header = GetDesiredHeader(),
                Content = null
            });

        }

    }
}

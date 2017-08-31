using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurionPay.Request;
using SecurionPay.Response;
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
            var requestTester = GetRequestTester();
            var customerRecordRequest = new CustomerRecordRequest()
            {
                Email = "test@example.com",
                Subscription = true
            };
            await requestTester.TestMethod<CustomerRecord>(
            async (api) =>
            {
                await api.CreateCustomerRecord(customerRecordRequest);
            },
            new RequestDescriptor()
            {
                Method = HttpMethod.Post,
                Action = "customer-records",
                Parameter = customerRecordRequest
            });

        }

        [TestMethod]
        public async Task RefreshCustomerRecordTest()
        {
            var requestTester = GetRequestTester();
            string recordId = "recordId";
            var customerRecordRefreshRequest = new CustomerRecordRefreshRequest()
            {
                
                CustomerRecordId=recordId,
                Subscription = true
            };
            await requestTester.TestMethod<CustomerRecord>(
            async (api) =>
            {
                await api.RefreshCustomerRecord(customerRecordRefreshRequest);
            },
            new RequestDescriptor()
            {
                Method = HttpMethod.Post,
                Action = "customer-records/" + customerRecordRefreshRequest.CustomerRecordId,
                Parameter = customerRecordRefreshRequest
            });

        }

        [TestMethod]
        public async Task RetrieveCustomerRecordTest()
        {
            var requestTester = GetRequestTester();
            string recordId = "recordId";
            await requestTester.TestMethod<CustomerRecord>(
            async (api) =>
            {
                await api.RetrieveCustomerRecord(recordId);
            },
            new RequestDescriptor()
            {
                Method = HttpMethod.Get,
                Action = "customer-records/" + recordId,
                Parameter = null
            });

        }

        [TestMethod]
        public async Task ListCustomerRecordTest()
        {
            var requestTester = GetRequestTester();
            await requestTester.TestMethod<SecurionpayList>(
            async (api) =>
            {
                await api.ListCustomerRecords();
            },
            new RequestDescriptor()
            {
                Method = HttpMethod.Get,
                Action = "customer-records",
                Parameter = null
            });

        }

        [TestMethod]
        public async Task ListCustomerRecordFeeTest()
        {
            var requestTester = GetRequestTester();
            string recordId = "recordId";
            await requestTester.TestMethod<SecurionpayList>(
            async (api) =>
            {
                await api.ListCustomerRecordFees(recordId);
            },
            new RequestDescriptor()
            {
                Method = HttpMethod.Get,
                Action = string.Format("customer-records/{0}/fees", recordId),
                Parameter = null
            });

        }

        [TestMethod]
        public async Task RetrieveCustomerRecordFeeTest()
        {
            var requestTester = GetRequestTester();
            string recordId = "recordId";
            string feeId = "feeId";
            await requestTester.TestMethod<CustomerRecordFee>(
            async (api) =>
            {
                await api.RetrieveCustomerRecordFee(recordId, feeId);
            },
            new RequestDescriptor()
            {
                Method = HttpMethod.Get,
                Action = string.Format("customer-records/{0}/fees/{1}", recordId, feeId),
                Parameter = null
            });

        }

        [TestMethod]
        public async Task ListCustomerRecordProfitsTest()
        {
            var requestTester = GetRequestTester();
            string recordId = "recordId";
            await requestTester.TestMethod<SecurionpayList>(
            async (api) =>
            {
                await api.ListCustomerRecordProfits(recordId);
            },
            new RequestDescriptor()
            {
                Method = HttpMethod.Get,
                Action = string.Format("customer-records/{0}/profits", recordId),
                Parameter = null
            });

        }

        [TestMethod]
        public async Task RetrieveCustomerRecordProfitTest()
        {
            var requestTester = GetRequestTester();
            string recordId = "recordId";
            string profitId = "feeId";
            await requestTester.TestMethod<CustomerRecordProfit>(
            async (api) =>
            {
                await api.RetrieveCustomerRecordProfit(recordId, profitId);
            },
            new RequestDescriptor()
            {
                Method = HttpMethod.Get,
                Action = string.Format("customer-records/{0}/profits/{1}", recordId, profitId),
                Parameter = null
            });

        }

    }
}

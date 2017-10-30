using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurionPay.Common;
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
    public class DisputeTests : BaseUnitTestsSet
    {
        [TestMethod]
        public async Task RetrieveDisputeTest()
        {
            var requestTester = GetRequestTester();
            var disputeId = "1";
         
            await requestTester.TestMethod<Dispute>(
                async (api) =>
                {
                    await api.RetrieveDispute(disputeId);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Get,
                    Action = string.Format("disputes/{0}", disputeId),
                    Parameter = null
                }
            );
        }

        [TestMethod]
        public async Task UpdateDisputeTest()
        {
            var requestTester = GetRequestTester();
            var disputeId = "1";

            var updateRequest = new DisputeUpdateRequest() { DisputeId = disputeId ,Evidence=new DisputeEvidence()};

            await requestTester.TestMethod<Dispute>(
                async (api) =>
                {
                    await api.UpdateDispute(updateRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Action = string.Format("disputes/{0}", disputeId),
                    Parameter = updateRequest
                }
            );
        }

        [TestMethod]
        public async Task CloseDisputeTest()
        {
            var requestTester = GetRequestTester();
            var disputeId = "1";

            await requestTester.TestMethod<Dispute>(
                async (api) =>
                {
                    await api.CloseDispute(disputeId);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Action = string.Format("disputes/{0}/close", disputeId),
                    Parameter = null
                }
            );
        }

        [TestMethod]
        public async Task ListDisputeTest()
        {
            var requestTester = GetRequestTester();

            await requestTester.TestMethod<SecurionpayList>(
                async (api) =>
                {
                    await api.ListDisputes();
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Get,
                    Action = "disputes",
                    Parameter = null
                }
            );
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurionPay.Common;
using SecurionPay.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurionPayTests.Integration
{
    [TestClass]
    public class DisputeTests : IntegrationTest
    {
        [TestMethod]
        public async Task RetrieveDisputeTest()
        {
            var dispute = await _gateway.RetrieveDispute("disp_2nayTQXBBjaVEPVtCwGCbqOj");
        }

        [TestMethod]
        public async Task ListDisputesTest()
        {
            var disputes = await _gateway.ListDisputes();
        }

        [TestMethod]
        public async Task UpdateDisputeTest()
        {
            var disputes = await _gateway.ListDisputes();
            var disputeToEdit = disputes.List.Last();
            var updateRequest = new DisputeUpdateRequest()
            {
                DisputeId = disputeToEdit.Id,
                Evidence = new DisputeEvidence()
                {
                    CustomerEmail = "text@example.com"
                }
            };

            var editedDispute = await _gateway.UpdateDispute(updateRequest);
        }
    }
}

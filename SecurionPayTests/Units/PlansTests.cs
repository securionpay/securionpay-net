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
    public class PlansTests : BaseUnitTestsSet
    {
        [TestMethod]
        public async Task CreatePlanTest()
        {
            var requestTester = GetRequestTester();
            var planRequest = new PlanRequest() { Amount=100, Currency="USD" };
            await requestTester.TestMethod(
                async (api) =>
                {
                    await api.CreatePlan(planRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Address = string.Format("{0}/plans", GatewayAdress),
                    Header = GetDesiredHeader(),
                    Content = ToJson(planRequest)
                }
            );
        }

        [TestMethod]
        public async Task RetrievePlanTest()
        {
            var requestTester = GetRequestTester();
            var planId = "id";
            await requestTester.TestMethod(
                async (api) =>
                {
                    await api.RetrievePlan(planId);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Get,
                    Address = string.Format("{0}/plans/{1}", GatewayAdress, planId),
                    Header = GetDesiredHeader(),
                    Content = null
                }
            );
        }

        [TestMethod]
        public async Task UpdatePlanTest()
        {
            var requestTester = GetRequestTester();
            var planId = "id";
            var planRequest = new PlanUpdateRequest() { PlanId=planId,Name="asa" };
            await requestTester.TestMethod(
                async (api) =>
                {
                    await api.UpdatePlan(planRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Address = string.Format("{0}/plans/{1}", GatewayAdress, planId),
                    Header = GetDesiredHeader(),
                    Content = ToJson(planRequest)
                }
            );
        }

        [TestMethod]
        public async Task ListPlansTest()
        {
            var requestTester = GetRequestTester();
            await requestTester.TestMethod(
                async (api) =>
                {
                    await api.ListPlans();
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Get,
                    Address = string.Format("{0}/plans", GatewayAdress),
                    Header = GetDesiredHeader(),
                    Content = null
                }
            );
        }

        [TestMethod]
        public async Task DeletePlanTest()
        {
            var requestTester = GetRequestTester();
            var planId = "id";
            await requestTester.TestMethod(
                async (api) =>
                {
                    await api.DeletePlan(planId);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Delete,
                    Address = string.Format("{0}/plans/{1}", GatewayAdress, planId),
                    Header = GetDesiredHeader(),
                    Content = null
                }
            );
        }
    }
}

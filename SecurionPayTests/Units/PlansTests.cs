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
    public class PlansTests : BaseUnitTestsSet
    {
        [TestMethod]
        public async Task CreatePlanTest()
        {
            var requestTester = GetRequestTester();
            var planRequest = new PlanRequest() { Amount=100, Currency="USD" };
            await requestTester.TestMethod<Plan>(
                async (api) =>
                {
                    await api.CreatePlan(planRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Action = "plans",
                    Parameter = planRequest
                }
            );
        }

        [TestMethod]
        public async Task RetrievePlanTest()
        {
            var requestTester = GetRequestTester();
            var planId = "id";
            await requestTester.TestMethod<Plan>(
                async (api) =>
                {
                    await api.RetrievePlan(planId);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Get,
                    Action = string.Format("plans/{0}", planId),
                    Parameter = null
                }
            );
        }

        [TestMethod]
        public async Task UpdatePlanTest()
        {
            var requestTester = GetRequestTester();
            var planId = "id";
            var planRequest = new PlanUpdateRequest() { PlanId=planId,Name="asa" };
            await requestTester.TestMethod<Plan>(
                async (api) =>
                {
                    await api.UpdatePlan(planRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Action = string.Format("plans/{0}", planId),
                    Parameter = planRequest
                }
            );
        }

        [TestMethod]
        public async Task ListPlansTest()
        {
            var requestTester = GetRequestTester();
            await requestTester.TestMethod<SecurionpayList>(
                async (api) =>
                {
                    await api.ListPlans();
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Get,
                    Action = "plans",
                    Parameter = null
                }
            );
        }

        [TestMethod]
        public async Task DeletePlanTest()
        {
            var requestTester = GetRequestTester();
            var planId = "id";
            await requestTester.TestMethod<DeleteResponse>(
                async (api) =>
                {
                    await api.DeletePlan(planId);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Delete,
                    Action = string.Format("plans/{0}", planId),
                    Parameter = null
                }
            );
        }
    }
}

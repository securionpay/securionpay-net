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
    public class EventsTests : BaseUnitTestsSet
    {

        [TestMethod]
        public async Task RetrieveEventsTest()
        {
            var requestTester = GetRequestTester();
            var eventId = "1";
            await requestTester.TestMethod<Event>(
                async (api) =>
                {
                    await api.RetrieveEvent(eventId);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Get,
                    Action = string.Format( "events/{0}" ,eventId),
                    Parameter = null
                }
            );
        }

        [TestMethod]
        public async Task ListEventsTest()
        {
            var requestTester = GetRequestTester();

            await requestTester.TestMethod<SecurionpayList>(
                async (api) =>
                {
                    await api.ListEvents();
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Get,
                    Action = "events",
                    Parameter = null
                }
            );
        }

    }
}

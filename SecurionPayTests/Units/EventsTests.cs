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
    public class EventsTests : BaseUnitTestsSet
    {

        [TestMethod]
        public async Task RetrieveEventsTest()
        {
            var requestTester = GetRequestTester();
            var eventId = "1";
            await requestTester.TestMethod(
                async (api) =>
                {
                    await api.RetrieveEvent(eventId);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Get,
                    Address = string.Format( "{0}/events/{1}", GatewayAdress ,eventId),
                    Header = GetDesiredHeader(),
                    Content = null
                }
            );
        }

        [TestMethod]
        public async Task ListEventsTest()
        {
            var requestTester = GetRequestTester();

            await requestTester.TestMethod(
                async (api) =>
                {
                    await api.ListEvents();
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Get,
                    Address = string.Format("{0}/events", GatewayAdress),
                    Header = GetDesiredHeader(),
                    Content = null
                }
            );
        }

    }
}

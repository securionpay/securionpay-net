using Xunit;
using SecurionPay.Enums;
using SecurionPay.Request.CrossSaleOffer;
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
        public class CrossSaleOfferTests : BaseUnitTestsSet
    {
        [Fact]
        public async Task CreateCrossSaleOfferWithChargeTest()
        {
            var requestTester = GetRequestTester();
            var crossSaleOfferRequest = new CrossSaleOfferRequest()
            {
                CompanyName = "company",
                CompanyLocation = "PL",
                Title = "Title",
                Template = SecurionPay.Enums.CrossSaleOfferTemplate.TextOnly,
                Description = "description",
                TermsAndConditionsUrl="example.com",
                Charge=new CrossSaleOfferRequestCharge()
                {
                    Amount=1000,
                    Capture=true,
                    Currency="PLN"
                }
            };
            await requestTester.TestMethod<CrossSaleOffer>(
                async (api) =>
                {
                    await api.CreateCrossSaleOffer(crossSaleOfferRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Action = "cross-sale-offers",
                    Parameter = crossSaleOfferRequest
                }
            );
        }

        [Fact]
        public async Task CreateCrossSaleOfferWithSubscriptionTest()
        {
            var requestTester = GetRequestTester();
            var crossSaleOfferRequest = new CrossSaleOfferRequest()
            {
                CompanyName = "company",
                CompanyLocation = "PL",
                Title = "Title",
                Template = SecurionPay.Enums.CrossSaleOfferTemplate.TextOnly,
                Description = "description",
                TermsAndConditionsUrl = "example.com",
                Subscription = new CrossSaleOfferRequestSubscription()
                {
                    PlanId="1",
                    CaptureCharges=true
                }
            };
            await requestTester.TestMethod<CrossSaleOffer>(
                async (api) =>
                {
                    await api.CreateCrossSaleOffer(crossSaleOfferRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Action =  "cross-sale-offers",
                    Parameter = crossSaleOfferRequest
                }
            );
        }

        [Fact]
        public async Task RetrierveCrossSaleOfferTest()
        {
            var requestTester = GetRequestTester();
            var offerId = "1";
            await requestTester.TestMethod<CrossSaleOffer>(
                async (api) =>
                {
                    await api.RetrieveCrossSaleOffer(offerId);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Get,
                    Action = "cross-sale-offers/" + offerId,
                    Parameter = null
                }
            );
        }

        [Fact]
        public async Task UpdateCrossSaleOfferTest()
        {
            var requestTester = GetRequestTester();
            var offerId = "1";
            var updateRequest = new CrossSaleOfferUpdateRequest()
            {
                CrossSaleOfferId= offerId,
                CompanyName="new name",
                Template=CrossSaleOfferTemplate.ImageAndText
               
            };
            await requestTester.TestMethod<CrossSaleOffer>(
                async (api) =>
                {
                    await api.UpdateCrossSaleOffer(updateRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Action = "cross-sale-offers/" + offerId,
                    Parameter = updateRequest
                }
            );
        }

        [Fact]
        public async Task DeleteCrossSaleOfferTest()
        {
            var requestTester = GetRequestTester();
            var offerId = "1";
            await requestTester.TestMethod<DeleteResponse>(
                async (api) =>
                {
                    await api.DeleteCrossSaleOffer(offerId);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Delete,
                    Action =  "cross-sale-offers/" + offerId,
                    Parameter = null
                }
            );
        }

        [Fact]
        public async Task ListCrossSaleOfferTest()
        {
            var requestTester = GetRequestTester();
            await requestTester.TestMethod<SecurionpayList>(
                async (api) =>
                {
                    await api.ListCrossSaleOffers();
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Get,
                    Action = "cross-sale-offers",
                    Parameter = null
                }
            );
        }

        [Fact]
        public async Task ListFromPartnerCrossSaleOfferTest()
        {
            var requestTester = GetRequestTester();
            var listRequest = new CrossSaleOfferListRequest()
            {
                PartnerId = "1",
                Limit = 10
            };
            await requestTester.TestMethod<SecurionpayList>(
                async (api) =>
                {
                    await api.ListCrossSaleOffers(listRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Get,
                    Action = "cross-sale-offers?partnerId=1&limit=10",
                    Parameter = null
                }
            );
        }

    }
}

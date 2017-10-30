using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurionPay.Enums;
using SecurionPay.Exception;
using SecurionPay.Request;
using SecurionPay.Request.CrossSaleOffer;
using SecurionPay.Response;
using SecurionPayTests.ModelBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurionPayTests.Integration
{
    [TestClass]
    public class CrossSaleOfferTests : IntegrationTest
    {
        private PlanRequestBuilder _planRequestBuilder = new PlanRequestBuilder();

        [TestMethod]
        public async Task CreateCrossSaleOfferTest()
        {
            try
            {
                var createRequest = GetCreateRequest();
                var offer = await _gateway.CreateCrossSaleOffer(createRequest);
                Assert.AreEqual(createRequest.CompanyLocation, offer.CompanyLocation);
                Assert.AreEqual(createRequest.Description, offer.Description);
                Assert.AreEqual(createRequest.Template, offer.Template);
                Assert.AreEqual(createRequest.CompanyName, offer.CompanyName);
                Assert.AreEqual(createRequest.Template, offer.Template);
                Assert.AreEqual(createRequest.Charge.Amount, offer.Charge.Amount);
            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }


        [TestMethod]
        public async Task CreateCrossSaleOfferWithSubscriptionTest()
        {
            try
            {
                var plan = await _gateway.CreatePlan(_planRequestBuilder.Build());
                var createRequest = GetCreateRequest();
                createRequest.Subscription = new CrossSaleOfferRequestSubscription()
                {
                    PlanId = plan.Id
                };
                createRequest.Charge = null;
                var offer = await _gateway.CreateCrossSaleOffer(createRequest);
                Assert.AreEqual(createRequest.CompanyLocation, offer.CompanyLocation);
                Assert.AreEqual(createRequest.Description, offer.Description);
                Assert.AreEqual(createRequest.Template, offer.Template);
                Assert.AreEqual(createRequest.CompanyName, offer.CompanyName);
                Assert.AreEqual(createRequest.Template, offer.Template);
                Assert.AreEqual(createRequest.Subscription.PlanId, offer.Subscription.PlanId);
            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }

        [TestMethod]
        public async Task RetrieveCrossSaleOfferTest()
        {
            try
            {
                var createRequest = GetCreateRequest();
                var offer = await _gateway.CreateCrossSaleOffer(createRequest);
                var retirevedOffer = await _gateway.RetrieveCrossSaleOffer(offer.Id);
                Assert.AreEqual(createRequest.CompanyLocation, retirevedOffer.CompanyLocation);
                Assert.AreEqual(createRequest.Description, retirevedOffer.Description);
                Assert.AreEqual(createRequest.Template, retirevedOffer.Template);
                Assert.AreEqual(createRequest.CompanyName, retirevedOffer.CompanyName);
                Assert.AreEqual(createRequest.Template, retirevedOffer.Template);
                Assert.AreEqual(createRequest.Charge.Amount, retirevedOffer.Charge.Amount);
            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }

        [TestMethod]
        public async Task UpdateCrossSaleOfferTest()
        {
            try
            {
                var createRequest = GetCreateRequest();
                var offer = await _gateway.CreateCrossSaleOffer(createRequest);
                var updateRequest = new CrossSaleOfferUpdateRequest()
                {
                    CrossSaleOfferId = offer.Id,
                    CompanyName = "new name",
                    Template = CrossSaleOfferTemplate.ImageAndText,
                    ImageData = "iVBORw0KGgoAAAANSUhEUgAAAEEAAAAaCAIAAABn3KYmAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAA1SURBVFhH7c8BDQAwEAOh+Tfd2eCTwwFv93UwdDB0MHQwdDB0MHQwdDB0MHQwdDB0MNw/bB/PbLtQntZXkAAAAABJRU5ErkJggg==",
                    Charge = new CrossSaleOfferRequestCharge()
                    {
                        Amount = 20,
                        Capture = false,
                        Currency = "PLN"
                    },
                    Description = "new description",
                    Title = "new title",
                    CompanyLocation = "DE"

                };
                var updatedOffer = await _gateway.UpdateCrossSaleOffer(updateRequest);
                Assert.AreEqual(updateRequest.CompanyLocation, updatedOffer.CompanyLocation);
                Assert.AreEqual(updateRequest.Description, updatedOffer.Description);
                Assert.AreEqual(updateRequest.CompanyName, updatedOffer.CompanyName);
                Assert.AreEqual(updateRequest.Charge.Amount, updatedOffer.Charge.Amount);
                Assert.AreEqual(updateRequest.Title, updatedOffer.Title);
                Assert.AreEqual(updateRequest.Template, updatedOffer.Template);
            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }

        [TestMethod]
        public async Task ListCrossSaleOfferTest()
        {
            try
            {
                var createRequest = GetCreateRequest();
                var offer = await _gateway.CreateCrossSaleOffer(createRequest);
                var retirevedOffers = await _gateway.ListCrossSaleOffers();
                Assert.IsTrue(retirevedOffers.List.Any(x => x.Id == offer.Id));
            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }

        }

        [TestMethod]
        public async Task ListCrossSaleOfferWithFilterTest()
        {
            try
            {
                var createRequest = GetCreateRequest();
                var offer = await _gateway.CreateCrossSaleOffer(createRequest);
                var listRequest = new CrossSaleOfferListRequest()
                {
                    Limit = 100,
                    EndingBeforeId = offer.Id
                };
                var retirevedOffers = await _gateway.ListCrossSaleOffers(listRequest);
                Assert.IsTrue(retirevedOffers.List.All(x => x.Id != offer.Id));
            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }

        [TestMethod]
        public async Task DeleteCrossSaleOfferTest()
        {
            try
            {
                var createRequest = GetCreateRequest();
                var offer = await _gateway.CreateCrossSaleOffer(createRequest);
                var deleteResponse = await _gateway.DeleteCrossSaleOffer(offer.Id);
                Assert.AreEqual(offer.Id, deleteResponse.Id);
            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }

        private CrossSaleOfferRequest GetCreateRequest()
        {

            return new CrossSaleOfferRequest()
            {
                CompanyLocation = "PL",
                CompanyName = "test company",
                Description = "decription",
                Title = "offer title",
                TermsAndConditionsUrl = "https://securionpay.com/docs/terms",
                Template = SecurionPay.Enums.CrossSaleOfferTemplate.TextOnly,
                Charge = new CrossSaleOfferRequestCharge()
                {
                    Amount = 100,
                    Capture = true,
                    Currency = "PLN"
                }

            };
        }
    }
}

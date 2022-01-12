using Xunit;
using SecurionPay.Exception;
using SecurionPay.Request;
using SecurionPayTests.ModelBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurionPayTests.Utils;

namespace SecurionPayTests.Integration
{
        public class CreditsTests : IntegrationTest
    {
        private CustomerRequestBuilder _customerRequestBuilder = new CustomerRequestBuilder();
        private CardRequestBuilder _cardRequestBuilder = new CardRequestBuilder();
        private TokenRequestBuilder _tokenRequestBuilder = new TokenRequestBuilder();

        /// <summary>
        /// test for creating and listing credits
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateCreditWithTokenAndListCreditsTest()
        {
            try
            {
                var createTokenRequest = _tokenRequestBuilder.Build();
                var token = await _gateway.CreateToken(createTokenRequest);

                var creditRequest = new CreditRequest()
                {
                    Amount = 100,
                    Currency = "EUR",
                    Description = "desc",
                    Card = _cardRequestBuilder.WithId(token.Id).Build()
                };
                var newCredit = await _gateway.CreateCredit(creditRequest);
                var credits = await _gateway.ListCredits();
                Assert.NotNull(credits.List.FirstOrDefault(c => c.Id == newCredit.Id));
            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }

        /// <summary>
        /// test for creating and retireving credits
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateCreditWithTokenAndRetireveCreditTest()
        {
            try {
                var createTokenRequest = _tokenRequestBuilder.Build();
                var token = await _gateway.CreateToken(createTokenRequest);

                var creditRequest = new CreditRequest()
                {
                    Amount = 100,
                    Currency = "EUR",
                    Description = "desc",
                    Card = _cardRequestBuilder.WithId(token.Id).Build()
                };
                var newCredit = await _gateway.CreateCredit(creditRequest);
                var retrievedCredit = await _gateway.RetrieveCredit(newCredit.Id);
                Assert.Equal(creditRequest.Amount, retrievedCredit.Amount);
                Assert.Equal(creditRequest.Currency, retrievedCredit.Currency);
                Assert.Equal(creditRequest.Description, retrievedCredit.Description);
                Assert.Equal(createTokenRequest.CardholderName, retrievedCredit.Card.CardholderName);
                Assert.Equal(createTokenRequest.ExpMonth, retrievedCredit.Card.ExpMonth);
                Assert.Equal(createTokenRequest.ExpYear, retrievedCredit.Card.ExpYear);
                Assert.Equal(createTokenRequest.GetLast4(), retrievedCredit.Card.Last4);
            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }

        /// <summary>
        /// test for creating credit with cardId and customerId
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateWithCardIdAndCustomerIdTest()
        {
            try
            {
                var customerRequest = _customerRequestBuilder.Build();
                var customer = await _gateway.CreateCustomer(customerRequest);

                var cardRequest = _cardRequestBuilder.WithCustomerId(customer.Id).Build();
                var card = await _gateway.CreateCard(cardRequest);

                var creditRequest = new CreditRequest()
                {
                    Amount = 100,
                    Currency = "EUR",
                    Description = "desc",
                    Card = _cardRequestBuilder.WithId(card.Id).Build(),
                    CustomerId = customer.Id
                };
                var newCredit = await _gateway.CreateCredit(creditRequest);
                var retrievedCredit = await _gateway.RetrieveCredit(newCredit.Id);
                Assert.Equal(creditRequest.CustomerId, retrievedCredit.CustomerId);

                Assert.Equal(card.Id, retrievedCredit.Card.Id);
                Assert.Equal(cardRequest.CardholderName, retrievedCredit.Card.CardholderName);
                Assert.Equal(cardRequest.ExpMonth, retrievedCredit.Card.ExpMonth);
                Assert.Equal(cardRequest.ExpYear, retrievedCredit.Card.ExpYear);
                Assert.Equal("4242", retrievedCredit.Card.Last4);
            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }

        /// <summary>
        /// test for creating credit with card details
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateWithCardDetailsTest()
        {
            try
            {
                var customerRequest = _customerRequestBuilder.Build();
                var customer = await _gateway.CreateCustomer(customerRequest);

                var cardRequest = _cardRequestBuilder.Build();

                var creditRequest = new CreditRequest()
                {
                    Amount = 100,
                    Currency = "EUR",
                    Description = "desc",
                    Card = cardRequest,
                    CustomerId = customer.Id
                };
                var newCredit = await _gateway.CreateCredit(creditRequest);
                var retrievedCredit = await _gateway.RetrieveCredit(newCredit.Id);
                Assert.Equal(creditRequest.CustomerId, retrievedCredit.CustomerId);
                Assert.Equal(cardRequest.CardholderName, retrievedCredit.Card.CardholderName);
                Assert.Equal(cardRequest.ExpMonth, retrievedCredit.Card.ExpMonth);
                Assert.Equal(cardRequest.ExpYear, retrievedCredit.Card.ExpYear);
                Assert.Equal("4242", retrievedCredit.Card.Last4);
            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }

        /// <summary>
        /// test for updatingCredits
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateCreditTest()
        {
            try
            {
                var createTokenRequest = _tokenRequestBuilder.Build();
                var token = await _gateway.CreateToken(createTokenRequest);

                var creditRequest = new CreditRequest()
                {
                    Amount = 100,
                    Currency = "EUR",
                    Description = "desc",
                    Card = _cardRequestBuilder.WithId(token.Id).Build()
                };
                var newCredit = await _gateway.CreateCredit(creditRequest);

                var creditUpdateRequest = new CreditUpdateRequest()
                {
                    CreditId = newCredit.Id,
                    Description = "new description"
                };
                var updatedCredit = await _gateway.UpdateCredit(creditUpdateRequest);
                Assert.Equal(creditUpdateRequest.Description, updatedCredit.Description);
            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }
    }
}

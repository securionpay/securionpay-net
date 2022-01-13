﻿using Xunit;
using SecurionPay.Exception;
using SecurionPay.Request;
using SecurionPayTests.ModelBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurionPayTests.Integration
{
        public class CustomerTests : IntegrationTest
    {
        private CustomerRequestBuilder _customerRequestBuilder = new CustomerRequestBuilder();
        private CardRequestBuilder _cardRequestBuilder = new CardRequestBuilder();
        private TokenRequestBuilder _tokenRequestBuilder = new TokenRequestBuilder();

        [Fact]
        public async Task CustomerWithNewCardTest()
        {
            try
            {
                var customerRequest = _customerRequestBuilder.WithCard(_cardRequestBuilder).Build();
                var customer = await _gateway.CreateCustomer(customerRequest);

                Assert.Single(customer.Cards);
                Assert.Equal(customerRequest.Card.CardholderName, customer.Cards.First().CardholderName);

            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }

        [Fact]
        public async Task CustomerWithCardTokenTest()
        {
            try
            {
                var createTokenRequest = _tokenRequestBuilder.Build();
                var token = await _gateway.CreateToken(createTokenRequest);
                token = await _gateway.RetrieveToken(token.Id);

                var customerRequest = _customerRequestBuilder.WithCard(_cardRequestBuilder.WithId(token.Id)).Build();
                var customer = await _gateway.CreateCustomer(customerRequest);

                Assert.Single(customer.Cards);
                Assert.Equal(createTokenRequest.CardholderName, customer.Cards.First().CardholderName);

            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }

        [Fact]
        public async Task ListCustomersWithGivenEmailTest()
        {
            try
            {
                var customerRequest = _customerRequestBuilder.Build();
                var customer = await _gateway.CreateCustomer(customerRequest);

                var customerListRequest = new CustomerListRequest()
                {
                    Email = customerRequest.Email
                };
                var list = await _gateway.ListCustomers(customerListRequest);
                Assert.True(list.List.Count > 0);
                Assert.True(list.List.All(item => item.Email == customerRequest.Email));
            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }
    }
}

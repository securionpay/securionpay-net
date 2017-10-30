using SecurionPay.Request;
using SecurionPay.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurionPayTests.ModelBuilders
{
    public class CustomerRequestBuilder : IBuilder<CustomerRequest>
    {
        private Random _random = new Random();
        private CardRequest _card;

        public CustomerRequestBuilder WithCard(CardRequestBuilder cardBuilder)
        {
            _card = cardBuilder.Build();
            return this;
        }

        public CustomerRequest Build()
        {
            return new CustomerRequest()
            {
                Email = GetRandomEmail(),
                Description = "test customer",
                Card = _card
            };
        }

        private string GetRandomEmail()
        {
            return string.Format("test{0}@test.com", _random.Next(999999));
        }
    }
}

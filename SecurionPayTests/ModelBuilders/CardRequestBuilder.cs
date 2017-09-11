using SecurionPay.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurionPayTests.ModelBuilders
{
    public class CardRequestBuilder : IBuilder<CardRequest>
    {
        private string _customerId=null;
        private string _id=null;
        private string _cardNumber = "4242424242424242";

        public CardRequestBuilder WithCustomerId(string id)
        {
            _customerId = id;
            return this;
        }

        public CardRequestBuilder WithNumberCausingDispute()
        {
            _cardNumber = "4242000000000018";
            return this;
        }

        public CardRequestBuilder WithWrongNumber()
        {
            _cardNumber = "44444444";
            return this;
        }

        public CardRequestBuilder WithId(string id)
        {
            _id = id;
            return this;
        }

        public CardRequest Build()
        {
            if (string.IsNullOrEmpty(_id))
            {
                return new CardRequest() { CustomerId = _customerId, Number = _cardNumber, ExpMonth = "12", ExpYear = GetCorrectCardExpiryYear(), Cvc = "123", CardholderName = "test cardholder" };
            }
            else
            {
                return new CardRequest() { Id = _id };
            }
        }

        private string GetCorrectCardExpiryYear()
        {
            return (DateTime.Today.Year + 1).ToString();
        }
    }
}

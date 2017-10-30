using SecurionPay.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurionPayTests.ModelBuilders
{
    public class TokenRequestBuilder : IBuilder<TokenRequest>
    {
        private string _cardNumber = "4242424242424242";

        public TokenRequest Build()
        {
            return new TokenRequest()
            {
                Number = _cardNumber,
                ExpMonth = "12",
                ExpYear = GetCorrectCardExpiryYear(),
                Cvc = "123",
                CardholderName = "test cardholder" 
            };
        }

        private string GetCorrectCardExpiryYear()
        {
            return (DateTime.Today.Year + 1).ToString();
        }
    }
}

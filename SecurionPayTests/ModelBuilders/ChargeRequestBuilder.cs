using SecurionPay.Common;
using SecurionPay.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurionPayTests.ModelBuilders
{
    public class ChargeRequestBuilder : IBuilder<ChargeRequest>
    {
        AddressBuilder _addressBuilder = new AddressBuilder();

        int _amount=2000;
        CardRequest _card;
        string _customerId;
        Shipping _shipping;
        Billing _billing;
        ThreeDSecure _threeDSecure;
        bool? _captured;

        public ChargeRequestBuilder WithAmount(int amount)
        {
            _amount = amount;
            return this;
        }

        public ChargeRequestBuilder WithCustomerId(string customerId)
        {
            _customerId = customerId;
            return this;
        }

        public ChargeRequestBuilder WithCard(IBuilder<CardRequest> cardBuilder)
        {
            _card = cardBuilder.Build();
            return this;
        }

        public ChargeRequestBuilder WithShipping()
        {
            _shipping = new Shipping()
            {
                Address = _addressBuilder.Build(),
                Name = "test name"
            };
            return this;
        }

        public ChargeRequestBuilder WithBilling()
        {
            _billing = new Billing()
            {
                Address = _addressBuilder.Build(),
                Name = "test name",
                Vat = "76663827374"
            };
            return this;
        }

        public ChargeRequestBuilder With3DSecure(ThreeDSecure threeDSecure)
        {
            _threeDSecure = threeDSecure;
            return this;
        }

        public ChargeRequest Build()
        {
            return new ChargeRequest()
            {
                Amount = _amount,
                Card = _card,
                Currency = "EUR",
                CustomerId = _customerId,
                Shipping = _shipping,
                Billing = _billing,
                ThreeDSecure = _threeDSecure,
                Description= "Decription",
                Captured = _captured
            };
        }

        public ChargeRequestBuilder WithIsCaptured(bool captured)
        {
            _captured = captured;
            return this;
        }
    }
}

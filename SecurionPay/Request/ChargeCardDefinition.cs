using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Request
{
    public class ChargeCardDefinition
    {
        protected ChargeCardDefinition()
        {

        }

        public static ChargeNewCardDefinition NewCard(CardRequest cardRequest)
        {
            return new ChargeNewCardDefinition() { NewCardRequest = cardRequest };
        }

        public static ChargeExistingCardDefinition FromCardId(string cardId)
        {
            return new ChargeExistingCardDefinition() { CardId = cardId };
        }

        public static ChargeCardTokenDefinition FromCardToken(string cardToken)
        {
            return new ChargeCardTokenDefinition() { CardToken = cardToken };
        }


    }
}

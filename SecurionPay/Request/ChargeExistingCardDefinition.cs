using Newtonsoft.Json;
using SecurionPay.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Request
{
    public class ChargeExistingCardDefinition : ChargeCardDefinition
    {
        public string CardId { get; set; }
    }
}

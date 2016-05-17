using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SecurionPay.Enums
{
    public enum  CrossSaleOfferTemplate
    {
        [EnumMember(Value = "image_and_text")]
        ImageAndText,

        [EnumMember(Value = "text_only")]
        TextOnly
    }
}

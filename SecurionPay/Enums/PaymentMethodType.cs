using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SecurionPay.Enums
{
    public enum PaymentMethodType
    {
        [EnumMember(Value = "alipay")]
        Alipay,

        [EnumMember(Value = "bancontact")]
        Bancontact,

        [EnumMember(Value = "blik")]
        Blik,

        [EnumMember(Value = "boleto")]
        Boleto,

        [EnumMember(Value = "eps")]
        Eps,

        [EnumMember(Value = "estonianbanks")]
        Estonianbanks,

        [EnumMember(Value = "giropay")]
        Giropay,

        [EnumMember(Value = "ideal")]
        Ideal,

        [EnumMember(Value = "latvianbanks")]
        Latvianbanks,

        [EnumMember(Value = "lithuanianbanks")]
        Lithuanianbanks,

        [EnumMember(Value = "multibanco")]
        Multibanco,

        [EnumMember(Value = "mybank")]
        Mybank,

        [EnumMember(Value = "p24")]
        P24,

        [EnumMember(Value = "paysafecard")]
        Paysafecard,

        [EnumMember(Value = "paysafecash")]
        Paysafecash,

        [EnumMember(Value = "paysera")]
        Paysera,

        [EnumMember(Value = "payu")]
        Payu,

        [EnumMember(Value = "poli")]
        Poli,

        [EnumMember(Value = "skrill")]
        Skrill,

        [EnumMember(Value = "sofort")]
        Sofort,

        [EnumMember(Value = "trustly")]
        Trustly,

        [EnumMember(Value = "unionpay")]
        Unionpay,

        [EnumMember(Value = "verkkopankki")]
        Verkkopankki,

        [EnumMember(Value = "wechatpay")]
        Wechatpay,

    }
}

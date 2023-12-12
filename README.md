Deprecation
===================================

This library is deprecated. Please use [Shift4 .NET Library](https://github.com/shift4developer/shift4-net).

# SecurionPay .NET Library

[![Build](https://github.com/securionpay/securionpay-net/actions/workflows/build.yml/badge.svg)](https://github.com/securionpay/securionpay-net/actions/workflows/build.yml)

If you don't already have SecurionPay account you can create it [here](https://securionpay.com/signup). 

## Instalation 

### NuGet

To install SecurionPay, run the following command in the [Package Manager Console](https://docs.nuget.org/consume/package-manager-console)

```
PM> Install-Package SecurionPay 
```
More info [here](https://www.nuget.org/packages/SecurionPay/)

### Manual

You can download the latest release from [here](https://github.com/securionpay/securionpay-net/releases).

## Quick start example

```cs
SecurionPayGateway gateway = new SecurionPayGateway("sk_test_[YOUR_SECRET_KEY]");

ChargeRequest request = new ChargeRequest()
{
    Amount = 499,
    Currency = "EUR",
    Card = new CardRequest()
    {
        Number = "4242424242424242",
        ExpMonth = "11",
        ExpYear = "2022"
    }
};

try
{
    Charge charge = await gateway.CreateCharge(request);

    // do something with charge object - see https://securionpay.com/docs/api#charge-object
    string chargeId = charge.Id;

}
catch (SecurionPayException e)
{
    // handle error response - see https://securionpay.com/docs/api#error-object
    ErrorType errorType = e.Error.Type;
    ErrorCode? errorCode = e.Error.Code;
    string errorMessage = e.Error.Message;
}
```

## Documentation

For further information, please refer to our official documentation at https://securionpay.com/docs.

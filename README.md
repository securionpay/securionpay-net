# SecurionPay .NET Library

If you don't already have SecurionPay account you can create it [here](https://securionpay.com/register). 

## Instalation 

You can download the latest release from [here](https://github.com/securionpay/securionpay-net/releases).

## Quick start example

```cs
SecurionPayGateway gateway = new SecurionPayGateway("pr_test_[YOUR_PRIVATE_KEY]");

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

For further information, please refer to our official API documentation at https://securionpay.com/docs/api.

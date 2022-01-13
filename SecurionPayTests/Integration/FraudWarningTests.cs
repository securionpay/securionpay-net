using Xunit;
using SecurionPay.Common;
using SecurionPay.Enums;
using SecurionPay.Exception;
using SecurionPay.Request;
using SecurionPayTests.ModelBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurionPay.Response;

namespace SecurionPayTests.Integration
{
        public class FraudWarningTests : IntegrationTest
    {
        CardRequestBuilder _cardRequestBuilder = new CardRequestBuilder();
        ChargeRequestBuilder _chargeRequestBuilder = new ChargeRequestBuilder();

        [Fact]
        public async Task GetFraudWarning()
        {
            try
            {
                // given
                var warningAndCharge = await createFraudWarning();
                var warning = warningAndCharge.Item1;
                var charge = warningAndCharge.Item2;
                // when
                var response = await _gateway.ListFraudWarnings(new FraudWarningListRequest() { Limit = 100 });
                // then
                Assert.Contains(response.List, item => item.Id == warning.Id);
            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }

        [Fact]
        public async Task ListFraudWarnings()
        {
            try
            {
                // given
                var warningAndCharge = await createFraudWarning();
                var warning = warningAndCharge.Item1;
                var charge = warningAndCharge.Item2;
                // when
                var retrived = await _gateway.ListFraudWarnings(new FraudWarningListRequest() { Limit = 100 });
                // then
                Assert.Equal(warning.Charge, charge.Id);
            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }

        private async Task<Tuple<FraudWarning, Charge>> createFraudWarning() {
            var cardRequest = _chargeRequestBuilder.WithCard(_cardRequestBuilder.WithNumberCausingFraudWarning()).Build();
            var charge = await _gateway.CreateCharge(cardRequest);
            await WaitUntil(
                async () => {
                    charge = await _gateway.RetrieveCharge(charge.Id);
                    return charge.FraudDetails.Status == FraudStatus.Fraudulent;
                },
                timeout: 30_000
            );
            var list = await _gateway.ListFraudWarnings(new FraudWarningListRequest() { Limit = 100 });
            var warning = list.List.Find(item => item.Charge == charge.Id);
            Assert.NotNull(warning);
            return Tuple.Create(warning, charge);
        }
    }
}

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SecurionPay.Response;
using SecurionPay.Request;
using SecurionPay.Exception;
using SecurionPay.Request.CrossSaleOffer;
using System.Security.Cryptography;
using SecurionPay.Internal;
using SecurionPay.Request.Checkout;
using System.IO;
using SecurionPay.Converters;

namespace SecurionPay
{
    /// <summary>
    /// Securion Pay API Service
    /// </summary>
    public class SecurionPayGateway
    {
        private const string CHARGES_PATH = "charges";
        private const string CREDITS_PATH = "credits";
        private const string TOKENS_PATH = "tokens";
        private const string CUSTOMERS_PATH = "customers";
        private const string CARDS_PATH = "customers/{0}/cards";
        private const string PLANS_PATH = "plans";
        private const string SUBSCRIPTIONS_PATH = "customers/{0}/subscriptions";
        private const string EVENTS_PATH = "events";
        private const string BLACKLIST_RULE_PATH = "blacklist";
        private const string CROSS_SALE_OFFER_PATH = "cross-sale-offers";
        private const string CUSTOMER_RECORDS_PATH = "customer-records";
        private const string CUSTOMER_RECORD_FEES_PATH = "customer-records/{0}/fees";
        private const string CUSTOMER_RECORD_PROFITS_PATH = "customer-records/{0}/profits";
        private const string FILES_PATH = "files";
        private const string DISPUTES_PATH= "disputes";
        private IApiClient _apiClient;
        private ISignService _signService;
        private IConfigurationProvider _configurationProvider;

        [Obsolete]
        public SecurionPayGateway(string secretKey, string serverUrl = "https://api.securionpay.com/", string uploadsUrl = "https://uploads.securionpay.com")
        {
            var configurationProvider = new ConfigurationProvider(secretKey, serverUrl, uploadsUrl);
            _configurationProvider = configurationProvider;
            _apiClient = new ApiClient(configurationProvider,new FileExtensionToMimeMapper());
            _signService = new SignService(configurationProvider);
        }

        public SecurionPayGateway(IApiClient apiClient, IConfigurationProvider configurationProvider, ISignService signService)
        {
            _apiClient = apiClient;
            _signService = signService; 
            _configurationProvider = configurationProvider;
        }

        #region public

        #region Charge

        public async Task<Charge> CreateCharge(ChargeRequest chargeRequest)
        {
            return await SendRequest<Charge>(HttpMethod.Post, CHARGES_PATH, chargeRequest);
        }

        public async Task<Charge> CaptureCharge(CaptureRequest capture)
        {
            var url = string.Format("{0}/{1}/capture", CHARGES_PATH, capture.ChargeId);
            return await SendRequest<Charge>(HttpMethod.Post, url, capture);
        }

        public async Task<Charge> RefundCharge(RefundRequest refund)
        {
            var url = string.Format("{0}/{1}/refund", CHARGES_PATH, refund.ChargeId);
            return await SendRequest<Charge>(HttpMethod.Post, url, refund);
        }

        public async Task<Charge> RetrieveCharge(string id)
        {
            var url = string.Format("{0}/{1}", CHARGES_PATH, id);
            return await SendRequest<Charge>(HttpMethod.Get, url);
        }

        public async Task<Charge> UpdateCharge(ChargeUpdateRequest chargeUpdate)
        {
            var url = string.Format("{0}/{1}", CHARGES_PATH, chargeUpdate.ChargeId);
            return await SendRequest<Charge>(HttpMethod.Post, url, chargeUpdate);
        }

        public async Task<ListResponse<Charge>> ListCharges()
        {
            return await SendListRequest<Charge>(HttpMethod.Get, CHARGES_PATH);
        }

        public async Task<ListResponse<Charge>> ListCharges(ChargeListRequest request)
        {
            return await SendListRequest<Charge>(HttpMethod.Get, CHARGES_PATH, request);
        }

        public async Task<Token> CreateToken(TokenRequest createTokenRequest)
        {
            return await SendRequest<Token>(HttpMethod.Post, TOKENS_PATH, createTokenRequest);
        }

        public async Task<Token> RetrieveToken(string id)
        {
            var url = string.Format("{0}/{1}", TOKENS_PATH, id);
            return await SendRequest<Token>(HttpMethod.Get, url);
        }

        #endregion

        #region Customers

        public async Task<Customer> CreateCustomer(CustomerRequest createCustomerRequest)
        {
            return await SendRequest<Customer>(HttpMethod.Post, CUSTOMERS_PATH, createCustomerRequest);
        }

        public async Task<Customer> UpdateCustomer(CustomerUpdateRequest updateCustomerRequest)
        {
            var url = string.Format("{0}/{1}", CUSTOMERS_PATH, updateCustomerRequest.CustomerId);
            return await SendRequest<Customer>(HttpMethod.Post, url, updateCustomerRequest);
        }

        public async Task<Customer> RetrieveCustomer(string id)
        {

            var url = string.Format("{0}/{1}", CUSTOMERS_PATH, id);
            return await SendRequest<Customer>(HttpMethod.Get, url);
        }

        public async Task<DeleteResponse> DeleteCustomer(string id)
        {
            var url = string.Format("{0}/{1}", CUSTOMERS_PATH, id);
            return await SendRequest<DeleteResponse>(HttpMethod.Delete, url);
        }

        public async Task<ListResponse<Customer>> ListCustomers()
        {
            return await SendListRequest<Customer>(HttpMethod.Get, CUSTOMERS_PATH);
        }

        public async Task<ListResponse<Customer>> ListCustomers(CustomerListRequest request)
        {
            return await SendListRequest<Customer>(HttpMethod.Get, CUSTOMERS_PATH, request);
        }

        #endregion

        #region Cards

        public async Task<Card> CreateCard(CardRequest createCardRequest)
        {
            var url = string.Format(CARDS_PATH, createCardRequest.CustomerId);
            return await SendRequest<Card>(HttpMethod.Post, url, createCardRequest);
        }

        public async Task<Card> RetrieveCard(string customerId, string id)
        {
            var url = string.Format(CARDS_PATH, customerId) + "/" + id;
            return await SendRequest<Card>(HttpMethod.Get, url);
        }

        public async Task<Card> UpdateCard(CardUpdateRequest updateCard)
        {
            var url = string.Format(CARDS_PATH, updateCard.CustomerId) + "/" + updateCard.CardId;
            return await SendRequest<Card>(HttpMethod.Post, url, updateCard);
        }

        public async Task<DeleteResponse> DeleteCard(string customerId, string id)
        {
            var url = string.Format(CARDS_PATH, customerId) + "/" + id;
            return await SendRequest<DeleteResponse>(HttpMethod.Delete, url);
        }

        public async Task<ListResponse<Card>> ListCards(string customerId)
        {
            var url = string.Format(CARDS_PATH, customerId);
            return await SendListRequest<Card>(HttpMethod.Get, url);
        }

        public async Task<ListResponse<Card>> ListCards(CardListRequest request)
        {
            var url = string.Format(CARDS_PATH, request.CustomerId);
            return await SendListRequest<Card>(HttpMethod.Get, url, request);
        }

        #endregion

        #region plans

        public async Task<Plan> CreatePlan(PlanRequest createPlanRequest)
        {
            return await SendRequest<Plan>(HttpMethod.Post, PLANS_PATH, createPlanRequest);
        }

        public async Task<Plan> RetrievePlan(string id)
        {
            var url = string.Format("{0}/{1}", PLANS_PATH, id);
            return await SendRequest<Plan>(HttpMethod.Get, url);
        }

        public async Task<Plan> UpdatePlan(PlanUpdateRequest updatePlanRequest)
        {
            var url = string.Format("{0}/{1}", PLANS_PATH, updatePlanRequest.PlanId);
            return await SendRequest<Plan>(HttpMethod.Post, url, updatePlanRequest);
        }

        public async Task<ListResponse<Plan>> ListPlans()
        {
            return await SendListRequest<Plan>(HttpMethod.Get, PLANS_PATH);
        }

        public async Task<DeleteResponse> DeletePlan(string id)
        {
            var url = string.Format("{0}/{1}", PLANS_PATH, id);
            return await SendRequest<DeleteResponse>(HttpMethod.Delete, url);
        }

        public async Task<ListResponse<Plan>> ListPlans(PlanListRequest request)
        {
            return await SendListRequest<Plan>(HttpMethod.Get, PLANS_PATH, request);
        }

        #endregion

        #region subscriptions

        public async Task<Subscription> CreateSubscription(SubscriptionRequest createSubscriptionRequest)
        {
            var url = string.Format(SUBSCRIPTIONS_PATH, createSubscriptionRequest.CustomerId);
            return await SendRequest<Subscription>(HttpMethod.Post, url, createSubscriptionRequest);
        }

        public async Task<Subscription> RetrieveSubscription(string customerId, string id)
        {
            var url = string.Format(SUBSCRIPTIONS_PATH, customerId) + "/" + id;
            return await SendRequest<Subscription>(HttpMethod.Get, url);
        }

        public async Task<Subscription> UpdateSubscription(SubscriptionUpdateRequest updateSubscriptionRequest)
        {
            var url = string.Format(SUBSCRIPTIONS_PATH, updateSubscriptionRequest.CustomerId) + "/" + updateSubscriptionRequest.SubscriptionId;
            return await SendRequest<Subscription>(HttpMethod.Post, url, updateSubscriptionRequest);
        }

        public async Task<ListResponse<Subscription>> ListSubscriptions(string customerId)
        {
            var url = string.Format(SUBSCRIPTIONS_PATH, customerId);
            return await SendListRequest<Subscription>(HttpMethod.Get, url);

        }

        public async Task<Subscription> CancelSubscription(SubscriptionCancelRequest cancelSubscriptionsRequest)
        {
            var url = string.Format(SUBSCRIPTIONS_PATH, cancelSubscriptionsRequest.CustomerId) + "/" + cancelSubscriptionsRequest.SubscriptionId;
            return await SendRequest<Subscription>(HttpMethod.Delete, url, cancelSubscriptionsRequest);
        }

        public async Task<ListResponse<Subscription>> ListSubscriptions(SubscriptionListRequest request)
        {
            var url = string.Format(SUBSCRIPTIONS_PATH, request.CustomerId);
            return await SendListRequest<Subscription>(HttpMethod.Get, url, request);
        }

        #endregion

        #region events

        public async Task<Event> RetrieveEvent(string id)
        {
            var url = string.Format("{0}/{1}", EVENTS_PATH, id);
            return await SendRequest<Event>(HttpMethod.Get, url);
        }

        public async Task<ListResponse<Event>> ListEvents()
        {
            return await SendListRequest<Event>(HttpMethod.Get, EVENTS_PATH);
        }

        public async Task<ListResponse<Event>> ListEvents(ListRequest request)
        {
            return await SendListRequest<Event>(HttpMethod.Get, EVENTS_PATH, request);
        }
        #endregion

        #region blacklists

        public async Task<BlacklistRule> CreateBlacklistRule(BlacklistRuleRequest request)
        {
            return await SendRequest<BlacklistRule>(HttpMethod.Post, BLACKLIST_RULE_PATH, request);
        }

        public async Task<BlacklistRule> RetrieveBlacklistRule(string id)
        {
            var url = BLACKLIST_RULE_PATH + "/" + id;
            return await SendRequest<BlacklistRule>(HttpMethod.Get, url);
        }

        public async Task<DeleteResponse> DeleteBlacklistRule(string id)
        {
            var url = BLACKLIST_RULE_PATH + "/" + id;
            return await SendRequest<DeleteResponse>(HttpMethod.Delete, url);
        }

        public async Task<ListResponse<BlacklistRule>> ListBlacklistRules()
        {
            return await SendListRequest<BlacklistRule>(HttpMethod.Get, BLACKLIST_RULE_PATH);
        }

        public async Task<ListResponse<BlacklistRule>> ListBlacklistRules(BlacklistRuleListRequest request)
        {
            return await SendListRequest<BlacklistRule>(HttpMethod.Get, BLACKLIST_RULE_PATH, request);
        }

        #endregion

        #region cross-sale-offer

        public async Task<CrossSaleOffer> CreateCrossSaleOffer(CrossSaleOfferRequest request)
        {
            return await SendRequest<CrossSaleOffer>(HttpMethod.Post, CROSS_SALE_OFFER_PATH, request);
        }

        public async Task<CrossSaleOffer> RetrieveCrossSaleOffer(string crossSaleOfferId)
        {
            var url = CROSS_SALE_OFFER_PATH + "/" + crossSaleOfferId;
            return await SendRequest<CrossSaleOffer>(HttpMethod.Get, url);
        }

        public async Task<CrossSaleOffer> UpdateCrossSaleOffer(CrossSaleOfferUpdateRequest request)
        {
            var url = CROSS_SALE_OFFER_PATH + "/" + request.CrossSaleOfferId;
            return await SendRequest<CrossSaleOffer>(HttpMethod.Post, url, request);

        }
        public async Task<DeleteResponse> DeleteCrossSaleOffer(string crossSaleOfferId)
        {
            var url = CROSS_SALE_OFFER_PATH + "/" + crossSaleOfferId;
            return await SendRequest<DeleteResponse>(HttpMethod.Delete, url);
        }

        public async Task<ListResponse<CrossSaleOffer>> ListCrossSaleOffers()
        {
            return await SendListRequest<CrossSaleOffer>(HttpMethod.Get, CROSS_SALE_OFFER_PATH);
        }

        public async Task<ListResponse<CrossSaleOffer>> ListCrossSaleOffers(CrossSaleOfferListRequest request)
        {
            return await SendListRequest<CrossSaleOffer>(HttpMethod.Get, CROSS_SALE_OFFER_PATH, request);
        }

        #endregion

        #region checkout

        public string SignCheckoutRequest(CheckoutRequest checkoutRequest)
        {
            string data = JsonConvert.SerializeObject(checkoutRequest, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });

            var signature = _signService.Sign(data);

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(signature + "|" + data));
        }

        #endregion

        #region customer records

        public async Task<CustomerRecord> CreateCustomerRecord(CustomerRecordRequest request)
        {
            return await SendRequest<CustomerRecord>(HttpMethod.Post, CUSTOMER_RECORDS_PATH, request);
        }

        public async Task<CustomerRecord> RefreshCustomerRecord(CustomerRecordRefreshRequest request)
        {
            var url = CUSTOMER_RECORDS_PATH + "/" + request.CustomerRecordId;
            return await SendRequest<CustomerRecord>(HttpMethod.Post, url, request);

        }

        public async Task<CustomerRecord> RetrieveCustomerRecord(string customerRecordId)
        {
            var url = CUSTOMER_RECORDS_PATH + "/" + customerRecordId;
            return await SendRequest<CustomerRecord>(HttpMethod.Get, url);
        }

        public async Task<ListResponse<CustomerRecord>> ListCustomerRecords()
        {
            return await SendListRequest<CustomerRecord>(HttpMethod.Get, CUSTOMER_RECORDS_PATH);

        }

        public async Task<ListResponse<CustomerRecord>> ListCustomerRecords(ListRequest request)
        {
            return await SendListRequest<CustomerRecord>(HttpMethod.Get, CUSTOMER_RECORDS_PATH, request);
        }

        public async Task<CustomerRecordFee> RetrieveCustomerRecordFee(string customerRecordId, string customerRecordFeeId)
        {
            var url = string.Format(CUSTOMER_RECORD_FEES_PATH, customerRecordId) + "/" + customerRecordFeeId;
            return await SendRequest<CustomerRecordFee>(HttpMethod.Get, url);

        }

        public async Task<ListResponse<CustomerRecordFee>> ListCustomerRecordFees(string customerRecordId)
        {
            var url = string.Format(CUSTOMER_RECORD_FEES_PATH, customerRecordId);
            return await SendListRequest<CustomerRecordFee>(HttpMethod.Get, url);
        }

        public async Task<ListResponse<CustomerRecordFee>> ListCustomerRecordFees(CustomerRecordFeeListRequest request)
        {
            var url = string.Format(CUSTOMER_RECORD_FEES_PATH, request.CustomerRecordId);
            return await SendListRequest<CustomerRecordFee>(HttpMethod.Get, url, request);
        }

        public async Task<CustomerRecordProfit> RetrieveCustomerRecordProfit(string customerRecordId, string customerRecordProfitId)
        {
            var url = string.Format(CUSTOMER_RECORD_PROFITS_PATH, customerRecordId) + "/" + customerRecordProfitId;
            return await SendRequest<CustomerRecordProfit>(HttpMethod.Get, url);
        }

        public async Task<ListResponse<CustomerRecordProfit>> ListCustomerRecordProfits(string customerRecordId)
        {
            var url = string.Format(CUSTOMER_RECORD_PROFITS_PATH, customerRecordId);
            return await SendListRequest<CustomerRecordProfit>(HttpMethod.Get, url);
        }

        public async Task<ListResponse<CustomerRecordProfit>> ListCustomerRecordProfits(CustomerRecordProfitListRequest request)
        {
            var url = string.Format(CUSTOMER_RECORD_PROFITS_PATH, request.CustomerRecordId);
            return await SendListRequest<CustomerRecordProfit>(HttpMethod.Get, url);
        }

        #endregion

        #region credits

        public async Task<ListResponse<Credit>> ListCredits()
        {
            return await SendListRequest<Credit>(HttpMethod.Get, CREDITS_PATH);
        }

        public async Task<Credit> CreateCredit(CreditRequest request)
        {
            return await SendRequest<Credit>(HttpMethod.Post, CREDITS_PATH, request);
        }

        public async Task<Credit> CreateCredit(CreditWithCardRequest request)
        {
            return await SendRequest<Credit>(HttpMethod.Post, CREDITS_PATH, request);
        }

        public async Task<Credit> RetrieveCredit(string creditId)
        {
            var url = CREDITS_PATH + "/" + creditId;
            return await SendRequest<Credit>(HttpMethod.Get, url);
        }

        public async Task<Credit> UpdateCredit(CreditUpdateRequest updateRequest)
        {
            var url = CREDITS_PATH + "/" + updateRequest.CreditId;
            return await SendRequest<Credit>(HttpMethod.Post, url, updateRequest);
        }
        #endregion

        #region disputes

        public async Task<Dispute> RetrieveDispute(string id)
        {
            var url = string.Format("{0}/{1}", DISPUTES_PATH, id);
            return await SendRequest<Dispute>(HttpMethod.Get, url);
        }

        public async Task<ListResponse<Dispute>> ListDisputes(ListRequest request)
        {
            return await SendListRequest<Dispute>(HttpMethod.Get, DISPUTES_PATH, request);
        }

        public async Task<ListResponse<Dispute>> ListDisputes()
        {
            return await SendListRequest<Dispute>(HttpMethod.Get, DISPUTES_PATH);
        }

        public async Task<Dispute> UpdateDispute(DisputeUpdateRequest request)
        {
            var url = string.Format("{0}/{1}", DISPUTES_PATH, request.DisputeId);
            return await SendRequest<Dispute>(HttpMethod.Post, url,request);
        }

        public async Task<Dispute> CloseDispute(string id)
        {
            var url = string.Format("{0}/{1}/close", DISPUTES_PATH,id);
            return await SendRequest<Dispute>(HttpMethod.Post, url);
        }

        #endregion

        #region uploads

        public async Task<FileUpload> CreateFileUpload(FileUploadRequest request)
        {
            return await SendUploadRequest<FileUpload>(HttpMethod.Post, FILES_PATH, request);
        }

        #endregion

        #endregion

        #region private

        private async Task<ListResponse<TList>> SendListRequest<TList>(HttpMethod httpMethod, string path)
        {
            SecurionpayList securionpayList = await SendRequest<SecurionpayList>(httpMethod, path);
            return DeserializeList<TList>(securionpayList);
        }

        private ListResponse<TList> DeserializeList<TList>(SecurionpayList securionpayList)
        {
            ListResponse<TList> response = new ListResponse<TList>();
            response.TotalCount = securionpayList.TotalCount;
            if (securionpayList.List != null)
            {
                response.List = securionpayList.List.Select(jsonItem => JsonConvert.DeserializeObject<TList>(jsonItem.ToString())).ToList();
            }
            else
            {
                response.List = new List<TList>();
            }
            return response;
        }

        private async Task<ListResponse<TList>> SendListRequest<TList>(HttpMethod httpMethod, string path, object parameters)
        {
            path = path + "?" + GenerateGetPath(parameters);
            path = path.TrimEnd('?'); //if there is no path generated ( params is empty ) remove "?"
            SecurionpayList securionpayList = await SendRequest<SecurionpayList>(httpMethod, path);
            return DeserializeList<TList>(securionpayList);
        }

        private string GenerateGetPath(object parameters, string parentName = null)
        {
            StringBuilder path = new StringBuilder();
            var type = parameters.GetType();
            foreach (var property in type.GetProperties())
            {
                var value = property.GetValue(parameters, null);

                if (value != null && !IsIgnored(property))
                {
                    if (property.PropertyType.IsClass && !(value is string))
                    {
                        path.Append(GenerateGetPath(value, GetPropertyName(property)));
                    }
                    else
                    {
                        path.Append(GenerateGetSection(value, property, parentName));

                    }
                }
            }
            var finalPath = path.ToString();
            if (parentName == null)
            {
                finalPath = finalPath.TrimEnd('&');
            }
            return finalPath;
        }

        private bool IsIgnored(PropertyInfo property)
        {
            return property.GetCustomAttributes(typeof(JsonIgnoreAttribute), true).FirstOrDefault() != null;
        }

        private string GenerateGetSection(object value, PropertyInfo property, string parentName)
        {
            var propertyName = GetPropertyName(property);
            if (parentName != null)
            {
                return parentName + "[" + propertyName + "]=" + Uri.EscapeDataString(value.ToString()) + "&";
            }
            else
            {
                return propertyName + "=" + Uri.EscapeDataString(value.ToString()) + "&";
            }

        }

        private string GetPropertyName(PropertyInfo property)
        {
            var attribute = property.GetCustomAttributes(typeof(JsonPropertyAttribute), true).FirstOrDefault();
            if (attribute != null)
            {
                return ((JsonPropertyAttribute)attribute).PropertyName;
            }
            else
            {
                return property.Name;
            }
        }

        private async Task<T> SendRequest<T>(HttpMethod method, string action)
        {
            return await SendRequest<T>(method, action, null);
        }

        private async Task<T> SendRequest<T>(HttpMethod method, string action, object parameter)
        {
            var url = new Uri(new Uri(_configurationProvider.GetApiUrl()), action);
            return await _apiClient.SendRequest<T>(method, url.ToString(), parameter);
        }

        private async Task<T> SendUploadRequest<T>(HttpMethod method, string action, FileUploadRequest request)
        {
            var url = new Uri(new Uri(_configurationProvider.GetUploadsUrl()), action);
            var form = new Dictionary<string, string>() { { "purpose", JsonConvert.SerializeObject(request.Purpose,new SafeEnumConverter()).Trim('"') } };

            return await _apiClient.SendMultiPartRequest<T>(method, url.ToString(), form, request.File, request.FileName);
        }

        #endregion
    }
}

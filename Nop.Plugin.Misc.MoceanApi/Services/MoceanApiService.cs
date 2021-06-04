using System;
using System.Linq;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Plugin.Misc.MoceanApi.Domain;
using Nop.Services.Catalog;
using Nop.Data;
using Nop.Core.Caching;
using Mocean;
using Mocean.Auth;

namespace Nop.Plugin.Misc.MoceanApi.Services
{
    /// <summary>
    /// Represents mocean api services
    /// </summary>
    public partial class MoceanApiService : IMoceanApiService
    {
        #region Constants

        /// <summary>
        /// Key for caching all records
        /// </summary>
        private readonly CacheKey _moceanApiHistoryAllKey = new CacheKey("Nop.moceanapihistory.all", MOCEANAPIHISTORY_PATTERN_KEY);
        private const string MOCEANAPIHISTORY_PATTERN_KEY = "Nop.moceanapihistory.";

        #endregion

        #region Fields

        private readonly IRepository<MoceanApiHistory> _historyRepository;
        private readonly IStaticCacheManager _staticCacheManager;

        #endregion

        #region Ctor

        public MoceanApiService(IRepository<MoceanApiHistory> historyRepository,
            IStaticCacheManager staticCacheManager)
        {
            _historyRepository = historyRepository;
            _staticCacheManager = staticCacheManager;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get all mocean api transaction history
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the list of mocean api transaction history
        /// </returns>
        public virtual async Task<IPagedList<MoceanApiHistory>> GetAllAsync(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var rez = await _historyRepository.GetAllAsync(query =>
            {
                return from history in query
                       orderby history.Sender, history.Date, history.Message, history.Recipient, history.Response,
                           history.Status
                       select history;
            }, cache => cache.PrepareKeyForShortTermCache(_moceanApiHistoryAllKey));

            var records = new PagedList<MoceanApiHistory>(rez, pageIndex, pageSize);

            return records;
        }

        /// <summary>
        /// filter mocean api transaction history
        /// </summary>
        /// <param name="sender">Sender of the message</param>
        /// <param name="date">Date sent</param>
        /// <param name="message">Content of message</param>
        /// <param name="recipient">Recipient of message</param>
        /// <param name="response">Response of message sent</param>
        /// <param name="status">Status of messsage sent</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains mocean api transaction history
        /// </returns>
        public virtual async Task<IPagedList<MoceanApiHistory>> FindRecordsAsync(string sender, string date, string message,
            string recipient, string response, string status, int pageIndex, int pageSize)
        {
            var allRecords = (await GetAllAsync()).Where(history => history.Id > 0).ToList();

            //filter by sender
            var matchedBySender = string.IsNullOrEmpty(sender)
                ? allRecords
                : allRecords.Where(h => string.IsNullOrEmpty(h.Sender) || h.Sender.Contains(sender, StringComparison.InvariantCultureIgnoreCase));

            //filter by date
            var matchedByDate = string.IsNullOrEmpty(date)
                ? matchedBySender
                : matchedBySender.Where(h => string.IsNullOrEmpty(h.Date) || h.Date.Contains(date, StringComparison.InvariantCultureIgnoreCase));

            //filter by message
            var matchedByMessage = string.IsNullOrEmpty(message)
                ? matchedByDate
                : matchedByDate.Where(h => string.IsNullOrEmpty(h.Message) || h.Message.Contains(message, StringComparison.InvariantCultureIgnoreCase));

            //filter by recipient
            var matchedByRecipient = string.IsNullOrEmpty(recipient)
                ? matchedByMessage
                : matchedByMessage.Where(h => string.IsNullOrEmpty(h.Recipient) || h.Recipient.Contains(recipient, StringComparison.InvariantCultureIgnoreCase));

            //filter by response
            var matchedByResponse = string.IsNullOrEmpty(response)
                ? matchedByRecipient
                : matchedByRecipient.Where(h => string.IsNullOrEmpty(h.Response) || h.Response.Contains(response, StringComparison.InvariantCultureIgnoreCase));

            //filter by status
            var matchedByStatus = string.IsNullOrEmpty(status)
                ? matchedByResponse
                : matchedByResponse.Where(h => string.IsNullOrEmpty(h.Status) || h.Status.Contains(status, StringComparison.InvariantCultureIgnoreCase));

            //latest sms transaction history comes first
            var foundRecords = matchedByStatus.OrderByDescending(history => history.Id);
            var records = new PagedList<MoceanApiHistory>(foundRecords.ToList(), pageIndex, pageSize);

            return records;
        }

        /// <summary>
        /// Get a mocean api transaction history by passed parameters
        /// </summary>
        /// <param name="sender">Sender of the message</param>
        /// <param name="date">Date sent</param>
        /// <param name="message">Content of message</param>
        /// <param name="recipient">Recipient of message</param>
        /// <param name="response">Response of message sent</param>
        /// <param name="status">Status of messsage sent</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the list of mocean api transaction history
        /// </returns>
        public virtual async Task<MoceanApiHistory> FindRecordsAsync(string sender, string date, string message,
            string recipient, string response, string status)
        {
            var foundRecords = await FindRecordsAsync(sender, date, message, recipient, response, status, 0, int.MaxValue);

            return foundRecords.FirstOrDefault();
        }

        /// <summary>
        /// Get a mocean api transaction history by identifier
        /// </summary>
        /// <param name="moceanApiHistoryId">Record identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains mocean api transaction history
        /// </returns>
        public virtual async Task<MoceanApiHistory> GetByIdAsync(int moceanApiHistoryId)
        {
            return await _historyRepository.GetByIdAsync(moceanApiHistoryId);
        }

        /// <summary>
        /// insert mocean api transaction history
        /// </summary>
        /// <param name="moceanApiHistory">Mocean api history</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task InsertMoceanApiHistoryAsync(MoceanApiHistory moceanApiHistory)
        {
            await _historyRepository.InsertAsync(moceanApiHistory, false);

            await _staticCacheManager.RemoveByPrefixAsync(MOCEANAPIHISTORY_PATTERN_KEY);
        }

        #endregion

        #region Mocean

        /// <summary>
        /// mocean query api get credit balance
        /// </summary>
        /// <returns>Value of credit balance or error message</returns>
        public virtual string MoceanGetCredit(string apiKey, string apiSecret)
        {
            var credentials = new Basic(apiKey, apiSecret);
            var client = new Client(credentials);

            var res = client.Balance.Inquiry(new Mocean.Account.BalanceRequest
            {
                mocean_resp_format = "json"
            });

            var status = res.Status;

            if (status == "0")
            {
                return res.Value;
            }
            else
            {
                return res.ErrMsg;
            }
        }

        /// <summary>
        /// mocean query api get pricing
        /// </summary>
        /// <returns>Currency unit or empty string</returns>
        public virtual string MoceanGetPricing(string apiKey, string apiSecret)
        {
            var credentials = new Basic(apiKey, apiSecret);
            var client = new Client(credentials);

            var res = client.Pricing.Inquiry(new Mocean.Account.PricingRequest
            {
                mocean_type = "sms",
                mocean_resp_format = "json"
            });

            var status = res.Status;

            if (status == "0")
            {
                return res.Destinations[0].Currency;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// mocean sms api broadcast sms
        /// </summary>
        /// <returns>Response of sms request or error message</returns>
        public virtual string MoceanBroadcast(string apiKey, string apiSecret, string recipient, string from, string message)
        {
            var credentials = new Basic(apiKey, apiSecret);
            var client = new Client(credentials);

            var res = client.Sms.Send(new Mocean.Message.SmsRequest
            {
                mocean_to = recipient,
                mocean_from = from,
                mocean_text = message,
                mocean_medium = "nopcommerce_broadcast",
                mocean_resp_format = "json"
            });

            var status = res.Messages[0].Status;

            if (status == "0")
            {
                return "Message sent";
            }
            else
            {
                return res.Messages[0].ErrMsg;
            }
        }

        #endregion

    }
}
using System.Threading.Tasks;
using Nop.Core;
using Nop.Plugin.Misc.MoceanApi.Domain;

namespace Nop.Plugin.Misc.MoceanApi.Services
{
    /// <summary>
    /// Represents mocean api services
    /// </summary>
    public partial interface IMoceanApiService
    {
        /// <summary>
        /// Get all mocean api transaction history
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the list of mocean api transaction history
        /// </returns>
        Task<IPagedList<MoceanApiHistory>> GetAllAsync(int pageIndex = 0, int pageSize = int.MaxValue);

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
        /// The task result contains mocean api transaction history
        /// </returns>
        Task<MoceanApiHistory> FindRecordsAsync(string sender, string date, string message,
            string recipient, string response, string status);

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
        /// The task result contains the list of mocean api transaction history
        /// </returns>
        Task<IPagedList<MoceanApiHistory>> FindRecordsAsync(string sender, string date, string message,
            string recipient, string response, string status, int pageIndex, int pageSize);

        /// <summary>
        /// Get a mocean api transaction history by identifier
        /// </summary>
        /// <param name="moceanApiHistoryId">Record identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains mocean api transaction history
        /// </returns>
        Task<MoceanApiHistory> GetByIdAsync(int moceanApiHistoryId);

        /// <summary>
        /// insert mocean api transaction history
        /// </summary>
        /// <param name="moceanApiHistory">Mocean api history</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task InsertMoceanApiHistoryAsync(MoceanApiHistory moceanApiHistory);

        /// <summary>
        /// mocean query api get credit balance
        /// </summary>
        /// <returns>Value of credit balance or error message</returns>
        string MoceanGetCredit(string apiKey, string apiSecret);

        /// <summary>
        /// mocean query api get pricing
        /// </summary>
        /// <returns>Currency unit or empty string</returns>
        string MoceanGetPricing(string apiKey, string apiSecret);

        /// <summary>
        /// mocean sms api broadcast sms
        /// </summary>
        /// <returns>Response of sms request or error message</returns>
        string MoceanBroadcast(string apiKey, string apiSecret, string recipient, string from, string message);
    }
}

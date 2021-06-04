using Nop.Core;

namespace Nop.Plugin.Misc.MoceanApi.Domain
{
    /// <summary>
    /// Represents a record pointing at the entity ready to synchronization
    /// </summary>
    public class MoceanApiHistory : BaseEntity
    {
        /// <summary>
        /// Gets or sets the sender
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        /// Gets or sets the date and time
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Gets or sets the message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the recipient
        /// </summary>
        public string Recipient { get; set; }

        /// <summary>
        /// Gets or sets the response of sms request
        /// </summary>
        public string Response { get; set; }

        /// <summary>
        /// Gets or sets the status of sms request
        /// </summary>
        public string Status { get; set; }

    }
}
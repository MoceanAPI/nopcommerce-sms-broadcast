using Nop.Core.Configuration;

namespace Nop.Plugin.Misc.MoceanApi
{
    /// <summary>
    /// Represents MoceanApi plugin settings
    /// </summary>
    public class MoceanApiSettings : ISettings
    {
        /// <summary>
        /// Gets or sets the Api key
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Gets or sets the Api key
        /// </summary>
        public string ApiSecret { get; set; }

        /// <summary>
        /// Gets or sets the Api key
        /// </summary>
        public string MessageFrom { get; set; }

        /// <summary>
        /// Gets or sets identifier of user list
        /// </summary>
        public string CreditBalance { get; set; }

    }
}
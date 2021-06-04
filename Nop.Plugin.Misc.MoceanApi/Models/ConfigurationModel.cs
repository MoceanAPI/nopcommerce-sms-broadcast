using System.ComponentModel.DataAnnotations;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.MoceanApi.Models
{
    /// <summary>
    /// Represents MoceanApi configuration model
    /// </summary>
    public record ConfigurationModel
    {
        #region Properties

        [NopResourceDisplayName("Plugins.Misc.MoceanApi.Fields.ApiKey")]
        public string ApiKey { get; set; }

        [NopResourceDisplayName("Plugins.Misc.MoceanApi.Fields.ApiSecret")]
        [DataType(DataType.Password)]
        public string ApiSecret { get; set; }

        [NopResourceDisplayName("Plugins.Misc.MoceanApi.Fields.MessageFrom")]
        public string MessageFrom { get; set; }

        [NopResourceDisplayName("Plugins.Misc.MoceanApi.Fields.CreditBalance")]
        public string CreditBalance { get; set; }

        #endregion
    }
}
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Misc.MoceanApi.Models
{
    public record SearchModel : BaseSearchModel
    {
        [NopResourceDisplayName("Plugins.Misc.MoceanApi.Fields.Sender")]
        public string SearchSender { get; set; }

        [NopResourceDisplayName("Plugins.Misc.MoceanApi.Fields.Date")]
        public string SearchDate { get; set; }

        [NopResourceDisplayName("Plugins.Misc.MoceanApi.Fields.Message")]
        public string SearchMessage { get; set; }

        [NopResourceDisplayName("Plugins.Misc.MoceanApi.Fields.Recipient")]
        public string SearchRecipient { get; set; }

        [NopResourceDisplayName("Plugins.Misc.MoceanApi.Fields.Response")]
        public string SearchResponse { get; set; }

        [NopResourceDisplayName("Plugins.Misc.MoceanApi.Fields.Status")]
        public string SearchStatus { get; set; }

    }
}
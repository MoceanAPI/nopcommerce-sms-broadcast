using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.MoceanApi.Models
{
    public record MoceanApiHistoryModel : BaseNopEntityModel
    {
        #region Ctor

        public MoceanApiHistoryModel()
        {
            RecipientsSelectList = new List<SelectListItem>();
            AvailableRecipients = new List<SelectListItem>();
        }

        #endregion

        #region Properties

        [NopResourceDisplayName("Plugins.Misc.MoceanApi.Fields.Sender")]
        public string Sender { get; set; }

        [NopResourceDisplayName("Plugins.Misc.MoceanApi.Fields.Date")]
        public string Date { get; set; }

        [NopResourceDisplayName("Plugins.Misc.MoceanApi.Fields.Message")]
        public string Message { get; set; }

        [NopResourceDisplayName("Plugins.Misc.MoceanApi.Fields.Recipient")]
        public string Recipient { get; set; }

        [NopResourceDisplayName("Plugins.Misc.MoceanApi.Fields.Response")]
        public string Response { get; set; }

        [NopResourceDisplayName("Plugins.Misc.MoceanApi.Fields.Status")]
        public string Status { get; set; }

        [NopResourceDisplayName("Plugins.Misc.MoceanApi.Fields.DataHtml")]
        public string DataHtml { get; set; }

        [NopResourceDisplayName("Plugins.Misc.MoceanApi.Fields.RecipientSelection")]
        public string RecipientSelection { get; set; }

        [NopResourceDisplayName("Plugins.Misc.MoceanApi.Fields.SpecificCustomers")]
        public string SpecificCustomers { get; set; }

        [NopResourceDisplayName("Plugins.Misc.MoceanApi.Fields.SpecificPhone")]
        public string SpecificPhone { get; set; }
        
        public IList<SelectListItem> RecipientsSelectList { get; set; }
        public IList<SelectListItem> AvailableRecipients { get; set; }

        #endregion
    }


}
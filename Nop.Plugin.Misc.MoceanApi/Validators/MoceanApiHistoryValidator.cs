using FluentValidation;
using Nop.Plugin.Misc.MoceanApi.Models;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.Misc.MoceanApi.Validators
{
    public partial class MoceanApiHistoryValidator : BaseNopValidator<MoceanApiHistoryModel>
    {
        public MoceanApiHistoryValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.RecipientSelection).NotEmpty().WithMessage("Select recipients.");

            RuleFor(x => x.SpecificCustomers).NotEmpty().WithMessage("Recipient is required.");

            RuleFor(x => x.SpecificPhone).NotEmpty().WithMessage("Recipient is required.");

            RuleFor(x => x.Message).NotEmpty().WithMessage("Message is required.");
        }
    }
}

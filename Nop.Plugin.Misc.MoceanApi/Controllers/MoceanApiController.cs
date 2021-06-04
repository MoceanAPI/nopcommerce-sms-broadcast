using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Customers;
using Nop.Plugin.Misc.MoceanApi.Domain;
using Nop.Plugin.Misc.MoceanApi.Models;
using Nop.Plugin.Misc.MoceanApi.Services;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Stores;
using Nop.Services.Tasks;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Models.Extensions;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Misc.MoceanApi.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class MoceanApiController : BasePluginController
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IScheduleTaskService _scheduleTaskService;
        private readonly ISettingService _settingService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IStoreContext _storeContext;
        private readonly IStoreService _storeService;
        private readonly ICustomerService _customerService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IMoceanApiService _moceanApiService;

        #endregion

        #region Ctor

        public MoceanApiController(
            ILocalizationService localizationService,
            INotificationService notificationService,
            IScheduleTaskService scheduleTaskService,
            ISettingService settingService,
            IStaticCacheManager cacheManager,
            IStoreContext storeContext,
            IStoreService storeService,
            ICustomerService customerService,
            IGenericAttributeService genericAttributeService,
            IMoceanApiService moceanApiService)
        {
            _localizationService = localizationService;
            _notificationService = notificationService;
            _scheduleTaskService = scheduleTaskService;
            _settingService = settingService;
            _staticCacheManager = cacheManager;
            _storeContext = storeContext;
            _storeService = storeService;
            _customerService = customerService;
            _genericAttributeService = genericAttributeService;
            _moceanApiService = moceanApiService;
        }

        #endregion

        #region Methods

        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public async Task<IActionResult> Configure()
        {
            //load settings for a chosen store scope
            var storeId = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var moceanApiSettings = await _settingService.LoadSettingAsync<MoceanApiSettings>(storeId);
            
            var currencyUnit =  _moceanApiService.MoceanGetPricing(moceanApiSettings.ApiKey, moceanApiSettings.ApiSecret);
            var creditBalance = currencyUnit + " " + _moceanApiService.MoceanGetCredit(moceanApiSettings.ApiKey, moceanApiSettings.ApiSecret);

            //prepare model
            var model = new ConfigurationModel
            {
                ApiKey = moceanApiSettings.ApiKey,
                ApiSecret = moceanApiSettings.ApiSecret,
                MessageFrom = moceanApiSettings.MessageFrom,
                CreditBalance = creditBalance,
            };

            return View("~/Plugins/Misc.MoceanApi/Views/Configure.cshtml", model);
        }

        [HttpPost, ActionName("Configure")]
        [FormValueRequired("save")]
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public async Task<IActionResult> Configure(ConfigurationModel model)
        {
            if (!ModelState.IsValid)
                return await Configure();

            //load settings for a chosen store scope
            var storeId = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var moceanApiSettings = await _settingService.LoadSettingAsync<MoceanApiSettings>(storeId);

            //save settings
            moceanApiSettings.ApiKey = model.ApiKey.Trim();
            moceanApiSettings.ApiSecret = model.ApiSecret.Trim();
            moceanApiSettings.MessageFrom = model.MessageFrom.Trim();
            await _settingService.SaveSettingAsync(moceanApiSettings, x => x.ApiKey, clearCache: false);
            await _settingService.SaveSettingAsync(moceanApiSettings, x => x.ApiSecret, clearCache: false);
            await _settingService.SaveSettingAsync(moceanApiSettings, x => x.MessageFrom, clearCache: false);
            await _settingService.ClearCacheAsync();

            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

            return await Configure();
        }


        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public async Task<IActionResult> Broadcast()
        {
            //load settings for a chosen store scope
            var storeId = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var moceanApiSettings = await _settingService.LoadSettingAsync<MoceanApiSettings>(storeId);

            var model = new MoceanApiHistoryModel { };

            model.RecipientsSelectList.Add(new SelectListItem { Text = "*", Value = "" });
            model.RecipientsSelectList.Add(new SelectListItem { Text = "All Customers", Value = "all" });
            model.RecipientsSelectList.Add(new SelectListItem { Text = "Specific Customers", Value = "spec_cust" });
            model.RecipientsSelectList.Add(new SelectListItem { Text = "Specific Phone Numbers", Value = "spec_phone" });

            //stores
            model.AvailableRecipients.Add(new SelectListItem { Text = "*", Value = "" });
            foreach (var r in await _customerService.GetAllCustomersAsync())
            {
                var phone = await _genericAttributeService.GetAttributeAsync<string>(r, NopCustomerDefaults.PhoneAttribute);
                
                if (!string.IsNullOrEmpty(phone))
                {
                    var firstName = await _genericAttributeService.GetAttributeAsync<string>(r, NopCustomerDefaults.FirstNameAttribute);
                    var lastName = await _genericAttributeService.GetAttributeAsync<string>(r, NopCustomerDefaults.LastNameAttribute);
                    var fullName = firstName + " " + lastName;
                    model.AvailableRecipients.Add(new SelectListItem { Text = r.Username + " (" + fullName + ", " + phone + ")", Value = phone });
                }
            }

            return View("~/Plugins/Misc.MoceanApi/Views/Broadcast.cshtml", model);
        }

        [HttpPost, ActionName("Broadcast")]
        [FormValueRequired("send")]
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public async Task<IActionResult> Broadcast(MoceanApiHistoryModel model)
        {
            //load settings for a chosen store scope
            var storeId = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var moceanApiSettings = await _settingService.LoadSettingAsync<MoceanApiSettings>(storeId);
            var recipients = new List<string>();
            var allSent = true;

            if (model.RecipientSelection == "all")
            {
                foreach (var r in await _customerService.GetAllCustomersAsync())
                {
                    var phone = await _genericAttributeService.GetAttributeAsync<string>(r, NopCustomerDefaults.PhoneAttribute);
                    if (!string.IsNullOrEmpty(phone))
                        recipients.Add(phone);
                }
            }
            else if (model.RecipientSelection == "spec_cust")
            {
                recipients.Add(model.SpecificCustomers);
            }
            else if (model.RecipientSelection == "spec_phone")
            {
                string[] phone = (model.SpecificPhone).Split(' ');

                foreach (var p in phone)
                {
                    recipients.Add(p);
                }
            }

            foreach (var recipient in recipients)
            {
                //send sms
                var response = _moceanApiService.MoceanBroadcast(moceanApiSettings.ApiKey, moceanApiSettings.ApiSecret, recipient, moceanApiSettings.MessageFrom, model.Message);
                var status = "";

                if (response == "Message sent")
                    status = "Success";
                else
                {
                    status = "Fail";
                    allSent = false;
                }

                //store sms history
                await _moceanApiService.InsertMoceanApiHistoryAsync(new MoceanApiHistory
                {
                    Sender = moceanApiSettings.MessageFrom,
                    Date = (DateTime.Now).ToString(),
                    Message = model.Message,
                    Recipient = recipient,
                    Response = response,
                    Status = status,
                });
            }

            if (allSent)
                _notificationService.SuccessNotification("All SMS sent.");
            else
                _notificationService.ErrorNotification("Error occured during SMS sending, check SMS transaction history for more details.");

            ViewBag.RefreshPage = true;

            return await Broadcast();
        }

        [HttpGet, ActionName("History")]
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public IActionResult History()
        {
            SearchModel searchModel = new SearchModel{ };

            searchModel.SetGridPageSize();

            return View("~/Plugins/Misc.MoceanApi/Views/History.cshtml", searchModel);
        }

        [HttpPost, ActionName("MoceanApiHistoryList")]
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public async Task<IActionResult> MoceanApiHistoryList(SearchModel searchModel, SearchModel filter)
        {
            var records = await _moceanApiService.FindRecordsAsync(
              pageIndex: searchModel.Page - 1,
              pageSize: searchModel.PageSize,
              sender: filter.SearchSender,
              date: filter.SearchDate,
              message: filter.SearchMessage,
              recipient: filter.SearchRecipient,
              response: filter.SearchResponse,
              status: filter.SearchStatus
              );

            var gridModel = await new MoceanApiHistoryListModel().PrepareToGridAsync(searchModel, records, () =>
            {
                return records.SelectAwait(async record =>
                {
                    var model = new MoceanApiHistoryModel
                    {
                        Id = record.Id,
                        Sender = !string.IsNullOrEmpty(record.Sender) ? record.Sender : "*",
                        Date = !string.IsNullOrEmpty(record.Date) ? record.Date : "*",
                        Message = !string.IsNullOrEmpty(record.Message) ? record.Message : "*",
                        Recipient = !string.IsNullOrEmpty(record.Recipient) ? record.Recipient : "*",
                        Response = !string.IsNullOrEmpty(record.Response) ? record.Response : "*",
                        Status = !string.IsNullOrEmpty(record.Status) ? record.Status : "*"
                    };

                    var htmlSb = new StringBuilder("<div>");
                    htmlSb.AppendFormat("{0}: {1}",
                        await _localizationService.GetResourceAsync("Sender"),
                        model.Sender);
                    htmlSb.Append("<br />");
                    htmlSb.AppendFormat("{0}: {1}",
                        await _localizationService.GetResourceAsync("Date"),
                        model.Date);
                    htmlSb.Append("<br />");
                    htmlSb.AppendFormat("{0}: {1}",
                        await _localizationService.GetResourceAsync("Message"),
                        model.Message);
                    htmlSb.Append("<br />");
                    htmlSb.AppendFormat("{0}: {1}",
                        await _localizationService.GetResourceAsync("Recipient"),
                        model.Recipient);
                    htmlSb.Append("<br />");
                    htmlSb.AppendFormat("{0}: {1}",
                        await _localizationService.GetResourceAsync("Response"),
                        model.Response);
                    htmlSb.Append("<br />");
                    htmlSb.AppendFormat("{0}: {1}",
                        await _localizationService.GetResourceAsync("Status"),
                        model.Status);

                    htmlSb.Append("</div>");
                    model.DataHtml = htmlSb.ToString();

                    return model;
                });
            });

            return Json(gridModel);
        }

        #endregion
    }
}
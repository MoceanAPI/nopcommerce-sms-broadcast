using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using Nop.Core;
using Nop.Core.Domain.Cms;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Services.Tasks;
using Nop.Web.Framework.Menu;
using Task = System.Threading.Tasks.Task;

namespace Nop.Plugin.Misc.MoceanApi
{
    /// <summary>
    /// Represents the MoceanApi plugin 
    /// </summary>
    public class MoceanApiPlugin : BasePlugin, IMiscPlugin, IAdminMenuPlugin
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;
        private readonly WidgetSettings _widgetSettings;

        #endregion

        #region Ctor

        public MoceanApiPlugin(ILocalizationService localizationService,
            ISettingService settingService,
            IWebHelper webHelper,
            WidgetSettings widgetSettings)
        {
            _localizationService = localizationService;
            _settingService = settingService;
            _webHelper = webHelper;
            _widgetSettings = widgetSettings;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/MoceanApi/Configure";
        }

        public Task ManageSiteMapAsync(SiteMapNode rootNode)
        {
            var menuItem = new SiteMapNode()
            {
                SystemName = "MoceanAPI",
                Title = "MoceanApi",
                Visible = true,
                IconClass = "far fa-comment",
                ActionName = "List",
                RouteValues = new RouteValueDictionary() { { "area", "Admin" } },
            };

            var subMenuItem1 = new SiteMapNode()
            {
                SystemName = "Broadcast",
                Title = "SMS Broadcast",
                ControllerName = "MoceanApi",
                ActionName = "Broadcast",
                Visible = true,
                IconClass = "far fa-dot-circle",
                RouteValues = new RouteValueDictionary() { { "area", "Admin" } },
            };

            var subMenuItem2 = new SiteMapNode()
            {
                SystemName = "History",
                Title = "SMS Transaction History",
                ControllerName = "MoceanApi",
                ActionName = "History",
                Visible = true,
                IconClass = "far fa-dot-circle",
                RouteValues = new RouteValueDictionary() { { "area", "Admin" } },
            };

            menuItem.ChildNodes.Add(subMenuItem1);
            menuItem.ChildNodes.Add(subMenuItem2);

            var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Home");
            if (pluginNode != null)
                pluginNode.ChildNodes.Add(menuItem);
            else
                rootNode.ChildNodes.Add(menuItem);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Install the plugin
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public override async Task InstallAsync()
        {
            if (!_widgetSettings.ActiveWidgetSystemNames.Contains(MoceanApiDefaults.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Add(MoceanApiDefaults.SystemName);
                await _settingService.SaveSettingAsync(_widgetSettings);
            }

            //locales
            await _localizationService.AddLocaleResourceAsync(new Dictionary<string, string>
            {
                ["Plugins.Misc.MoceanApi.Fields.AccountInfo"] = "Account information",
                ["Plugins.Misc.MoceanApi.Fields.AccountInfo.Hint"] = "Display Mocean account information.",
                ["Plugins.Misc.MoceanApi.Fields.ApiKey"] = "Api Key",
                ["Plugins.Misc.MoceanApi.Fields.ApiKey.Hint"] = "Enter your Mocean account Api key.",
                ["Plugins.Misc.MoceanApi.Fields.ApiSecret"] = "Api Secret",
                ["Plugins.Misc.MoceanApi.Fields.ApiSecret.Hint"] = "Enter your Mocean account Api secret.",
                ["Plugins.Misc.MoceanApi.Fields.MessageFrom"] = "Message From",
                ["Plugins.Misc.MoceanApi.Fields.MessageFrom.Hint"] = "Sender of the SMS when the message is received at a mobile phone.",
                ["Plugins.Misc.MoceanApi.Fields.CreditBalance"] = "Credit Balance",
                ["Plugins.Misc.MoceanApi.Fields.CreditBalance.Hint"] = "Mocean account credit balance.",
                ["Plugins.Misc.MoceanApi.Fields.Sender"] = "Sender",
                ["Plugins.Misc.MoceanApi.Fields.Date"] = "Date",
                ["Plugins.Misc.MoceanApi.Fields.Message"] = "Message",
                ["Plugins.Misc.MoceanApi.Fields.Recipient"] = "Recipient",
                ["Plugins.Misc.MoceanApi.Fields.Response"] = "Response",
                ["Plugins.Misc.MoceanApi.Fields.Status"] = "Status",
                ["Plugins.Misc.MoceanApi.Fields.DataHtml"] = "DataHtml",
                ["Plugins.Misc.MoceanApi.Fields.RecipientSelection"] = "Recipient",
                ["Plugins.Misc.MoceanApi.Fields.SpecificCustomers"] = "Specific Customers",
                ["Plugins.Misc.MoceanApi.Fields.SpecificPhone"] = "Specific Phone Numbers",
                ["Plugins.Misc.MoceanApi.Fields.SpecificPhone.Hint"] = "Use space as delimiter, eg. 60123456789 60123456788",
            });
            await base.InstallAsync();
        }

        /// <summary>
        /// Uninstall the plugin
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public override async Task UninstallAsync()
        {
            //settings
            if (_widgetSettings.ActiveWidgetSystemNames.Contains(MoceanApiDefaults.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Remove(MoceanApiDefaults.SystemName);
                await _settingService.SaveSettingAsync(_widgetSettings);
            }
            await _settingService.DeleteSettingAsync<MoceanApiSettings>();

            //locales
            await _localizationService.DeleteLocaleResourcesAsync("Plugins.Misc.MoceanApi");

            await base.UninstallAsync();
        }

        #endregion
    }
}
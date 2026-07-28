using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Cms;
using Nop.Plugin.Widgets.ScrollingAnnouncement.Components;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Framework.Menu;

namespace Nop.Plugin.Widgets.ScrollingAnnouncement
{
    /// <summary>
    /// Kayan Yazı Duyuru Bandı Ana Eklenti Sınıfı.
    /// </summary>
    public class ScrollingAnnouncementPlugin : BasePlugin, IWidgetPlugin, IAdminMenuPlugin
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly IWebHelper _webHelper;
        private readonly ISettingService _settingService;
        private readonly WidgetSettings _widgetSettings;

        #endregion

        #region Ctor

        public ScrollingAnnouncementPlugin(
            ILocalizationService localizationService,
            IWebHelper webHelper,
            ISettingService settingService,
            WidgetSettings widgetSettings)
        {
            _localizationService = localizationService;
            _webHelper = webHelper;
            _settingService = settingService;
            _widgetSettings = widgetSettings;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Admin panelinde widget listesinde gizlenip gizlenmeyeceğini belirtir.
        /// </summary>
        public bool HideInWidgetList => false;

        #endregion

        #region Methods

        /// <summary>
        /// Duyuru widget'ının gösterileceği konumu TEK bir alan olarak belirler (Header sonrası / Menü üstü).
        /// </summary>
        public Task<IList<string>> GetWidgetZonesAsync()
        {
            return Task.FromResult<IList<string>>(new List<string>
            {
                PublicWidgetZones.HeaderAfter
            });
        }

        /// <summary>
        /// Widget zone çağrıldığında tetiklenecek ViewComponent türünü döndürür (nopCommerce 4.90).
        /// </summary>
        public Type GetWidgetViewComponent(string widgetZone)
        {
            return typeof(ScrollingAnnouncementViewComponent);
        }

        /// <summary>
        /// Widget zone çağrıldığında tetiklenecek ViewComponent adını döndürür (nopCommerce 4.5.x uyumu).
        /// </summary>
        public string GetWidgetViewComponentName(string widgetZone)
        {
            return "ScrollingAnnouncement";
        }

        /// <summary>
        /// Admin panelinde eklentiye ait yapılandırma sayfasının URL'ini verir.
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return _webHelper.GetStoreLocation() + "Admin/WidgetsScrollingAnnouncement/Configure";
        }

        /// <summary>
        /// Admin Paneli Yan Menüsüne Özel Menü Elemanı ekler.
        /// </summary>
        public async Task ManageSiteMapAsync(AdminMenuItem rootNode)
        {
            var title = await _localizationService.GetResourceAsync("Plugins.Widgets.ScrollingAnnouncement.AdminMenuTitle") ?? "Kayan Yazı Duyuruları";

            var pluginNode = new AdminMenuItem
            {
                SystemName = "Widgets.ScrollingAnnouncement",
                Title = title,
                Url = _webHelper.GetStoreLocation() + "Admin/WidgetsScrollingAnnouncement/Configure",
                IconClass = "far fa-dot-circle",
                Visible = true
            };

            rootNode.ChildNodes.Add(pluginNode);
        }

        /// <summary>
        /// Eklenti Kurulumu: Dil kaynaklarını yükler ve Widget'ı aktif hale getirir.
        /// </summary>
        public override async Task InstallAsync()
        {
            await _localizationService.AddOrUpdateLocaleResourceAsync(
                "Plugins.Widgets.ScrollingAnnouncement.AdminMenuTitle",
                "Kayan Yazı Duyuruları"
            );
            await _localizationService.AddOrUpdateLocaleResourceAsync(
                "Plugins.Widgets.ScrollingAnnouncement.Fields.Text",
                "Duyuru Metni"
            );
            await _localizationService.AddOrUpdateLocaleResourceAsync(
                "Plugins.Widgets.ScrollingAnnouncement.Fields.Text.Hint",
                "Kayan yazıda görünecek metni giriniz."
            );
            await _localizationService.AddOrUpdateLocaleResourceAsync(
                "Plugins.Widgets.ScrollingAnnouncement.Fields.Color",
                "Metin/Bant Rengi"
            );
            await _localizationService.AddOrUpdateLocaleResourceAsync(
                "Plugins.Widgets.ScrollingAnnouncement.Fields.Color.Hint",
                "Duyurunun HEX renk kodu (örn: #ff3300 veya darkred)."
            );
            await _localizationService.AddOrUpdateLocaleResourceAsync(
                "Plugins.Widgets.ScrollingAnnouncement.Fields.DisplayOrder",
                "Gösterim Sırası"
            );
            await _localizationService.AddOrUpdateLocaleResourceAsync(
                "Plugins.Widgets.ScrollingAnnouncement.Fields.IsActive",
                "Aktif mi?"
            );

            if (!_widgetSettings.ActiveWidgetSystemNames.Contains(PluginDescriptor.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Add(PluginDescriptor.SystemName);
                await _settingService.SaveSettingAsync(_widgetSettings);
            }

            await base.InstallAsync();
        }

        /// <summary>
        /// Eklenti Kaldırılması: Dil kaynaklarını veritabanından temizler.
        /// </summary>
        public override async Task UninstallAsync()
        {
            if (_widgetSettings.ActiveWidgetSystemNames.Contains(PluginDescriptor.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Remove(PluginDescriptor.SystemName);
                await _settingService.SaveSettingAsync(_widgetSettings);
            }

            await _localizationService.DeleteLocaleResourcesAsync("Plugins.Widgets.ScrollingAnnouncement");
            await base.UninstallAsync();
        }

        #endregion
    }
}

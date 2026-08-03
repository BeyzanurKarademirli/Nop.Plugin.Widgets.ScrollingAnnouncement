using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Duyuru widget'ının gösterileceği konumu ana resmin/slider'ın hemen üstü (HeaderMenuAfter) olarak belirler.
        /// </summary>
        public Task<IList<string>> GetWidgetZonesAsync()
        {
            return Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.HeaderMenuAfter });
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
        public async Task ManageSiteMapAsync(SiteMapNode rootNode)
        {
            var title = await _localizationService.GetResourceAsync("Plugins.Widgets.ScrollingAnnouncement.AdminMenuTitle") ?? "Kayan Yazı Duyuruları";

            var pluginNode = new SiteMapNode
            {
                SystemName = "Widgets.ScrollingAnnouncement",
                Title = title,
                ControllerName = "WidgetsScrollingAnnouncement",
                ActionName = "Configure",
                IconClass = "far fa-dot-circle",
                Visible = true
            };

            var configNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Configuration");
            if (configNode != null)
            {
                configNode.ChildNodes.Add(pluginNode);
            }
            else
            {
                rootNode.ChildNodes.Add(pluginNode);
            }
        }

        /// <summary>
        /// Eklenti Kurulumu: Tüm dil kaynaklarını yükler ve Widget'ı aktif hale getirir.
        /// </summary>
        public override async Task InstallAsync()
        {
            await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
            {
                ["Plugins.Widgets.ScrollingAnnouncement.AdminMenuTitle"] = "Kayan Yazı Duyuruları",
                ["Plugins.Widgets.ScrollingAnnouncement.PageTitle"] = "Kayan Yazı Duyuruları Yapılandırması",
                ["Plugins.Widgets.ScrollingAnnouncement.Edit"] = "Duyuru Düzenle #{0}",
                ["Plugins.Widgets.ScrollingAnnouncement.AddNew"] = "Yeni Duyuru Ekle",
                ["Plugins.Widgets.ScrollingAnnouncement.ListTitle"] = "Mevcut Duyurular Listesi",
                ["Plugins.Widgets.ScrollingAnnouncement.GeneralSettings"] = "Genel Ayarlar (Arka Plan)",
                ["Plugins.Widgets.ScrollingAnnouncement.Fields.BackgroundColor"] = "Genel Arka Plan Rengi",
                ["Plugins.Widgets.ScrollingAnnouncement.Fields.BackgroundColor.Hint"] = "Tüm duyuru bandının arkasında yer alan genel arka plan rengini belirler.",
                ["Plugins.Widgets.ScrollingAnnouncement.Fields.Text"] = "Duyuru Metni",
                ["Plugins.Widgets.ScrollingAnnouncement.Fields.Text.Hint"] = "Kayan yazıda görünecek duyuru metnini giriniz.",
                ["Plugins.Widgets.ScrollingAnnouncement.Fields.Color"] = "Duyuru Yazı Rengi",
                ["Plugins.Widgets.ScrollingAnnouncement.Fields.Color.Hint"] = "Bu duyuruya özel HEX renk kodunu seçiniz (örn: #ef4444 veya #22c55e).",
                ["Plugins.Widgets.ScrollingAnnouncement.Fields.DisplayOrder"] = "Gösterim Sırası",
                ["Plugins.Widgets.ScrollingAnnouncement.Fields.DisplayOrder.Hint"] = "Duyurunun listedeki sıralama değerini giriniz.",
                ["Plugins.Widgets.ScrollingAnnouncement.Fields.IsActive"] = "Aktif mi?",
                ["Plugins.Widgets.ScrollingAnnouncement.Fields.IsActive.Hint"] = "Duyurunun yayında olup olmayacağını belirler.",
                ["Plugins.Widgets.ScrollingAnnouncement.IsActive.Yes"] = "Evet",
                ["Plugins.Widgets.ScrollingAnnouncement.Active"] = "Aktif",
                ["Plugins.Widgets.ScrollingAnnouncement.Inactive"] = "Pasif",
                ["Plugins.Widgets.ScrollingAnnouncement.Save"] = "Kaydet",
                ["Plugins.Widgets.ScrollingAnnouncement.Update"] = "Güncelle",
                ["Plugins.Widgets.ScrollingAnnouncement.Cancel"] = "İptal",
                ["Plugins.Widgets.ScrollingAnnouncement.EditButton"] = "Düzenle",
                ["Plugins.Widgets.ScrollingAnnouncement.DeleteButton"] = "Sil",
                ["Plugins.Widgets.ScrollingAnnouncement.DeleteConfirm"] = "Bu duyuruyu silmek istediğinizden emin misiniz?",
                ["Plugins.Widgets.ScrollingAnnouncement.NoRecords"] = "Henüz eklenmiş bir duyuru bulunmamaktadır.",
                ["Plugins.Widgets.ScrollingAnnouncement.TextRequired"] = "Duyuru metni boş bırakılamaz.",
                ["Plugins.Widgets.ScrollingAnnouncement.AddedSuccess"] = "Duyuru başarıyla eklendi.",
                ["Plugins.Widgets.ScrollingAnnouncement.UpdatedSuccess"] = "Duyuru başarıyla güncellendi.",
                ["Plugins.Widgets.ScrollingAnnouncement.DeletedSuccess"] = "Duyuru başarıyla silindi.",
                ["Plugins.Widgets.ScrollingAnnouncement.SettingsSaved"] = "Genel ayarlar başarıyla kaydedildi."
            });

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.ScrollingAnnouncement.Domain;
using Nop.Plugin.Widgets.ScrollingAnnouncement.Models;
using Nop.Plugin.Widgets.ScrollingAnnouncement.Services;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Widgets.ScrollingAnnouncement.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    [AutoValidateAntiforgeryToken]
    public class WidgetsScrollingAnnouncementController : BasePluginController
    {
        #region Fields

        private readonly IAnnouncementService _announcementService;
        private readonly INotificationService _notificationService;
        private readonly ILocalizationService _localizationService;
        private readonly ILogger _logger;
        private readonly ISettingService _settingService;

        #endregion

        #region Ctor

        public WidgetsScrollingAnnouncementController(
            IAnnouncementService announcementService,
            INotificationService notificationService,
            ILocalizationService localizationService,
            ILogger logger,
            ISettingService settingService)
        {
            _announcementService = announcementService;
            _notificationService = notificationService;
            _localizationService = localizationService;
            _logger = logger;
            _settingService = settingService;
        }

        #endregion

        #region Actions

        /// <summary>
        /// Ayar ve Yönetim Sayfası (Configure)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Configure()
        {
            try
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
            }
            catch (Exception ex)
            {
                await _logger.ErrorAsync("Kayan duyuru dil kaynakları yüklenirken hata oluştu.", ex);
            }

            var modelList = new List<AnnouncementModel>();

            try
            {
                var announcements = await _announcementService.GetAllAnnouncementsAsync();
                if (announcements != null)
                {
                    modelList = announcements.Select(a => new AnnouncementModel
                    {
                        Id = a.Id,
                        Text = a.Text,
                        Color = a.Color,
                        DisplayOrder = a.DisplayOrder,
                        IsActive = a.IsActive
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                await _logger.ErrorAsync("Kayan duyuru listesi yüklenirken bir hata oluştu.", ex);
            }

            var settings = await _settingService.LoadSettingAsync<ScrollingAnnouncementSettings>();

            ViewBag.Announcements = modelList;
            ViewBag.BackgroundColor = string.IsNullOrWhiteSpace(settings.BackgroundColor) ? "#FFFDE7" : settings.BackgroundColor;

            var newModel = new AnnouncementModel
            {
                Color = "#ef4444",
                IsActive = true,
                DisplayOrder = 1
            };

            return View("~/Plugins/Widgets.ScrollingAnnouncement/Views/Configure.cshtml", newModel);
        }

        /// <summary>
        /// GENEL ARKA PLAN AYARINI KAYDETME (SaveSettings)
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> SaveSettings(string backgroundColor)
        {
            var settings = await _settingService.LoadSettingAsync<ScrollingAnnouncementSettings>();
            settings.BackgroundColor = string.IsNullOrWhiteSpace(backgroundColor) ? "#FFFDE7" : backgroundColor;
            await _settingService.SaveSettingAsync(settings);

            var successMessage = await _localizationService.GetResourceAsync("Plugins.Widgets.ScrollingAnnouncement.SettingsSaved") ?? "Genel ayarlar başarıyla kaydedildi.";
            _notificationService.SuccessNotification(successMessage);

            return RedirectToAction("Configure");
        }

        /// <summary>
        /// YENİ DUYURU EKLEME (Create)
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create(AnnouncementModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Text))
            {
                var textRequiredMessage = await _localizationService.GetResourceAsync("Plugins.Widgets.ScrollingAnnouncement.TextRequired") ?? "Duyuru metni boş bırakılamaz.";
                _notificationService.ErrorNotification(textRequiredMessage);
                return RedirectToAction("Configure");
            }

            var entity = new Announcement
            {
                Text = model.Text,
                Color = string.IsNullOrWhiteSpace(model.Color) ? "#111111" : model.Color,
                DisplayOrder = model.DisplayOrder,
                IsActive = model.IsActive
            };

            await _announcementService.InsertAnnouncementAsync(entity);
            var successMessage = await _localizationService.GetResourceAsync("Plugins.Widgets.ScrollingAnnouncement.AddedSuccess") ?? "Duyuru başarıyla eklendi.";
            _notificationService.SuccessNotification(successMessage);

            return RedirectToAction("Configure");
        }

        /// <summary>
        /// DUYURU GÜNCELLEME DÜZENLEME (Edit GET)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _announcementService.GetByIdAsync(id);
            if (entity == null)
                return RedirectToAction("Configure");

            var model = new AnnouncementModel
            {
                Id = entity.Id,
                Text = entity.Text,
                Color = entity.Color,
                DisplayOrder = entity.DisplayOrder,
                IsActive = entity.IsActive
            };

            var modelList = new List<AnnouncementModel>();
            try
            {
                var announcements = await _announcementService.GetAllAnnouncementsAsync();
                if (announcements != null)
                {
                    modelList = announcements.Select(a => new AnnouncementModel
                    {
                        Id = a.Id,
                        Text = a.Text,
                        Color = a.Color,
                        DisplayOrder = a.DisplayOrder,
                        IsActive = a.IsActive
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                await _logger.ErrorAsync("Kayan duyuru listesi yüklenirken bir hata oluştu.", ex);
            }

            var settings = await _settingService.LoadSettingAsync<ScrollingAnnouncementSettings>();

            ViewBag.Announcements = modelList;
            ViewBag.BackgroundColor = string.IsNullOrWhiteSpace(settings.BackgroundColor) ? "#FFFDE7" : settings.BackgroundColor;

            return View("~/Plugins/Widgets.ScrollingAnnouncement/Views/Configure.cshtml", model);
        }

        /// <summary>
        /// DUYURU GÜNCELLEME KAYDETME (Edit POST)
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Edit(AnnouncementModel model)
        {
            var entity = await _announcementService.GetByIdAsync(model.Id);
            if (entity == null)
                return RedirectToAction("Configure");

            if (string.IsNullOrWhiteSpace(model.Text))
            {
                var textRequiredMessage = await _localizationService.GetResourceAsync("Plugins.Widgets.ScrollingAnnouncement.TextRequired") ?? "Duyuru metni boş bırakılamaz.";
                _notificationService.ErrorNotification(textRequiredMessage);
                return RedirectToAction("Configure");
            }

            entity.Text = model.Text;
            entity.Color = string.IsNullOrWhiteSpace(model.Color) ? "#111111" : model.Color;
            entity.DisplayOrder = model.DisplayOrder;
            entity.IsActive = model.IsActive;

            await _announcementService.UpdateAnnouncementAsync(entity);
            var successMessage = await _localizationService.GetResourceAsync("Plugins.Widgets.ScrollingAnnouncement.UpdatedSuccess") ?? "Duyuru başarıyla güncellendi.";
            _notificationService.SuccessNotification(successMessage);

            return RedirectToAction("Configure");
        }

        /// <summary>
        /// DUYURU SİLME (Delete)
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _announcementService.GetByIdAsync(id);
            if (entity != null)
            {
                await _announcementService.DeleteAnnouncementAsync(entity);
                var successMessage = await _localizationService.GetResourceAsync("Plugins.Widgets.ScrollingAnnouncement.DeletedSuccess") ?? "Duyuru başarıyla silindi.";
                _notificationService.SuccessNotification(successMessage);
            }

            return RedirectToAction("Configure");
        }

        #endregion
    }
}

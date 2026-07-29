using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.ScrollingAnnouncement.Domain;
using Nop.Plugin.Widgets.ScrollingAnnouncement.Models;
using Nop.Plugin.Widgets.ScrollingAnnouncement.Services;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Widgets.ScrollingAnnouncement.Controllers
{
    [AuthorizeAdmin]
    [Area("Admin")]
    [AutoValidateAntiforgeryToken]
    public class WidgetsScrollingAnnouncementController : BasePluginController
    {
        #region Fields

        private readonly IAnnouncementService _announcementService;
        private readonly INotificationService _notificationService;
        private readonly ILocalizationService _localizationService;

        #endregion

        #region Ctor

        public WidgetsScrollingAnnouncementController(
            IAnnouncementService announcementService,
            INotificationService notificationService,
            ILocalizationService localizationService)
        {
            _announcementService = announcementService;
            _notificationService = notificationService;
            _localizationService = localizationService;
        }

        #endregion

        #region Actions

        /// <summary>
        /// Ayar ve Yönetim Sayfası (Configure)
        /// </summary>
        public async Task<IActionResult> Configure()
        {
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
            catch
            {
                // Tablo veritabanında hazır değilse boş liste döner
            }

            ViewBag.Announcements = modelList;

            var newModel = new AnnouncementModel
            {
                Color = "#4a0e17",
                IsActive = true,
                DisplayOrder = 1
            };

            return View("~/Plugins/Widgets.ScrollingAnnouncement/Views/Configure.cshtml", newModel);
        }

        /// <summary>
        /// YENİ DUYURU EKLEME (Create)
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create(AnnouncementModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Text))
            {
                _notificationService.ErrorNotification("Duyuru metni boş bırakılamaz.");
                return RedirectToAction("Configure");
            }

            var entity = new Announcement
            {
                Text = model.Text,
                Color = string.IsNullOrWhiteSpace(model.Color) ? "#4a0e17" : model.Color,
                DisplayOrder = model.DisplayOrder,
                IsActive = model.IsActive
            };

            await _announcementService.InsertAnnouncementAsync(entity);
            _notificationService.SuccessNotification("Duyuru başarıyla eklendi.");

            return RedirectToAction("Configure");
        }

        /// <summary>
        /// DUYURU GÜNCELLEME DÜZENLEME (Edit GET)
        /// </summary>
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
            catch
            {
            }

            ViewBag.Announcements = modelList;

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

            entity.Text = model.Text;
            entity.Color = string.IsNullOrWhiteSpace(model.Color) ? "#4a0e17" : model.Color;
            entity.DisplayOrder = model.DisplayOrder;
            entity.IsActive = model.IsActive;

            await _announcementService.UpdateAnnouncementAsync(entity);
            _notificationService.SuccessNotification("Duyuru başarıyla güncellendi.");

            return RedirectToAction("Configure");
        }

        /// <summary>
        /// DUYURU SİLME (Delete)
        /// </summary>
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _announcementService.GetByIdAsync(id);
            if (entity != null)
            {
                await _announcementService.DeleteAnnouncementAsync(entity);
                _notificationService.SuccessNotification("Duyuru başarıyla silindi.");
            }

            return RedirectToAction("Configure");
        }

        #endregion
    }
}

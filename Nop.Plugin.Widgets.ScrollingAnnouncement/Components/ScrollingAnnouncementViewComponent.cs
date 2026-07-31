using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.ScrollingAnnouncement.Domain;
using Nop.Plugin.Widgets.ScrollingAnnouncement.Models;
using Nop.Plugin.Widgets.ScrollingAnnouncement.Services;
using Nop.Services.Localization;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.ScrollingAnnouncement.Components
{
    /// <summary>
    /// Sitenin ön yüzünde ana sayfa üst bölümünde kayan duyuruları InvokeAsync ile gösteren görsel bileşen.
    /// </summary>
    public class ScrollingAnnouncementViewComponent : NopViewComponent
    {
        #region Fields

        private readonly IAnnouncementService _announcementService;
        private readonly ILocalizationService _localizationService;
        private readonly ScrollingAnnouncementSettings _scrollingAnnouncementSettings;

        #endregion

        #region Ctor

        public ScrollingAnnouncementViewComponent(
            IAnnouncementService announcementService,
            ILocalizationService localizationService,
            ScrollingAnnouncementSettings scrollingAnnouncementSettings)
        {
            _announcementService = announcementService;
            _localizationService = localizationService;
            _scrollingAnnouncementSettings = scrollingAnnouncementSettings;
        }

        #endregion

        #region Methods

        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            var announcements = await _announcementService.GetActiveAnnouncementsAsync();

            var modelList = announcements.Select(a => new AnnouncementModel
            {
                Id = a.Id,
                Text = a.Text,
                Color = a.Color,
                DisplayOrder = a.DisplayOrder,
                IsActive = a.IsActive
            }).ToList();

            ViewData["BackgroundColor"] = string.IsNullOrWhiteSpace(_scrollingAnnouncementSettings.BackgroundColor)
                ? "#1F1F2E"
                : _scrollingAnnouncementSettings.BackgroundColor;

            return View("~/Plugins/Widgets.ScrollingAnnouncement/Views/Default.cshtml", modelList);
        }

        #endregion
    }
}

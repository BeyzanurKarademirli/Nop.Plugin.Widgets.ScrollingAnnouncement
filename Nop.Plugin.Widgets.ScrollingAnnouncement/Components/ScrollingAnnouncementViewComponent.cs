using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.ScrollingAnnouncement.Models;
using Nop.Plugin.Widgets.ScrollingAnnouncement.Services;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.ScrollingAnnouncement.Components
{
    /// <summary>
    /// Sitenin ön yüzünde (Homepage) kayan duyuruları InvokeAsync ile gösteren görsel bileşen.
    /// </summary>
    public class ScrollingAnnouncementViewComponent : NopViewComponent
    {
        #region Fields

        private readonly IAnnouncementService _announcementService;

        #endregion

        #region Ctor

        public ScrollingAnnouncementViewComponent(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
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

            return View("~/Plugins/Widgets.ScrollingAnnouncement/Views/Default.cshtml", modelList);
        }

        #endregion
    }
}

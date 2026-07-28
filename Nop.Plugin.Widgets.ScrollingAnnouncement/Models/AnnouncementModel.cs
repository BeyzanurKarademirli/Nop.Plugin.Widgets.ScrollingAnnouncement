using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.ScrollingAnnouncement.Models
{
    /// <summary>
    /// Admin panelinde ekleme, silme ve güncelleme formlarında kullanılan duyuru modeli.
    /// </summary>
    public record AnnouncementModel : BaseNopEntityModel
    {
        #region Properties

        [NopResourceDisplayName("Plugins.Widgets.ScrollingAnnouncement.Fields.Text")]
        public string Text { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.ScrollingAnnouncement.Fields.Color")]
        public string Color { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.ScrollingAnnouncement.Fields.DisplayOrder")]
        public int DisplayOrder { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.ScrollingAnnouncement.Fields.IsActive")]
        public bool IsActive { get; set; }

        #endregion
    }
}

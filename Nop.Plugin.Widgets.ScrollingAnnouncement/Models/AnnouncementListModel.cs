using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.ScrollingAnnouncement.Models
{
    /// <summary>
    /// Admin panelindeki DataTables / Grid tablosunda duyuruları listelemek için model.
    /// </summary>
    public record AnnouncementListModel : BasePagedListModel<AnnouncementModel>
    {
    }
}

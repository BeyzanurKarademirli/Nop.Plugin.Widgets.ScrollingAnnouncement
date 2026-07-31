using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.ScrollingAnnouncement.Domain
{
    /// <summary>
    /// Eklenti genel ayarlarını saklayan sınıf.
    /// </summary>
    public class ScrollingAnnouncementSettings : ISettings
    {
        /// <summary>
        /// Tüm kayan bant için genel arka plan rengi (varsayılan #FFFDE7)
        /// </summary>
        public string BackgroundColor { get; set; } = "#FFFDE7";
    }
}

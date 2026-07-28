using Nop.Core;

namespace Nop.Plugin.Widgets.ScrollingAnnouncement.Domain
{
    /// <summary>
    /// Kayan yazı duyurularının veritabanı tablo yapısını (Entity) temsil eden sınıf.
    /// </summary>
    public class Announcement : BaseEntity
    {
        #region Properties

        /// <summary>
        /// Duyuru metni (Kayan yazıda görüntülenecek metin)
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Duyuru metninin veya bandının rengi (HEX veya CSS renk kodu)
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Ekranda gösterim sırası
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Duyurunun yayında (aktif) veya pasif olduğunu belirtir
        /// </summary>
        public bool IsActive { get; set; }

        #endregion
    }
}

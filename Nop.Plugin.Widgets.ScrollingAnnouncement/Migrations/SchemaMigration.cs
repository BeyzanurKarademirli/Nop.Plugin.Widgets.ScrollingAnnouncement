using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.ScrollingAnnouncement.Domain;

namespace Nop.Plugin.Widgets.ScrollingAnnouncement.Migrations
{
    /// <summary>
    /// Eklenti yüklendiğinde Announcement veritabanı tablosunu FluentMigrator ile oluşturan migration sınıfı.
    /// Eklenti kaldırıldığında tablo otomatik silinir (AutoReversingMigration).
    /// </summary>
    [NopMigration("2026/07/27 18:00:00", "ScrollingAnnouncement Tablo Oluşturma")]
    public class SchemaMigration : AutoReversingMigration
    {
        #region Methods

        /// <summary>
        /// Migration çalıştırıldığında (Eklenti Kurulumunda) tabloyu oluşturur.
        /// </summary>
        public override void Up()
        {
            Create.TableFor<Announcement>();
        }

        #endregion
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Plugin.Widgets.ScrollingAnnouncement.Domain;

namespace Nop.Plugin.Widgets.ScrollingAnnouncement.Services
{
    /// <summary>
    /// Kayan yazı duyurularının veritabanı CRUD işlemlerini yürüten servis arayüzü.
    /// </summary>
    public interface IAnnouncementService
    {
        Task<IList<Announcement>> GetActiveAnnouncementsAsync();
        Task<IList<Announcement>> GetAllAnnouncementsAsync();
        Task<Announcement> GetByIdAsync(int id);
        Task InsertAnnouncementAsync(Announcement announcement);
        Task UpdateAnnouncementAsync(Announcement announcement);
        Task DeleteAnnouncementAsync(Announcement announcement);
    }
}

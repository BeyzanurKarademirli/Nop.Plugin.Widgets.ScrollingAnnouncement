using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Data;
using Nop.Plugin.Widgets.ScrollingAnnouncement.Domain;

namespace Nop.Plugin.Widgets.ScrollingAnnouncement.Services
{
    /// <summary>
    /// IRepository kullanarak veritabanı CRUD işlemlerini gerçekleştiren servis sınıfı.
    /// </summary>
    public class AnnouncementService : IAnnouncementService
    {
        #region Fields

        private readonly IRepository<Announcement> _announcementRepository;

        #endregion

        #region Ctor

        public AnnouncementService(IRepository<Announcement> announcementRepository)
        {
            _announcementRepository = announcementRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sadece aktif olan duyuruları gösterim sırasına göre getirir.
        /// </summary>
        public async Task<IList<Announcement>> GetActiveAnnouncementsAsync()
        {
            return await _announcementRepository.GetAllAsync(query =>
            {
                return from a in query
                       where a.IsActive
                       orderby a.DisplayOrder
                       select a;
            });
        }

        /// <summary>
        /// Tüm duyuruları gösterim sırasına göre getirir (Admin Paneli için).
        /// </summary>
        public async Task<IList<Announcement>> GetAllAnnouncementsAsync()
        {
            return await _announcementRepository.GetAllAsync(query =>
            {
                return from a in query
                       orderby a.DisplayOrder
                       select a;
            });
        }

        /// <summary>
        /// ID'ye göre duyuruyu getirir.
        /// </summary>
        public async Task<Announcement> GetByIdAsync(int id)
        {
            return await _announcementRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Yeni duyuru ekler.
        /// </summary>
        public async Task InsertAnnouncementAsync(Announcement announcement)
        {
            await _announcementRepository.InsertAsync(announcement);
        }

        /// <summary>
        /// Var olan duyuruyu günceller.
        /// </summary>
        public async Task UpdateAnnouncementAsync(Announcement announcement)
        {
            await _announcementRepository.UpdateAsync(announcement);
        }

        /// <summary>
        /// Duyuruyu veritabanından siler.
        /// </summary>
        public async Task DeleteAnnouncementAsync(Announcement announcement)
        {
            await _announcementRepository.DeleteAsync(announcement);
        }

        #endregion
    }
}

using FirstApi07.DTOs;
using FirstApi07.DTOs.CreateUpdateObjects;
using FirstApi07.DTOs.PatchObject;

namespace FirstApi07.Repositories
{
    public interface IAnnouncementsRepository
    {
        public Task<IEnumerable<Announcement>> GetAnnouncementsAsync();

        public Task<Announcement> GetAnnouncementByIdAsync(Guid id);

        public Task CreateAnnouncementAsync(Announcement announcement);

        public Task<bool> DeleteAnnouncementAsync(Guid id);

        public Task<CreateUpdateAnnouncement> UpdateAnnouncementAsync(Guid id,CreateUpdateAnnouncement announcement);

        public Task<PatchAnnouncement> UpdatePartiallyAnnouncementAsync(Guid id, PatchAnnouncement announcement);


    }
}

using FirstApi07.Services;
using FirstApi07.DTOs;
using FirstApi07.DTOs.CreateUpdateObjects;
using FirstApi07.DTOs.PatchObject;

namespace FirstApi07.Services
{
    public interface IAnnouncementsService
    {
        public Task<IEnumerable<Announcement>> GetAnnouncementsAsync();
        public Task<Announcement> GetAnnouncementsByIdAsync(Guid id);

        public Task CreateAnnouncementAsync(Announcement newAnnouncement);

        public Task<bool> DeleteAnnouncementAsync(Guid id);
        public Task <CreateUpdateAnnouncement> UpdateAnnouncementAsync(Guid id, CreateUpdateAnnouncement announcement);

        public Task<PatchAnnouncement> UpdatePartiallyAnnouncementAsync(Guid id, PatchAnnouncement announcement);
    }
}

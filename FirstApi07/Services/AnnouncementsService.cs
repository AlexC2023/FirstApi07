using FirstApi07.Repositories;
using FirstApi07.DTOs;
using FirstApi07.DataContext;
using FirstApi07.Helpers;
using FirstApi07.DTOs.CreateUpdateObjects;
using FirstApi07.DTOs.PatchObject;

namespace FirstApi07.Services
{
    public class AnnouncementsService: IAnnouncementsService
    {
        private readonly IAnnouncementsRepository _repository;

        public AnnouncementsService(IAnnouncementsRepository repository)
        {
            _repository = repository;
        }

        public async Task<Announcement> GetAnnouncementsByIdAsync(Guid id)
        {
            return await _repository.GetAnnouncementByIdAsync(id);
        }

        public async Task<IEnumerable<Announcement>> GetAnnouncementsAsync()
        { 
         return await _repository.GetAnnouncementsAsync();
        }

        public async Task CreateAnnouncementAsync(Announcement newAnnouncement)
        {
            ValidationFunctions.ExceptionWhenDateIsNotValid(newAnnouncement.ValidFrom, newAnnouncement.ValidTo);
            await _repository.CreateAnnouncementAsync(newAnnouncement);
        }

        public async Task<bool> DeleteAnnouncementAsync(Guid id)
        { 
        return await _repository.DeleteAnnouncementAsync(id);
        }
       public async Task<CreateUpdateAnnouncement> UpdateAnnouncementAsync(Guid id, CreateUpdateAnnouncement announcement)
        {
            ValidationFunctions.ExceptionWhenDateIsNotValid(announcement.ValidFrom, announcement.ValidTo);
            return await _repository.UpdateAnnouncementAsync(id, announcement);
        }
        public async Task<PatchAnnouncement> UpdatePartiallyAnnouncementAsync(Guid id, PatchAnnouncement announcement)
        {
            ValidationFunctions.ExceptionWhenDateIsNotValid(announcement.ValidFrom, announcement.ValidTo);
            return await _repository.UpdatePartiallyAnnouncementAsync(id, announcement);
        }
    }
}

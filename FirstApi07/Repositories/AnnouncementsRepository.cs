using FirstApi07.DTOs;
using FirstApi07.DTOs.CreateUpdateObjects;
using FirstApi07.DataContext;
using Microsoft.EntityFrameworkCore;
using FirstApi07.DTOs.CreateUpdateObjects;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System;
using FirstApi07.DTOs.PatchObject;

namespace FirstApi07.Repositories
{
    public class AnnouncementsRepository : IAnnouncementsRepository
    {
        private readonly ProgrammingClubDataContext _context;
        private readonly IMapper _mapper;
        //private const string name = "alexandra";
        //const -> compile time ->asignata valoarea cand declaram
        //readonly -> runtime time -> asignam valoarea in constructor

       
        public AnnouncementsRepository(ProgrammingClubDataContext context)
        {
            _context = context;
        }

        public AnnouncementsRepository(ProgrammingClubDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Announcement>> GetAnnouncementsAsync()
        {
            return await _context.Announcements.ToListAsync();
        }

        public async Task<Announcement> GetAnnouncementByIdAsync(Guid id)
        {
            return await _context.Announcements.SingleOrDefaultAsync(a => a.IdAnnouncement == id);
        }
        public async Task CreateAnnouncementAsync(Announcement announcement)
        {
            announcement.IdAnnouncement = Guid.NewGuid();
            _context.Announcements.Add(announcement);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAnnouncementAsync(Guid id)
        {
            Announcement announcement = await GetAnnouncementByIdAsync(id);
            if (announcement == null)
            {
                return false;
            }
            _context.Announcements.Remove(announcement);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<CreateUpdateAnnouncement> UpdateAnnouncementAsync(Guid id, CreateUpdateAnnouncement announcement)
        {
           
            if (!await ExistAnnouncementAsync(id))
            {
                return null;
            }
            //exista un pachet automapper - ne ajuta sa transformam un obiect in alt obiect

            //announcementFromDb.EventDate = announcement.EventDate;
            //announcementFromDb.Text = announcement.Text;
            //announcementFromDb.Title = announcement.Title;
            //announcementFromDb.ValidFrom = announcement.ValidFrom;
            //announcementFromDb.ValidTo = announcement.ValidTo;
            //announcementFromDb.Tags = announcement.Tags;
            //_context.Announcements.Update(announcementFromDb);
            //await _context.SaveChangesAsync();

            var announcementUpdated = _mapper.Map<Announcement>(announcement);

            announcementUpdated.IdAnnouncement = id;

            _context.Update(announcementUpdated);

            await _context.SaveChangesAsync();

            return announcement;
        }

        public async Task<PatchAnnouncement> UpdatePartiallyAnnouncementAsync(Guid id, PatchAnnouncement announcement)
        {
            var announcementFromDB = await GetAnnouncementByIdAsync(id);
            if (announcementFromDB == null) 
            {
                return null;
            }
            if (!string.IsNullOrEmpty(announcement.Title) && announcement.Title != announcementFromDB.Title)
            { 
            announcementFromDB.Title = announcement.Title;
            }
            if (!string.IsNullOrEmpty(announcement.Text) && announcement.Text != announcementFromDB.Text)
            {
                announcementFromDB.Text = announcement.Text;
            }
            if (!string.IsNullOrEmpty(announcement.Tags) && announcement.Tags != announcementFromDB.Tags)
            {
                announcementFromDB.Tags = announcement.Tags;
            }

            if (announcement.ValidFrom.HasValue && announcement.ValidFrom != announcementFromDB.ValidFrom)
            {
                announcementFromDB.ValidFrom = announcement.ValidFrom;
            }
          
            if (announcement.ValidTo.HasValue && announcement.ValidTo != announcementFromDB.ValidTo)
            {
                announcementFromDB.ValidTo = announcement.ValidTo;
            }
            if (announcement.EventDate.HasValue && announcement.EventDate != announcementFromDB.EventDate)
            {
                announcementFromDB.EventDate = announcement.EventDate;
            }
            _context.Announcements.Update(announcementFromDB);
            await _context.SaveChangesAsync();
            return announcement;
        }
        private async Task<bool> ExistAnnouncementAsync(Guid id)
        {
            return await _context.Announcements.CountAsync(a => a.IdAnnouncement == id) > 0;
        }
    }
}

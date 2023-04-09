using FirstApi07.DataContext;
using FirstApi07.DTOs;
using FirstApi07.Repositories;
using FirstApi07.Tests.Helpers;

namespace FirstApi07.Tests.Repository.Tests
{
    public class AnnouncementsRepositoryTests
    {
        private readonly AnnouncementsRepository _repository;
        private readonly ProgrammingClubDataContext _context;
        public AnnouncementsRepositoryTests()
        {
            _context = DBContextHelper.GetDatabaseContext();
            _repository = new AnnouncementsRepository(_context, null);
        }
        [Fact]
        public async Task GetAllAnnouncementAsync_OK()
        {

            //arange
            Announcement a1 = new()
            {
                IdAnnouncement = Guid.NewGuid(),
                ValidFrom = DateTime.Now,
                ValidTo = DateTime.Now.AddDays(14),
                EventDate = DateTime.Now.AddDays(15),
                Tags = "announcement 1",
                Text = "Announcement 1",
                Title = "Web API Conference"
            };

            Announcement a2 = new()
            {
                IdAnnouncement = Guid.NewGuid(),
                ValidFrom = DateTime.Now,
                ValidTo = DateTime.Now.AddDays(14),
                EventDate = DateTime.Now.AddDays(15),
                Tags = "announcement 1",
                Text = "Announcement 1",
                Title = "Web API Conference"
            };
            Helpers.DBContextHelper.AddAnnouncement(_context, a1);
            Helpers.DBContextHelper.AddAnnouncement(_context, a2);

            //Action
            var announcements = await _repository.GetAnnouncementsAsync();


            //Assert
            Assert.NotNull(announcements);
            Assert.Equal(2, announcements.Count());

        }
        [Fact]
        public async Task GetAnnouncementAsync_WithoutData()
        {
            //Act
            var announcements = await _repository.GetAnnouncementsAsync();

            //Assert
            Assert.NotNull(announcements);
            Assert.Empty(announcements);
        }
        [Fact]
        public async Task GetAnnouncementById_OK()
        {
            Announcement a1 = new()
            {
                IdAnnouncement = Guid.NewGuid(),
                ValidFrom = DateTime.Now,
                ValidTo = DateTime.Now.AddDays(14),
                EventDate = DateTime.Now.AddDays(15),
                Tags = "announcement 1",
                Text = "Announcement 1",
                Title = "Web API Conference"
            };
            Helpers.DBContextHelper.AddAnnouncement(_context, a1);
            //Act
            var result = await _repository.GetAnnouncementByIdAsync((Guid)a1.IdAnnouncement);
            Assert.NotNull(result);
            Assert.Equal(a1.Tags, result.Tags);
            Assert.Equal(a1.Title, result.Title);
            Assert.Equal(a1.ValidTo, result.ValidTo);
            Assert.Equal(a1.ValidFrom, result.ValidFrom);
            Assert.Equal(a1.EventDate, result.EventDate);
            Assert.Equal(a1.Text, result.Text);
        }
        [Fact]
        public async Task GetAnnouncementById_WhenNotExists()
        {
            //Act
            var result = await _repository.GetAnnouncementByIdAsync(Guid.Empty);
            Assert.Null(result);

        }
        [Fact]
        public async Task DeleteAnnouncementAsync_OK()
        {
            //Arrange
            Announcement a1 = new()
            {
                IdAnnouncement = Guid.NewGuid(),
                ValidFrom = DateTime.Now,
                ValidTo = DateTime.Now.AddDays(14),
                EventDate = DateTime.Now.AddDays(15),
                Tags = "announcement 1",
                Text = "Announcement 1",
                Title = "Web API Conference"
            };
            Helpers.DBContextHelper.AddAnnouncement(_context, a1);

            //Act
            var result = await _repository.DeleteAnnouncementAsync((Guid)a1.IdAnnouncement);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAnnouncementAsync_NotOK()
        {
            //ACT
            var result = await _repository.DeleteAnnouncementAsync(Guid.NewGuid());
            //Assert
            Assert.False(result);
        }
    }
}

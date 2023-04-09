using AutoMapper;
using FirstApi07.Controllers;
using FirstApi07.DataContext;
using FirstApi07.DTOs;
using FirstApi07.Helpers;
using FirstApi07.Repositories;
using FirstApi07.Services;
using FirstApi07.Tests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace FirstApi07.Tests.Controllers.Tests
{
    public class AnnouncementsControllerTests
    {
        private readonly AnnouncementsRepository _repository;
        private readonly ProgrammingClubDataContext _context;
        private readonly AnnouncementsService _service;
        private readonly Mock<ILogger<AnnouncementsController>> _mockLogger;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AnnouncementsController _controller;



        public AnnouncementsControllerTests()
        {
            _context = DBContextHelper.GetDatabaseContext();
            _mockLogger = new Mock<ILogger<AnnouncementsController>>();
            _mockMapper = new Mock<IMapper>();
            _repository = new AnnouncementsRepository(_context, _mockMapper.Object);

            _service = new AnnouncementsService(_repository);
            _controller = new AnnouncementsController(_service, _mockLogger.Object);


        }
        [Fact]
        public async Task GetAll_Should_Return_OK()
        { //Arange
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
            var announcements = await _repository.GetAnnouncementsAsync();

            //Act

            var result = await _controller.GetAllAsync();

            //Asert
            Assert.IsType<OkObjectResult>(result);


        }
        [Fact]
        public async Task GetAll_Should_Return_NoContent()
        {
            //Act
            ObjectResult result = (ObjectResult)await _controller.GetAllAsync();

            //Assert
            Assert.Equal(204, result.StatusCode);
            Assert.Equal(ErrorMessagesEnum.NoElementFound, result.Value);
        }
    }

}

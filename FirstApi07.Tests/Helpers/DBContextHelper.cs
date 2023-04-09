using FirstApi07.DataContext;
using FirstApi07.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstApi07.Tests.Helpers
{
    internal class DBContextHelper
    {
        public static ProgrammingClubDataContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ProgrammingClubDataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;
            var databaseContext = new ProgrammingClubDataContext(options);
            databaseContext.Database.EnsureCreated();
            return databaseContext;
        }
        public static Announcement AddAnnouncement(ProgrammingClubDataContext context, Announcement announcement)
        {
            context.Add(announcement);
            context.SaveChangesAsync();
            context.Entry(announcement).State = EntityState.Detached;
            return announcement;
        }

    }
}

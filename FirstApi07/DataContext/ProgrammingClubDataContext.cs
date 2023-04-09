
using FirstApi07.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FirstApi07.DataContext
{
    public class ProgrammingClubDataContext : DbContext
    {
        public ProgrammingClubDataContext(DbContextOptions<ProgrammingClubDataContext> options) : base(options)
        {

        }

        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Member> Members { get; set; }
    }
    //    public DbSet<CodeSnippetModel> CodeSnippets { get; set; }
    
    //    public DbSet<MembershipModel> Memberships { get; set; }
    //    public DbSet<MembershipTypeModel> MembershipTypes { get; set; }
    //}
}

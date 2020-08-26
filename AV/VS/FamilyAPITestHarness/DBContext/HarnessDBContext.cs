using FamilyAPITestHarness.Entites;
using Microsoft.EntityFrameworkCore;

namespace FamilyAPITestHarness.DBContext
{
    public class HarnessDBContext : DbContext
    {
        // Ctor.
        public HarnessDBContext(DbContextOptions<HarnessDBContext> options) : base(options) { }

        public DbSet<Person> Person { get; set; }
    }
}

using FamilyAPITestHarness.Entites;
using FamilyAPITestHarness.Model;
using Microsoft.EntityFrameworkCore;

namespace FamilyAPITestHarness.DBContext
{
    public class HarnessDBContext : DbContext
    {
        // Ctor.
        public HarnessDBContext(DbContextOptions<HarnessDBContext> options) : base(options) { }

        public DbSet<Person> Person { get; set; }
        public DbSet<Pet> Pet { get; set; }
    }
}

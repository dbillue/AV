using Microsoft.EntityFrameworkCore;
using FamilyDemoAPIv2.Entities;

namespace FamilyDemoAPIv2.DBContext
{
    public class FamilyDemoAPIv2Context : DbContext
    {
        public FamilyDemoAPIv2Context(DbContextOptions<FamilyDemoAPIv2Context> options)
            : base(options)
        {}

        public DbSet<Person> Persons { get; set; }
        public DbSet<BirthState> BirthStates { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetType> PetTypes { get; set; }
    }
}

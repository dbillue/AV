using FamilyAPIDemoEFCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace FamilyAPIDemoEFCore.Data
{
    public class FamilyContext : DbContext
    {
        public DbSet<Family> Families { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-2BFUPF5\\CRADLESQLSERVER;Initial Catalog=EFCore;Integrated Security=False;User=sa;Password=All13B1llu3;Connection Timeout=180");
        }
    }

}

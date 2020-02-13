using FamilyAPIDemoEFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace FamilyAPIDemoEFCore.Data
{
    public partial class EFCoreStandardContext : DbContext
    {
        public EFCoreStandardContext()
        {
        }

        public EFCoreStandardContext(DbContextOptions<EFCoreStandardContext> options)
            : base(options)
        {
        }

        private static readonly LoggerFactory ConsoleLoggerFactory
            = new LoggerFactory(new[] {
              new ConsoleLoggerProvider((category, level)
                  => category == DbLoggerCategory.Database.Command.Name
                  && level == LogLevel.Information, true) });

        public virtual DbSet<Battles> Battles { get; set; }
        public virtual DbSet<Families> Families { get; set; }
        public virtual DbSet<FamilyBattles> FamilyBattles { get; set; }
        public virtual DbSet<Quotes> Quotes { get; set; }
        public virtual DbSet<SecretIdentity> SecretIdentity { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLoggerFactory(ConsoleLoggerFactory)
                    .EnableSensitiveDataLogging(true)
                    .UseSqlServer(@"Data Source=DESKTOP-2BFUPF5\CRADLESQLSERVER;Initial Catalog=EFCoreStandard;Integrated Security=False;User=efc;Password=knobsw;Connection Timeout=180");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FamilyBattles>(entity =>
            {
                entity.HasKey(e => new { e.FamilyId, e.BattleId });

                entity.HasIndex(e => e.BattleId);

                entity.HasOne(d => d.Battle)
                    .WithMany(p => p.FamilyBattles)
                    .HasForeignKey(d => d.BattleId);

                entity.HasOne(d => d.Family)
                    .WithMany(p => p.FamilyBattles)
                    .HasForeignKey(d => d.FamilyId);
            });

            modelBuilder.Entity<Quotes>(entity =>
            {
                entity.HasIndex(e => e.FamilyId);

                entity.HasOne(d => d.Family)
                    .WithMany(p => p.Quotes)
                    .HasForeignKey(d => d.FamilyId);
            });

            modelBuilder.Entity<SecretIdentity>(entity =>
            {
                entity.HasIndex(e => e.FamilyId)
                    .IsUnique();

                entity.HasOne(d => d.Family)
                    .WithOne(p => p.SecretIdentity)
                    .HasForeignKey<SecretIdentity>(d => d.FamilyId);
            });
        }
    }
}

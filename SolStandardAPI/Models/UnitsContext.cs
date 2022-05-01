using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SolStandardAPI.Models
{
    public partial class UnitsContext : DbContext, IUnitsContext
    {
        public DbSet<Unit> Units { get; set; }
        public DbSet<UnitStatistics> UnitStatistics { get; set; }

        public UnitsContext()
        {
        }

        public UnitsContext(DbContextOptions<UnitsContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https: //go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postrgres;Password=postrgres");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var converter = new ValueConverter<int[], string>(
                array => string.Join(",", array),
                s => s.Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x))
                    .ToArray()
            );

            modelBuilder.Entity<UnitStatistics>()
                .Property(e => e.BaseAtkRange)
                .HasConversion(converter);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
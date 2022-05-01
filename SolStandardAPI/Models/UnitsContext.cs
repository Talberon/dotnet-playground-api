using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
            var numberArrayConverter = new ValueConverter<int[], string>(
                array => string.Join(",", array),
                s => s.Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x))
                    .ToArray()
            );

            var numberArrayComparer = new ValueComparer<int[]>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToArray());

            modelBuilder.Entity<UnitStatistics>()
                .Property(e => e.BaseAtkRange)
                .HasConversion(numberArrayConverter)
                .Metadata
                .SetValueComparer(numberArrayComparer);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
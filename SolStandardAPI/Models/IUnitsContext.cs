using Microsoft.EntityFrameworkCore;

namespace SolStandardAPI.Models;

public interface IUnitsContext
{
    DbSet<Unit> Units { get; set; }
    DbSet<UnitStatistics> UnitStatistics { get; set; }
    int SaveChanges();
}
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SolStandardAPI.Controllers;
using SolStandardAPI.Models;
using Xunit;

namespace SolStandardAPITest.Controllers;

public class UnitsControllerTest
{
    private readonly UnitsController controller;

    public UnitsControllerTest()
    {
        DbContextOptions<UnitsContext> options = new DbContextOptionsBuilder<UnitsContext>()
            .UseInMemoryDatabase(databaseName: "UnitsDatabase")
            .Options;

        var unitsDatabase = new UnitsContext(options);

        controller = new UnitsController(unitsDatabase);
    }

    [Fact]
    public void NoUnitsShouldExistOnStart()
    {
        Assert.Empty(controller.GetUnits());
    }

    [Fact]
    public void UnitShouldExistAfterCreating()
    {
        var newUnit = new Unit("Test", new UnitStatistics());
        controller.CreateUnit(newUnit);
        Assert.Equal(newUnit, controller.GetUnits().First());
    }
}
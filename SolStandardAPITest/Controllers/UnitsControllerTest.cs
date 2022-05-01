using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolStandardAPI.Controllers;
using SolStandardAPI.Models;
using Xunit;
using Xunit.Sdk;

namespace SolStandardAPITest.Controllers;

public class UnitsControllerTest
{
    private static UnitsController GenerateController()
    {
        string databaseName = Guid.NewGuid().ToString();
        Console.WriteLine($"Generating database with name: {databaseName}");

        DbContextOptions<UnitsContext> options = new DbContextOptionsBuilder<UnitsContext>()
            .UseInMemoryDatabase(databaseName: databaseName)
            .Options;

        var unitsDatabase = new UnitsContext(options);

        return new UnitsController(unitsDatabase);
    }

    [Fact]
    public void NoUnitsShouldExistOnStart()
    {
        UnitsController controller = GenerateController();
        Assert.Empty(controller.GetUnits());
    }

    [Fact]
    public void UnitShouldExistAfterCreating()
    {
        UnitsController controller = GenerateController();

        var newUnit = new Unit("Test", new UnitStatistics());
        controller.CreateUnit(newUnit);
        Assert.Equal(newUnit, controller.GetUnits().First());
    }

    [Fact]
    public void MultipleUnitsShouldExistAfterCreating()
    {
        UnitsController controller = GenerateController();

        var newUnit = new Unit("Test1", new UnitStatistics());
        controller.CreateUnit(newUnit);
        var newUnit2 = new Unit("Test2", new UnitStatistics());
        controller.CreateUnit(newUnit2);
        var newUnit3 = new Unit("Test3", new UnitStatistics());
        controller.CreateUnit(newUnit3);
        Assert.Equal(3, controller.GetUnits().Count());
    }


    [Fact]
    public void ShouldUpdateUnitRole()
    {
        UnitsController controller = GenerateController();

        var originalUnit = new Unit("Test", new UnitStatistics());
        ActionResult<Unit> createdResult = controller.CreateUnit(originalUnit);
        Assert.Equal(originalUnit, controller.GetUnits().First());

        if (createdResult.Result is not CreatedResult created)
        {
            throw new XunitException($"Unit was not returned: {createdResult.Value}");
        }

        if (created.Value is not Unit createdUnit)
        {
            throw new XunitException($"Unit was not returned: {createdResult.Value}");
        }

        var newUnit = new Unit("CHANGED", new UnitStatistics
        {
            Atk = 500
        });

        IActionResult updateResult = controller.UpdateUnit(createdUnit.Id, newUnit);

        Assert.True(updateResult is OkResult);

        Unit? updatedUnit = controller.GetUnit(createdUnit.Id);

        Assert.Equal(newUnit.Role, updatedUnit?.Role);
        Assert.Equal(newUnit.UnitStatistics.Atk, updatedUnit?.UnitStatistics.Atk);
    }
}
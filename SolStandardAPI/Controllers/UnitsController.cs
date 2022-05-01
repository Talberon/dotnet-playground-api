using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SolStandardAPI.Models;
using SolStandardAPI.Utility;

namespace SolStandardAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UnitsController : ControllerBase
    {
        private readonly IUnitsContext db = new UnitsContext();

        [ActivatorUtilitiesConstructor]
        public UnitsController()
        {
        }

        public UnitsController(IUnitsContext db)
        {
            this.db = db;
        }

        [HttpGet("")]
        public IEnumerable<Unit> GetUnits()
        {
            return db.Units
                .Include(unit => unit.UnitStatistics)
                .ToList();
        }

        [HttpGet("{index:int}")]
        public Unit? GetUnit(int index)
        {
            return db.Units
                .Include(unit => unit.UnitStatistics)
                .FirstOrDefault(unit => unit.Id == index);
        }

        [HttpDelete("{index:int}")]
        public IActionResult DeleteUnit(int index)
        {
            Unit? unitToRemove = db.Units
                .Include(unit => unit.UnitStatistics)
                .FirstOrDefault(unit => unit.Id == index);

            if (unitToRemove is null) return NotFound();

            db.Units.Remove(unitToRemove);
            db.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public ActionResult<Unit> CreateUnit(Unit? unit)
        {
            if (unit is null) return BadRequest(new ArgumentNullException());

            db.Units.Add(unit);
            db.SaveChanges();

            return Created($"/{unit.Id}", unit);
        }

        [HttpPut("{index:int}")]
        public IActionResult UpdateUnit(int index, Unit newUnit)
        {
            Unit? unitToModify = db.Units
                .Include(unit => unit.UnitStatistics)
                .FirstOrDefault(unit => unit.Id == index);

            if (unitToModify is null)
            {
                return NotFound();
            }

            Debug.WriteLine($"Updating unit [{index}] role: {newUnit.Role} => {unitToModify.Role}");


            db.Units.Update(unitToModify);

            if (newUnit.Role.IsNullOrWhitespace()) return BadRequest("Role must be defined!");

            unitToModify.Role = newUnit.Role;

            if (!newUnit.UnitStatistics.IsIdentity())
            {
                Debug.WriteLine(
                    $"Updating unit [{index}] stats: {newUnit.UnitStatistics} => {unitToModify.UnitStatistics}");
                unitToModify.UnitStatistics = newUnit.UnitStatistics;
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return Ok();
        }

        [HttpGet("random")]
        public Unit? GetRandomUnit()
        {
            return db.Units.ToList().Shuffle(Program.Random).FirstOrDefault();
        }
    }
}
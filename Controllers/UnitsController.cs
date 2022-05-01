using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sol_standard_api.Models;
using sol_standard_api.Utility;

namespace sol_standard_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UnitsController : ControllerBase
    {
        /*
                // Create
                Console.WriteLine("Inserting a new blog");
                db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
                db.SaveChanges();

                // Read
                Console.WriteLine("Querying for a blog");
                var blog = db.Blogs
                    .OrderBy(b => b.BlogId)
                    .First();

                // Update
                Console.WriteLine("Updating the blog and adding a post");
                blog.Url = "https://devblogs.microsoft.com/dotnet";
                blog.Posts.Add(
                    new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
                db.SaveChanges();

                // Delete
                Console.WriteLine("Delete the blog");
                db.Remove(blog);
                db.SaveChanges();
        */

        [HttpGet("")]
        public IEnumerable<Unit> GetUnits()
        {
            using var db = new PostgresContext();
            return db.Units
                .Include(unit => unit.UnitStatistics)
                .ToList();
        }

        [HttpGet("{index:int}")]
        public Unit? GetUnit(int index)
        {
            using var db = new PostgresContext();
            return db.Units
                .Include(unit => unit.UnitStatistics)
                .FirstOrDefault(unit => unit.Id == index);
        }

        [HttpDelete("{index:int}")]
        public IActionResult DeleteUnit(int index)
        {
            using var db = new PostgresContext();
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

            using var db = new PostgresContext();

            db.Units.Add(unit);
            db.SaveChanges();

            return Created($"/{unit.Id}", unit);
        }

        [HttpPut("{index:int}")]
        public IActionResult UpdateUnit(int index, Unit newUnit)
        {
            using var db = new PostgresContext();
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
            using var db = new PostgresContext();
            return db.Units.ToList().Shuffle(Program.Random).FirstOrDefault();
        }
    }
}
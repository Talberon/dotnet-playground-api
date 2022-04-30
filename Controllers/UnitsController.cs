using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using sol_standard_api.Models;
using sol_standard_api.Utility;

namespace sol_standard_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UnitsController : ControllerBase
    {
        private readonly List<Unit> units = new()
        {
            new Unit(role: "Champion", unitStatistics: new(
                hp: 7,
                armor: 8,
                atk: 4,
                ret: 3,
                blk: 0,
                luck: 1,
                mv: 5,
                atkRange: new[] { 1 },
                maxCmd: 5
            )),
            new Unit(role: "Lancer", unitStatistics: new(
                hp: 10,
                armor: 5,
                atk: 5,
                ret: 3,
                blk: 0,
                luck: 1,
                mv: 5,
                atkRange: new[] { 1 },
                maxCmd: 5
            )),
            new Unit(role: "Archer", unitStatistics: new(
                hp: 8,
                armor: 5,
                atk: 5,
                ret: 3,
                blk: 0,
                luck: 1,
                mv: 4,
                atkRange: new[] { 2 },
                maxCmd: 5
            )),
            new Unit(role: "Cleric", unitStatistics: new(
                hp: 7,
                armor: 5,
                atk: 0,
                ret: 0,
                blk: 0,
                luck: 4,
                mv: 5,
                atkRange: new[] { 1, 2 },
                maxCmd: 5
            ))
        };

        [HttpGet("")]
        public IEnumerable<Unit> GetUnits()
        {
            return units;
        }

        [HttpGet("{index:int}")]
        public Unit? GetUnit(int index)
        {
            if (index >= 0 && index < units.Count) return units[index];

            return null;
        }

        [HttpGet("random")]
        public Unit? GetRandomUnit()
        {
            return units.Shuffle(Program.Random).FirstOrDefault();
        }
    }
}
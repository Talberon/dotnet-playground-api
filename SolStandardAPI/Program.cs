using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SolStandardAPI.Models;

namespace SolStandardAPI
{
    public class Program
    {
        public static Random Random { get; set; } = new();

        public static void Main(string[] args)
        {
            using (var db = new PostgresContext())
            {
                List<Unit> units = new()
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

                if (!db.Units.Any())
                {
                    Debug.WriteLine("Initializing unit database...");
                    foreach (var unit in units)
                    {
                        db.Add(unit.UnitStatistics);
                        db.Add(unit);
                    }
                    db.SaveChanges();
                    Debug.WriteLine("Done!");
                }
            }

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}
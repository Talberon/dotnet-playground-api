using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sol_standard_api.Models
{
    public class Unit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Role { get; set; }
        public UnitStatistics UnitStatistics { get; set; }

        public Unit() : this("UNDEFINED", new UnitStatistics())
        {
        }

        public Unit(string role, UnitStatistics unitStatistics)
        {
            Role = role;
            UnitStatistics = unitStatistics;
        }
    }
}
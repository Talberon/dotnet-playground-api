namespace sol_standard_api.Models
{
    public class Unit
    {
        public string Role { get; set; }
        public UnitStatistics UnitStatistics { get; set; }

        public Unit(string role, UnitStatistics unitStatistics)
        {
            Role = role;
            UnitStatistics = unitStatistics;
        }
    }
}
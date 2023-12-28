using System.Linq.Expressions;

namespace FuelEconomy.Model
{
    public class Vehicle
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Vehicle()
        {
            Name = string.Empty;
        }

        private Vehicle(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public static Vehicle New(string name) => new(name);
    }
}

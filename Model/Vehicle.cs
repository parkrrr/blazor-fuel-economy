namespace FuelEconomy.Model
{
    public class Vehicle
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Vehicle()
        {
            Id = Guid.NewGuid();
            Name = string.Empty;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Vehicle other)
            {
                return Id == other.Id;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}

using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace FuelEconomy.Model
{
    public class Entry
    {
        public Guid Id { get; set; }
        public Guid VehicleId { get; set; }

        private DateTimeOffset _timestamp;
        public DateTimeOffset Timestamp
        {
            get
            {
                return _timestamp;
            }
            set
            {
                _timestamp = value;
                TimestampUtc = value.UtcDateTime;
            }
        }

        public DateTime TimestampUtc { get; private set; }
        public decimal Distance { get; set; }
        public decimal Volume { get; set; }
        public decimal Price { get; set; }

        public Entry(Vehicle vehicle)
        {
            Id = Guid.NewGuid();
            VehicleId = vehicle.Id;
        }

        public Entry() { }

        // Calculated properties
        public decimal GetEconomy()
        {
            return Distance / Volume;
        }

        public decimal GetCost()
        {
            return Volume * Price;
        }

        public decimal GetCostPerDistance()
        {
            return GetCost() / Distance;
        }

        public decimal GetCostPerVolume()
        {
            return GetCost() / Volume;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Entry other)
            {
                return Id == other.Id && VehicleId == other.VehicleId;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, VehicleId);
        }
    }
}

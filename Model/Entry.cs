using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace FuelEconomy.Model
{
    public class Entry
    {
        public Guid Id { get; set; }
        public Guid VehicleId { get; set; }
        public DateTimeOffset Timestamp { get; set; }
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
        [JsonIgnore]
        public decimal? Economy => Distance / Volume;

        [JsonIgnore]
        public decimal? Cost => Volume * Price;

        [JsonIgnore]
        public decimal? CostPerDistance => Cost / Distance;

        [JsonIgnore]
        public decimal? CostPerVolume => Cost / Volume;

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

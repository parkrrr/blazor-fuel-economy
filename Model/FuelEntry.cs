using System.Reflection.Metadata.Ecma335;

namespace FuelEconomy.Model
{
    public class FuelEntry
    {
        public DateTimeOffset Timestamp { get; set; }
        public decimal Distance { get; set; }
        public decimal? Odometer { get; set; }
        public decimal Volume { get; set; }
        public decimal Price { get; set; }

        // Calculated properties
        public decimal? Economy => Distance / Volume;
        public decimal? Cost => Volume * Price;
        public decimal? CostPerDistance => Cost / Distance;
        public decimal? CostPerVolume => Cost / Volume;

    }
}

namespace FuelEconomy.Model
{
    public record VehicleSummaryModel(string Name, int EntriesCount, decimal? AverageEconomy, decimal? BestEconomy, IEnumerable<Entry> Last10Entries);
}

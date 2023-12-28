namespace FuelEconomy.Model
{
    public record VehicleSummaryModel(string Name, int EntriesCount, decimal? AverageEconomy, decimal? BestEconomy, IEnumerable<EntrySparklineModel> Last10Entries);
    public record EntrySparklineModel(int Index, decimal? Economy);
}

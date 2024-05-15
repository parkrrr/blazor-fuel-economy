using FuelEconomy.Model;

namespace FuelEconomy.Services
{
    public class VehicleStatisticsService(EntriesService entriesService)
    {
        public VehicleStatisticsModel Get(Guid vehicleId)
        {
            return Get(vehicleId, entry => true);
        }

        public VehicleStatisticsModel Get(Guid vehicleId, Func<Entry, bool> filter)
        {
            var entries = entriesService.Get(vehicleId).OrderBy(entry => entry.Timestamp).Where(filter).ToList();

            if (entries.Count == 0)
            {
                return new VehicleStatisticsModel();
            }

            var model = new VehicleStatisticsModel
            {
                TotalDistance = entries.Sum(e => e.Distance),
                TotalCost = entries.Sum(e => e.GetCost()),
                LatestDate = entries.Max(e => e.TimestampUtc),
                InitialDate = entries.Min(e => e.TimestampUtc),

                BestEconomy = entries.Max(e => e.GetEconomy()),
                AverageEconomy = entries.Average(e => e.GetEconomy()),
                WorstEconomy = entries.Min(e => e.GetEconomy()),
                MedianEconomy = entries.OrderBy(entry => entry.TimestampUtc).Skip(entries.Count / 2).First().GetEconomy(),
                HighestCost = entries.Max(e => e.GetCost()),
                LowestCost = entries.Min(e => e.GetCost()),

                CostPerDay = entries.Sum(e => e.GetCost()) / entries.Select(e => e.TimestampUtc.Date).Distinct().Count(),
                CostPerDistance = entries.Sum(e => e.GetCost()) / entries.Sum(e => e.Distance),
                CostPer100Distance = entries.Sum(e => e.GetCost()) / (entries.Sum(e => e.Distance) / 100),

                AverageYearlyDistance = entries.Sum(e => e.Distance) / (DateTime.UtcNow.Year - entries.Min(e => e.TimestampUtc).Year + 1),
                AverageYearlyCost = entries.Sum(e => e.GetCost()) / (DateTime.UtcNow.Year - entries.Min(e => e.TimestampUtc).Year + 1)
            };

            return model;
        }
    }
}

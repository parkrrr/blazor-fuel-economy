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
                TotalMiles = entries.Sum(e => e.Distance),
                TotalCost = entries.Sum(e => e.GetCost()),
                LatestDate = entries.Max(e => e.TimestampUtc),
                InitialDate = entries.Min(e => e.TimestampUtc),

                BestEconomy = entries.Max(e => e.GetEconomy()),
                AverageEconomy = entries.Average(e => e.GetEconomy()),
                WorstEconomy = entries.Min(e => e.GetEconomy()),
                MedianEconomy = entries.OrderBy(entry => entry.TimestampUtc).Skip(entries.Count / 2).First().GetEconomy(),

                SummerEconomy = entries.Where(e => e.TimestampUtc.Month >= 6 && e.TimestampUtc.Month <= 8).Average(e => e.GetEconomy()),
                SpringEconomy = entries.Where(e => e.TimestampUtc.Month >= 3 && e.TimestampUtc.Month <= 5).Average(e => e.GetEconomy()),
                FallEconomy = entries.Where(e => e.TimestampUtc.Month >= 9 && e.TimestampUtc.Month <= 11).Average(e => e.GetEconomy()),
                WinterEconomy = entries.Where(e => e.TimestampUtc.Month == 12 || e.TimestampUtc.Month <= 2).Average(e => e.GetEconomy()),

                HighestCost = entries.Max(e => e.GetCost()),
                LowestCost = entries.Min(e => e.GetCost()),

                AverageSummerCost = entries.Where(e => e.TimestampUtc.Month >= 6 && e.TimestampUtc.Month <= 8).Average(e => e.GetCost()),
                AverageSpringCost = entries.Where(e => e.TimestampUtc.Month >= 3 && e.TimestampUtc.Month <= 5).Average(e => e.GetCost()),
                AverageFallCost = entries.Where(e => e.TimestampUtc.Month >= 9 && e.TimestampUtc.Month <= 11).Average(e => e.GetCost()),
                AverageWinterCost = entries.Where(e => e.TimestampUtc.Month == 12 || e.TimestampUtc.Month <= 2).Average(e => e.GetCost()),

                CostPerDay = entries.Sum(e => e.GetCost()) / entries.Select(e => e.TimestampUtc.Date).Distinct().Count(),
                CostPerMile = entries.Sum(e => e.GetCost()) / entries.Sum(e => e.Distance),
                CostPer100Miles = entries.Sum(e => e.GetCost()) / (entries.Sum(e => e.Distance) / 100),

                AverageYearlyMiles = entries.Sum(e => e.Distance) / (DateTime.UtcNow.Year - entries.Min(e => e.TimestampUtc).Year + 1),
                AverageYearlyCost = entries.Sum(e => e.GetCost()) / (DateTime.UtcNow.Year - entries.Min(e => e.TimestampUtc).Year + 1)
            };

            return model;
        }
    }
}

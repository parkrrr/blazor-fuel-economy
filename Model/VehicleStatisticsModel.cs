namespace FuelEconomy.Model
{
    public record VehicleStatisticsModel
    {
        public decimal TotalMiles { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime LatestDate { get; set; }
        public DateTime InitialDate { get; set; }

        public decimal BestEconomy { get; set; }
        public decimal AverageEconomy { get; set; }
        public decimal WorstEconomy { get; set; }
        public decimal MedianEconomy { get; set; }

        public decimal SummerEconomy { get; set; }
        public decimal SpringEconomy { get; set; }
        public decimal FallEconomy { get; set; }
        public decimal WinterEconomy { get; set; }

        public decimal HighestCost { get; set; }
        public decimal LowestCost { get; set; }

        public decimal AverageSummerCost { get; set; }
        public decimal AverageSpringCost { get; set; }
        public decimal AverageFallCost { get; set; }
        public decimal AverageWinterCost { get; set; }

        public decimal CostPerDay { get; set; }
        public decimal CostPerMile { get; set; }
        public decimal CostPer100Miles { get; set; }

        public decimal AverageYearlyMiles { get; set; }
        public decimal AverageYearlyCost { get; set; }
    }
}

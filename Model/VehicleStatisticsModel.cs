namespace FuelEconomy.Model
{
    public record VehicleStatisticsModel
    {
        public decimal TotalDistance { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime LatestDate { get; set; }
        public DateTime InitialDate { get; set; }

        public decimal BestEconomy { get; set; }
        public decimal AverageEconomy { get; set; }
        public decimal WorstEconomy { get; set; }
        public decimal MedianEconomy { get; set; }

        public decimal HighestCost { get; set; }
        public decimal LowestCost { get; set; }

        public decimal CostPerDay { get; set; }
        public decimal CostPerDistance { get; set; }
        public decimal CostPer100Distance { get; set; }

        public decimal AverageYearlyDistance { get; set; }
        public decimal AverageYearlyCost { get; set; }
    }
}

using FuelEconomy.Model;
using FuelEconomy.Services;
using Microsoft.AspNetCore.Components;

namespace FuelEconomy.Pages
{
    public partial class Home
    {
        [Inject]
        public AppStateService AppStateService { get; set; } = null!;

        private List<VehicleSummaryModel> _vehicleSummaries = new();

        protected override void OnInitialized()
        {
            base.OnInitialized();

            var vehicles = AppStateService.Current.Vehicles.Take(4);
            foreach (var vehicle in vehicles)
            {
                _vehicleSummaries.Add(AppStateService.GetSummary(vehicle));
            }
        }
    }
}

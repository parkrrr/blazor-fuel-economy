using FuelEconomy.Model;
using FuelEconomy.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Globalization;
using System.Xml.Xsl;

namespace FuelEconomy.Pages
{
    public partial class Home
    {
        [Inject]
        public AppStateService AppStateService { get; set; } = null!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public VehicleService VehicleService { get; set; } = null!;

        [Inject]
        public EntriesService EntriesService { get; set; } = null!;

        [Inject]
        public HttpClient HttpClient { get; set; } = null!;

        private List<VehicleSummaryModel> _vehicleSummaries = new();
        private bool _loadingExampleData = false;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            LoadVehicleSummaries();
        }

        private void AddNewVehicle()
        {
            NavigationManager.NavigateTo("vehicles?adding=true");
        }

        private void LoadVehicleSummaries()
        {
            var vehicles = AppStateService.Current.Vehicles.Take(4);
            foreach (var vehicle in vehicles)
            {
                _vehicleSummaries.Add(AppStateService.GetSummary(vehicle));
            }
        }

        private async Task AddExampleVehicle()
        {
            try
            {
                _loadingExampleData = true;
                var vehicle = await CreateExampleVehicle();
                await LoadExampleData(vehicle);
                LoadVehicleSummaries();
            }
            finally
            {
                _loadingExampleData = false;
            }
        }

        private async Task<Vehicle> CreateExampleVehicle()
        {
            var vehicle = new Vehicle() { Name = "Example Vehicle" };
            await VehicleService.AddAsync(vehicle);
            return vehicle;
        }

        private async Task LoadExampleData(Vehicle vehicle)
        {
            var response = await HttpClient.GetAsync("https://raw.githubusercontent.com/parkrrr/blazor-fuel-economy/main/example.csv");
            if (response.IsSuccessStatusCode == false)
            {
                throw new InvalidOperationException($"Failed to download example data (response code is {response.StatusCode})");
            }

            using var reader = new StreamReader(response.Content.ReadAsStream());

            var newEntries = new List<Entry>();
            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                var fields = line.Split(',');
                var entry = new Entry(vehicle)
                {
                    Timestamp = DateTime.Parse(fields[0]),
                    Distance = decimal.Parse(fields[1]),
                    Volume = decimal.Parse(fields[2]),
                    Price = decimal.Parse(fields[3], NumberStyles.Currency)
                };
                newEntries.Add(entry);
            }

            await EntriesService.ImportAsync(vehicle.Id, newEntries);
        }
    }
}

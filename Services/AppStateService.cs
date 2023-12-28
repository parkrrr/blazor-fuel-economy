using FuelEconomy.Model;
using FuelEconomy.Pages;
using System.Text.Json;

namespace FuelEconomy.Services
{
    public class AppStateService(LocalStorageService localStorageService)
    {
        private readonly LocalStorageService _localStorageService = localStorageService;

        public AppState Current { get; private set; } = null!;

        public async Task InitializeIfNecessaryAsync()
        {
            if (Current == null)
            {
                var stored = await GetAsync();
                Current = stored.Increment();
            }
        }

        public async Task<AppState> GetAsync()
        {
            var state = await _localStorageService.GetAsync<AppState>("state");

            if (state == null)
            {
                return new AppState();
            }

            return state;
        }

        public async Task SetAsync(AppState state)
        {
            var existingState = await _localStorageService.GetAsync<AppState>("state");

            if (existingState != null && existingState.Version >= state.Version)
            {
                // conflict -- fail and prompt user to refresh
                throw new AppStateConflictException(state.Version, existingState.Version);
            }

            await _localStorageService.SetAsync("state", state);

            Current = state.Increment();
        }

        public VehicleSummaryModel GetSummary(Vehicle vehicle)
        {
            var entries = Current.Entries.Where(e => e.VehicleId == vehicle.Id);

            var summaryModel = new VehicleSummaryModel(vehicle.Name, entries.Count(), entries.Average(e => e.Economy), entries.Max(e => e.Economy));

            return summaryModel;
        }
    }

}

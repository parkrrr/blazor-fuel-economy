using FuelEconomy.Model;

namespace FuelEconomy.Services
{
    public class VehicleService(AppStateService appStateService)
    {
        private readonly AppStateService _appStateService = appStateService;

        public async Task<List<Vehicle>> GetAsync()
        {
            var state = await _appStateService.GetAsync();
            return state.Vehicles;
        }

        public async Task AddAsync(Vehicle vehicle)
        {
            var state = await _appStateService.GetAsync();
            state.Vehicles.Add(vehicle);
            await _appStateService.SetAsync(state);
        }
    }
}

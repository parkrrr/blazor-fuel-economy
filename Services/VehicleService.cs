using FuelEconomy.Model;
using FuelEconomy.Pages;

namespace FuelEconomy.Services
{
    public class VehicleService(AppStateService appStateService)
    {
        private readonly AppStateService _appStateService = appStateService;

        public event EventHandler<VehiclesChangedArgs>? VehiclesChanged;

        public List<Vehicle> Get()
        {
            var state = _appStateService.Current;
            return state.Vehicles;
        }

        public Vehicle? Get(Guid Id)
        {
            return Get().Where(v => v.Id == Id).FirstOrDefault();
        }

        public async Task AddAsync(Vehicle vehicle)
        {
            var state = _appStateService.Current;
            state.Vehicles.Add(vehicle);
            await _appStateService.SetAsync(state);

            VehiclesChanged?.Invoke(this, new VehiclesChangedArgs(state.Vehicles));
        }

        public async Task UpdateAsync(Vehicle vehicle)
        {
            var state = _appStateService.Current;

            var existingVehicle = state.Vehicles.Where(v => v.Id == vehicle.Id).FirstOrDefault();
            if (existingVehicle == null)
            {
                throw new InvalidOperationException($"Could not find vehicle ID={vehicle.Id}");
            }

            state.Vehicles.Remove(existingVehicle);
            state.Vehicles.Add(vehicle);

            await _appStateService.SetAsync(state);

            VehiclesChanged?.Invoke(this, new VehiclesChangedArgs(state.Vehicles));
        }

        public async Task RemoveAsync(Vehicle vehicle)
        {
            var state = _appStateService.Current;
            state.Vehicles.Remove(vehicle);
            await _appStateService.SetAsync(state);

            VehiclesChanged?.Invoke(this, new VehiclesChangedArgs(state.Vehicles));
        }
    }

    public class VehiclesChangedArgs : EventArgs
    {
        public VehiclesChangedArgs(List<Vehicle> vehicles)
        {
            Vehicles = vehicles;
        }

        public List<Vehicle> Vehicles { get; set; } = new();
    }
}

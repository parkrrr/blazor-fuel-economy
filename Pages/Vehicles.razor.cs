using FuelEconomy.Model;
using FuelEconomy.Services;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;

namespace FuelEconomy.Pages
{
    public partial class Vehicles
    {
        [Inject]
        public VehicleService VehicleService { get; set; } = null!;

        private RadzenDataGrid<Vehicle> _grid = null!;

        private List<Vehicle> _data = new();

        private Vehicle? _currentlyEditing;

        void Reset()
        {
            _currentlyEditing = null;
        }

        async Task EditRow(Vehicle vehicle)
        {
            _currentlyEditing = vehicle;
            await _grid.EditRow(vehicle);
        }

        async Task OnUpdateRow(Vehicle vehicle)
        {
            Reset();

            await VehicleService.UpdateAsync(vehicle);
        }

        async Task SaveRow(Vehicle vehicle)
        {
            await _grid.UpdateRow(vehicle);
        }

        void CancelEdit(Vehicle vehicle)
        {
            Reset();

            _grid.CancelEditRow(vehicle);
        }

        async Task DeleteRow(Vehicle vehicle)
        {
            Reset();

            if (_data.Contains(vehicle))
            {
                _data.Remove(vehicle);
                await VehicleService.RemoveAsync(vehicle);
                await _grid.Reload();
            }
            else
            {
                _grid.CancelEditRow(vehicle);
                await _grid.Reload();
            }
        }

        async Task InsertRow()
        {
            _currentlyEditing = new Vehicle();
            await _grid.InsertRow(_currentlyEditing);
        }

        async Task OnCreateRow(Vehicle vehicle)
        {
            await VehicleService.AddAsync(vehicle);
            _currentlyEditing = null;
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            _data = VehicleService.Get();
        }
    }
}

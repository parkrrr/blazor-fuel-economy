﻿using FuelEconomy.Model;
using FuelEconomy.Services;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;

namespace FuelEconomy.Pages
{
    public partial class Entries
    {
        [Inject]
        public EntriesService EntriesService { get; set; } = null!;

        [Inject]
        public VehicleService VehicleService { get; set; } = null!;

        private RadzenDataGrid<Entry> _grid = null!;

        private List<Entry> _data = new();
        private List<Vehicle> _vehicles = new();

        private Vehicle? _selectedVehicle;

        private Entry? _currentlyEditing;

        void Reset()
        {
            _currentlyEditing = null;
        }

        async Task EditRow(Entry entry)
        {
            _currentlyEditing = entry;
            await _grid.EditRow(entry);
        }

        async Task OnUpdateRow(Entry entry)
        {
            Reset();

            await EntriesService.UpdateAsync(entry);
        }

        async Task SaveRow(Entry entry)
        {
            await _grid.UpdateRow(entry);
        }

        void CancelEdit(Entry entry)
        {
            Reset();

            _grid.CancelEditRow(entry);
        }

        async Task DeleteRow(Entry entry)
        {
            Reset();

            if (_data.Contains(entry))
            {
                _data.Remove(entry);
                await EntriesService.RemoveAsync(entry);
                await _grid.Reload();
            }
            else
            {
                _grid.CancelEditRow(entry);
                await _grid.Reload();
            }
        }

        async Task InsertRow(Vehicle? vehicle)
        {
            if (vehicle == null)
            {
                return;
            }

            _currentlyEditing = new Entry(vehicle);
            await _grid.InsertRow(_currentlyEditing);
        }

        async Task OnCreateRow(Entry entry)
        {
            await EntriesService.AddAsync(entry);
            _currentlyEditing = null;
        }

        private void OnVehicleChange(Vehicle? vehicle)
        {
            _selectedVehicle = vehicle;

            if (_selectedVehicle != null)
            {
                _data = EntriesService.Get(_selectedVehicle);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            _vehicles = VehicleService.Get();
            OnVehicleChange(_vehicles.FirstOrDefault());
        }
    }
}

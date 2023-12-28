using FuelEconomy.Model;

namespace FuelEconomy.Services
{
    public class EntriesService(AppStateService appStateService)
    {
        private readonly AppStateService _appStateService = appStateService;

        public List<Entry> Get(Vehicle vehicle)
        {
            var state = _appStateService.Current;
            return state.Entries.Where(e => e.VehicleId == vehicle.Id).ToList();
        }

        public async Task AddAsync(Entry entry)
        {
            var state = _appStateService.Current;
            state.Entries.Add(entry);
            await _appStateService.SetAsync(state);
        }

        public async Task UpdateAsync(Entry entry)
        {
            var state = _appStateService.Current;

            var existingEntry = state.Entries.Where(e => e.Id == entry.Id).FirstOrDefault();
            if (existingEntry == null)
            {
                throw new InvalidOperationException($"Could not find entry ID={entry.Id}");
            }

            state.Entries.Remove(existingEntry);
            state.Entries.Add(entry);

            await _appStateService.SetAsync(state);
        }

        public async Task RemoveAsync(Entry entry)
        {
            var state = _appStateService.Current;
            state.Entries.Remove(entry);
            await _appStateService.SetAsync(state);
        }
    }
}

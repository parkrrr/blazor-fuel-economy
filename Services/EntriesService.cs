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

        public List<Entry> Get(Guid vehicleId)
        {
            var state = _appStateService.Current;
            return state.Entries.Where(e => e.VehicleId == vehicleId).ToList();
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

        public async Task ImportAsync(Guid vehicleId, IEnumerable<Entry> entries)
        {
            var state = _appStateService.Current;

            var existingEntries = state.Entries.Where(e => e.VehicleId == vehicleId).ToList();
            foreach (var entry in existingEntries)
            {
                state.Entries.Remove(entry);
            }

            state.Entries.AddRange(entries);

            await _appStateService.SetAsync(state);
        }

        public VehicleSummaryModel GetSummary(Vehicle vehicle)
        {
            var state = _appStateService.Current;

            var entries = state.Entries.Where(e => e.VehicleId == vehicle.Id);

            var last10 = entries.OrderBy(e => e.Timestamp).Take(20).Select((e, i) => new EntrySparklineModel(i, e.GetEconomy())).ToList();
            var summaryModel = new VehicleSummaryModel(vehicle, entries.Count(), entries.Average(e => e.GetEconomy()), entries.Max(e => e.GetEconomy()), last10);

            return summaryModel;
        }
    }
}

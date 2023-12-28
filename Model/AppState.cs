using FuelEconomy.Services;

namespace FuelEconomy.Model
{
    public class AppState
    {
        public int Version { get; private set; } = 0;
        public List<Vehicle> Vehicles { get; set; } = [];
        public List<FuelEntry> FuelEntries { get; set; } = [];
    }

    public record AppStateRecord(int Version, AppState State);

    public class AppStateConflictException(int currentVersion, int actualVersion) : Exception($"Current state is version {actualVersion}, but we are on version {currentVersion}")
    {
    }
}

namespace FuelEconomy.Model
{
    public class AppState
    {
        public int Version { get; set; }
        public List<Vehicle> Vehicles { get; set; } = [];
        public List<FuelEntry> FuelEntries { get; set; } = [];

        public AppState Increment()
        {
            var newState = this;
            newState.Version++;
            return newState;
        }
    }

    public class AppStateConflictException(int currentVersion, int actualVersion) : Exception($"Current state is version {actualVersion}, but we are on version {currentVersion}")
    {
    }
}

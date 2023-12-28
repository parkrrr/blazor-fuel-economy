using FuelEconomy.Model;
using System.Text.Json;

namespace FuelEconomy.Services
{
    public class AppStateService
    {
        private readonly LocalStorageService _localStorageService;

        public AppStateService(LocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
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
            var existingState = await _localStorageService.GetAsync<AppStateRecord>("state");

            if (existingState != null && existingState.Version >= state.Version)
            {
                // conflict -- fail and prompt user to refresh
                throw new AppStateConflictException(state.Version, existingState.Version);
            }

            await _localStorageService.SetAsync("state", state);
        }
    }

}

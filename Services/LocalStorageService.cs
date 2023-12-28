using FuelEconomy.Model;
using Microsoft.JSInterop;
using System.Text.Json;

namespace FuelEconomy.Services
{
    public class LocalStorageService(IJSRuntime jsRuntime)
    {
        private readonly IJSRuntime _jsRuntime = jsRuntime;

        public async Task SetAsync(string key, object value)
        {
            var json = JsonSerializer.Serialize(value);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, json);
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            // get value from localStorage
            var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
            if (json == null)
            {
                return default;
            }

            var obj = JsonSerializer.Deserialize<T>(json);
            return obj;
        }

        public async Task RemoveAsync(string key)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }
    }
}

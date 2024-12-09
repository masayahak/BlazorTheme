using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.JSInterop;
using System.Security.Cryptography;

namespace BlazorTheme.Components.Services
{
    public class ThemeService
    {
        private readonly ProtectedLocalStorage _localStorage;
        private readonly IJSRuntime _jsRuntime;
        private const string ThemeKey = "theme";
        private const string DefaultTheme = "light";

        public string CurrentTheme { get; private set; } = DefaultTheme;

        public ThemeService(ProtectedLocalStorage localStorage, IJSRuntime jsRuntime)
        {
            _localStorage = localStorage;
            _jsRuntime = jsRuntime;
        }

        public async Task<string> GetCurrentTheme()
        {
            try
            {
                var result = await _localStorage.GetAsync<string>(ThemeKey);
                CurrentTheme = result.Success ? result.Value! : DefaultTheme;
            }
            catch (CryptographicException)
            {
                CurrentTheme = "light";
            }
            return CurrentTheme;
        }

        public async Task<string> ToggleTheme()
        {
            CurrentTheme = CurrentTheme == DefaultTheme ? "dark" : DefaultTheme;
            await _localStorage.SetAsync(ThemeKey, CurrentTheme);
            return CurrentTheme;
        }

        public async Task ApplyCurrentTheme()
        {
            await _jsRuntime.InvokeVoidAsync("setTheme", CurrentTheme);
        }
    }
}

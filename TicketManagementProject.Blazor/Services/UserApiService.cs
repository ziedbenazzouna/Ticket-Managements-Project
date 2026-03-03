using System.Net.Http.Json;
using TicketManagementProject.Blazor.ViewModels;

namespace TicketManagementProject.Blazor.Services
{
    public class UserApiService
    {
        private readonly HttpClient _http;

        public UserApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<AuthResponseViewModel?> Login(LoginViewModel model)
        {
            var response = await _http.PostAsJsonAsync("api/auth/login", model);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<AuthResponseViewModel>();
        }

        public async Task<bool> Register(RegisterViewModel model)
        {
            var response = await _http.PostAsJsonAsync("api/auth/register", model);
            return response.IsSuccessStatusCode;
        }
    }
}

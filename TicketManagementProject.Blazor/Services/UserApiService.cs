using System.Net.Http.Json;
using TicketManagementProject.Blazor.ViewModels;
using TicketManagementProject.Shared.DTOs;

namespace TicketManagementProject.Blazor.Services
{
    public class UserApiService
    {
        private readonly HttpClient _http;

        public UserApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<AuthResponseViewModel?> Login(LoginDto model)
        {
            var response = await _http.PostAsJsonAsync("api/auth/login", model);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<AuthResponseViewModel>();
        }

        public async Task<(bool success, string message)> Register(RegisterDto model)
        {
            var response = await _http.PostAsJsonAsync("api/auth/register", model);
            if (response.IsSuccessStatusCode)
            {
                return (true, null);
            }

            // Récupère le message d'erreur envoyé par le BadRequest(ex.Message)
            var errorContent = await response.Content.ReadAsStringAsync();
            return (false, errorContent);
        }
    }
}

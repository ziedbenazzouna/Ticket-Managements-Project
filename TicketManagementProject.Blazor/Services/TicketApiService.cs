using System.Net.Http.Json;
using TicketManagementProject.Blazor.ViewModels;

namespace TicketManagementProject.Blazor.Services
{
    public class TicketApiService
    {
        private readonly HttpClient _http;

        public TicketApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<TicketViewModel>?> GetTickets()
        {
            return await _http.GetFromJsonAsync<List<TicketViewModel>>("api/tickets");            
        }

        public async Task<TicketViewModel?> GetTicket(string id)
        {
            return await _http.GetFromJsonAsync<TicketViewModel>($"api/tickets/{id}");
        }

        public async Task<bool> DeleteTicket(string id)
        {
            var response = await _http.DeleteAsync($"api/tickets/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreateTicket(TicketViewModel model)
        {
            var response = await _http.PostAsJsonAsync("api/tickets", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateTicket(TicketViewModel model)
        {
            var response = await _http.PutAsJsonAsync("api/tickets", model);
            return response.IsSuccessStatusCode;
        }
    }
}

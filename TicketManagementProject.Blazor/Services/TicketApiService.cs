using System.Net.Http.Json;
using TicketManagementProject.Shared.DTOs;

namespace TicketManagementProject.Blazor.Services
{
    public class TicketApiService
    {
        private readonly HttpClient _http;

        public TicketApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<TicketDto>?> GetTickets()
        {
            return await _http.GetFromJsonAsync<List<TicketDto>>("api/tickets");            
        }

        public async Task<TicketDto?> GetTicket(string id)
        {
            return await _http.GetFromJsonAsync<TicketDto>($"api/tickets/{id}");
        }

        public async Task<bool> DeleteTicket(string id)
        {
            var response = await _http.DeleteAsync($"api/tickets/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreateTicket(TicketDto model)
        {
            var response = await _http.PostAsJsonAsync("api/tickets", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateTicket(TicketDto model)
        {
            var response = await _http.PutAsJsonAsync("api/tickets", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> PatchTicket(string id, string fieldName, object value)
        {
            var patchData = new Dictionary<string, object> { { fieldName, value } };
            var response = await _http.PatchAsJsonAsync($"api/tickets/{id}", patchData);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddComment(string ticketId, CommentDto newComment, List<CommentDto> currentComments)
        {
            currentComments ??= new List<CommentDto>();
            currentComments.Add(newComment);
            return await PatchTicket(ticketId, "Commentaires", currentComments);
        }
    }
}

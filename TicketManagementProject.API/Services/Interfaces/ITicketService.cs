using TicketManagementProject.Shared.DTOs;

namespace TicketManagementProject.API.Services.Interfaces
{
    public interface ITicketService
    {
        Task<IEnumerable<TicketDto>> GetAllAsync();
        Task<TicketDto?> GetByIdAsync(string id);
        Task CreateAsync(TicketDto ticket);
        Task UpdateAsync(string id, TicketDto ticket);
        Task PatchAsync(string id, Dictionary<string, object> updatedFields);
        Task DeleteAsync(string id);
    }
}

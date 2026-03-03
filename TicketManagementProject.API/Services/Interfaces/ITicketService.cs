using TicketManagementProject.API.Entities;

namespace TicketManagementProject.API.Services.Interfaces
{
    public interface ITicketService
    {
        Task<IEnumerable<Ticket>> GetAllAsync();
        Task<Ticket?> GetByIdAsync(string id);
        Task CreateAsync(Ticket ticket);
        Task UpdateAsync(string id, Ticket ticket);
        Task PatchAsync(string id, Dictionary<string, object> updatedFields);
        Task DeleteAsync(string id);
    }
}

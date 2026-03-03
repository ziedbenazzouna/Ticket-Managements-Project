using TicketManagementProject.API.Entities;
using TicketManagementProject.API.Repository.Interfaces;
using TicketManagementProject.API.Services.Interfaces;

namespace TicketManagementProject.API.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<IEnumerable<Ticket>> GetAllAsync()
            => await _ticketRepository.GetAllAsync();

        public async Task<Ticket?> GetByIdAsync(string id)
            => await _ticketRepository.GetByIdAsync(id);

        public async Task CreateAsync(Ticket ticket)
        {
            await _ticketRepository.CreateAsync(ticket);
        }

        public async Task UpdateAsync(string id, Ticket ticket)
            => await _ticketRepository.UpdateAsync(id, ticket);

        public async Task PatchAsync(string id, Dictionary<string, object> updatedFields)
            => await _ticketRepository.PatchAsync(id, updatedFields);

        public async Task DeleteAsync(string id)
            => await _ticketRepository.DeleteAsync(id);
    }
}

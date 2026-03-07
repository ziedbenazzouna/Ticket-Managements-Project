using AutoMapper;
using TicketManagementProject.API.Entities;
using TicketManagementProject.API.Repository.Interfaces;
using TicketManagementProject.API.Services.Interfaces;
using TicketManagementProject.Shared.DTOs;

namespace TicketManagementProject.API.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public TicketService(ITicketRepository ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TicketDto>> GetAllAsync()
        {
            var tickets = await _ticketRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TicketDto>>(tickets);
        }

        public async Task<TicketDto?> GetByIdAsync(string id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);
            return _mapper.Map<TicketDto>(ticket);
        }

        public async Task CreateAsync(TicketDto dto)
        {
            var ticket = _mapper.Map<Ticket>(dto);
            await _ticketRepository.CreateAsync(ticket);
        }

        public async Task UpdateAsync(string id, TicketDto dto)
        {
            var ticket = _mapper.Map<Ticket>(dto);
            await _ticketRepository.UpdateAsync(id, ticket);
        }

        public async Task PatchAsync(string id, Dictionary<string, object> updatedFields)
            => await _ticketRepository.PatchAsync(id, updatedFields);

        public async Task DeleteAsync(string id)
            => await _ticketRepository.DeleteAsync(id);
    }
}

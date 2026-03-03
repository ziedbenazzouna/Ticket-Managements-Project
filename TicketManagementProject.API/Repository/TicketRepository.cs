using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TicketManagementProject.API.Entities;
using TicketManagementProject.API.Repository.Interfaces;
using TicketManagementProject.API.Settings;

namespace TicketManagementProject.API.Repository
{
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        public TicketRepository(IOptions<MongoDbSettings> settings) : base(settings)
        {
        }
    }
}

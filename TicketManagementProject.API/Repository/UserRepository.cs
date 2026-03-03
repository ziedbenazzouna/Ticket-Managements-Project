using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TicketManagementProject.API.Entities;
using TicketManagementProject.API.Repository.Interfaces;
using TicketManagementProject.API.Settings;

namespace TicketManagementProject.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _collection;

        public UserRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _collection = database.GetCollection<User>("Users");
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _collection
                .Find(u => u.Username == username)
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(User user)
        {
            await _collection.InsertOneAsync(user);
        }
    }
}

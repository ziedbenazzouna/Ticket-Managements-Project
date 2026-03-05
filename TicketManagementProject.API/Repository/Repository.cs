using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Reflection;
using System.Text.Json;
using TicketManagementProject.API.Repository.Interfaces;
using TicketManagementProject.API.Settings;

namespace TicketManagementProject.API.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly IMongoCollection<T> _collection;
        public Repository(IOptions<MongoDbSettings> settings)
        {

            // Instance of Mongo Client
            var client = new MongoClient(settings.Value.ConnectionString);

            // Database instance
            var database = client.GetDatabase(settings.Value.DatabaseName);

            // Collection from the database
            _collection = database.GetCollection<T>(typeof(T).Name);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            var objectId = new ObjectId(id);

            return await _collection.Find(Builders<T>.Filter.Eq("_id", objectId)).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(string id, T entity)
        {
            var objectId = new ObjectId(id);

            await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", objectId), entity);
        }

        public async Task PatchAsync(string id, Dictionary<string, object> updatedFields)
        {
            var objectId = new ObjectId(id);

            var updates = new List<UpdateDefinition<T>>();

            foreach (var field in updatedFields)
            {
                var prop = typeof(T).GetProperty(field.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (prop == null) continue;

                object value = field.Value;

                if (value is JsonElement je)
                {
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                    value = JsonSerializer.Deserialize(je.GetRawText(), prop.PropertyType, options);
                }

                updates.Add(Builders<T>.Update.Set(prop.Name, value));
            }

            if (updates.Count == 0)
                return;

            var update = Builders<T>.Update.Combine(updates);
            var filter = Builders<T>.Filter.Eq("_id", objectId);

            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task DeleteAsync(string id)
        {
            var objectId = new ObjectId(id);

            await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", objectId));
        }
    }
}

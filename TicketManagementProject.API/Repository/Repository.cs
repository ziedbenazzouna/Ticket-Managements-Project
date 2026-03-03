using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
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
                object value = field.Value;

                // Convert JsonElement en .NET type si nécessaire
                if (value is JsonElement je)
                {
                    switch (je.ValueKind)
                    {
                        case JsonValueKind.String: value = je.GetString(); break;
                        case JsonValueKind.Number:
                            if (je.TryGetInt32(out int i)) value = i;
                            else if (je.TryGetInt64(out long l)) value = l;
                            else value = je.GetDouble();
                            break;
                        case JsonValueKind.True: case JsonValueKind.False: value = je.GetBoolean(); break;
                        case JsonValueKind.Null: value = null; break;
                        default: value = je.GetRawText(); break;
                    }
                }

                // Map JSON key → propriété C# (respecte la casse)
                var prop = typeof(T).GetProperty(field.Key,
                             System.Reflection.BindingFlags.IgnoreCase |
                             System.Reflection.BindingFlags.Public |
                             System.Reflection.BindingFlags.Instance);

                if (prop != null)
                {
                    string propName = prop.Name; // Nom exact en C#
                    updates.Add(Builders<T>.Update.Set(propName, value));
                }
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

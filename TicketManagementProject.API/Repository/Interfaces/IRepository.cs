namespace TicketManagementProject.API.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(string id);
        Task CreateAsync(T entity);
        Task UpdateAsync(string id, T entity);
        Task PatchAsync(string id, Dictionary<string, object> updatedFields);
        Task DeleteAsync(string id);
    }
}

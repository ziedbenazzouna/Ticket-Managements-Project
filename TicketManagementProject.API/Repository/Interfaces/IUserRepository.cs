using TicketManagementProject.API.Entities;

namespace TicketManagementProject.API.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task CreateAsync(User user);
    }
}

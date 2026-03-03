using TicketManagementProject.API.DTOs;
using TicketManagementProject.API.Entities;

namespace TicketManagementProject.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto dto);
        Task<string?> LoginAsync(LoginDto dto);
    }
}

using Microsoft.AspNetCore.Components.Authorization;

namespace TicketManagementProject.Blazor.Helpers
{
    public interface IUserAccessor
    {
        Task<string> GetCurrentUserNameAsync();
    }

    public class UserAccessor : IUserAccessor
    {
        private readonly AuthenticationStateProvider _authStateProvider;

        public UserAccessor(AuthenticationStateProvider authStateProvider)
        {
            _authStateProvider = authStateProvider;
        }

        public async Task<string> GetCurrentUserNameAsync()
        {
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            return user.Identity?.Name ?? "Anonyme";
        }
    }
}

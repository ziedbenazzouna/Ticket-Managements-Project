using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;

namespace TicketManagementProject.Blazor.Services
{
    public class AuthMessageHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage;
        private readonly NavigationManager _navigation;

        public AuthMessageHandler(ILocalStorageService localStorage, NavigationManager navigation)
        {
            _localStorage = localStorage;
            _navigation = navigation;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");

            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await base.SendAsync(request, cancellationToken);

            // Si 401 -> logout automatique et redirection
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await _localStorage.RemoveItemAsync("authToken"); // supprime token
                if (!_navigation.Uri.EndsWith("/login", StringComparison.OrdinalIgnoreCase))
                {
                    _navigation.NavigateTo("/login", true);
                }
            }

            return response;
        }
    }
}

using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using TicketManagementProject.Blazor.Services;
using TicketManagementProject.Blazor.ViewModels;


namespace TicketManagementProject.Blazor.Pages
{
    public partial class Login
    {
        [Inject]
        private NavigationManager Navigation { get; set; } = default!;

        [Inject]
        private UserApiService UserService { get; set; } = default!;

        [Inject]
        private CustomAuthStateProvider AuthProvider { get; set; } = default!;


        private LoginViewModel model = new();
        private string? errorMessage;
        private bool isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthProvider.GetAuthenticationStateAsync();

            if (authState.User.Identity?.IsAuthenticated == true)
            {
                Navigation.NavigateTo("/", true); // redirige tout utilisateur connecté vers home
            }
            else
            {
                isLoading = false; // afficher le formulaire
            }
        }

        private async Task HandleLogin()
        {
            var result = await UserService.Login(model);

            if (result != null)
            {
                await AuthProvider.MarkUserAsAuthenticated(result.Token);
                Navigation.NavigateTo("/", true);
            }
            else
            {
                errorMessage = "Invalid credentials";
            }
        }
    }
}

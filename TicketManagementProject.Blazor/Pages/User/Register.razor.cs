using Microsoft.AspNetCore.Components;
using MudBlazor;
using TicketManagementProject.Blazor.Services;
using TicketManagementProject.Blazor.ViewModels;


namespace TicketManagementProject.Blazor.Pages.User
{
    public partial class Register
    {

        [Inject]
        private NavigationManager Navigation { get; set; } = default!;

        [Inject]
        private UserApiService UserService { get; set; } = default!;

        [Inject]
        private ISnackbar Snackbar { get; set; } = default!;


        private RegisterViewModel model = new();
        private string? errorMessage;
        private bool _isProcessing;

        private async Task HandleRegister()
        {
            if (model.Password != model.ConfirmPassword)
            {
                errorMessage = "Les mots de passe ne correspondent pas.";
                return;
            }

            _isProcessing = true;
            errorMessage = null;

            var success = await UserService.Register(model);

            if (success)
            {
                Snackbar.Add("Utilisateur ajouté avec succès !", Severity.Success);
                await Task.Delay(1500);
                Navigation.NavigateTo("/login");
            }
            else
            {
                errorMessage = "Erreur lors de l'inscription. L'utilisateur existe peut-être déjà.";
            }
            _isProcessing = false;
        }
    }
}

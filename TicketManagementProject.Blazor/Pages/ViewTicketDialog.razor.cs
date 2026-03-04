using Microsoft.AspNetCore.Components;
using MudBlazor;
using TicketManagementProject.Blazor.Enum;
using TicketManagementProject.Blazor.Services;
using TicketManagementProject.Blazor.ViewModels;

namespace TicketManagementProject.Blazor.Pages
{
    public partial class ViewTicketDialog
    {
        [Parameter]
        public UIActionEnum Action { get; set; } = UIActionEnum.Insert;

        [Parameter]
        public DateTime? _date { get; set; } = DateTime.Today;

        [Parameter]
        public TicketViewModel model { get; set; } = new TicketViewModel();

        [CascadingParameter] IMudDialogInstance MudDialog { get; set; } = default!;

        [Inject]
        private TicketApiService TicketService { get; set; } = default!;

        [Inject]
        private ISnackbar Snackbar { get; set; } = default!;

        [Inject] 
        private IDialogService DialogService { get; set; }

        private async Task CloturerTicket()
        {
            // 1. Afficher la boîte de confirmation
            bool? result = await DialogService.ShowMessageBoxAsync(
                "Confirmation",
                "Voulez-vous vraiment clôturer ce ticket ? Cette action est irréversible.",
                yesText: "Confirmer la clôture",
                cancelText: "Annuler"
            );

            if (result == true)
            {
                if (string.IsNullOrEmpty(model.Id)) return;

                bool success = await TicketService.PatchTicket(model.Id, "Statut", StatutIntervention.Closed.ToString());

                if (success)
                {
                    model.Statut = StatutIntervention.Closed.ToString();
                    Snackbar.Add("Ticket clôturé !", Severity.Success);

                    // Optionnel : On peut fermer le dialogue de consultation et renvoyer "Ok" 
                    // pour que la liste principale se rafraîchisse automatiquement.
                    MudDialog.Close(DialogResult.Ok(true));
                }
                else
                {
                    Snackbar.Add("Erreur lors de la clôture.", Severity.Error);
                }
            }
        }

        private void Cancel() => MudDialog.Cancel();
    }
}

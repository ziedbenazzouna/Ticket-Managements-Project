using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using TicketManagementProject.Blazor.Enum;
using TicketManagementProject.Blazor.Services;
using TicketManagementProject.Shared.DTOs;


namespace TicketManagementProject.Blazor.Pages.Tickets
{
    public partial class CreateOrUpdateTicket
    {
        [Parameter]
        public UIActionEnum Action { get; set; } = UIActionEnum.Insert;

        [Parameter]
        public DateTime? _date { get; set; } = DateTime.Today;

        [Parameter]
        public TicketDto model { get; set; } = new TicketDto();

        [CascadingParameter]
        private IMudDialogInstance MudDialog { get; set; }

        [Inject]
        private TicketApiService TicketService { get; set; } = default!;

        [Inject]
        private ISnackbar Snackbar { get; set; } = default!;


        private async Task OnValidSubmit(EditContext editContext)
        {
            if (_date is not null)
            {
                model.Date = _date.Value;
            }
            if (Action == UIActionEnum.Insert)
                CreateTicket();
            else if (Action == UIActionEnum.Update)
                UpdateTicket();

        }

        private async Task CreateTicket()
        {
            var result = await TicketService.CreateTicket(model);
            if (result)
            {
                Snackbar.Add("Ticket créé avec succès !", Severity.Success);
                MudDialog.Close();
            }
            else
            {
                Snackbar.Add("Impossible de créer le ticket !", Severity.Error);
            }
        }

        private async Task UpdateTicket()
        {
            var result = await TicketService.UpdateTicket(model);
            if (result)
            {
                Snackbar.Add("Ticket mis à jour avec succès !", Severity.Success);
                MudDialog.Close();
            }
            else
            {
                Snackbar.Add("La mise à jour du ticket a échoué !", Severity.Error);
            }
        }

        private void Cancel() => MudDialog.Cancel();
    }
}

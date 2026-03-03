using Microsoft.AspNetCore.Components;
using MudBlazor;
using TicketManagementProject.Blazor.Enum;
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
        // [Parameter] public TicketViewModel Ticket { get; set; } = new();

        private void Close() => MudDialog.Close();

        private Color GetStatutColor(StatutIntervention statut) => statut switch
        {
            StatutIntervention.New => Color.Info,
            StatutIntervention.Active => Color.Success,
            StatutIntervention.Closed => Color.Default,
            _ => Color.Default
        };
    }
}

using MudBlazor;
using TicketManagementProject.Shared.Enum;

namespace TicketManagementProject.Blazor.Helpers
{
    public static class StatutExtensions
    {
        public static Color GetColor(this StatutIntervention statut) => statut switch
        {
            StatutIntervention.New => Color.Info,
            StatutIntervention.Active => Color.Success,
            StatutIntervention.InProgress => Color.Warning,
            StatutIntervention.Closed => Color.Error,
            _ => Color.Default
        };
    }
}

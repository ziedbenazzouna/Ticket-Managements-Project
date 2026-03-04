using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using TicketManagementProject.Blazor.Enum;
using TicketManagementProject.Blazor.Services;
using TicketManagementProject.Blazor.ViewModels;

namespace TicketManagementProject.Blazor.Pages.Tickets
{
    [Authorize(Roles = "Admin")]
    public partial class Ticket
    {
        [Inject]
        private TicketApiService TicketService { get; set; } = default!;

        [Inject]
        private IDialogService DialogService { get; set; } = default!;

        [Inject]
        private ISnackbar Snackbar { get; set; } = default!;


        private MudDataGrid<TicketViewModel> dataGrid;

        private string searchString = null;

        [CascadingParameter] private Task<AuthenticationState> AuthenticationStateTask { get; set; } = default!;
        [Inject] private NavigationManager Navigation { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateTask;
            var user = authState.User;

            if (!user.IsInRole("Admin"))
            {
                Navigation.NavigateTo("/forbidden");
            }
        }


        /// <summary>
        /// Chargement des données via API
        /// </summary>
        private async Task<GridData<TicketViewModel>> ServerReload(GridState<TicketViewModel> state, CancellationToken cancellationToken)
        {
            var data = await TicketService.GetTickets();
            await Task.Delay(200); // juste pour UX

            // Filtrage
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                data = data.Where(e =>
                    e.Objet.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    e.Auteur.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            var totalItems = data.Count;

            // Tri
            var sortDef = state.SortDefinitions.FirstOrDefault();
            if (sortDef != null)
            {
                Func<TicketViewModel, object> sortFunc = sortDef.SortBy switch
                {
                    nameof(TicketViewModel.Objet) => e => e.Objet,
                    nameof(TicketViewModel.Auteur) => e => e.Auteur,
                    nameof(TicketViewModel.Date) => e => e.Date,
                    nameof(TicketViewModel.Categorie) => e => e.Categorie,
                    nameof(TicketViewModel.Statut) => e => e.Statut,
                    _ => e => e.Id
                };

                data = sortDef.Descending
                    ? data.OrderByDescending(sortFunc).ToList()
                    : data.OrderBy(sortFunc).ToList();
            }

            // Pagination
            var pagedData = data.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();

            return new GridData<TicketViewModel>
            {
                TotalItems = totalItems,
                Items = pagedData
            };
        }

        private Task OnSearch(string text)
        {
            searchString = text;
            return dataGrid.ReloadServerData();
        }

        private async Task ViewTicket(CellContext<TicketViewModel> ticket)
        {
            var ticketView = await TicketService.GetTicket(ticket.Item.Id);
            if (ticketView is null)
            {
                Snackbar.Add("Something wrong when try finding the ticket", Severity.Error);
                return;
            }
            var options = new DialogOptions { MaxWidth = MaxWidth.Large };

            var parameters = new DialogParameters<ViewTicketDialog>
        {
            {x=> x.Action, UIActionEnum.View},
            {x=> x.model, ticketView},
            {x=> x._date, ticketView.Date}
        };

            var dialog = await DialogService.ShowAsync<ViewTicketDialog>("View Ticket", parameters, options);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                await dataGrid.ReloadServerData();
            }
        }

        private async Task CreateNewTicket()
        {
            var options = new DialogOptions { MaxWidth = MaxWidth.Large };

            var parameters = new DialogParameters<CreateOrUpdateTicket>
        {
            {x=> x.Action, UIActionEnum.Insert},
            {x=> x.model, new TicketViewModel()}
        };

            var dialog = await DialogService.ShowAsync<CreateOrUpdateTicket>("Add Ticket", parameters, options);
            var result = await dialog.Result;

            if (result is not null && !result.Canceled)
            {
                await dataGrid.ReloadServerData();
            }
        }

        private async Task UpdateTicket(CellContext<TicketViewModel> ticket)
        {
            var ticketView = await TicketService.GetTicket(ticket.Item.Id);
            if (ticketView is null)
            {
                Snackbar.Add("Something wrong when try finding the ticket", Severity.Error);
                return;
            }
            var options = new DialogOptions { MaxWidth = MaxWidth.Large };

            var parameters = new DialogParameters<CreateOrUpdateTicket>
        {
            {x=> x.Action, UIActionEnum.Update},
            {x=> x.model, ticketView},
            {x=> x._date, ticketView.Date}
        };

            var dialog = await DialogService.ShowAsync<CreateOrUpdateTicket>("Update Ticket", parameters, options);
            var result = await dialog.Result;

            if (result is not null && !result.Canceled)
            {
                await dataGrid.ReloadServerData();
            }
        }

        private async Task RemoveTicket(CellContext<TicketViewModel> ticket)
        {
            var dialog = await DialogService.ShowAsync<RemoveConfirmationDialog>("Remove: " + ticket.Item.Objet +" Of " +ticket.Item.Auteur);
            var result = await dialog.Result;

            if (result is not null && !result.Canceled)
            {
                var confirmed = (bool)(result.Data ?? false);
                if (confirmed)
                {
                    var deleteResult = await TicketService.DeleteTicket(ticket.Item.Id);
                    if (deleteResult)
                    {
                        Snackbar.Add("Successfully remove the ticket!", Severity.Success);
                        await dataGrid.ReloadServerData();
                    }
                    else
                    {
                        Snackbar.Add("Failed remove the ticket!", Severity.Error);
                    }
                }
            }
        }

    }
}

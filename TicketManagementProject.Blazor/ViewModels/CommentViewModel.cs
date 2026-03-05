namespace TicketManagementProject.Blazor.ViewModels
{
    public class CommentViewModel
    {
        public string Auteur { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
